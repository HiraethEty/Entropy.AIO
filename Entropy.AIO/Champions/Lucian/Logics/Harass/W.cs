using System.Linq;
using Entropy.AIO.General;
using Entropy.AIO.Utilities;
using Entropy.AIO.Utility;
using Entropy.SDK.Extensions.Objects;

namespace Entropy.AIO.Champions.Lucian.Logics
{
	internal partial class Harass
	{
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
