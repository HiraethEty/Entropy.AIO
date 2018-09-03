namespace Entropy.AIO.Champions
{
	using SDK.Spells;

	public abstract class Champion
	{
		internal static Spell Q { get; set; }
		internal static Spell W { get; set; }
		internal static Spell E { get; set; }
		internal static Spell R { get; set; }
	}
}