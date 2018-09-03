namespace Entropy.AIO.Champions.Lucian.Logics
{
	using System.Linq;
	using General;
	using Misc;
	using SDK.Caching;
	using SDK.Damage;
	using SDK.Extensions.Objects;

	internal class Killsteal
	{
		public static void Q(EntropyEventArgs args)
		{
			if (!BaseMenu.Root["killSteal"]["normalQ"].Enabled)
			{
				return;
			}

			foreach (var target in ObjectCache.EnemyHeroes
				.Where(t =>
					t.IsValidTarget(Champion.Q.Range) &&
					!Invulnerable.IsInvulnerable(t, DamageType.Physical, damage: Champion.Q.GetDamage(t)) &&
					Champion.Q.GetDamage(t) >= t.GetRealHealth(DamageType.Physical)))
			{
				Champion.Q.CastOnUnit(target);
				return;
			}
		}

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

		public static void W(EntropyEventArgs args)
		{
			if (!BaseMenu.Root["killSteal"]["w"].Enabled)
			{
				return;
			}

			foreach (var target in ObjectCache.EnemyHeroes
				.Where(t =>
					t.IsValidTarget(Champion.W.Range) &&
					!Invulnerable.IsInvulnerable(t, DamageType.Magical, damage: Champion.W.GetDamage(t)) &&
					Champion.W.GetDamage(t) >= t.GetRealHealth(DamageType.Magical)))
			{
				Champion.W.Cast(target);
				return;
			}
		}
	}
}
