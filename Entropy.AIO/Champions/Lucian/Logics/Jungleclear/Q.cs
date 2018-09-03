
using System.Linq;
using Entropy.AIO.General;
using Entropy.AIO.Utility;
using Entropy.SDK.Caching;
using Entropy.SDK.Damage;
using Entropy.SDK.Extensions.Objects;
using Entropy.SDK.Orbwalking.EventArgs;

namespace Entropy.AIO.Champions.Lucian.Logics
{
	internal partial class Jungleclear
	{
		public static void Q(OnPostAttackEventArgs args)
		{
			if (!ObjectCache.JungleMinions.Contains(args.Target) ||
			    args.Target.HP < LocalPlayer.Instance.GetAutoAttackDamage(args.Target as AIBaseClient) * 3)
			{
				return;
			}

			var jungleClearMenu = BaseMenu.Root["jungleClear"]["q"];
			if (jungleClearMenu.Enabled &&
			    LocalPlayer.Instance.MPPercent() > ManaManager.GetNeededMana(Champion.Q.Slot, jungleClearMenu))
			{
				Champion.Q.CastOnUnit(args.Target);
			}
		}
	}
}
