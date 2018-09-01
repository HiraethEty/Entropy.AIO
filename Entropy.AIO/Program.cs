namespace Entropy.AIO
{
	using SDK.Events;
	using SDK.Utils;

	static class Program
	{
		private static void Main(string[] args)
		{
			Loading.OnLoadingComplete += () =>
				{
					DelayAction.Queue(Bootstrap.Initialize, 100);
				};
		}
	}
}
