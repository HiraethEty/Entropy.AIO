using System.Linq;
using Entropy.AIO.General;
using Entropy.SDK.Caching;
using Entropy.SDK.Damage;
using Entropy.SDK.Extensions.Objects;

namespace Entropy.AIO.Champions.Lucian.Logics
{
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
