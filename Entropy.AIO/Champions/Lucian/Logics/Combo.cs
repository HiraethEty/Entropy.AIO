namespace Entropy.AIO.Champions.Lucian.Logics
{
	using System.Linq;
	using General;
	using SDK.Caching;
	using SDK.Damage;
	using SDK.Enumerations;
	using SDK.Extensions;
	using SDK.Extensions.Geometry;
	using SDK.Extensions.Objects;
	using Utilities;
	using Champion = Champion;

	internal class Combo
	{
		public static void E(EntropyEventArgs args)
		{
			if (!BaseMenu.Root["combo"]["eengage"].Enabled)
			{
				return;
			}

			var bestTarget = Extensions.GetBestEnemyHeroTargetInRange(Champion.E.Range);
			if (bestTarget == null ||
			    Invulnerable.IsInvulnerable(bestTarget, DamageType.Physical) ||
			    bestTarget.IsValidTarget(LocalPlayer.Instance.GetAutoAttackRange(bestTarget)))
			{
				return;
			}

			var posAfterE = LocalPlayer.Instance.Position.Extend(Hud.CursorPositionUnclipped, 425f);
			if (posAfterE.EnemyHeroesCount(1000f) < 3 &&
			    LocalPlayer.Instance.Distance(Hud.CursorPositionUnclipped) > LocalPlayer.Instance.GetAutoAttackRange() &&
			    bestTarget.Distance(posAfterE) < LocalPlayer.Instance.GetAutoAttackRange(bestTarget))
			{
				Champion.E.Cast(posAfterE);
			}
		}

		public static void R(EntropyEventArgs args)
		{
			if (Champion.Q.Ready || Champion.W.Ready || Champion.E.Ready || Definitions.IsCulling())
			{
				return;
			}

			var bestTarget = ObjectCache.EnemyHeroes
			                            .Where(t =>
				                                   BaseMenu.Root["combo"]["whitelists"]["semiAutomaticR"][t.CharName.ToLower()].Enabled &&
				                                   t.IsValidTarget() &&
				                                   !Invulnerable.IsInvulnerable(t, DamageType.Physical, false))
			                            .MinBy(o => o.GetRealHealth(DamageType.Physical));

			if (bestTarget == null ||
			    Definitions.HasPassive() && bestTarget.DistanceToPlayer() <= LocalPlayer.Instance.GetAutoAttackRange(bestTarget))
			{
				return;
			}

			if (BaseMenu.Root["combo"]["normalR"].Enabled)
			{
				Champion.R.Cast(bestTarget);
			}

			if (BaseMenu.Root["combo"]["essenceR"].Enabled &&
			    LocalPlayer.Instance.HasItem(ItemID.EssenceReaver))
			{
				Champion.R.Cast(bestTarget);
			}
		}
	}
}