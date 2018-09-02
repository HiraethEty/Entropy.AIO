// ReSharper disable VirtualMemberCallInConstructor

namespace Entropy.AIO.Champions
{
	using SDK.Events;
	using SDK.Orbwalking.EventArgs;
	using SDK.Spells;

	public abstract class Champion
	{
		internal Spell Q { get; set; }
		internal Spell W { get; set; }
		internal Spell E { get; set; }
		internal Spell R { get; set; }

		protected Champion()
		{
			this.LoadMenu();
			this.LoadSpells();
		}

		protected abstract void LoadMenu();
		protected abstract void LoadSpells();

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