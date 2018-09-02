namespace Entropy.AIO.Champions.Lucian
{
	using Drawings;
	using Misc;
	using SDK.Orbwalking;
	using SDK.Orbwalking.EventArgs;
	using Spells;
	using Champion = Champion;

	internal sealed class Lucian : Champion
	{
		private ExtendedQ ExtendedQ { get; set; }

		public Lucian()
		{
			Menu.Menu.LoadMenu();
			Spells = new []{this.Q.Spell, this.ExtendedQ.Spell, this.W.Spell, this.E.Spell, this.R.Spell};
			DamageValues = new Damage(Spells);
			this.Drawing = new Drawing(Spells);
			Orbwalker.OnPostAttack += this.OnPostAttack;
		}

		protected override void LoadSpells()
		{
			this.Q = new Q();
			this.ExtendedQ = new ExtendedQ();
			this.W = new W();
			this.E = new E();
			this.R = new R();
		}

		public override void OnTick(EntropyEventArgs args)
		{
			if (LocalPlayer.Instance.IsDead || Orbwalker.IsWindingUp)
			{
				return;
			}

			if (Definitions.IsCulling())
			{
				return;
			}
		}

		public override void OnPostAttack(OnPostAttackEventArgs args)
		{
			if (this.E.Spell.Ready)
			{
				this.E.OnPostAttack(args);
				return;
			}
			if (this.Q.Spell.Ready)
			{
				this.Q.OnPostAttack(args);
				return;
			}
			if (this.W.Spell.Ready)
			{
				this.W.OnPostAttack(args);
			}
		}
	}
}