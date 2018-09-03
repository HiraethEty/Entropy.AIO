namespace Entropy.AIO.Champions.Lucian.Logics
{
	using System.Linq;
	using General;
	using SDK.Caching;
	using SDK.Extensions.Geometry;
	using SDK.Extensions.Objects;
	using SDK.Orbwalking.EventArgs;
	using SharpDX;

	internal class Weaving
	{
		public static void E(OnPostAttackEventArgs args)
		{
			if (!CanCastE(args.Target))
			{
				return;
			}

			switch (BaseMenu.Root["combo"]["e"].Value)
			{
				case 0:
					CastDynamicE();
					return;

				case 1:
					Champion.E.Cast(LocalPlayer.Instance.Position.Extend(Hud.CursorPositionUnclipped, 425f));
					return;

				case 2:
					Champion.E.Cast(LocalPlayer.Instance.Position.Extend(Hud.CursorPositionUnclipped,
					                                                     LocalPlayer.Instance.BoundingRadius));
					return;
			}
		}

		public static void Q(OnPostAttackEventArgs args)
		{
			if (BaseMenu.Root["combo"]["q"].Enabled)
			{
				Champion.Q.CastOnUnit(args.Target);
			}
		}

		public static void W(OnPostAttackEventArgs args)
		{
			if (BaseMenu.Root["combo"]["w"].Enabled)
			{
				Champion.W.Cast(args.Target as AIBaseClient);
			}
		}

		private static bool CanCastE(AttackableUnit target)
		{
			if (LocalPlayer.Instance.Distance(Hud.CursorPositionUnclipped) <= LocalPlayer.Instance.GetAutoAttackRange() &&
			    BaseMenu.Root["miscellaneous"]["e"]["onlyeifmouseoutaarange"].Enabled)
			{
				return false;
			}

			var posAfterE = LocalPlayer.Instance.Position.Extend(Hud.CursorPositionUnclipped, 300f);
			if (ObjectCache.EnemyHeroes.Count() > 1)
			{
				if (BaseMenu.Root["miscellaneous"]["e"]["erangecheck"].Enabled &&
				    posAfterE.EnemyHeroesCount(LocalPlayer.Instance.GetAutoAttackRange() +
				                               LocalPlayer.Instance.BoundingRadius) >=
				    BaseMenu.Root["miscellaneous"]["e"]["erangecheck"].Value)
				{
					return false;
				}
			}

			if (posAfterE.Distance(target) >
			    LocalPlayer.Instance.GetAutoAttackRange(target) &&
			    BaseMenu.Root["miscellaneous"]["e"]["noeoutaarange"].Enabled)
			{
				return false;
			}

			return !posAfterE.IsUnderEnemyTurret() || !BaseMenu.Root["miscellaneous"]["e"]["noeturret"].Enabled;
		}

		private static void CastDynamicE()
		{
			Vector3 point;
			if (LocalPlayer.Instance.Position.IsUnderEnemyTurret() ||
			    LocalPlayer.Instance.Distance(Hud.CursorPositionUnclipped) <
			    LocalPlayer.Instance.GetAutoAttackRange())
			{
				point = LocalPlayer.Instance.Position.Extend(Hud.CursorPositionUnclipped,
				                                             LocalPlayer.Instance.BoundingRadius);
			}
			else
			{
				point = LocalPlayer.Instance.Position.Extend(Hud.CursorPositionUnclipped, 475f);
			}

			Champion.E.Cast(point);
		}
	}
}