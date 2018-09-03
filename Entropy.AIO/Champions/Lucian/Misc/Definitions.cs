namespace Entropy.AIO.Champions.Lucian.Misc
{
	using SDK.Extensions.Geometry;
	using SDK.Extensions.Objects;
	using SDK.Geometry;

	internal class Definitions
	{
		/// <summary>
		///     Returns true if the player is using the ultimate.
		/// </summary>
		public static bool IsCulling() => LocalPlayer.Instance.HasBuff("LucianR");

		/// <summary>
		///     Returns true if the player has passive attack ready.
		/// </summary>
		public static bool HasPassive() => LocalPlayer.Instance.HasBuff("LucianPassiveBuff");

		/// <summary>
		///     The Q Rectangle.
		/// </summary>
		/// <param name="unit">The unit.</param>
		public static Rectangle QRectangle(AIBaseClient unit) => new Rectangle(LocalPlayer.Instance.Position,
		                                                                       LocalPlayer.Instance.Position.Extend(unit.Position, Spells.ExtendedQ.Range),
																			   Spells.ExtendedQ.Width);
	}
}
