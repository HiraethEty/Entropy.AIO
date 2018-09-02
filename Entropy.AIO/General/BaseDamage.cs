namespace Entropy.AIO.General
{
	internal abstract class BaseDamage
	{
		public virtual float QDamage(AIBaseClient target) => 0f;
		public virtual float WDamage(AIBaseClient target) => 0f;
		public virtual float EDamage(AIBaseClient target) => 0f;
		public virtual float RDamage(AIBaseClient target) => 0f;
	}
}
