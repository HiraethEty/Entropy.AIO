using System.Linq;
using Entropy.AIO.General;
using Entropy.SDK.Caching;
using Entropy.SDK.Damage;
using Entropy.SDK.Enumerations;
using Entropy.SDK.Extensions;
using Entropy.SDK.Extensions.Geometry;
using Entropy.SDK.Extensions.Objects;

namespace Entropy.AIO.Champions.Lucian.Logics
{
	internal partial class Combo
	{
		public static void R(EntropyEventArgs args)
		{
			if (Champion.Q.Ready || Champion.W.Ready || Champion.E.Ready || Definitions.IsCulling() )
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
