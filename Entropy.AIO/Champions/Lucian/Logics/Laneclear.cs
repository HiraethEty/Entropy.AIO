namespace Entropy.AIO.Champions.Lucian.Logics
{
	using System.Linq;
	using General;
	using SDK.Caching;
	using SDK.Extensions.Geometry;
	using SDK.Extensions.Objects;
	using Utility;

	internal class Laneclear
	{
		public static void Q(EntropyEventArgs args)
		{
			var laneclearQMenu = BaseMenu.Root["laneClear"]["q"];
			if (!laneclearQMenu.Enabled ||
			    LocalPlayer.Instance.MPPercent() <= ManaManager.GetNeededMana(Champion.Q.Slot, laneclearQMenu))
			{
				return;
			}

			var minions = ObjectCache.EnemyLaneMinions.Where(m => m.IsValidUnit() && m.DistanceToPlayer() <= Spells.ExtendedQ.Range).ToList();
			var minionToAttack = minions.FirstOrDefault(m => m.DistanceToPlayer() <= Champion.Q.Range);
			if (minionToAttack == null)
			{
				return;
			}

			var farmLocation = Spells.ExtendedQ.GetLineFarmLocation(minions, Spells.ExtendedQ.Width);
			if (farmLocation.MinionsHit < BaseMenu.Root["laneClear"]["customization"]["q"].Value)
			{
				return;
			}

			Champion.Q.CastOnUnit(minionToAttack);
		}

		public static void W(EntropyEventArgs args)
		{
			var laneclearWMenu = BaseMenu.Root["laneClear"]["w"];
			if (!laneclearWMenu.Enabled ||
			    LocalPlayer.Instance.MPPercent() <= ManaManager.GetNeededMana(Champion.W.Slot, laneclearWMenu))
			{
				return;
			}

			var minions = ObjectCache.EnemyLaneMinions.Where(m => m.IsValidUnit() && m.DistanceToPlayer() <= Champion.W.Range).ToList();
			var farmLocation = Champion.W.GetCircularFarmLocation(minions, Champion.W.Width);
			if (farmLocation.MinionsHit < BaseMenu.Root["laneClear"]["customization"]["w"].Value)
			{
				return;
			}

			Champion.W.Cast(farmLocation.Position);
		}
	}
}