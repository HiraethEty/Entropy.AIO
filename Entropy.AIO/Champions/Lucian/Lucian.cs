using Entropy.SDK.Damage;
using Entropy.SDK.Enumerations;
using Entropy.SDK.Extensions;
using Entropy.SDK.Extensions.Geometry;
using Entropy.SDK.Geometry;
using Entropy.SDK.Utils;

namespace Entropy.AIO.Champions.Lucian
{
	using System.Linq;
	using General;
	using SDK.Caching;
	using SDK.Events;
	using SDK.Extensions.Objects;
	using SDK.Orbwalking;
	using SDK.Orbwalking.EventArgs;
	using SDK.Spells;
	using SDK.UI;
	using SDK.UI.Components;

	internal sealed class Lucian : Champion
	{
		private Spell ExtendedQ { get; set; }

		/// <summary>
		///     Returns true if the player is using the ultimate.
		/// </summary>
		public bool IsCulling()
		{
			return LocalPlayer.Instance.HasBuff("LucianR");
		}

		/// <summary>
		///     The Q Rectangle.
		/// </summary>
		/// <param name="unit">The unit.</param>
		public Rectangle QRectangle(AIBaseClient unit)
		{
			return new Rectangle(LocalPlayer.Instance.Position, LocalPlayer.Instance.Position.Extend(unit.Position, ExtendedQ.Range), ExtendedQ.Width);
		}

		public Lucian()
		{
			Tick.OnTick += OnTick;
			new CustomTick(2000).OnTick += OnCustomTick;
			Gapcloser.OnNewGapcloser += OnNewGapcloser;
			Orbwalker.OnPostAttack += OnPostAttack;
			Renderer.OnRender += OnRender;
		}

