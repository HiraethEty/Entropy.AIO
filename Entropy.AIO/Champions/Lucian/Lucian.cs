namespace Entropy.AIO.Champions.Lucian
{
	using Drawings;
	using SDK.Events;
	using SDK.Extensions.Geometry;
	using SDK.Extensions.Objects;
	using SDK.Geometry;
	using SDK.Orbwalking;
	using SDK.Spells;
	using Spells;
	using Champion = Champion;

	internal sealed class Lucian : Champion
	{
		private Spell ExtendedQ { get; set; }

		public Lucian()
		{
			Menu.Menu.LoadMenu();
			Spells = new []{this.Q,	this.ExtendedQ, this.W,	this.E,	this.R};
			DamageValues = new Damage(Spells);
			this.Drawing = new Drawing(Spells);
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
			this.Q = new Q().Spell;
			this.ExtendedQ = new ExtendedQ().Spell;
			this.W = new W().Spell;
			this.E = new E().Spell;
			this.R = new R().Spell;
		}

		public override void OnNewGapcloser(Gapcloser.GapcloserArgs args) { }
	}
}