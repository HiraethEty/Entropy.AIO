using Entropy.AIO.Utilities;
using Entropy.SDK.Damage;
using Entropy.SDK.Extensions.Geometry;
using Entropy.SDK.Extensions.Objects;

namespace Entropy.AIO.Champions.Lucian.Spells
{
	using General;
	using SDK.Events;
	using SDK.Spells;

	internal sealed class E : BaseSpell
	{
		protected override void SetSpellData()
		{
			this.Spell = new Spell(SpellSlot.E, LocalPlayer.Instance.GetAutoAttackRange() + 425f);
		}

		protected override void SubscribeToEvents()
		{
			Tick.OnTick += this.OnTick;
		}

		public override void OnTick(EntropyEventArgs args)
		{
			if (this.Spell.Ready &&
			    BaseMenu.Root["e"]["combo"]["eengage"].Enabled)
			{
				var bestTarget = Extensions.GetBestEnemyHeroTargetInRange(this.Spell.Range);
				if (bestTarget != null &&
				    !Invulnerable.IsInvulnerable(bestTarget, DamageType.Physical) &&
				    !bestTarget.IsValidTarget(LocalPlayer.Instance.GetAutoAttackRange(bestTarget)))
				{
					var posAfterE = LocalPlayer.Instance.Position.Extend(Hud.CursorPositionUnclipped, 425f);
					if (posAfterE.EnemyHeroesCount(1000f) < 3 &&
					    LocalPlayer.Instance.Distance(Hud.CursorPositionUnclipped) > LocalPlayer.Instance.GetAutoAttackRange() &&
					    bestTarget.Distance(posAfterE) < LocalPlayer.Instance.GetAutoAttackRange(bestTarget))
					{
						this.Spell.Cast(posAfterE);
					}
				}
			}
		}
	}
}
