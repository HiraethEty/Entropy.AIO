namespace Entropy.AIO.Champions.Lucian.Logics
{
	using System.Linq;
	using General;
	using Misc;
	using SDK.Extensions.Objects;
	using Utilities;
	using Utility;

	internal class Harass
	{
		public static void ExtendedQ(EntropyEventArgs args)
		{
			var extendedQHarass = BaseMenu.Root["harass"]["extendedQ"];
			if (!extendedQHarass.Enabled ||
			    LocalPlayer.Instance.MPPercent() <= ManaManager.GetNeededMana(Champion.Q.Slot, extendedQHarass))
			{
				return;
			}

			foreach (var target in Extensions.GetBestSortedTargetsInRange(Spells.ExtendedQ.Range)
				.Where(t =>
					!t.IsValidTarget(Champion.Q.Range) &&
					BaseMenu.Root["harass"]["whitelists"]["extendedQ"][t.CharName.ToLower()].Enabled))
			{
				foreach (var minion in Extensions.GetAllGenericUnitTargetsInRange(Champion.Q.Range))
				{
					if (!Definitions.QRectangle(minion).IsInsidePolygon(Spells.ExtendedQ.GetPrediction(target).CastPosition))
					{
						continue;
					}

					Champion.Q.CastOnUnit(minion);
					break;
				}
			}
		}

		public static void Q(EntropyEventArgs args)
		{
			var normalQHarassMenu = BaseMenu.Root["harass"]["normalQ"];
			if (!normalQHarassMenu.Enabled ||
			    LocalPlayer.Instance.MPPercent() <= ManaManager.GetNeededMana(Champion.Q.Slot, normalQHarassMenu))
			{
				return;
			}

			foreach (var target in Extensions.GetBestSortedTargetsInRange(Champion.Q.Range)
				.Where(t =>
					t.IsValidTarget(Champion.Q.Range) &&
					BaseMenu.Root["harass"]["whitelists"]["normalQ"][t.CharName.ToLower()].Enabled))
			{
				Champion.Q.CastOnUnit(target);
				break;
			}
		}

		public static void W(EntropyEventArgs args)
		{
			var wHarassMenu = BaseMenu.Root["harass"]["w"];
			if (!wHarassMenu.Enabled ||
			    LocalPlayer.Instance.MPPercent() <= ManaManager.GetNeededMana(Champion.W.Slot, wHarassMenu))
			{
				return;
			}

			foreach (var target in Extensions.GetBestSortedTargetsInRange(Champion.W.Range)
				.Where(t =>
					!t.IsValidTarget(Champion.W.Range) &&
					BaseMenu.Root["harass"]["whitelists"]["w"][t.CharName.ToLower()].Enabled))
			{
				Champion.W.Cast(target);
				break;
			}
		}
	}
}
