namespace Entropy.AIO.Champions.Lucian.Logics.Combo
{
	using General;
	using SDK.Damage;
	using SDK.Extensions.Geometry;
	using SDK.Extensions.Objects;
	using Utilities;

	internal partial class Combo
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
	}
}
