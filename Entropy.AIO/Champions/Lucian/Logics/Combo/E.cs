using Entropy.AIO.General;
using Entropy.AIO.Utilities;
using Entropy.SDK.Damage;
using Entropy.SDK.Extensions.Geometry;
using Entropy.SDK.Extensions.Objects;

namespace Entropy.AIO.Champions.Lucian.Logics
{
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
