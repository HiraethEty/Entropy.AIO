using System.Linq;
using Entropy.SDK.Caching;
using Entropy.SDK.Damage;
using Entropy.SDK.Enumerations;
using Entropy.SDK.Extensions;
using Entropy.SDK.Extensions.Geometry;
using Entropy.SDK.Extensions.Objects;
using Entropy.SDK.Orbwalking;
using Entropy.SDK.UI.Components;
using Entropy.SDK.Utils;

namespace Entropy.AIO.Champions.Lucian.Spells
{
	using General;
	using Misc;
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
			Game.OnWndProc += this.OnWndProc;
		}

		public override void OnWndProc(GameWndProcEventArgs args)
		{
			if (!BaseMenu.Root["combo"]["bool"].Enabled ||
				args.WParam != BaseMenu.Root["combo"]["key"].As<MenuKeyBind>().Key)
			{
				return;
			}

			switch (args.Message)
			{
				case WindowMessage.KEYDOWN when !Definitions.IsCulling():
					if (BaseMenu.Root["combo"]["bool"].Enabled &&
					    BaseMenu.Root["combo"]["key"].Enabled)
					{
						DelayAction.Queue(() => { Orbwalker.Move(Hud.CursorPositionUnclipped); },
							100 + EnetClient.Ping);
					}

					if (LocalPlayer.Instance.IsDead || Orbwalker.IsWindingUp)
					{
						return;
					}

					if (this.Spell.Ready)
					{
						// Cast Semi-Automatic R
						this.CastSemiAutomaticR();
					}
					break;

				case WindowMessage.KEYUP when Definitions.IsCulling():
					this.Spell.Cast();
					break;
			}
		}

		public override void OnTick(EntropyEventArgs args)
		{
			// Orbwalk while pressing Semi-Automatic R Key.
			if (BaseMenu.Root["combo"]["bool"].Enabled &&
			    BaseMenu.Root["combo"]["key"].Enabled)
			{
				DelayAction.Queue(() => { Orbwalker.Move(Hud.CursorPositionUnclipped); },
					100 + EnetClient.Ping);
			}

			if (!this.Spell.Ready)
			{
				return;
			}

			if (Champion.Spells[0].Ready ||
			    Champion.Spells[2].Ready ||
			    Champion.Spells[3].Ready)
			{
				return;
			}

			if (!Definitions.IsCulling() &&
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

				if (BaseMenu.Root["combo"]["normalR"].Enabled)
				{
					this.Spell.Cast(bestTarget);
				}

				if (BaseMenu.Root["combo"]["essenceR"].Enabled &&
				    LocalPlayer.Instance.HasItem(ItemID.EssenceReaver))
				{
					this.Spell.Cast(bestTarget);
				}
			}
		}

		private void CastSemiAutomaticR()
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
}
