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

		public static readonly Color[] BoldColors =
		{
			new Color(255, 0, 0),
			new Color(0, 255, 0),
			new Color(0, 0, 255),
			new Color(255, 255, 255),
			new Color(0, 0, 0)
		};

		public static readonly Color[] FlatColors =
		{
			new Color(249, 202, 36),
			new Color(240, 147, 43),
			new Color(235, 77, 75),
			new Color(106, 176, 76),
			new Color(199, 236, 238)
		};

		public static readonly Color[] BrazilianColors =
		{
			new Color(0, 156, 55),
			new Color(254, 224, 0),
			new Color(0, 34, 119),
			new Color(255, 255, 255),
			new Color(0, 0, 0)
		};

		public static readonly Color[] FrenchColors =
		{
			new Color(255, 255, 255),
			new Color(255, 255, 255),
			new Color(255, 255, 255),
			new Color(255, 255, 255),
			new Color(255, 255, 255)
		};

		public static readonly Color[] Feminine =
		{
			new Color(255, 159, 243),
			new Color(243, 104, 224),
			new Color(155, 89, 182),
			new Color(142, 68, 173),
			new Color(255, 204, 204)
		};

		#endregion
	}
}