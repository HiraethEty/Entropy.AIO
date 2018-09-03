namespace Entropy.AIO.Champions.Lucian.Logics.Harass
{
	using System.Linq;
	using General;
	using SDK.Extensions.Objects;
	using Utilities;
	using Utility;

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
