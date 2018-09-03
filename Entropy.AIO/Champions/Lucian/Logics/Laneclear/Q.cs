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
		public static void Q(EntropyEventArgs args)
		{
			var laneclearQMenu = BaseMenu.Root["laneClear"]["q"];
			if (!laneclearQMenu.Enabled ||
			    LocalPlayer.Instance.MPPercent() <= ManaManager.GetNeededMana(Champion.Q.Slot, laneclearQMenu))
			{
				return;
			}

			var minions = ObjectCache.EnemyLaneMinions.Where(m => m.IsValidUnit() && m.DistanceToPlayer() <= Spells.Spells.ExtendedQ.Range).ToList();
			var minionToAttack = minions.FirstOrDefault(m => m.DistanceToPlayer() <= Champion.Q.Range);
			if (minionToAttack == null)
			{
				return;
			}

			var farmLocation = Spells.Spells.ExtendedQ.GetLineFarmLocation(minions, Spells.Spells.ExtendedQ.Width);
			if (farmLocation.MinionsHit < BaseMenu.Root["laneClear"]["customization"]["q"].Value)
			{
				return;
			}

			Champion.Q.CastOnUnit(minionToAttack);
		}
	}
}
