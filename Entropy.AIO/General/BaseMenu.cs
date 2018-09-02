namespace Entropy.AIO.General
{
	using SDK.Orbwalking;
	using SDK.UI;
	using SDK.UI.Components;
	using Enumerations;

	internal static class BaseMenu
	{
		public static Menu Root { get; private set; }

		public static void Initialize()
		{
			Root = new Menu("root", LocalPlayer.Instance.CharName, true)
			{
				new Menu("general", "General Menu")
				{
					new MenuBool("supportmode", "Support Mode", false),
					new MenuSliderBool("disableaa", "Disable attacks in Combo / If Level >= x", false, 2, 2, 18),
					new MenuBool("junglesmall", "Cast to small minions too in JungleClear", false),

					new Menu("stormrazor", "Stormrazor Menu")
					{
						new MenuSeperator("stormsep", "Stop AA'ing until it procs in:"),
						new MenuBool("combo", "Combo", false),
						new MenuBool("laneclear", "Laneclear", false),
						new MenuBool("mixed", "Harass", false),
						new MenuBool("lasthit", "Lasthit", false)
					},

					new Menu("preservemana", "Preserve Mana Menu")
					{
						new MenuSeperator("separator", "Preserve Mana for:")
					},

					new Menu("preservespells", "Preserve Spells Menu")
					{
						new MenuSeperator("separator", "Only works for inside-AA-range targets"),
						new MenuSeperator("separator2", "0 = Don't limit.")
					}
				}
			};

			if (LocalPlayer.Instance.MaxMP >= 200)
			{
				Root["general"].As<Menu>().Add(new MenuBool("nomanagerifblue", "Ignore ManaManagers if you have Blue Buff", false));
			}

			if (LocalPlayer.Instance.IsMelee)
			{
				var hydraMenu = new Menu("hydra", "Hydras Menu")
				{
					new MenuSeperator("hydrasep", "Use Hydras in:"),
					new MenuBool("combo", "Combo"),
					new MenuBool("laneclear", "Laneclear"),
					new MenuBool("mixed", "Harass"),
					new MenuBool("lasthit", "Lasthit"),
					new MenuBool("manual", "While playing manually")
				};

				Root["general"].As<Menu>().Add(hydraMenu);
			}

			foreach (var spellSlot in Enumerations.SpellSlots)
			{
				Root["general"]["preservemana"].As<Menu>().Add(new MenuBool(spellSlot.ToString().ToLower(), spellSlot.ToString(), false));
				Root["general"]["preservespells"].As<Menu>().Add(new MenuSlider(spellSlot.ToString().ToLower(), $"Don't cast {spellSlot} if target killable in X AAs", 0, 0, 10));
			}

			Root.Attach();
			Orbwalker.Attach(Root);
		}
	}
}