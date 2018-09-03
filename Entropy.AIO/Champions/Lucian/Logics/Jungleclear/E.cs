
using System.Linq;
using Entropy.AIO.General;
using Entropy.AIO.Utility;
using Entropy.SDK.Caching;
using Entropy.SDK.Damage;
using Entropy.SDK.Extensions.Geometry;
using Entropy.SDK.Extensions.Objects;
using Entropy.SDK.Orbwalking.EventArgs;

namespace Entropy.AIO.Champions.Lucian.Logics
{
	internal partial class Jungleclear
	{
		public static void E(OnPostAttackEventArgs args)
		{
			if (!ObjectCache.JungleMinions.Contains(args.Target) ||
			    args.Target.HP < LocalPlayer.Instance.GetAutoAttackDamage(args.Target as AIBaseClient) * 3)
			{
				return;
			}

			var jungleClearMenu = BaseMenu.Root["jungleClear"]["e"];
			if (jungleClearMenu.Enabled &&
			    LocalPlayer.Instance.MPPercent() > ManaManager.GetNeededMana(Champion.E.Slot, jungleClearMenu))
			{
				Champion.E.Cast(LocalPlayer.Instance.Position.Extend(Hud.CursorPositionUnclipped, LocalPlayer.Instance.BoundingRadius));
			}
		}
	}
}
