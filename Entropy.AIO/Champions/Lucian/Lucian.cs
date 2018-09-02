namespace Entropy.AIO.Champions.Lucian
{
	using Drawings;
	using SDK.Events;
	using SDK.Extensions.Geometry;
	using SDK.Extensions.Objects;
	using SDK.Geometry;
	using SDK.Orbwalking;
	using SDK.Orbwalking.EventArgs;
	using SDK.Spells;
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

		/// <summary>
		///     Returns true if the player is using the ultimate.
		/// </summary>
		public static bool IsCulling() => LocalPlayer.Instance.HasBuff("LucianR");

		/// <summary>
		///     The Q Rectangle.
		/// </summary>
		/// <param name="unit">The unit.</param>
		public static Rectangle QRectangle(AIBaseClient unit) => new Rectangle(LocalPlayer.Instance.Position,
		                                                                LocalPlayer.Instance.Position.Extend(unit.Position, Spells[1].Range),
		                                                                Spells[1].Width);

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

			if (IsCulling())
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

		public override void OnNewGapcloser(Gapcloser.GapcloserArgs args) { }
	}
}