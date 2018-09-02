using System.Linq;
using Entropy.AIO.Utilities;
using Entropy.AIO.Utility;
using Entropy.SDK.Caching;
using Entropy.SDK.Damage;
using Entropy.SDK.Extensions.Geometry;
using Entropy.SDK.Extensions.Objects;
using Entropy.SDK.Orbwalking;
using Entropy.SDK.Orbwalking.EventArgs;
using SharpDX;

namespace Entropy.AIO.Champions.Lucian.Spells
{
	using General;
	using SDK.Events;
	using SDK.Spells;

	internal sealed class E : BaseSpell
	{
		protected override void SetSpellData()
		{
			this.Spell = new Spell(SpellSlot.E, LocalPlayer.Instance.GetAutoAttackRange() + 425f);
		}

		protected override void SubscribeToEvents()
		{
			Tick.OnTick += this.OnTick;
			Orbwalker.OnPostAttack += OnPostAttack;
		}

		public override void OnTick(EntropyEventArgs args)
		{
			if (!this.Spell.Ready ||
			    !BaseMenu.Root["combo"]["eengage"].Enabled)
			{
				return;
			}

			// Engager E
			this.EngagerE();
		}

		public override void OnPostAttack(OnPostAttackEventArgs args)
		{
			if (!this.Spell.Ready)
			{
				return;
			}

			var target = args.Target;
			switch (target.Type.TypeID)
			{
				// Combo E
				case GameObjectTypeID.AIHeroClient:
					if (!CanCastE(target))
					{
						return;
					}

					switch (BaseMenu.Root["combo"]["e"].Value)
					{
						case 0:
							this.DynamicE();
							break;

						case 1:
							this.Spell.Cast(LocalPlayer.Instance.Position.Extend(Hud.CursorPositionUnclipped, 425f));
							break;

						case 2:
							this.Spell.Cast(LocalPlayer.Instance.Position.Extend(Hud.CursorPositionUnclipped,
								LocalPlayer.Instance.BoundingRadius));
							break;
					}
					break;

				// JungleClear and LaneClear E
				case GameObjectTypeID.AIMinionClient:
					if (ObjectCache.JungleMinions.Contains(target))
					{
						this.JungleClear();
					}
					else
					{
						this.LaneClear();
					}
					break;
				
				// StructureClear E
				case GameObjectTypeID.HQ:
				case GameObjectTypeID.AITurretClient:
				case GameObjectTypeID.BarracksDampener:
					this.StructureClear();
					break;
			}
		}

		private void EngagerE()
		{
			var bestTarget = Extensions.GetBestEnemyHeroTargetInRange(this.Spell.Range);
			if (bestTarget == null ||
			    Invulnerable.IsInvulnerable(bestTarget, DamageType.Physical) ||
			    bestTarget.IsValidTarget(LocalPlayer.Instance.GetAutoAttackRange(bestTarget)))
			{
				return;
			}

			var posAfterE = LocalPlayer.Instance.Position.Extend(Hud.CursorPositionUnclipped, 425f);
			if (posAfterE.EnemyHeroesCount(1000f) < 3 &&
			    LocalPlayer.Instance.Distance(Hud.CursorPositionUnclipped) > LocalPlayer.Instance.GetAutoAttackRange() &&
			    bestTarget.Distance(posAfterE) < LocalPlayer.Instance.GetAutoAttackRange(bestTarget))
			{
				this.Spell.Cast(posAfterE);
			}
		}

		private void StructureClear()
		{
			var eStructureClearMenu = BaseMenu.Root["structureClear"]["e"];
			if (eStructureClearMenu.Enabled &&
			    LocalPlayer.Instance.MPPercent() > ManaManager.GetNeededMana(this.Spell.Slot, eStructureClearMenu))
			{
				this.Spell.Cast(LocalPlayer.Instance.Position.Extend(Hud.CursorPositionUnclipped, 25f));
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
				                               LocalPlayer.Instance.BoundingRadius) >= BaseMenu.Root["miscellaneous"]["e"]["erangecheck"].Value)
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

			if (posAfterE.IsUnderEnemyTurret() &&
			    BaseMenu.Root["miscellaneous"]["e"]["noeturret"].Enabled)
			{
				return false;
			}

			return true;
		}

		private void DynamicE()
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

			this.Spell.Cast(point);
		}

		private void LaneClear()
		{
			var laneClearMenu = BaseMenu.Root["laneClear"]["e"];
			if (laneClearMenu.Enabled &&
			    LocalPlayer.Instance.MPPercent() > ManaManager.GetNeededMana(this.Spell.Slot, laneClearMenu))
			{
				this.Spell.Cast(LocalPlayer.Instance.Position.Extend(Hud.CursorPositionUnclipped, LocalPlayer.Instance.BoundingRadius));
			}
		}

		private void JungleClear()
		{
			var target = Orbwalker.GetOrbwalkingTarget() as AIBaseClient;
			if (target == null ||
			    target.HP < LocalPlayer.Instance.GetAutoAttackDamage(target) * 2)
			{
				return;
			}

			var jungleClearMenu = BaseMenu.Root["jungleClear"]["e"];
			if (jungleClearMenu.Enabled &&
				LocalPlayer.Instance.MPPercent() > ManaManager.GetNeededMana(this.Spell.Slot, jungleClearMenu))
			{
				this.Spell.Cast(LocalPlayer.Instance.Position.Extend(Hud.CursorPositionUnclipped, LocalPlayer.Instance.BoundingRadius));
			}
		}
	}
}
