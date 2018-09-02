using System.Linq;
using Entropy.SDK.Caching;
using Entropy.SDK.Damage;
using Entropy.SDK.Extensions.Objects;

namespace Entropy.AIO.Champions.Lucian.Spells
{
	using General;
	using SDK.Events;
	using SDK.Spells;

	internal sealed class Q : BaseSpell
	{
		protected override void SetSpellData()
		{
			this.Spell = new Spell(SpellSlot.Q, 550f);
		}

		protected override void SubscribeToEvents()
		{
			new CustomTick(2000).OnTick += this.OnCustomTick;
		}

		public override void OnCustomTick(EntropyEventArgs args)
		{
			if (!this.Spell.Ready ||
			    !BaseMenu.Root["killsteal"]["normalQ"].Enabled)
			{
				return;
			}

			// Normal Q Killsteal
			foreach (var target in ObjectCache.EnemyHeroes
				.Where(t =>
					t.IsValidTarget(this.Spell.Range) &&
					!Invulnerable.IsInvulnerable(t, DamageType.Physical, damage: this.Spell.GetDamage(t)) &&
					this.Spell.GetDamage(t) >= t.GetRealHealth(DamageType.Physical)))
			{
				this.Spell.CastOnUnit(target);
				break;
			}
		}
	}
}
