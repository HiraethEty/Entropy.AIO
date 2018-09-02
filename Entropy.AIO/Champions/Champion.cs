// ReSharper disable VirtualMemberCallInConstructor
using Entropy.SDK.Orbwalking.EventArgs;

namespace Entropy.AIO.Champions
{
	using SDK.Spells;

	public abstract class Champion
	{
		internal Spell Q { get; set; }
		internal Spell W { get; set; }
		internal Spell E { get; set; }
		internal Spell R { get; set; }

		protected Champion()
		{
			LoadMenu();
			LoadSpells();
		}

		protected abstract void LoadMenu();
		protected abstract void LoadSpells();

		public virtual void OnTick(EntropyEventArgs args)
		{
		}

		public virtual void OnRender(EntropyEventArgs args)
		{
		}

		public virtual void OnEndScene(EntropyEventArgs args)
		{
		}

		public virtual void OnPreAttack(OnPreAttackEventArgs args)
		{
		}

		public virtual void OnPostAttack(OnPostAttackEventArgs args)
		{
		}

		public virtual void OnProcessSpellCast(AIBaseClientCastEventArgs args)
		{
		}

		public virtual void OnFinishCast(AIBaseClientCastEventArgs args)
		{
		}

		public virtual void OnLocalCastSpell(SpellbookLocalCastSpellEventArgs args)
		{
		}

		public virtual void OnCreate(GameObjectCreateEventArgs args)
		{
		}

		public virtual void OnDelete(GameObjectDeleteEventArgs args)
		{
		}

		public virtual void OnLevelUp(AIBaseClientLevelUpEventArgs args)
		{
		}

		public virtual void OnNewGapcloser(Utility.Gapcloser.GapcloserArgs args)
		{
		}
	}
}
