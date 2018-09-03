﻿namespace Entropy.AIO.Champions.Lucian.Logics.Laneclear
{
	using General;
	using SDK.Extensions.Objects;
	using Utility;

	internal partial class Laneclear
	{
		public static void W(EntropyEventArgs args)
		{
			var laneclearWMenu = BaseMenu.Root["laneClear"]["w"];
			if (laneclearWMenu.Enabled &&
			    LocalPlayer.Instance.MPPercent() > ManaManager.GetNeededMana(Champion.W.Slot, laneclearWMenu))
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
