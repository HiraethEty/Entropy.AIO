using Entropy.AIO.General;
using Entropy.AIO.Utility;
using Entropy.SDK.Extensions.Objects;

namespace Entropy.AIO.Champions.Lucian.Logics
{
	internal partial class Laneclear
	{
		public static void Q(EntropyEventArgs args)
		{
			var laneclearQMenu = BaseMenu.Root["laneClear"]["q"];
			if (laneclearQMenu.Enabled &&
			    LocalPlayer.Instance.MPPercent() > ManaManager.GetNeededMana(Champion.Q.Slot, laneclearQMenu))
			{
				/*
				var farmLocation = SpellClass.Q2.GetLineFarmLocation(Extensions.GetEnemyLaneMinionsTargets(), SpellClass.Q2.Width);
				if (farmLocation.MinionsHit >= MenuClass.Q["customization"]["laneclear"].Value)
				{
				    SpellClass.Q.CastOnUnit(farmLocation.FirstMinion);
				    return;
				}
				*/
			}
		}
	}
}
