namespace Entropy.AIO.Champions.Lucian
{
	using SDK.Events;
	using SDK.Orbwalking;

	internal class Methods
	{
		public Methods()
		{
			Initialize();
		}

		public static void Initialize()
		{
			Tick.OnTick += Lucian.OnTick;
			new CustomTick(2000).OnTick += Lucian.OnCustomTick;
			Game.OnWndProc += Lucian.OnWndProc;
			Orbwalker.OnPostAttack += Lucian.OnPostAttack;
			Gapcloser.OnNewGapcloser += Lucian.OnNewGapcloser;
		}
	}
}