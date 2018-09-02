using Entropy.SDK.Extensions.Objects;

namespace Entropy.AIO.Champions.Lucian.Spells
{
	using General;
	using SDK.Events;
	using SDK.Spells;

	sealed class E : BaseSpell
	{
		protected override void SetSpellData()
		{
			this.Spell = new Spell(SpellSlot.E, LocalPlayer.Instance.GetAutoAttackRange() + 425f);
		}

		protected override void SubscribeToEvents()
		{
			Tick.OnTick += this.OnTick;
		}

		public override void OnTick(EntropyEventArgs args)
		{
			this.Spell.Cast(/*target*/);
		}
	}
}
