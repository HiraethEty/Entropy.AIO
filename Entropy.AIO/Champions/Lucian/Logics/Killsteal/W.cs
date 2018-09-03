namespace Entropy.AIO.Champions.Lucian.Logics.Killsteal
{
	using System.Linq;
	using General;
	using SDK.Caching;
	using SDK.Damage;
	using SDK.Extensions.Objects;

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
