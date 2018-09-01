namespace Entropy.AIO.General
{
	using SDK.Orbwalking;
	using SDK.UI;
	using SDK.UI.Components;

	static class BaseMenu
	{
		public static Menu Root { get; private set; }

		public static void Initialize()
		{
			Root = new Menu("root", LocalPlayer.Instance.CharName, true);
			{
				var menuItem = new Menu("menuItem", "Menu Item")
				{
					new MenuBool("test", "test")
				};
				Root.Add(menuItem);
			}
			Root.Attach();
			Orbwalker.Attach(Root);
		}
	}
}