		protected override void LoadMenu()
		{
			var comboMenu = new Menu("combo", "Combo")
			{
				new MenuBool("q", "Use Q"),
				new MenuBool("w", "Use W"),
				new MenuList("e", "Use E", new []{"Dynamic Range", "Always Short", "Always Long", "Don't use E"}).SetToolTip("Always directed to Cursor"),
				new MenuBool("normalR", "Use R if all spells on CD", false).SetToolTip("It will stop as soon as one spell returns ready to use, if the enemy is in its range."),
				new MenuBool("essenceR", "Use R to proc Essence Reaver").SetToolTip("If you have Essence Reaver, uses R and immediately stops it to make full use of its passive."),
				new MenuBool("bool", "Use Semi-Automatic R"),
				new MenuKeyBind("key", "Key:", WindowMessageWParam.U, KeybindType.Hold),
				new Menu("whitelists", "Whitelists")
				{
					new Menu("semiAutomaticR", "Semi-Automatic R Whitelist")
				}
			};

			foreach (var target in ObjectCache.EnemyHeroes)
			{
				comboMenu["whitelists"]["semiAutomaticR"].As<Menu>().Add(new MenuBool(target.CharName.ToLower(), "Use against: " + target.CharName));
			}

			var harassMenu = new Menu("harass", "Harass")
			{
				new MenuSliderBool("normalQ", "Use Normal Q / If Mana >= x%", true, 50),
				new MenuSliderBool("extendedQ", "Use Extended Q / If Mana >= x%", true, 50),
				new MenuSliderBool("w", "Use W / If Mana >= x%", false, 50),

				new Menu("whitelists", "Whitelists")
				{
					new Menu("normalQ", "Normal Q Whitelist"),
					new Menu("extendedQ", "Extended Q Whitelist"),
					new Menu("w", "W Whitelist")
				}
			};

			var killStealMenu = new Menu("killSteal", "KillSteal")
			{
				new MenuBool("normalQ", "Use Normal Q"),
				new MenuBool("extendedQ", "Use Extended Q"),
				new MenuBool("w", "Use W")
			};

			var harassWhitelistMenu = harassMenu["whitelists"];
			foreach (var enemy in ObjectCache.EnemyHeroes)
			{
				harassWhitelistMenu["normalQ"].As<Menu>().Add(new MenuBool(enemy.CharName, $"Normal Q on: {enemy.CharName}"));
				harassWhitelistMenu["extendedQ"].As<Menu>().Add(new MenuBool(enemy.CharName, $"Extended Q on: {enemy.CharName}"));
				harassWhitelistMenu["w"].As<Menu>().Add(new MenuBool(enemy.CharName, $"W on: {enemy.CharName}"));
			}

			var laneClearMenu = new Menu("laneClear", "Lane Clear")
			{
				new MenuSliderBool("q", "Use Q / If Mana >= x%", true, 50),
				new MenuSliderBool("w", "Use W / If Mana >= x%", false, 50),
				new MenuSliderBool("e", "Use E / If Mana >= x%", false, 50),

				new Menu("customization", "Customization")
				{
					new MenuSlider("q", "Use Q if hittable minions >= x", 3, 1, 5),
					new MenuSlider("w", "Use W if hittable minions >= x", 3, 1, 5),
					new MenuSlider("e", "Use E if minions around >= x", 3, 1, 5)
				}
			};

			var jungleClearMenu = new Menu("jungleClear", "Jungle Clear")
			{
				new MenuSliderBool("q", "Use Q / If Mana >= x%", true, 50),
				new MenuSliderBool("w", "Use W / If Mana >= x%", false, 50),
				new MenuSliderBool("e", "Use E / If Mana >= x%", false, 50)
			};

			var structureClearMenu = new Menu("structureClear", "Structure Clear")
			{
				new MenuSliderBool("w", "Use W / If Mana >= x%", false, 50),
				new MenuSliderBool("e", "Use E / If Mana >= x%", false, 50)
			};

			var antiGapcloserMenu = new Menu("antiGapcloser", "Anti-Gapcloser")
			{
				new Menu("e", "E")
				{
					new MenuBool("enabled", "Enable"),
					new MenuSeperator(string.Empty)
				}
			};

			foreach (var enemy in ObjectCache.EnemyHeroes.Where(enemy =>
				enemy.IsMelee &&
				Gapcloser.Spells.Any(spell => spell.Champion == enemy.GetChampion())))
			{
				var subAntiGapcloserMenu = new Menu(enemy.CharName.ToLower(), enemy.CharName);
				{
					foreach (var spell in Gapcloser.Spells.Where(spell => spell.Champion == enemy.GetChampion()))
					{
						subAntiGapcloserMenu.Add(new MenuBool($"{enemy.CharName.ToLower()}.{spell.SpellName.ToLower()}", $"Slot: {spell.Slot} ({spell.SpellName})"));
					}
				}

				antiGapcloserMenu["e"].As<Menu>().Add(subAntiGapcloserMenu);
			}

			var miscellaneousMenu = new Menu("miscellaneous", "Miscellaneous")
			{
				new Menu("e", "E Combo Customization")
				{
					new MenuBool("noeoutaarange", "Don't E out of AA range from enemies"),
					new MenuBool("onlyeifmouseoutaarange", "Only E if mouse out of self AA Range", false),
					new MenuSliderBool("erangecheck", "Don't E if X enemies in range from dash position", true, 3, 2, 6),
					new MenuBool("noeturret", "Don't use E under Enemy Turret")
				}
			};

			var drawingsMenu = new Menu("drawing", "Drawing")
			{
				new MenuBool("q", "Q Range", false),
				new MenuBool("qextended", "Extended Q Range"),
				new MenuBool("w", "W Range", false),
				new MenuBool("e", "E Range", false),
				new MenuBool("r", "R Range", false)
			};

			var menuList = new[]
			{
				comboMenu,
				harassMenu,
				killStealMenu,
				laneClearMenu,
				jungleClearMenu,
				structureClearMenu,
				antiGapcloserMenu,
				miscellaneousMenu,
				drawingsMenu
			};

			foreach (var menu in menuList)
			{
				BaseMenu.Root.Add(menu);
			}
		}

		protected override void LoadSpells()
		{
			Q = new Spell(SpellSlot.Q, 550f);
			ExtendedQ = new Spell(SpellSlot.Q, Q.Range + 400f - LocalPlayer.Instance.BoundingRadius);
			W = new Spell(SpellSlot.W, 900f);
			E = new Spell(SpellSlot.E, LocalPlayer.Instance.GetAutoAttackRange() + 425f);
			R = new Spell(SpellSlot.R, 1150f);
		}

		public override void OnTick(EntropyEventArgs args)
		{
			if (LocalPlayer.Instance.IsDead || Orbwalker.IsWindingUp)
			{
				return;
			}

			Automatic();

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

			//KillSteal();
		}

		private void Automatic()
		{
			if (BaseMenu.Root["combo"]["bool"].Enabled &&
			    BaseMenu.Root["combo"]["key"].Enabled)
			{
				DelayAction.Queue(() =>
					{
						Orbwalker.Move(Hud.CursorPositionUnclipped);
					}, 100 + EnetClient.Ping);
			}

			if (R.Ready &&
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
						if (W.Ready &&
						    bestTarget.DistanceToPlayer() <= W.Range)
						{
							W.Cast(bestTarget.Position);
						}

						R.Cast(bestTarget.Position);
					}
				}
				else if (IsCulling() &&
				    !BaseMenu.Root["combo"]["key"].Enabled)
				{
					R.Cast();
				}
			}
		}

		public override void OnRender(EntropyEventArgs args)
		{

		}

		public override void OnPostAttack(OnPostAttackEventArgs args)
		{

		}

		public override void OnNewGapcloser(Gapcloser.GapcloserArgs args)
		{
			
		}
	}
}
