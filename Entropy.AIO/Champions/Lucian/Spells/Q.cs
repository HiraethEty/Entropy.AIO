using System.Linq;
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

	internal sealed class Q : BaseSpell
	{
		protected override void SetSpellData()
		{
			this.Spell = new Spell(SpellSlot.Q, 550f);
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

			//Laneclear Q
			var laneclearQMenu = BaseMenu.Root["laneClear"]["q"];
			if (laneclearQMenu.Enabled &&
				LocalPlayer.Instance.MPPercent() > ManaManager.GetNeededMana(this.Spell.Slot, laneclearQMenu))
			{
				/*
				var farmLocation = SpellClass.Q2.GetLineFarmLocation(Extensions.GetEnemyLaneMinionsTargets(), SpellClass.Q2.Width);
				if (farmLocation.MinionsHit >= MenuClass.Q["customization"]["laneclear"].Value)
				{
				    SpellClass.Q.CastOnUnit(farmLocation.FirstMinion);
				    return;
				}
				*/
			}
		}

		public override void OnCustomTick(EntropyEventArgs args)
		{
			if (LocalPlayer.Instance.IsDead || Orbwalker.IsWindingUp)
			{
				return;
			}

			if (!this.Spell.Ready ||
			    !BaseMenu.Root["killSteal"]["normalQ"].Enabled)
			{
				return;
			}

			// Normal Q Killsteal
			foreach (var target in ObjectCache.EnemyHeroes
				.Where(t =>
					t.IsValidTarget(this.Spell.Range) &&
					!Invulnerable.IsInvulnerable(t, DamageType.Physical, damage: this.Spell.GetDamage(t)) &&
					this.Spell.GetDamage(t) >= t.GetRealHealth(DamageType.Physical)))
			{
				this.Spell.CastOnUnit(target);
				break;
			}
		}

		public override void OnPostAttack(OnPostAttackEventArgs args)
		{
			if (!this.Spell.Ready)
			{
				return;
			}

			var target = args.Target;
			switch (target.Type.TypeID)
			{
				case GameObjectTypeID.AIHeroClient when BaseMenu.Root["combo"]["q"].Enabled:
					// Weaving Q
					this.Spell.CastOnUnit(target);
					return;

				case GameObjectTypeID.AIMinionClient when BaseMenu.Root["jungleClear"]["q"].Enabled:
					// Jungleclear Q
					if (LocalPlayer.Instance.MPPercent() > ManaManager.GetNeededMana(this.Spell.Slot, BaseMenu.Root["jungleClear"]["q"]))
					{
						this.Spell.CastOnUnit(target);
					}
					break;
			}
		}
	}
}
