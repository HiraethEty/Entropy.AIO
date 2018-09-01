// ReSharper disable VirtualMemberCallInConstructor
namespace Entropy.AIO.Champions
{
	using SDK.Events;

	public abstract class Champion
	{
		protected Champion()
		{
			LoadMenu();
			LoadSpells();
			Tick.OnTick += OnTick;
			Renderer.OnRender += OnRender;
		}

		protected abstract void LoadMenu();
		protected abstract void LoadSpells();
		protected abstract void OnTick(EntropyEventArgs args);
		protected abstract void OnRender(EntropyEventArgs args);
		public virtual void OnEndScene(EntropyEventArgs args) { }
		public virtual void PreAttack(EntropyEventArgs args) { }
		public virtual void PostAttack(EntropyEventArgs args) { }
		public virtual void OnSpellCast(AIBaseClientCastEventArgs args) { }
	}
}
