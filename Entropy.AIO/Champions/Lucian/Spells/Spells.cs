namespace Entropy.AIO.Champions.Lucian
{
	using SDK.Extensions.Objects;
	using SDK.Spells;

	internal class Spells
	{
		public static Spell ExtendedQ { get; private set; }

		public Spells()
		{
			Initialize();
		}

		private static void Initialize()
		{
			Champion.Q = new Spell(SpellSlot.Q, 550f);
			ExtendedQ = new Spell(SpellSlot.Q, 950f - LocalPlayer.Instance.BoundingRadius);
			Champion.W = new Spell(SpellSlot.W, 900f);
			Champion.E = new Spell(SpellSlot.E, LocalPlayer.Instance.GetAutoAttackRange() + 425f);
			Champion.R = new Spell(SpellSlot.R, 1150f);

			ExtendedQ.SetSkillshot(0.25f, 65f, float.MaxValue, collision: false);
			Champion.W.SetSkillshot(0.25f, 80f, 1600f, collision: false);
			Champion.R.SetSkillshot(0.25f, 110f, 2500f, collision: false);

			new Damage(new[] {Champion.Q, Champion.W});
		}
	}
}