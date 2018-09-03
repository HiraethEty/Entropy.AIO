namespace Entropy.AIO.Champions.Lucian.Logics.Jungleclear
{
	using System.Linq;
	using General;
	using SDK.Caching;
	using SDK.Damage;
	using SDK.Extensions.Objects;
	using SDK.Orbwalking.EventArgs;
	using Utility;

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
