namespace Entropy.AIO.General
{
	using SDK.Rendering;
	using SharpDX;

	abstract class BaseDrawing
	{
		internal static void DrawCircle(Vector3 worldPosition, float radius, Color color)
		{
			if (BaseMenu.Root["drawing"]["mode"].Value == 0)
			{
				Renderer.DrawCircularRangeIndicator(worldPosition, radius, color);
				return;
			}
			CircleRendering.Render(color, radius, 4f, false, worldPosition);
		}
	}
}
