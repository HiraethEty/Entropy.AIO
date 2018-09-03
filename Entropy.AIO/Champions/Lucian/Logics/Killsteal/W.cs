using System.Linq;
using Entropy.AIO.General;
using Entropy.SDK.Caching;
using Entropy.SDK.Damage;
using Entropy.SDK.Extensions.Objects;

namespace Entropy.AIO.Champions.Lucian.Logics
{
	internal partial class Killsteal
	{
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
