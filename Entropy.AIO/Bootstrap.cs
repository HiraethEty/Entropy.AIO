namespace Entropy.AIO
{
	using System;
	using General;

	static class Bootstrap
	{
		public static void Initialize()
		{
			BaseMenu.Initialize();
			ChampionLoader.Initialize();
		}
	}
}
