using HearthDb.Enums;
using Hearthstone_Deck_Tracker.API;
using System.Collections.Generic;
using System.Linq;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class LastPlayedView
    {
        internal static ViewConfig GreySageParrotConfig
        {
            get => _GreySageParrotConfig ?? (_GreySageParrotConfig = new LastCardView.ViewConfig(Mage.GreySageParrot)
            {
                Name = "GreySageParrot",
                UpdateOn = GameEvents.OnPlayerPlay,
                Condition = card => card.Type == "Spell" && card.Cost >= 6,
            });
        }
        private static ViewConfig _GreySageParrotConfig;

        internal static ViewConfig SunwingSquawkerConfig
        {
            get => _SunwingSquawkerConfig ?? (_SunwingSquawkerConfig = new LastCardView.ViewConfig(Paladin.SunwingSquawker)
            {
                Name = "SunwingSquawker",
                UpdateOn = GameEvents.OnPlayerPlay,
                Condition = card => card.Type == "Spell" && LadyLiadrinView.SpellList.Contains(card.Id),
            });
        }
        private static ViewConfig _SunwingSquawkerConfig;

        internal static ViewConfig BrilliantMacawConfig
        {
            get => _BrilliantMacawConfig ?? (_BrilliantMacawConfig= new LastCardView.ViewConfig(Shaman.BrilliantMacaw)
            {
                Name = "BrilliantMacaw",
                UpdateOn = GameEvents.OnPlayerPlay,
                Condition = card => card.Mechanics.Contains("Battlecry") && card.Id != Shaman.BrilliantMacaw,
            });
        }
        private static ViewConfig _BrilliantMacawConfig;

        internal static ViewConfig MonstrousParrotConfig
        {
            get => _MonstrousParrotConfig ?? (_MonstrousParrotConfig = new LastCardView.ViewConfig(Hunter.MonstrousParrot)
            {
                Name = "MonstrousParrot",
                UpdateOn = GameEvents.OnPlayerPlayToGraveyard,
                Condition = card => card.Mechanics.Contains("Deathrattle") && card.Id != Rogue.UnearthedRaptor,
            });
        }
        private static ViewConfig _MonstrousParrotConfig;

        internal static ViewConfig VanessaVanCleefConfig
        {
            get => _VanessaVanCleefConfig ?? (_VanessaVanCleefConfig = new LastCardView.ViewConfig(Rogue.VanessaVancleefLegacy)
            {
                Name = "VanessaVanCleef",
                UpdateOn = GameEvents.OnOpponentPlay,
                Condition = card => true,
            });
        }
        private static ViewConfig _VanessaVanCleefConfig;

        internal static ViewConfig LadyDarkveinConfig
        {
            get => _LadyDarkveinConfig ?? (_LadyDarkveinConfig = new LastCardView.ViewConfig(Warlock.LadyDarkvein)
            {
                Name = "LadyDarkvein",
                UpdateOn = GameEvents.OnPlayerPlayToGraveyard,
                Condition = card => (card.SpellSchool == SpellSchool.SHADOW),
            });
        }
        private static ViewConfig _LadyDarkveinConfig;

        internal static ViewConfig AsvedonConfig
        {
            get => _AsvedonConfig ?? (_AsvedonConfig = new LastCardView.ViewConfig(Warrior.AsvedonTheGrandshield)
            {
                Name = "Asvedon",
                UpdateOn = GameEvents.OnOpponentPlay,
                Condition = card => card.Type == "Spell",
            });
        }
        private static ViewConfig _AsvedonConfig;

        internal static ViewConfig LastRiffConfig
        {
            get => _LastRiffConfig ?? (_LastRiffConfig = new LastCardView.ViewConfig(Riffs.ToArray())
            {
                Name = "LastRiff",
                UpdateOn = GameEvents.OnPlayerPlay,
                Condition = card => Riffs.Contains(card.Id),
            });
        }
        private static ViewConfig _LastRiffConfig;

        internal static readonly List<string> Riffs = new List<string>
        {
            Warrior.VerseRiff,
            Warrior.BridgeRiff,
            Warrior.ChorusRiff,
        };

        internal static ViewConfig EndgameConfig
        {
            get => _EndgameConfig ?? (_EndgameConfig = new LastCardView.ViewConfig(Warlock.Endgame) 
            {
                Name = "Endgame",
                UpdateOn = GameEvents.OnPlayerPlayToGraveyard,
                Condition = card => card.Race == "Demon",
            });
        }
        private static ViewConfig _EndgameConfig;
    }
}
