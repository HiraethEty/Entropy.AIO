namespace Entropy.AIO.Utility
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using SDK.Enumerations;
	using SDK.Extensions.Objects;
	using SDK.Spells;

	internal static class Utilities
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

		#endregion

		#region Public Methods and Operators

		/// <summary>
		///     The PreserveMana Dictionary.
		/// </summary>
		public static readonly Dictionary<SpellSlot, int> PreserveManaData = new Dictionary<SpellSlot, int>();

		/// <summary>
		///     The ManaCost Array.
		/// </summary>
		public static readonly Dictionary<string, Dictionary<SpellSlot, int[]>> ManaCostArray =
			new Dictionary<string, Dictionary<SpellSlot, int[]>>
			{
				{
					"Ahri", new Dictionary<SpellSlot, int[]>
					{
						{
							SpellSlot.Q, new[]
							{
								65, 70, 75, 80, 85
							}
						},
						{
							SpellSlot.W, new[]
							{
								50, 50, 50, 50, 50
							}
						},
						{
							SpellSlot.E, new[]
							{
								85, 85, 85, 85, 85
							}
						},
						{
							SpellSlot.R, new[]
							{
								100, 100, 100
							}
						}
					}
				},
				{
					"Akali", new Dictionary<SpellSlot, int[]>
					{
						{
							SpellSlot.Q, new[]
							{
								60, 60, 60, 60, 60
							}
						},
						{
							SpellSlot.W, new[]
							{
								60, 55, 50, 45, 40
							}
						},
						{
							SpellSlot.E, new[]
							{
								60, 55, 50, 45, 40
							}
						},
						{
							SpellSlot.R, new[]
							{
								0, 0, 0
							}
						}
					}
				},
				{
					"Anivia", new Dictionary<SpellSlot, int[]>
					{
						{
							SpellSlot.Q, new[]
							{
								80, 90, 100, 110, 120
							}
						},
						{
							SpellSlot.W, new[]
							{
								70, 70, 70, 70, 70
							}
						},
						{
							SpellSlot.E, new[]
							{
								50, 60, 70, 80, 90
							}
						},
						{
							SpellSlot.R, new[]
							{
								75, 75, 75
							}
						}
					}
				},
				{
					"Ashe", new Dictionary<SpellSlot, int[]>
					{
						{
							SpellSlot.Q, new[]
							{
								50, 50, 50, 50, 50
							}
						},
						{
							SpellSlot.W, new[]
							{
								50, 50, 50, 50, 50
							}
						},
						{
							SpellSlot.E, new[]
							{
								0, 0, 0, 0, 0
							}
						},
						{
							SpellSlot.R, new[]
							{
								100, 100, 100
							}
						}
					}
				},
				{
					"Caitlyn", new Dictionary<SpellSlot, int[]>
					{
						{
							SpellSlot.Q, new[]
							{
								50, 60, 70, 80, 90
							}
						},
						{
							SpellSlot.W, new[]
							{
								20, 20, 20, 20, 20
							}
						},
						{
							SpellSlot.E, new[]
							{
								75, 75, 75, 75, 75
							}
						},
						{
							SpellSlot.R, new[]
							{
								100, 100, 100
							}
						}
					}
				},
				{
					"Corki", new Dictionary<SpellSlot, int[]>
					{
						{
							SpellSlot.Q, new[]
							{
								60, 70, 80, 90, 100
							}
						},
						{
							SpellSlot.W, new[]
							{
								100, 100, 100, 100, 100
							}
						},
						{
							SpellSlot.E, new[]
							{
								50, 50, 50, 50, 50
							}
						},
						{
							SpellSlot.R, new[]
							{
								20, 20, 20
							}
						}
					}
				},
				{
					"Darius", new Dictionary<SpellSlot, int[]>
					{
						{
							SpellSlot.Q, new[]
							{
								30, 35, 40, 45, 50
							}
						},
						{
							SpellSlot.W, new[]
							{
								30, 30, 30, 30, 30
							}
						},
						{
							SpellSlot.E, new[]
							{
								45, 45, 45, 45, 45
							}
						},
						{
							SpellSlot.R, new[]
							{
								100, 100, 0
							}
						}
					}
				},
				{
					"Evelynn", new Dictionary<SpellSlot, int[]>
					{
						{
							SpellSlot.Q, new[]
							{
								40, 45, 50, 55, 60
							}
						},
						{
							SpellSlot.W, new[]
							{
								60, 70, 80, 90, 100
							}
						},
						{
							SpellSlot.E, new[]
							{
								40, 45, 50, 55, 60
							}
						},
						{
							SpellSlot.R, new[]
							{
								100, 100, 100
							}
						}
					}
				},
				{
					"Ezreal", new Dictionary<SpellSlot, int[]>
					{
						{
							SpellSlot.Q, new[]
							{
								28, 31, 34, 37, 40
							}
						},
						{
							SpellSlot.W, new[]
							{
								50, 60, 70, 80, 90
							}
						},
						{
							SpellSlot.E, new[]
							{
								90, 90, 90, 90, 90
							}
						},
						{
							SpellSlot.R, new[]
							{
								100, 100, 100
							}
						}
					}
				},
				{
					"Jhin", new Dictionary<SpellSlot, int[]>
					{
						{
							SpellSlot.Q, new[]
							{
								40, 45, 50, 55, 60
							}
						},
						{
							SpellSlot.W, new[]
							{
								50, 60, 70, 80, 90
							}
						},
						{
							SpellSlot.E, new[]
							{
								30, 35, 40, 45, 50
							}
						},
						{
							SpellSlot.R, new[]
							{
								100, 100, 100
							}
						}
					}
				},
				{
					"Jinx", new Dictionary<SpellSlot, int[]>
					{
						{
							SpellSlot.Q, new[]
							{
								20, 20, 20, 20, 20
							}
						},
						{
							SpellSlot.W, new[]
							{
								50, 60, 70, 80, 90
							}
						},
						{
							SpellSlot.E, new[]
							{
								70, 70, 70, 70, 70
							}
						},
						{
							SpellSlot.R, new[]
							{
								100, 100, 100
							}
						}
					}
				},
				{
					"Kaisa", new Dictionary<SpellSlot, int[]>
					{
						{
							SpellSlot.Q, new[]
							{
								55, 55, 55, 55, 55
							}
						},
						{
							SpellSlot.W, new[]
							{
								55, 60, 65, 70, 75
							}
						},
						{
							SpellSlot.E, new[]
							{
								30, 30, 30, 30, 30
							}
						},
						{
							SpellSlot.R, new[]
							{
								100, 100, 100
							}
						}
					}
				},
				{
					"Kalista", new Dictionary<SpellSlot, int[]>
					{
						{
							SpellSlot.Q, new[]
							{
								50, 55, 60, 65, 70
							}
						},
						{
							SpellSlot.W, new[]
							{
								20, 20, 20, 20, 20
							}
						},
						{
							SpellSlot.E, new[]
							{
								30, 30, 30, 30, 30
							}
						},
						{
							SpellSlot.R, new[]
							{
								100, 100, 100
							}
						}
					}
				},
				{
					"KogMaw", new Dictionary<SpellSlot, int[]>
					{
						{
							SpellSlot.Q, new[]
							{
								40, 40, 40, 40, 40
							}
						},
						{
							SpellSlot.W, new[]
							{
								40, 40, 40, 40, 40
							}
						},
						{
							SpellSlot.E, new[]
							{
								80, 90, 100, 110, 120
							}
						},
						{
							SpellSlot.R, new[]
							{
								40, 40, 40
							}
						}
					}
				},
				{
					"Lucian", new Dictionary<SpellSlot, int[]>
					{
						{
							SpellSlot.Q, new[]
							{
								50, 60, 70, 80, 90
							}
						},
						{
							SpellSlot.W, new[]
							{
								50, 50, 50, 50, 50
							}
						},
						{
							SpellSlot.E, new[]
							{
								40, 20, 30, 10, 0
							}
						},
						{
							SpellSlot.R, new[]
							{
								100, 100, 100
							}
						}
					}
				},
				{
					"MissFortune", new Dictionary<SpellSlot, int[]>
					{
						{
							SpellSlot.Q, new[]
							{
								43, 46, 49, 52, 55
							}
						},
						{
							SpellSlot.W, new[]
							{
								30, 30, 30, 30, 30
							}
						},
						{
							SpellSlot.E, new[]
							{
								80, 80, 80, 80, 80
							}
						},
						{
							SpellSlot.R, new[]
							{
								100, 100, 100
							}
						}
					}
				},
				{
					"Olaf", new Dictionary<SpellSlot, int[]>
					{
						{
							SpellSlot.Q, new[]
							{
								60, 60, 60, 60, 60
							}
						},
						{
							SpellSlot.W, new[]
							{
								30, 30, 30, 30, 30
							}
						},
						{
							SpellSlot.E, new[]
							{
								0, 0, 0, 0, 0
							}
						},
						{
							SpellSlot.R, new[]
							{
								100, 90, 80
							}
						}
					}
				},
				{
					"Orianna", new Dictionary<SpellSlot, int[]>
					{
						{
							SpellSlot.Q, new[]
							{
								30, 35, 40, 45, 50
							}
						},
						{
							SpellSlot.W, new[]
							{
								70, 80, 90, 100, 110
							}
						},
						{
							SpellSlot.E, new[]
							{
								60, 60, 60, 60, 60
							}
						},
						{
							SpellSlot.R, new[]
							{
								100, 100, 100
							}
						}
					}
				},
				{
					"Quinn", new Dictionary<SpellSlot, int[]>
					{
						{
							SpellSlot.Q, new[]
							{
								50, 55, 60, 65, 70
							}
						},
						{
							SpellSlot.W, new[]
							{
								0, 0, 0, 0, 0
							}
						},
						{
							SpellSlot.E, new[]
							{
								50, 50, 50, 50, 50
							}
						},
						{
							SpellSlot.R, new[]
							{
								100, 50, 0
							}
						}
					}
				},
				{
					"Sivir", new Dictionary<SpellSlot, int[]>
					{
						{
							SpellSlot.Q, new[]
							{
								70, 80, 90, 100, 110
							}
						},
						{
							SpellSlot.W, new[]
							{
								60, 60, 60, 60, 60
							}
						},
						{
							SpellSlot.E, new[]
							{
								0, 0, 0, 0, 0
							}
						},
						{
							SpellSlot.R, new[]
							{
								100, 100, 100
							}
						}
					}
				},
				{
					"Syndra", new Dictionary<SpellSlot, int[]>
					{
						{
							SpellSlot.Q, new[]
							{
								40, 50, 60, 70, 80
							}
						},
						{
							SpellSlot.W, new[]
							{
								60, 70, 80, 90, 100
							}
						},
						{
							SpellSlot.E, new[]
							{
								50, 50, 50, 50, 50
							}
						},
						{
							SpellSlot.R, new[]
							{
								100, 100, 100
							}
						}
					}
				},
				{
					"Taliyah", new Dictionary<SpellSlot, int[]>
					{
						{
							SpellSlot.Q, new[]
							{
								60, 70, 80, 90, 100
							}
						},
						{
							SpellSlot.W, new[]
							{
								70, 80, 90, 100, 110
							}
						},
						{
							SpellSlot.E, new[]
							{
								90, 95, 100, 105, 110
							}
						},
						{
							SpellSlot.R, new[]
							{
								100, 100, 100
							}
						}
					}
				},
				{
					"Tristana", new Dictionary<SpellSlot, int[]>
					{
						{
							SpellSlot.Q, new[]
							{
								0, 0, 0, 0, 0
							}
						},
						{
							SpellSlot.W, new[]
							{
								60, 60, 60, 60, 60
							}
						},
						{
							SpellSlot.E, new[]
							{
								70, 75, 80, 85, 90
							}
						},
						{
							SpellSlot.R, new[]
							{
								100, 100, 100
							}
						}
					}
				},
				{
					"Twitch", new Dictionary<SpellSlot, int[]>
					{
						{
							SpellSlot.Q, new[]
							{
								40, 40, 40, 40, 40
							}
						},
						{
							SpellSlot.W, new[]
							{
								70, 70, 70, 70, 70
							}
						},
						{
							SpellSlot.E, new[]
							{
								50, 60, 70, 80, 90
							}
						},
						{
							SpellSlot.R, new[]
							{
								100, 100, 100
							}
						}
					}
				},
				{
					"Varus", new Dictionary<SpellSlot, int[]>
					{
						{
							SpellSlot.Q, new[]
							{
								70, 75, 80, 85, 90
							}
						},
						{
							SpellSlot.W, new[]
							{
								0, 0, 0, 0, 0
							}
						},
						{
							SpellSlot.E, new[]
							{
								80, 80, 80, 80, 80
							}
						},
						{
							SpellSlot.R, new[]
							{
								100, 100, 100
							}
						}
					}
				},
				{
					"Vayne", new Dictionary<SpellSlot, int[]>
					{
						{
							SpellSlot.Q, new[]
							{
								30, 30, 30, 30, 30
							}
						},
						{
							SpellSlot.W, new[]
							{
								0, 0, 0, 0, 0
							}
						},
						{
							SpellSlot.E, new[]
							{
								90, 90, 90, 90, 90
							}
						},
						{
							SpellSlot.R, new[]
							{
								80, 80, 80
							}
						}
					}
				},
				{
					"Xayah", new Dictionary<SpellSlot, int[]>
					{
						{
							SpellSlot.Q, new[]
							{
								50, 50, 50, 50, 50
							}
						},
						{
							SpellSlot.W, new[]
							{
								60, 55, 50, 45, 40
							}
						},
						{
							SpellSlot.E, new[]
							{
								40, 40, 40, 40, 40
							}
						},
						{
							SpellSlot.R, new[]
							{
								80, 90, 100
							}
						}
					}
				}
			};

		/// <summary>
		///     Gets the mana cost of a spell using the ManaCostArray.
		/// </summary>
		/// <param name="slot">
		///     The spellslot.
		/// </param>
		/// <returns>
		///     The mana cost.
		/// </returns>
		public static int GetManaCost(this SpellSlot slot)
		{
			var championSlots = ManaCostArray.FirstOrDefault(e => e.Key == LocalPlayer.Instance.CharName).Value;
			var selectedSlot = championSlots.FirstOrDefault(e => e.Key == slot);
			var selectedSlotLevel = selectedSlot.Value[LocalPlayer.Instance.Spellbook.GetSpell(slot).Level - 1];

			return selectedSlotLevel;
		}

		/// <summary>
		///     Gets the angle by 'degrees' degrees.
		/// </summary>
		/// <param name="degrees">
		///     The angle degrees.
		/// </param>
		/// <returns>
		///     The angle by 'degrees' degrees.
		/// </returns>
		public static float GetAngleByDegrees(float degrees) => (float) (degrees * Math.PI / 180);

		/// <summary>
		///     Gets the remaining buff time of the 'buff' Buff in seconds.
		/// </summary>
		/// <param name="buff">
		///     The buff.
		/// </param>
		/// <returns>
		///     The remaining buff time.
		/// </returns>
		public static double GetRemainingBuffTime(this BuffInstance buff) => buff.ExpireTime - Game.ClockTime;

		/// <summary>
		///     Gets the remaining cooldown time of the 'spell' spell.
		/// </summary>
		/// <param name="spell">
		///     The spell.
		/// </param>
		/// <returns>
		///     The remaining cooldown time.
		/// </returns>
		public static double GetRemainingCooldownTime(this Spell spell) => spell.CooldownExpires - Game.ClockTime;

		public static float RangeMultiplier(float range)
		{
			if (LocalPlayer.Instance.HasItem(ItemID.RapidFirecannon) &&
			    LocalPlayer.Instance.GetBuffStacks("itemstatikshankcharge") == 100)
			{
				return Math.Min(range * 0.35f, 150);
			}

			return 0;
		}

		#endregion
	}
}