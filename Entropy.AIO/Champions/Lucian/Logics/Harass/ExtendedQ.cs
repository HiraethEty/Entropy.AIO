using System.Linq;
using Entropy.AIO.General;
using Entropy.AIO.Utilities;
using Entropy.AIO.Utility;
using Entropy.SDK.Extensions.Objects;

namespace Entropy.AIO.Champions.Lucian.Logics
{
	internal partial class Harass
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
	}
}
