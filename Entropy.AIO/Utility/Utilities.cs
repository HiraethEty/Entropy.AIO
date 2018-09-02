using Entropy.SDK.Caching;
using Entropy.SDK.Events;
using Entropy.SDK.Extensions;
using Entropy.SDK.Extensions.Geometry;
using Entropy.SDK.Orbwalking;
using SharpDX;

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

		#region Public Methods and Operators

		/// <summary>
		///     Returns if a Vector2 position is On Screen.
		/// </summary>
		/// <param name="position">The position.</param>
		public static bool OnScreen(this Vector2 position)
		{
			return !position.IsZero;
		}

		/// <summary>
		///     Returns if the name is an auto attack
		/// </summary>
		/// <param name="name">Name of spell</param>
		/// <returns>The <see cref="bool" /></returns>
		public static bool IsAutoAttack(string name)
		{
			name = name.ToLower();
			return name.Contains("attack") && !Orbwalker.NoAttacks.Contains(name) ||
				   Orbwalker.SpecialAttacks.Contains(name);
		}

		/// <returns>
		///     true if an unit has a Sheen-Like buff; otherwise, false.
		/// </returns>
		public static bool HasSheenLikeBuff(this AIHeroClient unit)
		{
			var sheenLikeBuffNames = new[]{"sheen", "LichBane", "dianaarcready", "ItemFrozenFist", "sonapassiveattack"};
			return sheenLikeBuffNames.Any(b => LocalPlayer.Instance.HasBuff(b));
		}

		/// <summary>
		///     Gets a value indicating whether a determined hero has a stackable item.
		/// </summary>
		public static bool HasTearLikeItem(this AIHeroClient unit)
		{
			return TearLikeItems.Any(p => LocalPlayer.Instance.HasItem(p));
		}

		/// <summary>
		///     Gets a value indicating whether a determined hero has a stackable item.
		/// </summary>
		public static bool IsTearLikeItemReady(this AIHeroClient unit)
		{
			if (!LocalPlayer.Instance.HasTearLikeItem())
			{
				return false;
			}

			var slot = LocalPlayer.Instance.InventorySlots.FirstOrDefault(s => TearLikeItems.Contains((ItemID)s.ItemID));
			if (slot != null)
			{
				var spellSlot = slot.Slot;
				if (spellSlot != SpellSlot.Unknown &&
					!LocalPlayer.Instance.Spellbook.GetSpellState(spellSlot).HasFlag(SpellState.Cooldown))
				{
					return true;
				}
			}

			return false;
		}

		/// <summary>
		///     Returns true if there is a Wall between X pos and Y pos.
		/// </summary>
		public static bool AnyWallInBetween(Vector3 startPos, Vector3 endPos)
		{
			for (var i = 0; i < startPos.Distance(endPos); i += 5)
			{
				var point = NavGrid.WorldToCell(startPos.Extend(endPos, i));
				if (point.IsWall() || point.IsBuilding())
				{
					return true;
				}
			}

			return false;
		}

		/// <summary>
		///     Returns true if a the player is being grabbed by an enemy unit.
		/// </summary>
		public static bool IsBeingGrabbed(this AIHeroClient hero)
		{
			var grabsBuffs = new[] { "ThreshQ", "rocketgrab2" };
			return hero.GetActiveBuffs().Any(b => grabsBuffs.Contains(b.Name));
		}

		/// <summary>
		///     Returns true if a the player is being grabbed by an enemy unit.
		/// </summary>
		public static bool HasImmobileBuff(this AIHeroClient hero)
		{
			// Objects: Guardian Angel..
			var immobileObjectLinked =
				ObjectCache.AllGameObjects.FirstOrDefault(t => t.IsValid && t.Name == "LifeAura.troy");
			if (immobileObjectLinked != null &&
				ObjectCache.AllHeroes.MinBy(t => t.Distance(immobileObjectLinked)) == hero)
			{
				return true;
			}

			// Minions: Zac Passive
			if (hero.CharName == "Zac" &&
				ObjectCache.AllMinions.Any(m =>
					m.Team == hero.Team && m.CharName == "ZacRebirthBloblet" && m.Distance(hero) < 500))
			{
				return true;
			}

			// Buffs: Zilean's Chronoshift, Zhonyas, Stopwatch, Anivia Egg,
			var immobileBuffs = new[] { "chronorevive", "zhonyasringshield", "rebirth" };
			if (hero.GetActiveBuffs().Any(b => immobileBuffs.Contains(b.Name)))
			{
				return true;
			}

			return false;
		}

		/// <summary>
		///     Returns true if a determined buff is a Hard CC Buff.
		/// </summary>
		// ReSharper disable once InconsistentNaming
		public static bool IsHardCC(this BuffInstance buff)
		{
			// ReSharper disable once InconsistentNaming
			var hardCCList = new List<BuffType>
			{
				BuffType.Stun,
				BuffType.Fear,
				BuffType.Flee,
				BuffType.Snare,
				BuffType.Taunt,
				BuffType.Charm,
				BuffType.Knockup,
				BuffType.Suppression
			};

			return hardCCList.Contains(buff.Type);
		}

		/// <summary>
		///     Gets a value indicating whether a determined champion can move or not.
		/// </summary>
		/// <param name="unit">The hero.</param>
		/// <param name="minTime">The minimum time remaining for the CC to trigger this function.</param>
		public static bool IsImmobile(this AIBaseClient unit, double minTime)
		{
			var hero = unit as AIHeroClient;
			if (hero != null &&
				hero.HasImmobileBuff())
			{
				return true;
			}

			if (unit.IsDead ||
				unit.IsDashing() ||
				unit.HasBuffOfType(BuffType.Knockback))
			{
				return false;
			}

			return unit.GetActiveBuffs().Any(b =>
				b.IsHardCC() &&
				b.GetRemainingBuffTime() >= minTime);
		}

		/// <returns>
		///     true if the sender is a hero, a turret or an important jungle monster; otherwise, false.
		/// </returns>
		public static bool ShouldShieldAgainstSender(AIBaseClient sender)
		{
			return
				ObjectCache.EnemyHeroes.Contains(sender) ||
				ObjectCache.EnemyTurrets.Contains(sender) ||
				ObjectCache.LargeJungleMinions.Concat(ObjectCache.LegendaryJungleMinions).Contains(sender);
		}

		/// <summary>
		///     Returns whether the hero is in fountain range.
		/// </summary>
		/// <param name="hero">The Hero</param>
		/// <returns>Is Hero in fountain range</returns>
		public static bool InFountain(this AIHeroClient hero)
		{
			var heroTeam = hero.Team == GameObjectTeam.Order ? "Order" : "Chaos";
			var fountainTurret =
				ObjectCache.AllGameObjects.FirstOrDefault(o =>
					o.IsValid && o.Name == "Turret_" + heroTeam + "TurretShrine");
			if (fountainTurret == null)
			{
				return false;
			}

			return hero.Distance(fountainTurret) < 1300f;
		}

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
					"Lucian", new Dictionary<SpellSlot, int[]>
					{
						{SpellSlot.Q, new[] {50, 60, 70, 80, 90}},
						{SpellSlot.W, new[] {50, 50, 50, 50, 50}},
						{SpellSlot.E, new[] {40, 20, 30, 10, 0}},
						{SpellSlot.R, new[] {100, 100, 100}}
					}
				},
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