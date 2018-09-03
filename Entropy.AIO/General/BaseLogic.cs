namespace Entropy.AIO.General
{
	using System.Linq;
	using Enumerations;
	using SDK.Caching;
	using SDK.Damage;
	using SDK.Enumerations;
	using SDK.Extensions.Geometry;
	using SDK.Extensions.Objects;
	using SDK.Orbwalking;
	using SDK.Orbwalking.EventArgs;
	using ToolKit;
	using Utility;

	internal static class BaseLogic
	{
		public static void Initialize()
		{
			Orbwalker.OnPreAttack += OnPreAttack;
			//Orbwalker.OnPostAttack += OnPostAttack;
			Spellbook.OnLocalCastSpell += OnLocalCastSpell;
		}

		private static void OnPreAttack(OnPreAttackEventArgs args)
		{
			switch (Orbwalker.Mode)
			{
				// Disable AA until level
				case OrbwalkingMode.Combo:
					if (BaseMenu.Root["general"]["disableaa"].Enabled &&
					    !LocalPlayer.Instance.HasSheenLikeBuff() &&
					    LocalPlayer.Instance.Level() >= BaseMenu.Root["general"]["disableaa"].Value)
					{
						args.Cancel = true;
					}
					break;

				// Support mode
				case OrbwalkingMode.Harass:
				case OrbwalkingMode.LaneClear:
					if (BaseMenu.Root["general"]["supportmode"].Enabled &&
					    ObjectCache.EnemyLaneMinions.Contains(args.Target))
					{
						args.Cancel = ObjectCache.AllyHeroes.Any(a => !a.IsMe() && a.DistanceToPlayer() < 2500);
					}
					break;
			}

			if (args.Target.IsStructure())
			{
				return;
			}

			var stormrazorSlot = LocalPlayer.Instance.InventorySlots.FirstOrDefault(s => s.IsValid && s.ItemID == (uint) ItemID.Stormrazor);
			if (stormrazorSlot != null)
			{
				var stormRazorMenu = BaseMenu.Root["general"]["stormrazor"];
				switch (Orbwalker.Mode)
				{
					case OrbwalkingMode.Combo when !stormRazorMenu["combo"].Enabled: return;
					case OrbwalkingMode.Harass when !stormRazorMenu["mixed"].Enabled: return;
					case OrbwalkingMode.LaneClear when !stormRazorMenu["laneclear"].Enabled: return;
					case OrbwalkingMode.LastHit when !stormRazorMenu["lasthit"].Enabled: return;
				}

				if (!LocalPlayer.Instance.HasBuff("windbladebuff"))
				{
					args.Cancel = true;
				}
			}
		}

		/*
		private static void OnPostAttack(OnPostAttackEventArgs args)
		{
			if (!LocalPlayer.Instance.IsMelee || args.Target.IsStructure())
			{
				return;
			}

			// Hydra casting
			if (BaseMenu.Root["hydra"] != null)
			{
				var slot = LocalPlayer.Instance.InventorySlots.FirstOrDefault(i => Enumerations.Hydras.Contains((ItemID)i.ItemID));
				if (slot != null)
				{
					CastHydra(slot);
				}
			}
		}
		*/

		/*
		private static void CastHydra(InventorySlot slot)
		{
			var hydraMenu = BaseMenu.Root["general"]["hydra"];
			var hydra = LocalPlayer.Instance.GetItem((ItemID)slot.ItemID);
			if (hydra != null)
			{
				switch (Orbwalker.Mode)
				{
					case OrbwalkingMode.Combo when !hydraMenu["combo"].Enabled:
						return;
					case OrbwalkingMode.Harass when !hydraMenu["mixed"].Enabled:
						return;
					case OrbwalkingMode.LaneClear when !hydraMenu["laneclear"].Enabled:
						return;
					case OrbwalkingMode.LastHit when !hydraMenu["lasthit"].Enabled:
						return;
					case OrbwalkingMode.None when !hydraMenu["manual"].Enabled:
						return;
				}

				var hydraSpellSlot = hydra.Slot;
				if (hydraSpellSlot != SpellSlot.Unknown &&
				    LocalPlayer.Instance.Spellbook.GetSpellState(hydraSpellSlot).HasFlag(SpellState.Ready))
				{
					Spellbook.CastSpell(hydraSpellSlot);
				}
			}
		}
		*/

		private static void OnLocalCastSpell(SpellbookLocalCastSpellEventArgs args)
		{
			var slot = args.Slot;
			if (Enumerations.SpellSlots.Contains(slot))
			{
				var championSpellManaCosts = Utilities.ManaCostArray.FirstOrDefault(v => v.Key == LocalPlayer.Instance.CharName).Value;
				if (championSpellManaCosts != null)
				{
					var spellbook = LocalPlayer.Instance.Spellbook;
					var data = Utilities.PreserveManaData;

					var spell = spellbook.GetSpell(slot);
					var preserveManaMenu = BaseMenu.Root["general"]["preservemana"];
					var menuOption = preserveManaMenu[slot.ToString().ToLower()];
					if (menuOption != null &&
					    menuOption.Enabled)
					{
						var registeredSpellData = data.FirstOrDefault(d => d.Key == slot).Value;
						var actualSpellData = championSpellManaCosts[slot][spell.Level - 1];

						if (data.ContainsKey(slot) &&
						    registeredSpellData != actualSpellData)
						{
							data.Remove(slot);
							Logging.Log($"Preserve Mana List: Removed {slot} (Updated ManaCost).");
						}

						if (spell.Level > 0 &&
						    !data.ContainsKey(slot))
						{
							data.Add(slot, actualSpellData);
							Logging.Log($"Preserve Mana List: Added {slot}, Cost: {actualSpellData}.");
						}
					}
					else
					{
						if (data.ContainsKey(slot))
						{
							data.Remove(slot);
							Logging.Log($"Preserve Mana List: Removed {slot} (Disabled).");
						}
					}

					var sum = data
						.Where(s => preserveManaMenu[s.Key.ToString().ToLower()].Enabled)
						.Sum(s => s.Value);
					if (sum <= 0)
					{
						return;
					}

					var spellCost = championSpellManaCosts[slot][LocalPlayer.Instance.Spellbook.GetSpell(slot).Level - 1];
					if (!data.Keys.Contains(slot) &&
					    LocalPlayer.Instance.MP - spellCost < sum)
					{
						args.Execute = false;
					}
				}

				switch (Orbwalker.Mode)
				{
					case OrbwalkingMode.Combo:
					case OrbwalkingMode.Harass:
						var target = Orbwalker.GetOrbwalkingTarget() as AIHeroClient;
						if (target != null &&
						    target.GetRealHealth(DamageType.Physical) <=
						    LocalPlayer.Instance.GetAutoAttackDamage(target) * BaseMenu.Root["general"]["preservespells"][args.Slot.ToString().ToLower()].Value)
						{
							args.Execute = false;
						}
						break;
				}
			}
		}
	}
}