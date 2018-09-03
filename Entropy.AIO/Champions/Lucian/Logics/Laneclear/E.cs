
using System.Linq;
using Entropy.AIO.General;
using Entropy.AIO.Utility;
using Entropy.SDK.Caching;
using Entropy.SDK.Extensions.Geometry;
using Entropy.SDK.Extensions.Objects;
using Entropy.SDK.Orbwalking.EventArgs;

namespace Entropy.AIO.Champions.Lucian.Logics
{
	internal partial class Laneclear
	{
		public static void E(OnPostAttackEventArgs args)
		{
			var laneClearMenu = BaseMenu.Root["laneClear"]["e"];
			if (laneClearMenu.Enabled &&
			    LocalPlayer.Instance.MPPercent() > ManaManager.GetNeededMana(Champion.E.Slot, laneClearMenu) &&
			    ObjectCache.EnemyLaneMinions.Count(m => m.DistanceToPlayer() <= Champion.E.Range) >= BaseMenu.Root["laneClear"]["customization"]["e"].Value)
			{
				Champion.E.Cast(LocalPlayer.Instance.Position.Extend(Hud.CursorPositionUnclipped, LocalPlayer.Instance.BoundingRadius));
			}
		}
	}
}
