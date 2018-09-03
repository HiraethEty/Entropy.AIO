using Entropy.SDK.Enumerations;
using Entropy.SDK.Extensions.Objects;

namespace Entropy.AIO.Champions.Lucian
{
	using Misc;
	using SDK.Orbwalking;
	using SDK.Events;
	using SDK.Utils;
	using SDK.Orbwalking.EventArgs;
	using Logics;

	internal sealed class Lucian : Champion
	{
		public Lucian()
		{
			Spells.Initialize();
			Menu.Initialize();
			Methods.Initialize();
			this.DamageValues = new Damage(new[] {Q, W});
			new Drawings.Drawings(new[] {Q, W, E, R});
		}

		public static void OnWndProc(GameWndProcEventArgs args)
		{
			Automatic.SemiAutomaticR(args);
		}

		public static void OnNewGapcloser(Gapcloser.GapcloserArgs args)
		{
			AntiGapcloser.E(args);
		}

		public static void OnCustomTick(EntropyEventArgs args)
		{
			if (LocalPlayer.Instance.IsDead || Orbwalker.IsWindingUp)
			{
				return;
			}

			if (Q.Ready)
			{
				Killsteal.Q(args);
				Killsteal.ExtendedQ(args);
			}

			if (W.Ready)
			{
				Killsteal.W(args);
			}
		}

		public static void OnPostAttack(OnPostAttackEventArgs args)
		{
			switch (Orbwalker.Mode)
			{
				case OrbwalkingMode.Combo:
					if (E.Ready)
					{
						Weaving.E(args);
						return;
					}

					if (Q.Ready)
					{
						Weaving.Q(args);
						return;
					}

					if (W.Ready)
					{
						Weaving.W(args);
					}
					break;

				case OrbwalkingMode.LaneClear:
					if (E.Ready)
					{
						Laneclear.E(args);
						Jungleclear.E(args);

						if (args.Target.IsStructure())
						{
							Structureclear.E(args);
							return;
						}
					}

					if (Q.Ready)
					{
						Jungleclear.Q(args);
						return;
					}

					if (W.Ready)
					{
						Jungleclear.W(args);

						if (args.Target.IsStructure())
						{
							Structureclear.W(args);
						}
					}
					break;
			}
		}

		public static void OnTick(EntropyEventArgs args)
		{
			if (LocalPlayer.Instance.IsDead)
			{
				return;
			}

			if (Definitions.IsCulling())
			{
				DelayAction.Queue(() => { Orbwalker.Move(Hud.CursorPositionUnclipped); },
					100 + EnetClient.Ping);
			}

			if (Orbwalker.IsWindingUp)
			{
				return;
			}

			switch (Orbwalker.Mode)
			{
				case OrbwalkingMode.Combo:
					if (E.Ready)
					{
						Combo.E(args);
					}

					if (R.Ready)
					{
						Combo.R(args);
					}
					break;

				case OrbwalkingMode.LaneClear:
					if (Q.Ready)
					{
						Laneclear.Q(args);
					}

					if (W.Ready)
					{
						Laneclear.W(args);
					}
					break;

				case OrbwalkingMode.Harass:
					if (Q.Ready)
					{
						Harass.Q(args);
						Harass.ExtendedQ(args);
					}

					if (W.Ready)
					{
						Harass.W(args);
					}
					break;
			}
		}
	}
}