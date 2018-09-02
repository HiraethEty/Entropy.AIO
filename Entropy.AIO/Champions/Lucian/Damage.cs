namespace Entropy.AIO.Champions.Lucian
{
	using SDK.Damage;
	using SDK.Extensions.Objects;
	using SDK.Spells;
	using General;

	internal class Damage : BaseDamage
	{
		private readonly Spell[] Spells;
		public Damage(Spell[] spells)
		{
			this.Spells = spells;
			SetDamageCalculations();
		}

		private void SetDamageCalculations()
		{
			this.Spells[0].SetCustomDamageCalculateFunction(this.QDamage);
			this.Spells[2].SetCustomDamageCalculateFunction(this.WDamage);
		}

		public override float QDamage(AIBaseClient target)
		{
			var qLevel = this.Spells[0].Level;

			var qBaseDamage = new[] { 0f, 85f, 120f, 155f, 190f, 225f }[qLevel]
			                  + new[] { 0f, 0.6f, 0.7f, 0.8f, 0.8f, 1f }[qLevel]
			                  * LocalPlayer.Instance.CharIntermediate.BonusPhysicalDamage();

			return LocalPlayer.Instance.CalculateDamage(target, DamageType.Physical, qBaseDamage);
		}

		public override float WDamage(AIBaseClient target)
		{
			var wLevel = this.Spells[1].Level;

			var wBaseDamage = new[] { 85f, 125f, 165f, 205f, 245f }[wLevel - 1]
			                  + 0.9f * LocalPlayer.Instance.CharIntermediate.TotalAbilityDamage();

			return LocalPlayer.Instance.CalculateDamage(target, DamageType.Magical, wBaseDamage);
		}
	}
}
