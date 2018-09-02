namespace Entropy.AIO.Champions.Vayne.Spells
{
	using General;
	using SDK.Spells;

	class Q : BaseSpell
	{
		protected override void SetSpellData()
		{
			this.Spell = new Spell(SpellSlot.Q, 300);
		}
		protected override void SubscribeToEvents() { }
	}
}
