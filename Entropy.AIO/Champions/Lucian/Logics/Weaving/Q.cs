using Entropy.AIO.General;
using Entropy.SDK.Orbwalking.EventArgs;

namespace Entropy.AIO.Champions.Lucian.Logics
{
	internal partial class Weaving
	{
		public static void Q(OnPostAttackEventArgs args)
		{
			if (BaseMenu.Root["combo"]["q"].Enabled)
			{
				Champion.Q.CastOnUnit(args.Target);
			}
		}
	}
}
