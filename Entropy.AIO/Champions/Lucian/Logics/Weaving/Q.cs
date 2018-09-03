namespace Entropy.AIO.Champions.Lucian.Logics.Weaving
{
	using General;
	using SDK.Orbwalking.EventArgs;

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
