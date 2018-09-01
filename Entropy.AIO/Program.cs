namespace Entropy.AIO
{
	using SDK.Events;
	using SDK.Utils;

	internal static class Program
	{
		private static void Main()
		{
			Loading.OnLoadingComplete += () =>
				{
					DelayAction.Queue(Bootstrap.Initialize, 100);
				};
		}
	}
}
