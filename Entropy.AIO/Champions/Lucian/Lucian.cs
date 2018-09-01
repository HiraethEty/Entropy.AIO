namespace Entropy.AIO.Champions.Lucian
{
	using System.Collections.Generic;
	using General;
	using SDK.Caching;
	using SDK.Orbwalking;
	using SDK.UI;
	using SDK.UI.Components;

	class Lucian : Champion
	{
		public Lucian()
		{
			Orbwalker.OnPostAttack += PostAttack;
		}

		protected sealed override void LoadMenu()
		{
			var combo = new Menu("combo", "Combo")
			{
				new MenuBool("q", "Q"),
				new MenuBool("extendedQ", "Extended Q"),
				new MenuBool("w", "W"),
				new MenuList("e", "E", new []{"Automatic", "Cursor"}, 1),
				new MenuBool("r", "R"),
				new Menu("rWhitelist", "R Whitelist")
			};
			foreach (var enemy in ObjectCache.EnemyHeroes)
			{
				combo["rWhitelist"].As<Menu>().Add(new MenuBool($"r{enemy.CharName}", $"R on {enemy.CharName}"));
			}
			var harass = new Menu("harass", "Harass")
			{

			};
			var laneClear = new Menu("laneClear", "Lane Clear")
			{

			};
			var misc = new Menu("misc", "Misc")
			{

			};
			var drawing = new Menu("drawing", "Drawing")
			{

			};

			var menuList = new []
			{
				combo,
				harass,
				laneClear,
				misc,
				drawing
			};

			foreach (var x in menuList)
			{
				BaseMenu.Root.Add(x);
			}
		}

		protected sealed override void LoadSpells()
		{
		}

		protected override void OnTick(EntropyEventArgs args)
		{

		}

		protected override void OnRender(EntropyEventArgs args)
		{

		}

		public override void PostAttack(EntropyEventArgs args)
		{

		}
	}
}
