namespace Entropy.AIO.Champions.Lucian.Spells
{
	using General;
	using SDK.Events;
	using SDK.Spells;

	sealed class R : BaseSpell
	{
		protected override void SetSpellData()
		{
			this.Spell = new Spell(SpellSlot.R, 1150f);
			this.Spell.SetSkillshot(0.25f, 110f, 2500f, collision: false);
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
