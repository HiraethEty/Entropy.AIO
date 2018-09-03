namespace Entropy.AIO.Champions.Lucian.Logics
{
	using System.Linq;
	using General;
	using Misc;
	using SDK.Caching;
	using SDK.Damage;
	using SDK.Extensions;
	using SDK.Extensions.Geometry;
	using SDK.Extensions.Objects;
	using SDK.Orbwalking;
	using SDK.UI.Components;

	internal class Automatic
	{
		public static void SemiAutomaticR(GameWndProcEventArgs args)
		{
			if (!BaseMenu.Root["combo"]["bool"].Enabled ||
			    args.WParam != BaseMenu.Root["combo"]["key"].As<MenuKeyBind>().Key)
			{
				return;
			}

			switch (args.Message)
			{
				case WindowMessage.KEYDOWN when !Definitions.IsCulling():
					if (LocalPlayer.Instance.IsDead || Orbwalker.IsWindingUp)
					{
						return;
					}

					if (Champion.R.Ready)
					{
						ExecuteSemiAutomaticR();
					}
					break;

				case WindowMessage.KEYUP when Definitions.IsCulling():
					Champion.R.Cast();
					break;
			}
		}

		private static void ExecuteSemiAutomaticR()
		{
			var bestTarget = ObjectCache.EnemyHeroes
				.Where(t =>
					BaseMenu.Root["combo"]["whitelists"]["semiAutomaticR"][t.CharName.ToLower()].Enabled &&
					t.IsValidTarget() &&
					!Invulnerable.IsInvulnerable(t, DamageType.Physical, false))
				.MinBy(o => o.GetRealHealth(DamageType.Physical));

			if (bestTarget == null)
			{
				return;
			}

			if (Champion.W.Ready &&
			    bestTarget.DistanceToPlayer() <= Champion.W.Range)
			{
				Champion.W.Cast(bestTarget.Position);
			}

			Champion.R.Cast(bestTarget.Position);
		}
	}
}
