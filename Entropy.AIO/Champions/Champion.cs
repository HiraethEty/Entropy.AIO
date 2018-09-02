// ReSharper disable VirtualMemberCallInConstructor

namespace Entropy.AIO.Champions
{
	using General;
	using Lucian.Drawings;
	using Lucian.Spells;
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
		public virtual void OnPostAttack(OnPostAttackEventArgs args) { }
	}
}