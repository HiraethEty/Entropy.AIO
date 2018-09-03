using Entropy.AIO.General;
using Entropy.SDK.Orbwalking.EventArgs;

namespace Entropy.AIO.Champions.Lucian.Logics
{
	internal partial class Weaving
	{
		public static void W(OnPostAttackEventArgs args)
		{
			if (BaseMenu.Root["combo"]["w"].Enabled)
			{
				Champion.W.Cast(args.Target as AIBaseClient);
			}
		}
	}
}
