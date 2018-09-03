namespace Entropy.AIO.Champions.Lucian.Logics.Weaving
{
	using General;
	using SDK.Orbwalking.EventArgs;

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
