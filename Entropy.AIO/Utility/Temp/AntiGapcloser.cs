using Entropy.SDK.Databases.Gapclosers;
using Entropy.SDK.Enumerations;
using Entropy.SDK.Extensions.Geometry;
using Entropy.SDK.Extensions.Objects;
using Entropy.SDK.Utils;

namespace Entropy.AIO.Utility
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using SharpDX;
	using ToolKit;

	public static class Gapcloser
	{
		public delegate void OnGapcloserDelegate(GapcloserArgs args);
		public static event OnGapcloserDelegate OnNewGapcloser;

		private static readonly Dictionary<uint, GapcloserArgs> DetectedGapcloser;

		public static readonly List<SDK.Events.GapcloserSpellData> Spells;

		static Gapcloser()
		{
			Spells = new List<SDK.Events.GapcloserSpellData>();
			DetectedGapcloser = new Dictionary<uint, GapcloserArgs>();
			GapcloserDatabase.Init(Spells);

			if (!Spells.Any())
			{
				return;
			}

			AIBaseClient.OnNewPath += OnNewPath;
			AIBaseClient.OnProcessSpellCast += OnProcessSpellCast;
		}

		private static void OnProcessSpellCast(AIBaseClientCastEventArgs args)
		{
			try
			{
				if (args.Caster == null || !args.Caster.IsValid || !(args.Caster is AIHeroClient sender))
				{
					return;
				}

				if (!Spells.Any(x => x.SpellName.ToLower().Contains(args.SpellData.Name.ToLower())))
				{
					return;
				}

				var speed = args.SpellData.MissileSpeed;
				var startPos = args.StartPosition;

				var usedSpell = Spells.FirstOrDefault(s => s.SpellName.ToLower().Equals(args.SpellData.Name.ToLower()));
				var endPos = usedSpell.SpellType == (SDK.Events.SpellType) SpellType.Targeted
					? args.Target.Position
					: usedSpell.Range > 0
						? args.Caster.Position.Extend(args.EndPosition, usedSpell.IsReversedDash ? -usedSpell.Range : usedSpell.Range)
						: args.EndPosition;

				var startTick = Game.TickCount;
				var endTick = startTick + 1000f * (endPos.Distance(startPos.To2D()) / speed);
				var champion = sender.GetChampion();

				var newGapcloser = new GapcloserArgs
				{
					Sender = sender,
					Type = usedSpell.SpellType,
					EndPosition = endPos,
					StartPosition = startPos,
					Champion = champion,
					Duration = endTick - startTick,
					EndTick = endTick,
					StartTick = startTick
				};

				DelayAction.Queue(() =>
				{
					if (DetectedGapcloser.ContainsKey(args.Caster.NetworkID))
					{
						DetectedGapcloser.Remove(args.Caster.NetworkID);
					}
				}, 3000);

				DetectedGapcloser.Add(sender.NetworkID, newGapcloser);
				OnNewGapcloser?.Invoke(newGapcloser);
			}
			catch (Exception e)
			{
				e.ToolKitLog();
			}
		}

		private static void OnNewPath(AIBaseClientNewPathEventArgs args)
		{
			try
			{
				if (!args.Sender.IsValid || !(args.Sender is AIHeroClient sender) || !args.IsDash || sender.IsAlly())
				{
					return;
				}

				switch (sender.GetChampion())
				{
					case Champion.Fizz:
					case Champion.Vi:
					case Champion.Sion:
					case Champion.Kayn:
						break;
				}

				var speed = args.Sender.CharIntermediate.MoveSpeed;
				var startPos = args.Sender.Position;
				var endPos = args.Nodes.LastOrDefault();
				var startTick = Game.TickCount;
				var endTick = startTick + 1000f * (endPos.Distance(startPos.To2D()) / speed);
				var champion = sender.GetChampion();

				var newGapcloser = new GapcloserArgs
				{
					Sender = sender,
					Type = SDK.Events.SpellType.Dash,
					EndPosition = endPos,
					StartPosition = startPos,
					Champion = champion,
					Duration = endTick - startTick,
					EndTick = endTick,
					StartTick = startTick
				};

				if (DetectedGapcloser.ContainsKey(sender.NetworkID))
				{
					DetectedGapcloser.Remove(sender.NetworkID);
				}

				DetectedGapcloser.Add(sender.NetworkID, newGapcloser);
				OnNewGapcloser?.Invoke(newGapcloser);
			}
			catch (Exception e)
			{
				e.ToolKitLog();
			}
		}

		public static GapcloserArgs GetGapcloserInfo(this GameObject target)
		{
			return DetectedGapcloser.TryGetValue(target.NetworkID, out var value) ? value : new GapcloserArgs();
		}

		public class GapcloserArgs : EntropyEventArgs
		{
			public AIHeroClient Sender { get; set; }
			public SDK.Events.SpellType Type { get; set; }
			public Vector3 StartPosition { get; set; }
			public Vector3 EndPosition { get; set; }
			public int StartTick { get; set; }
			public float EndTick { get; set; }
			public float Duration { get; set; }
			public Champion Champion { get; set; }
		}
	}

	public struct GapcloserSpellData
	{
		public Champion Champion { get; set; }
		public string SpellName { get; set; }
		public SpellSlot Slot { get; set; }
		public SpellType SpellType { get; set; }
		public bool IsReversedDash { get; set; }
		public int Range { get; set; }
	}

	public enum SpellType
	{
		Dash = 0,
		Targeted = 1
	}
}