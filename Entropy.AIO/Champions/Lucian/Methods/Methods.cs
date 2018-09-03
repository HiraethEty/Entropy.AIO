using Entropy.SDK.Events;
using Entropy.SDK.Orbwalking;

namespace Entropy.AIO.Champions.Lucian
{
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
