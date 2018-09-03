namespace Entropy.AIO.Champions.Lucian
{
	using System.Linq;
	using General;
	using SDK.Caching;
	using SDK.Events;
	using SDK.Extensions.Objects;
	using SDK.UI.Components;

	internal class Menu
	{
		public Menu()
		{
			Initialize();
		}

		public static void Initialize()
		{
			var comboMenu = new SDK.UI.Menu("combo", "Combo")
			{
				new MenuBool("q", "Use Q"),
				new MenuBool("w", "Use W"),
				new MenuList("e", "Use E", new[] {"Dynamic Range", "Always Short", "Always Long", "Don't use E"}).SetToolTip("Always directed to Cursor"),
				new MenuBool("eengage", "^ Also use e to Engage", false),
				new MenuBool("normalR", "Use R if all spells on CD", false).SetToolTip("It will stop as soon as one spell returns ready to use, if the enemy is in its range."),
				new MenuBool("essenceR", "Use R to proc Essence Reaver").SetToolTip("If you have Essence Reaver, uses R and immediately stops it to make full use of its passive."),
				new MenuBool("bool", "Use Semi-Automatic R"),
				new MenuKeyBind("key", "Key:", WindowMessageWParam.U, KeybindType.Hold),
				new SDK.UI.Menu("whitelists", "Whitelists")
				{
					new SDK.UI.Menu("semiAutomaticR", "Semi-Automatic R Whitelist")
				}
			};

			foreach (var target in ObjectCache.EnemyHeroes)
			{
				comboMenu["whitelists"]["semiAutomaticR"].As<SDK.UI.Menu>().Add(new MenuBool(target.CharName.ToLower(), "Use against: " + target.CharName));
			}

			var harassMenu = new SDK.UI.Menu("harass", "Harass")
			{
				new MenuSliderBool("normalQ", "Use Normal Q / If Mana >= x%", true, 50),
				new MenuSliderBool("extendedQ", "Use Extended Q / If Mana >= x%", true, 50),
				new MenuSliderBool("w", "Use W / If Mana >= x%", false, 50),

				new SDK.UI.Menu("whitelists", "Whitelists")
				{
					new SDK.UI.Menu("normalQ", "Normal Q Whitelist"),
					new SDK.UI.Menu("extendedQ", "Extended Q Whitelist"),
					new SDK.UI.Menu("w", "W Whitelist")
				}
			};

			var harassWhitelistMenu = harassMenu["whitelists"];
			foreach (var enemy in ObjectCache.EnemyHeroes)
			{
				harassWhitelistMenu["normalQ"].As<SDK.UI.Menu>().Add(new MenuBool(enemy.CharName.ToLower(), $"Normal Q on: {enemy.CharName}"));
				harassWhitelistMenu["extendedQ"].As<SDK.UI.Menu>().Add(new MenuBool(enemy.CharName.ToLower(), $"Extended Q on: {enemy.CharName}"));
				harassWhitelistMenu["w"].As<SDK.UI.Menu>().Add(new MenuBool(enemy.CharName.ToLower(), $"W on: {enemy.CharName}"));
			}

			var killStealMenu = new SDK.UI.Menu("killSteal", "KillSteal")
			{
				new MenuBool("normalQ", "Use Normal Q"),
				new MenuBool("extendedQ", "Use Extended Q"),
				new MenuBool("w", "Use W")
			};

			var laneClearMenu = new SDK.UI.Menu("laneClear", "Lane Clear")
			{
				new MenuSliderBool("q", "Use Q / If Mana >= x%", true, 50),
				new MenuSliderBool("w", "Use W / If Mana >= x%", false, 50),

				new SDK.UI.Menu("customization", "Customization")
				{
					new MenuSlider("q", "Use Q if hittable minions >= x", 3, 1, 5),
					new MenuSlider("w", "Use W if hittable minions >= x", 3, 1, 5)
				}
			};

			var jungleClearMenu = new SDK.UI.Menu("jungleClear", "Jungle Clear")
			{
				new MenuSliderBool("q", "Use Q / If Mana >= x%", true, 50),
				new MenuSliderBool("w", "Use W / If Mana >= x%", false, 50),
				new MenuSliderBool("e", "Use E / If Mana >= x%", false, 50)
			};

			var structureClearMenu = new SDK.UI.Menu("structureClear", "Structure Clear")
			{
				new MenuSliderBool("w", "Use W / If Mana >= x%", false, 50),
				new MenuSliderBool("e", "Use E / If Mana >= x%", false, 50)
			};

			var antiGapcloserMenu = new SDK.UI.Menu("antiGapcloser", "Anti-Gapcloser")
			{
				new SDK.UI.Menu("e", "E")
				{
					new MenuBool("enabled", "Enable"),
					new MenuSeperator(string.Empty)
				}
			};

			foreach (var enemy in ObjectCache.EnemyHeroes.Where(enemy =>
				                                                    enemy.IsMelee &&
				                                                    Gapcloser.Spells.Any(spell => spell.Champion == enemy.GetChampion())))
			{
				var subAntiGapcloserMenu = new SDK.UI.Menu(enemy.CharName.ToLower(), enemy.CharName);
				{
					foreach (var spell in Gapcloser.Spells.Where(spell => spell.Champion == enemy.GetChampion()))
					{
						subAntiGapcloserMenu.Add(new MenuBool($"{enemy.CharName.ToLower()}.{spell.SpellName.ToLower()}", $"Slot: {spell.Slot} ({spell.SpellName})"));
					}
				}

				antiGapcloserMenu["e"].As<SDK.UI.Menu>().Add(subAntiGapcloserMenu);
			}

			var miscellaneousMenu = new SDK.UI.Menu("miscellaneous", "Miscellaneous")
			{
				new SDK.UI.Menu("e", "E Combo Customization")
				{
					new MenuBool("noeoutaarange", "Don't E out of AA range from enemies"),
					new MenuBool("onlyeifmouseoutaarange", "Only E if mouse out of self AA Range", false),
					new MenuSliderBool("erangecheck", "Don't E if X enemies in range from dash position", true, 3, 2, 6),
					new MenuBool("noeturret", "Don't use E under Enemy Turret")
				}
			};

			var drawingsMenu = BaseMenu.Root["drawing"].As<SDK.UI.Menu>();
			{
				drawingsMenu.Add(new MenuBool("q", "Q Range", false));
				drawingsMenu.Add(new MenuBool("qextended", "Extended Q Range"));
				drawingsMenu.Add(new MenuBool("w", "W Range", false));
				drawingsMenu.Add(new MenuBool("e", "E Range", false));
				drawingsMenu.Add(new MenuBool("r", "R Range", false));
			}

			var menuList = new[]
			{
				comboMenu,
				harassMenu,
				killStealMenu,
				laneClearMenu,
				jungleClearMenu,
				structureClearMenu,
				antiGapcloserMenu,
				miscellaneousMenu
			};

			foreach (var menu in menuList)
			{
				BaseMenu.Root.Add(menu);
			}
		}
	}
}