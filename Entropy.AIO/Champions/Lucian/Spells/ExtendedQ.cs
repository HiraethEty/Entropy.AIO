namespace Entropy.AIO.Champions.Lucian.Spells
{
	using General;
	using SDK.Events;
	using SDK.Spells;

	sealed class ExtendedQ : BaseSpell
	{
		protected override void SetSpellData()
		{
			this.Spell = new Spell(SpellSlot.Q, 950f - LocalPlayer.Instance.BoundingRadius);
			this.Spell.SetSkillshot(0.25f, 65f, float.MaxValue, collision: false);
			//this.Spell.SetCustomDamageCalculateFunction(Champion.DamageValues.Q);
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
