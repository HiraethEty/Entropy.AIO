
using Entropy.AIO.General;
using Entropy.AIO.Utility;
using Entropy.SDK.Extensions.Objects;
using Entropy.SDK.Orbwalking.EventArgs;

namespace Entropy.AIO.Champions.Lucian.Logics
{
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
