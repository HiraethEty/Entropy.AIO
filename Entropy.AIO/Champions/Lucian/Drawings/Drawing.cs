namespace Entropy.AIO.Champions.Lucian.Drawings
{
	using Enumerations;
	using General;
	using SDK.Spells;

	class Drawing
	{
		private Spell[] Spells { get; set; }
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
				if (!BaseMenu.Root["drawing"][$"{spell.Slot.ToString().ToLower()}"].Enabled)
				{
					continue;
				}

				if (!spell.Ready)
				{
					continue;
				}

				Renderer.DrawCircularRangeIndicator(LocalPlayer.Instance.Position, spell.Range, Enumerations.Colors[(int)spell.Slot]);
			}
		}
	}
}
