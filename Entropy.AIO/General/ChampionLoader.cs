namespace Entropy.AIO.General
{
	using System;
	using System.Reflection;
	using ToolKit;

	static class ChampionLoader
	{
		public static void Initialize()
		{
			try
			{
				var path = "Champions." + LocalPlayer.Instance.CharName;
				var type = Type.GetType(path, true);

				Activator.CreateInstance(type);
			}
			catch (Exception e)
			{
				switch (e)
				{
					case TargetInvocationException _:
						Logging.Log($"Entropy.AIO - Error occurred while trying to load {LocalPlayer.Instance.CharName}.");
						e.ToolKitLog();
						break;
					case TypeLoadException _:
						BaseMenu.Root.DisplayName = $"{LocalPlayer.Instance.CharName} - Unsupported";
						break;
				}
			}
		}
	}
}