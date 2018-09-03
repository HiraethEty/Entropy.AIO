namespace Entropy.AIO.Champions.Lucian.Logics.Laneclear
{
	using System.Linq;
	using General;
	using SDK.Caching;
	using SDK.Extensions.Geometry;
	using SDK.Extensions.Objects;
	using Utility;

	internal partial class Laneclear
	{
		public static void Q(EntropyEventArgs args)
		{
			var laneclearQMenu = BaseMenu.Root["laneClear"]["q"];
			if (laneclearQMenu.Enabled &&
			    LocalPlayer.Instance.MPPercent() > ManaManager.GetNeededMana(Champion.Q.Slot, laneclearQMenu))
			{
				var farmLocation = FarmManager.GetLinearFarmLocation(Champion.Q.Width * 2, Spells.Spells.ExtendedQ.Range);
				if (farmLocation.MinionsHit >= BaseMenu.Root["laneClear"]["customization"]["q"].Value)
				{
					if (farmLocation.Position == null || !farmLocation.Position.IsValid)
					{
						return;
					}
					Champion.Q.CastOnUnit(farmLocation.Position);
				}
			}
		}
	}
}
