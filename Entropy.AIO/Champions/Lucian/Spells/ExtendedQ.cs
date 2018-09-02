using System.Linq;
using Entropy.AIO.Utilities;
using Entropy.AIO.Utility;
using Entropy.SDK.Caching;
using Entropy.SDK.Damage;
using Entropy.SDK.Extensions.Objects;
using Entropy.SDK.Orbwalking;

namespace Entropy.AIO.Champions.Lucian.Spells
{
	using General;
	using SDK.Events;
	using SDK.Spells;

	internal sealed class ExtendedQ : BaseSpell
	{
		protected override void SetSpellData()
		{
			this.Spell = new Spell(SpellSlot.Q, 950f - LocalPlayer.Instance.BoundingRadius);
			this.Spell.SetSkillshot(0.25f, 65f, float.MaxValue, collision: false);
		}

		protected override void SubscribeToEvents()
		{
			new CustomTick(2000).OnTick += this.OnCustomTick;
		}

		public override void OnCustomTick(EntropyEventArgs args)
		{
			if (LocalPlayer.Instance.IsDead || Orbwalker.IsWindingUp)
			{
				return;
			}

			if (!this.Spell.Ready ||
			    !BaseMenu.Root["killSteal"]["extendedQ"].Enabled)
			{
				return;
			}

			// Extended Q Killsteal
			foreach (var target in ObjectCache.EnemyHeroes
				.Where(t =>
					t.IsValidTarget(this.Spell.Range) &&
					!Invulnerable.IsInvulnerable(t, DamageType.Physical, damage: this.Spell.GetDamage(t)) &&
					this.Spell.GetDamage(t) >= t.GetRealHealth(DamageType.Physical)))
			{
				foreach (var minion in ObjectCache.EnemyLaneMinions
					.Where(m => m.IsValidTarget(this.Spell.Range) && Lucian.QRectangle(m).IsInsidePolygon(this.Spell.GetPrediction(target).CastPosition)))
				{
					this.Spell.CastOnUnit(minion);
					break;
				}
			}

			// Extended Q Harass
			var extendedQHarass = BaseMenu.Root["harass"]["extendedQ"];
			if (extendedQHarass.Enabled &&
				LocalPlayer.Instance.MPPercent() > ManaManager.GetNeededMana(this.Spell.Slot, extendedQHarass))
			{
				var normalQ = Champion.Spells[0];
				foreach (var target in Extensions.GetBestSortedTargetsInRange(this.Spell.Range).Where(t =>
					!t.IsValidTarget(normalQ.Range) &&
					BaseMenu.Root["harass"]["whitelist"]["extendedQ"][t.CharName.ToLower()].Enabled))
				{
					foreach (var minion in Extensions.GetAllGenericUnitTargetsInRange(normalQ.Range))
					{
						if (!Lucian.QRectangle(minion).IsInsidePolygon(normalQ.GetPrediction(target).CastPosition))
						{
							continue;
						}

						this.Spell.CastOnUnit(minion);
						break;
					}
				}
			}
		}
	}
}
