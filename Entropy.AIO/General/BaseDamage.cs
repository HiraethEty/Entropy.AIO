namespace Entropy.AIO.General
{
	internal abstract class BaseDamage
	{
		public virtual float Q(AIBaseClient target) => 0f;
		public virtual float W(AIBaseClient target) => 0f;
		public virtual float E(AIBaseClient target) => 0f;
		public virtual float R(AIBaseClient target) => 0f;
	}
}
