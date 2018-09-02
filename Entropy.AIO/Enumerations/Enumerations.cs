namespace Entropy.AIO.Enumerations
{
	using SDK.Enumerations;

	internal static class Enumerations
	{
		#region Static Fields

		/// <summary>
		///     Gets the spellslots.
		/// </summary>
		public static readonly SpellSlot[] SpellSlots =
		{
			SpellSlot.Q,
			SpellSlot.W,
			SpellSlot.E,
			SpellSlot.R
		};

		/// <summary>
		///     Gets the summoner spellslots.
		/// </summary>
		public static SpellSlot[] SummonerSpellSlots =
		{
			SpellSlot.Summoner1,
			SpellSlot.Summoner2
		};

		/// <summary>
		///     Gets the tear-like items.
		/// </summary>
		public static readonly ItemID[] TearLikeItems =
		{
			ItemID.Manamune,
			ItemID.ArchangelsStaff,
			ItemID.TearoftheGoddess,
			ItemID.ManamuneQuickCharge,
			ItemID.ArchangelsStaffQuickCharge,
			ItemID.TearoftheGoddessQuickCharge
		};

		/// <summary>
		///     Gets the Hydras.
		/// </summary>
		public static readonly ItemID[] Hydras =
		{
			ItemID.TitanicHydra,
			ItemID.RavenousHydra,
			ItemID.Tiamat
		};

		#endregion
	}
}
