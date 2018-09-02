namespace Entropy.AIO.General
{
	using SDK.Orbwalking;
	using SDK.UI;
	using SDK.UI.Components;

	internal static class BaseMenu
	{
		public static Menu Root { get; private set; }

		public static void Initialize()
		{
			Root = new Menu("root", LocalPlayer.Instance.CharName, true);
			Root.Attach();
			Orbwalker.Attach(Root);
		}
	}
}
