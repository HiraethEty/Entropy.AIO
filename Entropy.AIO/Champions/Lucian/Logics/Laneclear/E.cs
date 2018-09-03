namespace Entropy.AIO.Champions.Lucian.Logics.Laneclear
{
	using System.Linq;
	using General;
	using SDK.Caching;
	using SDK.Extensions.Geometry;
	using SDK.Extensions.Objects;
	using SDK.Orbwalking.EventArgs;
	using Utility;

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
