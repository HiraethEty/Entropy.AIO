using System.Linq;
using Entropy.SDK.Caching;
using Entropy.SDK.Damage;
using Entropy.SDK.Extensions.Objects;
using Entropy.SDK.Orbwalking;

namespace Entropy.AIO.Champions.Lucian.Spells
{
	using General;
	using SDK.Events;
	using SDK.Spells;

	internal sealed class W : BaseSpell
	{
		protected override void SetSpellData()
		{
			this.Spell = new Spell(SpellSlot.W, 900f);
			this.Spell.SetSkillshot(0.25f, 80f, 1600f, collision: false);
		}

		protected override void SubscribeToEvents()
		{
			new CustomTick(2000).OnTick += this.OnCustomTick;
		}

		public override void OnCustomTick(EntropyEventArgs args)
		{
			if (LocalPlayer.Instance.IsDead || Orbwalker.IsWindingUp)
			{
				return;
			}

			if (!this.Spell.Ready ||
			    !BaseMenu.Root["killSteal"]["w"].Enabled)
			{
				return;
			}

			// Killsteal W
			foreach (var target in ObjectCache.EnemyHeroes
				.Where(t =>
					t.IsValidTarget(this.Spell.Range) &&
					!Invulnerable.IsInvulnerable(t, DamageType.Magical, damage: this.Spell.GetDamage(t)) &&
					this.Spell.GetDamage(t) >= t.GetRealHealth(DamageType.Magical)))
			{
				this.Spell.Cast(target);
				break;
			}
		}
	}
}
