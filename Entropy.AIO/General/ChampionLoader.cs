namespace Entropy.AIO.General
{
	using System;
	using System.Reflection;
	using ToolKit;
	using ToolKit.Enumerations;

	internal static class ChampionLoader
	{
		public static void Initialize()
		{
			try
			{
				var path = $"Entropy.AIO.Champions.{LocalPlayer.Instance.CharName}.{LocalPlayer.Instance.CharName}";
				var type = Type.GetType(path, true);

				Activator.CreateInstance(type);
				Logging.Log($"Entropy.AIO: {LocalPlayer.Instance.CharName} Loaded successfully.");
			}
			catch (Exception e)
			{
				switch (e)
				{
					case TargetInvocationException _:
						Logging.Log($"Entropy.AIO: Error occurred while trying to load {LocalPlayer.Instance.CharName}.", LogLevels.error);
						break;
					case TypeLoadException _:
						Logging.Log($"Entropy.AIO: {LocalPlayer.Instance.CharName} is not supported.", LogLevels.warning);
						break;
				}

				e.ToolKitLog();
			}
		}
	}
}