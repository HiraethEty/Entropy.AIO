namespace Entropy.AIO.Utility
{
	using System.Collections.Generic;
	using System.Linq;
	using AIO.Utilities;
	using General;
	using SDK.Caching;
	using SDK.Extensions.Geometry;
	using SharpDX;

	class FarmManager
	{
		public static FarmLocation GetLinearFarmLocation(float width, float range)
		{
			var minionPositions = ObjectCache.EnemyLaneMinions.Select(minion => minion.Position.To2D()).ToList();

			var result = new Vector2();
			var minionCount = 0;
			var startPos = LocalPlayer.Instance.Position.To2D();

			var possiblePositions = new List<Vector2>();
			possiblePositions.AddRange(minionPositions);

			var max = minionPositions.Count;
			for (var i = 0; i < max; i++)
			{
				for (var j = 0; j < max; j++)
				{
					if (minionPositions[j] != minionPositions[i])
					{
						possiblePositions.Add((minionPositions[j] + minionPositions[i]) / 2);
					}
				}
			}

			foreach (var pos in possiblePositions)
			{
				if (!(pos.DistanceSquared(startPos) <= range * range))
				{
					continue;
				}
				var endPos = startPos + range * (pos - startPos).Normalized();

				var count = minionPositions.Count(pos2 => pos2.Distance(startPos, endPos, true, true) <= width * width);

				if (count < minionCount)
				{
					continue;
				}
				result = pos;
				minionCount = count;
			}

			return new FarmLocation(ObjectCache.EnemyLaneMinions.OrderBy(x => x.Distance(result.To3D())).FirstOrDefault(), minionCount);
		}

//		public static FarmLocation GetCircularFarmLocation(float width, float range, int useMECMax = 9)
//		{
//			var minionPositions = ObjectCache.EnemyLaneMinions.Select(minion => minion.Position.To2D()).ToList();
//
//			var result = new Vector2();
//			var minionCount = 0;
//			var startPos = LocalPlayer.Instance.Position.To2D();
//
//			range = range * range;
//
//			if (minionPositions.Count == 0)
//			{
//				return new FarmLocation(result, minionCount);
//			}
//
//			if (minionPositions.Count <= useMECMax)
//			{
//				var subGroups = GetCombinations(minionPositions);
//				foreach (var subGroup in subGroups)
//				{
//					if (subGroup.Count <= 0)
//					{
//						continue;
//					}
//					var circle = MEC.GetMec(subGroup);
//
//					if (!(circle.Radius <= width) || !(circle.Center.DistanceSquared(startPos) <= range))
//					{
//						continue;
//					}
//					minionCount = subGroup.Count;
//					return new FarmLocation(circle.Center, minionCount);
//				}
//			}
//			else
//			{
//				foreach (var pos in minionPositions)
//				{
//					if (!(pos.DistanceSquared(startPos) <= range))
//					{
//						continue;
//					}
//					var count = minionPositions.Count(pos2 => pos.DistanceSquared(pos2) <= width * width);
//
//					if (count < minionCount)
//					{
//						continue;
//					}
//					result = pos;
//					minionCount = count;
//				}
//			}
//
//			return new FarmLocation(result, minionCount);
//		}

		private static IEnumerable<List<Vector2>> GetCombinations(IReadOnlyCollection<Vector2> allValues)
		{
			var collection = new List<List<Vector2>>();
			for (var counter = 0; counter < (1 << allValues.Count); ++counter)
			{
				var combination = allValues.Where((t, i) => (counter & (1 << i)) == 0).ToList();

				collection.Add(combination);
			}
			return collection;
		}

		public struct FarmLocation
		{
			#region Fields

			/// <summary>
			///     The minions hit
			/// </summary>
			public int MinionsHit;

			/// <summary>
			///     The position
			/// </summary>
			public AIMinionClient Position;

			#endregion

			#region Constructors and Destructors

			/// <summary>
			///     Initializes a new instance of the <see cref="FarmLocation" /> struct.
			/// </summary>
			/// <param name="position">The position.</param>
			/// <param name="minionsHit">The minions hit.</param>
			public FarmLocation(AIMinionClient position, int minionsHit)
			{
				this.Position = position;
				this.MinionsHit = minionsHit;
			}

			#endregion
		}
	}
}
