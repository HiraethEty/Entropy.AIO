namespace Entropy.AIO.Utility
{
	using System.Linq;
	using General;
	using SDK.Extensions.Objects;
	using SDK.UI;

	internal class ManaManager
	{
		#region Public Methods and Operators

		/// <summary>
		///     The minimum mana needed to cast the Spell from the 'slot' SpellSlot.
		/// </summary>
		public static int GetNeededMana(SpellSlot slot, IMenuComponent value)
		{
			var ignoreManaManagerMenu = BaseMenu.Root["general"]["nomanagerifblue"];
			if (ignoreManaManagerMenu != null &&
			    LocalPlayer.Instance.HasBuff("crestoftheancientgolem") &&
			    ignoreManaManagerMenu.Enabled)
			{
				return 0;
			}

			var spellData = Utilities.ManaCostArray.FirstOrDefault(v => v.Key == LocalPlayer.Instance.CharName);
			var cost = spellData.Value[slot][LocalPlayer.Instance.Spellbook.GetSpell(slot).Level - 1];
			return (int) (value.Value + cost / LocalPlayer.Instance.MaxMP * 100);
		}

		/// <summary>
		///     The minimum health needed to cast the Spell from the 'slot' SpellSlot.
		/// </summary>
		public int GetNeededHealth(SpellSlot slot, MenuComponent value)
		{
			var spellData = Utilities.ManaCostArray.FirstOrDefault(v => v.Key == LocalPlayer.Instance.CharName);
			var cost = spellData.Value[slot][LocalPlayer.Instance.Spellbook.GetSpell(slot).Level - 1];
			return (int) (value.Value + cost / LocalPlayer.Instance.MaxHP * 100);
		}

		#endregion
	}
}