namespace Entropy.AIO.Champions.Lucian.Drawings
{
	using Enumerations;
	using General;
	using SDK.Spells;

	internal class Drawing
	{
		private Spell[] Spells { get; }
		public Drawing(Spell[] spells)
		{
			this.Spells = spells;
			Renderer.OnRender += this.OnRender;
		}

		private void OnRender(EntropyEventArgs args)
		{
			if (this.Spells[1].Ready && BaseMenu.Root["drawing"]["qextended"].Enabled)
			{
				Renderer.DrawCircularRangeIndicator(LocalPlayer.Instance.Position, this.Spells[1].Range, Enumerations.Colors[4]);
			}

			foreach (var spell in this.Spells)
			{
				if (!spell.Ready ||
				    !BaseMenu.Root["drawing"][$"{spell.Slot.ToString().ToLower()}"].Enabled)
				{
					continue;
				}

				Renderer.DrawCircularRangeIndicator(LocalPlayer.Instance.Position, spell.Range, Enumerations.Colors[(int)spell.Slot]);
			}
		}
	}
}
