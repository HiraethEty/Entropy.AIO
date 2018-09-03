namespace Entropy.AIO.Champions.Lucian.Drawings
{
	using Enumerations;
	using General;
	using SDK.Spells;

	internal class Drawings
	{
		private Spell[] Spells { get; set; }
		public Drawings(Spell[] spells)
		{
			this.Spells = spells;
			Renderer.OnRender += this.OnRender;
		}

		private void OnRender(EntropyEventArgs args)
		{
			if (Champions.Lucian.Spells.ExtendedQ.Ready && BaseMenu.Root["drawing"]["qextended"].Enabled)
			{
				Renderer.DrawCircularRangeIndicator(LocalPlayer.Instance.Position, this.Spells[1].Range, Enumerations.FlatColors[4]);
			}
			
			foreach (var spell in this.Spells)
			{
				if (!spell.Ready ||
				    !BaseMenu.Root["drawing"][$"{spell.Slot.ToString().ToLower()}"].Enabled || BaseMenu.Root["drawing"][$"{spell.Slot.ToString().ToLower()}"] == null)
				{
					continue;
				}

				Renderer.DrawCircularRangeIndicator(LocalPlayer.Instance.Position, spell.Range, Enumerations.FlatColors[(int)spell.Slot]);
			}
		}
	}
}
