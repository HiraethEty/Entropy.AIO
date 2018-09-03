namespace Entropy.AIO.Champions.Lucian.Logics
{
	using General;
	using SDK.Damage;
	using SDK.Extensions.Geometry;
	using SDK.Extensions.Objects;
	using SDK.Orbwalking.EventArgs;
	using Utility;

	internal class Jungleclear
	{
		public static void E(OnPostAttackEventArgs args)
		{
			if (args.Target.HP < LocalPlayer.Instance.GetAutoAttackDamage(args.Target as AIBaseClient) * 3)
			{
				return;
			}

			var jungleClearMenu = BaseMenu.Root["jungleClear"]["e"];
			if (jungleClearMenu.Enabled &&
			    LocalPlayer.Instance.MPPercent() > ManaManager.GetNeededMana(Champion.E.Slot, jungleClearMenu))
			{
				Champion.E.Cast(LocalPlayer.Instance.Position.Extend(Hud.CursorPositionUnclipped, LocalPlayer.Instance.BoundingRadius));
			}
		}

		public static void Q(OnPostAttackEventArgs args)
		{
			if (args.Target.HP < LocalPlayer.Instance.GetAutoAttackDamage(args.Target as AIBaseClient) * 3)
			{
				return;
			}

			var jungleClearMenu = BaseMenu.Root["jungleClear"]["q"];
			if (jungleClearMenu.Enabled &&
			    LocalPlayer.Instance.MPPercent() > ManaManager.GetNeededMana(Champion.Q.Slot, jungleClearMenu))
			{
				Champion.Q.CastOnUnit(args.Target);
			}
		}

		public static void W(OnPostAttackEventArgs args)
		{
			if (args.Target.HP < LocalPlayer.Instance.GetAutoAttackDamage(args.Target as AIBaseClient) * 3)
			{
				return;
			}

			var jungleClearMenu = BaseMenu.Root["jungleClear"]["w"];
			if (jungleClearMenu.Enabled &&
			    LocalPlayer.Instance.MPPercent() > ManaManager.GetNeededMana(Champion.W.Slot, jungleClearMenu))
			{
				Champion.W.Cast(args.Target.Position);
			}
		}
	}
}