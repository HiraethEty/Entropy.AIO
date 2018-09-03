namespace Entropy.AIO.Champions.Lucian.Logics.Structureclear
{
	using General;
	using SDK.Extensions.Geometry;
	using SDK.Extensions.Objects;
	using SDK.Orbwalking.EventArgs;
	using Utility;

	internal partial class Structureclear
	{
		public static void E(OnPostAttackEventArgs args)
		{
			var target = args.Target;
			if (!target.IsStructure())
			{
				return;
			}

			var eStructureClearMenu = BaseMenu.Root["structureClear"]["e"];
			if (eStructureClearMenu.Enabled &&
			    LocalPlayer.Instance.MPPercent() > ManaManager.GetNeededMana(Champion.E.Slot, eStructureClearMenu))
			{
				Champion.E.Cast(LocalPlayer.Instance.Position.Extend(Hud.CursorPositionUnclipped, LocalPlayer.Instance.BoundingRadius));
			}
		}
	}
}
