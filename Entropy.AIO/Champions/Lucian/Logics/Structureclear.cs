namespace Entropy.AIO.Champions.Lucian.Logics
{
	using General;
	using SDK.Extensions.Geometry;
	using SDK.Extensions.Objects;
	using SDK.Orbwalking.EventArgs;
	using Utility;

	internal class Structureclear
	{
		public static void E(OnPostAttackEventArgs args)
		{
			var eStructureClearMenu = BaseMenu.Root["structureClear"]["e"];
			if (eStructureClearMenu.Enabled &&
			    LocalPlayer.Instance.MPPercent() > ManaManager.GetNeededMana(Champion.E.Slot, eStructureClearMenu))
			{
				Champion.E.Cast(LocalPlayer.Instance.Position.Extend(Hud.CursorPositionUnclipped, LocalPlayer.Instance.BoundingRadius));
			}
		}

		public static void W(OnPostAttackEventArgs args)
		{
			var wStructureClearMenu = BaseMenu.Root["structureClear"]["w"];
			if (wStructureClearMenu.Enabled &&
			    LocalPlayer.Instance.MPPercent() > ManaManager.GetNeededMana(Champion.W.Slot, wStructureClearMenu))
			{
				Champion.W.Cast(Hud.CursorPositionUnclipped);
			}
		}
	}
}
