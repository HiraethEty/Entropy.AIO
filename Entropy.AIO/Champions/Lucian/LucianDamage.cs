namespace Entropy.AIO.Champions.Lucian
{
	using General;
	using SDK.Damage;
	using SDK.Extensions.Objects;
	using SDK.Spells;

	class LucianDamage : BaseDamage
	{
		private readonly Spell[] Spells;
		public LucianDamage(Spell[] spells)
		{
			this.Spells = spells;
		}

		public override float QDamage(AIBaseClient target)
		{
			var qLevel = this.Spells[0].Level;

			var qBaseDamage = new[] { 0f, 85f, 120f, 155f, 190f, 225f }[qLevel]
			                  + new[] { 0f, 0.6f, 0.7f, 0.8f, 0.8f, 1f }[qLevel]
			                  * LocalPlayer.Instance.CharIntermediate.BonusPhysicalDamage();

			return LocalPlayer.Instance.CalculateDamage(target, DamageType.Physical, qBaseDamage);
		}
	}
}
