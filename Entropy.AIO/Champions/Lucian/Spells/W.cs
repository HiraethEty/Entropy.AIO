using System.Linq;
using Entropy.AIO.Utilities;
using Entropy.AIO.Utility;
using Entropy.SDK.Caching;
using Entropy.SDK.Damage;
using Entropy.SDK.Extensions.Objects;
using Entropy.SDK.Orbwalking;
using Entropy.SDK.Orbwalking.EventArgs;

namespace Entropy.AIO.Champions.Lucian.Spells
{
	using General;
	using SDK.Events;
	using SDK.Spells;

	internal sealed class W : BaseSpell
	{
		protected override void SetSpellData()
		{
			this.Spell = new Spell(SpellSlot.W, 900f);
			this.Spell.SetSkillshot(0.25f, 80f, 1600f, collision: false);
		}

		protected override void SubscribeToEvents()
		{
			Tick.OnTick += this.OnTick;
			new CustomTick(2000).OnTick += this.OnCustomTick;
			Orbwalker.OnPostAttack += this.OnPostAttack;
		}

		public override void OnTick(EntropyEventArgs args)
		{
			if (LocalPlayer.Instance.IsDead || Orbwalker.IsWindingUp || !this.Spell.Ready)
			{
				return;
			}

			//Laneclear W
			var laneClearWMenu = BaseMenu.Root["laneClear"]["w"];
			if (laneClearWMenu.Enabled &&
			    LocalPlayer.Instance.MPPercent() > ManaManager.GetNeededMana(this.Spell.Slot, laneClearWMenu))
			{
				/*
				var farmLocation = SpellClass.W.GetCircularFarmLocation(Extensions.GetEnemyLaneMinionsTargets(), SpellClass.W.Width);
				if (farmLocation.MinionsHit >= MenuClass.W["customization"]["laneclear"].Value)
				{
				    SpellClass.W.Cast(farmLocation.FirstMinion);
				    return;
				}
				*/
			}
		}

		public override void OnCustomTick(EntropyEventArgs args)
		{
			if (LocalPlayer.Instance.IsDead || Orbwalker.IsWindingUp || !this.Spell.Ready)
			{
				return;
			}

			// Killsteal W
			if (BaseMenu.Root["killSteal"]["w"].Enabled)
			{
				foreach (var target in ObjectCache.EnemyHeroes
					.Where(t =>
						t.IsValidTarget(this.Spell.Range) &&
						!Invulnerable.IsInvulnerable(t, DamageType.Magical, damage: this.Spell.GetDamage(t)) &&
						this.Spell.GetDamage(t) >= t.GetRealHealth(DamageType.Magical)))
				{
					this.Spell.Cast(target);
					break;
				}
			}

			// Harass W
			var wHarassMenu = BaseMenu.Root["harass"]["w"];
			if (wHarassMenu.Enabled &&
			    LocalPlayer.Instance.MPPercent() > ManaManager.GetNeededMana(this.Spell.Slot, wHarassMenu))
			{
				foreach (var target in Extensions.GetBestSortedTargetsInRange(this.Spell.Range).Where(t =>
					BaseMenu.Root["harass"]["whitelist"]["w"][t.CharName.ToLower()].Enabled))
				{
					this.Spell.Cast(target);
					break;
				}
			}
		}

		public override void OnPostAttack(OnPostAttackEventArgs args)
		{
			var heroTarget = args.Target as AIHeroClient;
			if (heroTarget == null ||
			    !this.Spell.Ready ||
			    !BaseMenu.Root["combo"]["w"].Enabled)
			{
				return;
			}

			// Weaving W
			this.Spell.Cast(heroTarget.Position);
		}
	}
}
