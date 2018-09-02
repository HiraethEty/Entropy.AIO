namespace Entropy.AIO.General
{
	using SDK.Events;
	using SDK.Orbwalking.EventArgs;
	using SDK.Spells;

	internal abstract class BaseSpell
	{
		public Spell Spell { get; protected set; }

		protected BaseSpell()
		{
			SetSpellData();
			SubscribeToEvents();
		}

		protected abstract void SetSpellData();
		protected abstract void SubscribeToEvents();

		public virtual void OnTick(EntropyEventArgs args) { }
		public virtual void OnCustomTick(EntropyEventArgs args) { }
		public virtual void OnRender(EntropyEventArgs args) { }
		public virtual void OnEndScene(EntropyEventArgs args) { }
		public virtual void OnPreAttack(OnPreAttackEventArgs args) { }
		public virtual void OnPostAttack(OnPostAttackEventArgs args) { }
		public virtual void OnProcessSpellCast(AIBaseClientCastEventArgs args) { }
		public virtual void OnFinishCast(AIBaseClientCastEventArgs args) { }
		public virtual void OnLocalCastSpell(SpellbookLocalCastSpellEventArgs args) { }
		public virtual void OnCreate(GameObjectCreateEventArgs args) { }
		public virtual void OnDelete(GameObjectDeleteEventArgs args) { }
		public virtual void OnLevelUp(AIBaseClientLevelUpEventArgs args) { }
		public virtual void OnNewGapcloser(Gapcloser.GapcloserArgs args) { }
	}
}
