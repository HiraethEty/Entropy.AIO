using System.Linq;
using Entropy.SDK.Caching;
using Entropy.SDK.Damage;
using Entropy.SDK.Extensions;
using Entropy.SDK.Extensions.Geometry;
using Entropy.SDK.Extensions.Objects;
using Entropy.SDK.Orbwalking;
using Entropy.SDK.Utils;

namespace Entropy.AIO.Champions.Lucian.Spells
{
	using General;
	using SDK.Events;
	using SDK.Spells;

	internal sealed class R : BaseSpell
	{
		protected override void SetSpellData()
		{
			this.Spell = new Spell(SpellSlot.R, 1150f);
			this.Spell.SetSkillshot(0.25f, 110f, 2500f, collision: false);
		}

		protected override void SubscribeToEvents()
		{
			Tick.OnTick += this.OnTick;
		}

		public override void OnTick(EntropyEventArgs args)
		{
			// Orbwalk while pressing Semi-Automatic R Key.
			if (BaseMenu.Root["combo"]["bool"].Enabled &&
			    BaseMenu.Root["combo"]["key"].Enabled)
			{
				DelayAction.Queue(() =>
					{
						Orbwalker.Move(Hud.CursorPositionUnclipped);
					},
					100 + EnetClient.Ping);
			}

			// Cast Semi-Automatic R
			if (this.Spell.Ready &&
			    BaseMenu.Root["combo"]["bool"].Enabled)
			{
				if (!Lucian.IsCulling() &&
				    BaseMenu.Root["combo"]["key"].Enabled)
				{
					var bestTarget = ObjectCache.EnemyHeroes
						.Where(t =>
							BaseMenu.Root["combo"]["whitelists"]["semiAutomaticR"][t.CharName.ToLower()].Enabled &&
							t.IsValidTarget() &&
							!Invulnerable.IsInvulnerable(t, DamageType.Physical, false))
						.MinBy(o => o.GetRealHealth(DamageType.Physical));

					if (bestTarget == null)
					{
						return;
					}

					var W = Champion.Spells[2];
					if (W.Ready &&
					    bestTarget.DistanceToPlayer() <= W.Range)
					{
						W.Cast(bestTarget.Position);
					}

					this.Spell.Cast(bestTarget.Position);
				}
			}

			// Stop Semi-Automatic R
			if (Lucian.IsCulling() &&
			    !BaseMenu.Root["combo"]["key"].Enabled)
			{
				this.Spell.Cast();
			}
		}
	}
}
