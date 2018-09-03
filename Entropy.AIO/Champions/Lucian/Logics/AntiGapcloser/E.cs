namespace Entropy.AIO.Champions.Lucian.Logics.AntiGapcloser
{
	using General;
	using SDK.Events;
	using SDK.Extensions.Geometry;
	using SDK.Extensions.Objects;

	internal class AntiGapcloser
	{
		public static void Execute(Gapcloser.GapcloserArgs args)
		{
			if (LocalPlayer.Instance.IsDead || !Champion.E.Ready)
			{
				return;
			}

			var antiGapcloserMenu = BaseMenu.Root["antiGapcloser"];
			if (!antiGapcloserMenu["enabled"].Enabled)
			{
				return;
			}

			var sender = args.Sender;
			if (sender == null || !sender.IsEnemy() || !sender.IsMelee)
			{
				return;
			}

			var spellOption = antiGapcloserMenu[$"{sender.CharName.ToLower()}.{args.SpellName.ToLower()}"];
			if (spellOption == null || !spellOption.Enabled)
			{
				return;
			}

			OnGapcloser(args);
		}

		private static void OnGapcloser(Gapcloser.GapcloserArgs args)
		{
			switch (args.Type)
			{
				case SpellType.Targeted:
					if (args.Sender.IsMelee &&
					    args.Target.IsMe())
					{
						var targetPos = LocalPlayer.Instance.Position.Extend(args.StartPosition, -Champion.E.Range);
						if (targetPos.IsUnderEnemyTurret())
						{
							targetPos = Hud.CursorPositionUnclipped;
						}

						Champion.E.Cast(targetPos);
					}

					break;
				case SpellType.Dash:
					var targetPos2 = LocalPlayer.Instance.Position.Extend(args.EndPosition, -Champion.E.Range);
					if (targetPos2.IsUnderEnemyTurret())
					{
						targetPos2 = Hud.CursorPositionUnclipped;
					}

					if (args.EndPosition.DistanceToPlayer() <= LocalPlayer.Instance.GetAutoAttackRange())
					{
						Champion.E.Cast(targetPos2);
					}
					break;
			}
		}
	}
}
