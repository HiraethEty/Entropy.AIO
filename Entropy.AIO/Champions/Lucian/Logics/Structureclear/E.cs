
using Entropy.AIO.General;
using Entropy.AIO.Utility;
using Entropy.SDK.Extensions.Geometry;
using Entropy.SDK.Extensions.Objects;
using Entropy.SDK.Orbwalking.EventArgs;

namespace Entropy.AIO.Champions.Lucian.Logics
{
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
