namespace Entropy.AIO.Champions.Lucian
{
	using General;
	using SDK.Spells;

	internal class Drawings : BaseDrawing
	{
		private Spell[] Spells { get; }

		public Drawings(Spell[] spells)
		{
			this.Spells = spells;
			Renderer.OnRender += this.OnRender;
		}

		private void OnRender(EntropyEventArgs args)
		{
			var colorScheme = ColorScheme.ColorSchemes[BaseMenu.Root["drawing"]["colorscheme"].Value];
			if (Champions.Lucian.Spells.ExtendedQ.Ready && BaseMenu.Root["drawing"]["qextended"].Enabled)
			{
				DrawCircle(LocalPlayer.Instance.Position, this.Spells[1].Range, colorScheme[4]);
			}

			foreach (var spell in this.Spells)
			{
				if (!spell.Ready ||
				    !BaseMenu.Root["drawing"][$"{spell.Slot.ToString().ToLower()}"].Enabled ||
				    BaseMenu.Root["drawing"][$"{spell.Slot.ToString().ToLower()}"] == null)
				{
					continue;
				}

				DrawCircle(LocalPlayer.Instance.Position, spell.Range, colorScheme[(int) spell.Slot]);
			}
		}
	}
}