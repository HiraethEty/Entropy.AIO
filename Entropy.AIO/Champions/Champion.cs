// ReSharper disable VirtualMemberCallInConstructor

namespace Entropy.AIO.Champions
{
	using General;
	using Lucian.Drawings;
	using Lucian.Spells;
	using SDK.Events;
	using SDK.Orbwalking.EventArgs;
	using SDK.Spells;

	public abstract class Champion
	{
		protected abstract void LoadSpells();
		protected Champion()
		{
			this.LoadSpells();
		}

		internal Q Q { get; set; }
		internal W W { get; set; }
		internal E E { get; set; }
		internal R R { get; set; }
		internal Drawing Drawing { get; set; }

		internal static Spell[] Spells { get; set; }
		internal static BaseDamage DamageValues { get; set; }

		public virtual void OnTick(EntropyEventArgs args) { }
		public virtual void OnCustomTick(EntropyEventArgs args) { }
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