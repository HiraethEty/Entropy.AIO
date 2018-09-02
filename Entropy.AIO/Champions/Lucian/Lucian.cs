namespace Entropy.AIO.Champions.Lucian
{
	using System.Linq;
	using General;
	using SDK.Caching;
	using SDK.Events;
	using SDK.Extensions.Objects;
	using SDK.Orbwalking;
	using SDK.Orbwalking.EventArgs;
	using SDK.UI;
	using SDK.UI.Components;

	internal sealed class Lucian : Champion
	{
		public Lucian()
		{
			Tick.OnTick += OnTick;
			Utility.Gapcloser.OnNewGapcloser += OnNewGapcloser;
			Orbwalker.OnPostAttack += OnPostAttack;
			Renderer.OnRender += OnRender;
		}

		protected override void LoadMenu()
		{
			var combo = new Menu("combo", "Combo")
			{
				new MenuBool("normalQ", "Use normal Q"),
				new MenuBool("extendedQ", "Use extended Q"),
				new MenuBool("w", "Use W"),
				new MenuList("e", "Use E", new []{"Dynamic Range", "Always Short", "Always Long", "Don't use E"}).SetToolTip("Always directed to Cursor"),
				new MenuBool("normalR", "Use R if all spells on CD", false),
				new MenuBool("essenceR", "Use R to proc Essence Reaver").SetToolTip("If you have Essence Reaver, uses R and immediately stops it to make full use of its passive.")
			};

			var harass = new Menu("harass", "Harass")
			{
				new MenuBool("normalQ", "Use normal Q"),
				new MenuBool("extendedQ", "Use extended Q"),
				new MenuBool("w", "Use W", false),

				new Menu("whitelists", "Whitelists")
				{
					new Menu("normalQ", "Normal Q Whitelist"),
					new Menu("extendedQ", "Extended Q Whitelist"),
					new Menu("w", "W Whitelist")
				}
			};

			var harassWhitelist = harass["whitelists"];
			foreach (var enemy in ObjectCache.EnemyHeroes)
			{
				harassWhitelist["normalQ"].As<Menu>().Add(new MenuBool(enemy.CharName, $"Normal Q on: {enemy.CharName}"));
				harassWhitelist["extendedQ"].As<Menu>().Add(new MenuBool(enemy.CharName, $"Extended Q on: {enemy.CharName}"));
				harassWhitelist["w"].As<Menu>().Add(new MenuBool(enemy.CharName, $"W on: {enemy.CharName}"));
			}

			var laneClear = new Menu("laneClear", "Lane Clear")
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

			var jungleClear = new Menu("jungleClear", "Jungle Clear")
			{
				new MenuSliderBool("q", "Use Q / If Mana >= x%", true, 50),
				new MenuSliderBool("w", "Use W / If Mana >= x%", false, 50),
				new MenuSliderBool("e", "Use E / If Mana >= x%", false, 50)
			};

			var structureClear = new Menu("structureClear", "Structure Clear")
			{
				new MenuSliderBool("w", "Use W / If Mana >= x%", false, 50),
				new MenuSliderBool("e", "Use E / If Mana >= x%", false, 50)
			};

			var antiGapcloser = new Menu("antiGapcloser", "Anti-Gapcloser")
			{
				new Menu("e", "E")
				{
					new MenuBool("enabled", "Enable"),
					new MenuSeperator(string.Empty)
				}
			};

			foreach (var enemy in ObjectCache.EnemyHeroes.Where(enemy =>
				enemy.IsMelee &&
				Utility.Gapcloser.Spells.Any(spell => spell.Champion == enemy.GetChampion())))
			{
				var subAntiGapcloser = new Menu(enemy.CharName.ToLower(), enemy.CharName);
				{
					foreach (var spell in Utility.Gapcloser.Spells.Where(spell => spell.Champion == enemy.GetChampion()))
					{
						subAntiGapcloser.Add(new MenuBool($"{enemy.CharName.ToLower()}.{spell.SpellName.ToLower()}", $"Slot: {spell.Slot} ({spell.SpellName})"));
					}
				}

				antiGapcloser["e"].As<Menu>().Add(subAntiGapcloser);
			}

			var miscellaneous = new Menu("miscellaneous", "Miscellaneous")
			{
				new Menu("e", "E Combo Customization")
				{
					new MenuBool("noeoutaarange", "Don't E out of AA range from enemies"),
					new MenuBool("onlyeifmouseoutaarange", "Only E if mouse out of self AA Range", false),
					new MenuSliderBool("erangecheck", "Don't E if X enemies in range from dash position", true, 3, 2, 6),
					new MenuBool("noeturret", "Don't use E under Enemy Turret")
				}
			};

			var drawing = new Menu("drawing", "Drawing")
			{
				new MenuBool("q", "Q Range", false),
				new MenuBool("qextended", "Extended Q Range"),
				new MenuBool("w", "W Range", false),
				new MenuBool("e", "E Range", false),
				new MenuBool("r", "R Range", false)
			};

			var menuList = new[]
			{
				combo,
				harass,
				laneClear,
				jungleClear,
				structureClear,
				antiGapcloser,
				miscellaneous,
				drawing
			};

			foreach (var menu in menuList)
			{
				BaseMenu.Root.Add(menu);
			}
		}

		protected override void LoadSpells()
		{
		}

		public override void OnTick(EntropyEventArgs args)
		{

		}

		public override void OnRender(EntropyEventArgs args)
		{

		}

		public override void OnPostAttack(OnPostAttackEventArgs args)
		{

		}

		public override void OnNewGapcloser(Utility.Gapcloser.GapcloserArgs args)
		{
			
		}
	}
}
