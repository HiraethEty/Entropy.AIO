namespace Entropy.AIO.Champions.Lucian
{
	using System.Linq;
	using General;
	using SDK.Caching;
	using SDK.Damage;
	using SDK.Enumerations;
	using SDK.Events;
	using SDK.Extensions;
	using SDK.Extensions.Geometry;
	using SDK.Extensions.Objects;
	using SDK.Geometry;
	using SDK.Orbwalking;
	using SDK.Orbwalking.EventArgs;
	using SDK.Spells;
	using SDK.Utils;
	using Spells;
	using Champion = Champion;

	internal sealed class Lucian : Champion
	{
		private Spell ExtendedQ { get; set; }

		public Lucian()
		{
			Spells = new []{this.Q,	this.ExtendedQ, this.W,	this.E,	this.R};
			DamageValues = new Damage(Spells);
		}

		/// <summary>
		///     Returns true if the player is using the ultimate.
		/// </summary>
		private static bool IsCulling() => LocalPlayer.Instance.HasBuff("LucianR");

		/// <summary>
		///     The Q Rectangle.
		/// </summary>
		/// <param name="unit">The unit.</param>
		public Rectangle QRectangle(AIBaseClient unit) => new Rectangle(LocalPlayer.Instance.Position,
		                                                                LocalPlayer.Instance.Position.Extend(unit.Position, this.ExtendedQ.Range),
		                                                                this.ExtendedQ.Width);

		protected override void LoadSpells()
		{
			this.Q = new Q().Spell;
			this.ExtendedQ = new ExtendedQ().Spell;
			this.W = new W().Spell;
			this.E = new E().Spell;
			this.R = new R().Spell;
		}

		public override void OnTick(EntropyEventArgs args)
		{
			if (LocalPlayer.Instance.IsDead || Orbwalker.IsWindingUp)
			{
				return;
			}

			this.Automatic();

			if (IsCulling())
			{
				return;
			}

			switch (Orbwalker.Mode)
			{
				case OrbwalkingMode.Combo:
					//Combo();
					break;

				case OrbwalkingMode.LaneClear:
					//LaneClear();
					break;

				case OrbwalkingMode.Harass:
					//Harass();
					break;
			}
		}

		public override void OnCustomTick(EntropyEventArgs args)
		{
			if (LocalPlayer.Instance.IsDead)
			{
				return;
			}

			this.Killsteal();
		}

		private void Automatic()
		{
			if (BaseMenu.Root["combo"]["bool"].Enabled &&
			    BaseMenu.Root["combo"]["key"].Enabled)
			{
				DelayAction.Queue(() =>
					                  {
						                  Orbwalker.Move(Hud.CursorPositionUnclipped);
					                  },
				                  100 + EnetClient.Ping);
			}

			if (this.R.Ready &&
			    BaseMenu.Root["combo"]["bool"].Enabled)
			{
				if (!IsCulling() &&
				    BaseMenu.Root["combo"]["key"].Enabled)
				{
					var bestTarget = ObjectCache.EnemyHeroes
					                            .Where(t =>
						                                   BaseMenu.Root["combo"]["whitelists"]["semiAutomaticR"][t.CharName.ToLower()].Enabled &&
						                                   t.IsValidTarget() &&
						                                   !Invulnerable.IsInvulnerable(t, DamageType.Physical, false))
					                            .MinBy(o => o.GetRealHealth(DamageType.Physical));

					if (bestTarget != null)
					{
						if (this.W.Ready &&
						    bestTarget.DistanceToPlayer() <= this.W.Range)
						{
							this.W.Cast(bestTarget.Position);
						}

						this.R.Cast(bestTarget.Position);
					}
				}
				else if (IsCulling() &&
				         !BaseMenu.Root["combo"]["key"].Enabled)
				{
					this.R.Cast();
				}
			}
		}

		private void Killsteal()
		{
			if (this.Q.Ready &&
			    (BaseMenu.Root["killsteal"]["normalQ"].Enabled || BaseMenu.Root["killsteal"]["extendedQ"].Enabled))
			{
				foreach (var target in ObjectCache.EnemyHeroes
				                                  .Where(t =>
					                                         t.IsValidTarget() &&
					                                         !Invulnerable.IsInvulnerable(t, DamageType.Physical, damage: this.Q.GetDamage(t)) &&
					                                         this.Q.GetDamage(t) >= t.GetRealHealth(DamageType.Physical)))
				{
					if (BaseMenu.Root["killsteal"]["normalQ"].Enabled &&
					    target.DistanceToPlayer() < this.Q.Range)
					{
						this.Q.CastOnUnit(target);
						break;
					}

					if (BaseMenu.Root["killsteal"]["extendedQ"].Enabled)
					{
						foreach (var minion in ObjectCache.EnemyLaneMinions
						                                  .Where(m => m.IsValidTarget(this.Q.Range) && this.QRectangle(m).IsInsidePolygon(this.Q.GetPrediction(target).CastPosition)))
						{
							this.Q.CastOnUnit(minion);
							break;
						}
					}
				}
			}

			if (this.W.Ready &&
			    BaseMenu.Root["killsteal"]["w"].Enabled)
			{
				foreach (var target in ObjectCache.EnemyHeroes
				                                  .Where(t =>
					                                         t.IsValidTarget() &&
					                                         !Invulnerable.IsInvulnerable(t, DamageType.Magical, damage: this.W.GetDamage(t)) &&
					                                         this.W.GetDamage(t) >= t.GetRealHealth(DamageType.Magical)))
				{
					this.W.Cast(target);
					break;
				}
			}
		}

		public override void OnRender(EntropyEventArgs args) { }

		public override void OnPostAttack(OnPostAttackEventArgs args)
		{
			switch (Orbwalker.Mode)
			{
				case OrbwalkingMode.Combo:
					//Weaving(args);
					break;

				case OrbwalkingMode.LaneClear:
					//Laneclear(args);
					//Jungleclear(args);
					//Buildingclear(args);
					break;
			}
		}

		public override void OnNewGapcloser(Gapcloser.GapcloserArgs args) { }
	}
}