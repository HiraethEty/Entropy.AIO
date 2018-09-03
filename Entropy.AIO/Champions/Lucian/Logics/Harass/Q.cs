namespace Entropy.AIO.Champions.Lucian.Logics.Harass
{
	using System.Linq;
	using General;
	using SDK.Extensions.Objects;
	using Utilities;
	using Utility;

	internal partial class Harass
	{
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
					!t.IsValidTarget(Champion.Q.Range) &&
					BaseMenu.Root["harass"]["whitelists"]["normalQ"][t.CharName.ToLower()].Enabled))
			{
				Champion.Q.CastOnUnit(target);
				break;
			}
		}
	}
}
