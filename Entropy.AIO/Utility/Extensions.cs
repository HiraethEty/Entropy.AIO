namespace Entropy.AIO.Utilities
{
	using System.Collections.Generic;
	using System.Linq;
	using General;
	using SDK.Caching;
	using SDK.Damage;
	using SDK.Extensions.Geometry;
	using SDK.Extensions.Objects;
	using SDK.Orbwalking;
	using SDK.TS;
	using SharpDX;
	using Utilities = Utility.Utilities;

	internal static class Extensions
	{
		#region Public Methods and Operators

		/// <summary>
		///     Gets the valid generic (lane or jungle) minions targets in the game.
		/// </summary>
		public static List<AIMinionClient> GetAllGenericMinionsTargets() => GetAllGenericMinionsTargetsInRange(float.MaxValue);

		/// <summary>
		///     Gets the valid generic (lane or jungle) minions targets in the game inside a determined range.
		/// </summary>
		public static List<AIMinionClient> GetAllGenericMinionsTargetsInRange(float range) => GetEnemyLaneMinionsTargetsInRange(range)
			.Concat(GetGenericJungleMinionsTargetsInRange(range))
			.ToList();

		/// <summary>
		///     Gets the valid generic unit targets in the game.
		/// </summary>
		public static List<AIBaseClient> GetAllGenericUnitTargets() => GetAllGenericUnitTargetsInRange(float.MaxValue);

		/// <summary>
		///     Gets the valid generic unit targets in the game inside a determined range.
		/// </summary>
		public static List<AIBaseClient> GetAllGenericUnitTargetsInRange(float range)
		{
			return ObjectCache.EnemyHeroes.Where(h => h.IsValidTarget(range))
			                  .Concat<AIBaseClient>(GetAllGenericMinionsTargetsInRange(range))
			                  .ToList();
		}

		/// <summary>
		///     Gets the valid enemy pet targets in the game.
		/// </summary>
		public static List<AIMinionClient> GetEnemyPets() => GetEnemyPetsInRange(float.MaxValue);

		/// <summary>
		///     Gets the valid enemy pets in the game inside a determined range.
		/// </summary>
		public static List<AIMinionClient> GetEnemyPetsInRange(float range)
		{
			return ObjectCache.EnemyMinions
			                  .Where(h => h.DistanceToPlayer() < range && Utilities.PetList.Contains(h.Name))
			                  .ToList();
		}

		/// <summary>
		///     Gets the valid ally pet targets in the game.
		/// </summary>
		public static List<AIMinionClient> GetAllyPets() => GetAllyPetsInRange(float.MaxValue);

		/// <summary>
		///     Gets the valid ally pets in the game inside a determined range.
		/// </summary>
		public static List<AIMinionClient> GetAllyPetsInRange(float range)
		{
			return ObjectCache.AllyMinions
			                  .Where(h => h.DistanceToPlayer() < range && Utilities.PetList.Contains(h.Name))
			                  .ToList();
		}

		/// <summary>
		///     Gets the valid ally heroes targets in the game.
		/// </summary>
		public static List<AIHeroClient> GetAllyHeroesTargets() => GetAllyHeroesTargetsInRange(float.MaxValue);

		/// <summary>
		///     Gets the valid ally heroes targets in the game inside a determined range.
		/// </summary>
		public static List<AIHeroClient> GetAllyHeroesTargetsInRange(float range)
		{
			return ObjectCache.AllyHeroes.Where(h => h.DistanceToPlayer() < range).ToList();
		}

		/// <summary>
		///     Gets the valid ally lane minions targets in the game.
		/// </summary>
		public static List<AIMinionClient> GetAllyLaneMinionsTargets() => GetAllyLaneMinionsTargetsInRange(float.MaxValue);

		/// <summary>
		///     Gets the valid ally lane minions targets in the game inside a determined range.
		/// </summary>
		public static List<AIMinionClient> GetAllyLaneMinionsTargetsInRange(float range)
		{
			return ObjectCache.AllyMinions.Where(m => m.DistanceToPlayer() < range).ToList();
		}

		/// <summary>
		///     Gets the best valid enemy heroes targets in the game.
		/// </summary>
		public static List<AIHeroClient> GetBestEnemyHeroesTargets() => GetBestEnemyHeroesTargetsInRange(float.MaxValue);

		/// <summary>
		///     Gets the best valid enemy heroes targets in the game inside a determined range.
		/// </summary>
		public static List<AIHeroClient> GetBestEnemyHeroesTargetsInRange(float range)
		{
			return TargetSelector.GetOrderedTargets(ObjectCache.EnemyHeroes)
			                     .Where(t => t.DistanceToPlayer() < range)
			                     .ToList();
		}

		/// <summary>
		///     Gets the best valid enemy hero target in the game.
		/// </summary>
		public static AIHeroClient GetBestEnemyHeroTarget() => GetBestEnemyHeroTargetInRange(float.MaxValue);

		/// <summary>
		///     Gets the best valid enemy hero target in the game inside a determined range.
		/// </summary>
		public static AIHeroClient GetBestEnemyHeroTargetInRange(float range)
		{
			/*
			var selectedTarget = TargetSelector.GetSelectedTarget();
			if (selectedTarget != null &&
				selectedTarget.IsValidTarget(range))
			{
				return selectedTarget;
			}*/

			var orbTarget = Orbwalker.GetOrbwalkingTarget() as AIHeroClient;
			if (orbTarget != null &&
			    orbTarget.IsValidTarget(range))
			{
				return orbTarget;
			}

			var tsTarget = TargetSelector.LastTarget;
			if (tsTarget != null &&
			    tsTarget.IsValidTarget(range))
			{
				return tsTarget;
			}

			var lastTarget = ObjectCache.EnemyHeroes.FirstOrDefault(t =>
				                                                        t.IsValidTarget(range) && !t.IsZombie() && !Invulnerable.IsInvulnerable(t));
			if (lastTarget != null)
			{
				return lastTarget;
			}

			return null;
		}

		/// <summary>
		///     Returns true if this unit is able to be targetted by spells
		/// </summary>
		/// <param name="unit">The unit.</param>
		/// <param name="range">The range.</param>
		public static bool IsValidSpellTarget(this AttackableUnit unit, float range = float.MaxValue)
		{
			if (!unit.IsValidTarget(range))
			{
				return false;
			}

			if (unit is AIHeroClient)
			{
				return true;
			}

			var mUnit = unit as AIMinionClient;
			if (mUnit == null)
			{
				return false;
			}

			if (mUnit.IsWard() || mUnit.IsPlant() || mUnit.IsBarrel() || mUnit.Name.ToLower().Contains("seed") || mUnit.Name.ToLower().Contains("beacon"))
			{
				return false;
			}

			return true;
		}

		/// <summary>
		///     Gets the best valid killable enemy hero target in the game inside a determined range.
		/// </summary>
		public static AIHeroClient GetBestSortedTarget(
			DamageType damageType = DamageType.True,
			bool ignoreShields = false)
		{
			var target = TargetSelector.GetOrderedTargets(ObjectCache.EnemyHeroes)
			                           .FirstOrDefault(t =>
				                                           !t.IsZombie() &&
				                                           !Invulnerable.IsInvulnerable(t, damageType, ignoreShields));
			return target;
		}

		/// <summary>
		///     Gets the best valid killable enemy heroes targets in the game inside a determined range.
		/// </summary>
		public static IEnumerable<AIHeroClient> GetBestSortedTargetsInRange(
			float range,
			DamageType damageType = DamageType.True,
			bool ignoreShields = false)
		{
			var targets = TargetSelector.GetOrderedTargets(ObjectCache.EnemyHeroes)
			                            .Where(t =>
				                                   !t.IsZombie() &&
				                                   t.IsValidTarget(range) &&
				                                   !Invulnerable.IsInvulnerable(t, damageType, ignoreShields));
			return targets;
		}

		/// <summary>
		///     Gets the valid enemy heroes targets in the game.
		/// </summary>
		public static List<AIHeroClient> GetEnemyHeroesTargets() => GetEnemyHeroesTargetsInRange(float.MaxValue);

		/// <summary>
		///     Gets the valid enemy heroes targets in the game inside a determined range.
		/// </summary>
		public static List<AIHeroClient> GetEnemyHeroesTargetsInRange(float range)
		{
			return ObjectCache.EnemyHeroes.Where(h => h.IsValidTarget(range)).ToList();
		}

		/// <summary>
		///     Gets the valid lane minions targets in the game.
		/// </summary>
		public static List<AIMinionClient> GetEnemyLaneMinionsTargets() => GetEnemyLaneMinionsTargetsInRange(float.MaxValue);

		/// <summary>
		///     Gets the valid lane minions targets in the game inside a determined range.
		/// </summary>
		public static List<AIMinionClient> GetEnemyLaneMinionsTargetsInRange(float range)
		{
			return ObjectCache.EnemyLaneMinions.Where(h => h.IsValidTarget(range)).ToList();
		}

		/// <summary>
		///     Gets the valid generic (All but small) jungle minions targets in the game.
		/// </summary>
		public static List<AIMinionClient> GetGenericJungleMinionsTargets() => GetGenericJungleMinionsTargetsInRange(float.MaxValue);

		/// <summary>
		///     Gets the valid generic (All but small) jungle minions targets in the game inside a determined range.
		/// </summary>
		public static List<AIMinionClient> GetGenericJungleMinionsTargetsInRange(float range)
		{
			return ObjectCache.JungleMinions
			                  .Where(m => (!m.IsSmallJungleMinion() || BaseMenu.Root["general"]["junglesmall"].Enabled) && m.IsValidSpellTarget(range))
			                  .ToList();
		}

		/// <summary>
		///     Gets the valid large jungle minions targets in the game.
		/// </summary>
		public static List<AIMinionClient> GetLargeJungleMinionsTargets() => GetLargeJungleMinionsTargetsInRange(float.MaxValue);

		/// <summary>
		///     Gets the valid large jungle minions targets in the game inside a determined range.
		/// </summary>
		public static List<AIMinionClient> GetLargeJungleMinionsTargetsInRange(float range)
		{
			return ObjectCache.LargeJungleMinions.Where(m => m.IsValidSpellTarget(range)).ToList();
		}

		/// <summary>
		///     Gets the valid legendary jungle minions targets in the game.
		/// </summary>
		public static List<AIMinionClient> GetLegendaryJungleMinionsTargets() => GetLegendaryJungleMinionsTargetsInRange(float.MaxValue);

		/// <summary>
		///     Gets the valid legendary jungle minions targets in the game inside a determined range.
		/// </summary>
		public static List<AIMinionClient> GetLegendaryJungleMinionsTargetsInRange(float range)
		{
			return ObjectCache.LegendaryJungleMinions.Where(m => m.IsValidSpellTarget(range)).ToList();
		}

		/// <summary>
		///     Gets the valid small jungle minions targets in the game.
		/// </summary>
		public static List<AIMinionClient> GetSmallJungleMinionsTargets() => GetSmallJungleMinionsTargetsInRange(float.MaxValue);

		/// <summary>
		///     Gets the valid small jungle minions targets in the game inside a determined range.
		/// </summary>
		public static List<AIMinionClient> GetSmallJungleMinionsTargetsInRange(float range)
		{
			return ObjectCache.SmallJungleMinions.Where(m => m.IsValidSpellTarget(range)).ToList();
		}

		public static float Distance(
			this Vector2 point,
			Vector2 segmentStart,
			Vector2 segmentEnd,
			bool onlyIfOnSegment = false,
			bool squared = false)
		{
			var objects = point.ProjectOn(segmentStart, segmentEnd);

			if (objects.IsOnSegment || onlyIfOnSegment == false)
			{
				return squared
					? Vector2.DistanceSquared(objects.SegmentPoint, point)
					: Vector2.Distance(objects.SegmentPoint, point);
			}
			return float.MaxValue;
		}

		#endregion
	}
}