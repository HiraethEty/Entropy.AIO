namespace Entropy.AIO.Champions.Lucian.Logics.Structureclear
{
	using General;
	using SDK.Extensions.Objects;
	using SDK.Orbwalking.EventArgs;
	using Utility;

	internal partial class Structureclear
	{
		public static void W(OnPostAttackEventArgs args)
		{
			var target = args.Target;
			if (!target.IsStructure())
			{
				return;
			}

			var wStructureClearMenu = BaseMenu.Root["structureClear"]["w"];
			if (wStructureClearMenu.Enabled &&
			    LocalPlayer.Instance.MPPercent() > ManaManager.GetNeededMana(Champion.W.Slot, wStructureClearMenu))
			{
				Champion.W.Cast(Hud.CursorPositionUnclipped);
			}
		}
	}
}
