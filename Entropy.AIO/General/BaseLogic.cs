namespace Entropy.AIO.General
{
	using System.Linq;
	using SDK.Caching;
	using SDK.Enumerations;
	using SDK.Events;
	using SDK.Extensions.Geometry;
	using SDK.Extensions.Objects;
	using SDK.Orbwalking;
	using SDK.Orbwalking.EventArgs;

	internal static class BaseLogic
	{
		public static void Initialize()
		{
			// Cleanup un-needed subscriptions
			// Gonna think of a way to dynamically add them depending on need.
			Tick.OnTick += OnTick;
			Renderer.OnRender += OnRender;
			Orbwalker.OnPreAttack += OnPreAttack;
			Orbwalker.OnPostAttack += OnPostAttack;
			Spellbook.OnLocalCastSpell += OnLocalCastSpell;
			BuffManager.OnGainBuff += OnGainBuff;
			BuffManager.OnLoseBuff += OnLoseBuff;
			BuffManager.OnUpdateBuff += OnUpdateBuff;
		}

		private static void OnTick(EntropyEventArgs args) { }

		private static void OnRender(EntropyEventArgs args) { }

		private static void OnPreAttack(OnPreAttackEventArgs args)
		{
			if (BaseMenu.Root["general"]["supportmode"].Enabled)
			{
				SupportMode(args);
			}
		}

		private static void SupportMode(OnPreAttackEventArgs args)
		{
			if (args.Target.IsStructure())
			{
				return;
			}

			if (Orbwalker.Mode != OrbwalkingMode.LaneClear && Orbwalker.Mode != OrbwalkingMode.Harass)
			{
				return;
			}

			if (args.Target == null || !args.Target.IsValid)
			{
				return;
			}

			if (!(args.Target is AIMinionClient))
			{
				return;
			}

			args.Cancel = ObjectCache.AllyHeroes.Any(a => !a.IsMe() && a.DistanceToPlayer() < 2500);
		}

		private static void OnPostAttack(OnPostAttackEventArgs args) { }

		private static void OnLocalCastSpell(SpellbookLocalCastSpellEventArgs args) { }

		private static void OnGainBuff(BuffManagerGainBuffEventArgs args) { }

		private static void OnLoseBuff(BuffManagerLoseBuffEventArgs args) { }

		private static void OnUpdateBuff(BuffManagerUpdateBuffEventArgs args) { }
	}
}