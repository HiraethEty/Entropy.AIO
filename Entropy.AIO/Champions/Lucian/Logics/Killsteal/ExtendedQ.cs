namespace Entropy.AIO.Champions.Lucian.Logics.Killsteal
{
	using System.Linq;
	using General;
	using Misc;
	using SDK.Caching;
	using SDK.Damage;
	using SDK.Extensions.Objects;
	using Spells;

	internal partial class Killsteal
	{
		public static void ExtendedQ(EntropyEventArgs args)
		{
			if (!BaseMenu.Root["killSteal"]["extendedQ"].Enabled)
			{
				return;
			}

			foreach (var target in ObjectCache.EnemyHeroes
				.Where(t =>
					t.IsValidTarget(Spells.ExtendedQ.Range) &&
					!Invulnerable.IsInvulnerable(t, DamageType.Physical, damage: Champion.Q.GetDamage(t)) &&
					Champion.Q.GetDamage(t) >= t.GetRealHealth(DamageType.Physical)))
			{
				foreach (var minion in ObjectCache.EnemyLaneMinions
					.Where(m => m.IsValidTarget(Champion.Q.Range) && Definitions.QRectangle(m).IsInsidePolygon(Spells.ExtendedQ.GetPrediction(target).CastPosition)))
				{
					Champion.Q.CastOnUnit(minion);
					return;
				}
			}
		}
	}
}
