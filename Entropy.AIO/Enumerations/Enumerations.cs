namespace Entropy.AIO.Enumerations
{
	using SDK.Enumerations;
	using SharpDX;

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

		public static readonly Color[] Colors =
		{
			new Color(249,202,36), 
			new Color(240,147,43), 
			new Color(235,77,75), 
			new Color(106,176,76), 
			new Color(199,236,238)
		};

		#endregion
	}
}
