namespace Entropy.AIO
{
	using General;

	internal static class Bootstrap
	{
		public static void Initialize()
		{
			BaseMenu.Initialize();
			BaseLogic.Initialize();
			ChampionLoader.Initialize();
		}
	}
}