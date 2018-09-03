namespace Entropy.AIO.General
{
	using System.Collections.Generic;
	using Enumerations;
	using SharpDX;

	internal class ColorScheme
	{
		public static List<Color[]> ColorSchemes = new List<Color[]>
		{
			Enumerations.BoldColors,
			Enumerations.FlatColors,
			Enumerations.Feminine
		};
	}
}
