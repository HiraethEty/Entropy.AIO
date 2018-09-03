using System.Linq;
using Entropy.SDK.Caching;
using Entropy.SDK.Extensions.Geometry;

namespace Entropy.AIO.Champions.Lucian.Logics.Laneclear
{
	using General;
	using SDK.Extensions.Objects;
	using Utility;

	internal partial class Laneclear
	{
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
