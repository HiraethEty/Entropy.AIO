namespace Entropy.AIO.Champions.Lucian.Drawings
{
	using General;
	using SDK.Spells;

	class Drawing
	{
		private Spell[] Spells { get; set; }
		public Drawing(Spell[] spells)
		{
			this.Spells = spells;
			Renderer.OnRender += OnRender;
		}

		private void OnRender(EntropyEventArgs args)
		{
			foreach (var spell in this.Spells)
			{
				if (!BaseMenu.Root["drawing"][$"{spell.Slot.ToString().ToLower()}"].Enabled)
				{
					return;
				}

				if (!spell.Ready)
				{
					return;
				}

				Renderer.DrawCircularRangeIndicator(LocalPlayer.Instance.Position, spell.Range, S);
			}
		}
	}
}
