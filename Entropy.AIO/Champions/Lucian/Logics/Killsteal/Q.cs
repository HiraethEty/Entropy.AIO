namespace Entropy.AIO.Champions.Lucian.Logics.Killsteal
{
	using System.Linq;
	using General;
	using SDK.Caching;
	using SDK.Damage;
	using SDK.Extensions.Objects;

	internal partial class Killsteal
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
	}
}
