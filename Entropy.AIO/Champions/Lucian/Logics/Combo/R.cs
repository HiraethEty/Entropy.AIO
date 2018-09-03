namespace Entropy.AIO.Champions.Lucian.Logics.Combo
{
	using System.Linq;
	using General;
	using Misc;
	using SDK.Caching;
	using SDK.Damage;
	using SDK.Enumerations;
	using SDK.Extensions;
	using SDK.Extensions.Geometry;
	using SDK.Extensions.Objects;
	using Champion = Champions.Champion;

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
