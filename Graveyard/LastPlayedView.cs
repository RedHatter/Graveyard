using Hearthstone_Deck_Tracker;
//using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Hearthstone;
using System.Linq;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class LastPlayedView : ViewBase
    {
        internal static ViewConfig Config 
        {
            get => _Config ?? (_Config = new ViewConfig(
                GreySageParrotConfig.ShowOn
                .Concat(SunwingSquawkerConfig.ShowOn)
                .Concat(BrilliantMacawConfig.ShowOn)
                .Concat(MonstrousParrotConfig.ShowOn)
                .Concat(VanessaVanCleefConfig.ShowOn).ToArray())
            {
                Enabled = () => Settings.Default.LastPlayedEnabled,
            });
        }
        private static ViewConfig _Config;

        internal static ViewConfig GreySageParrotConfig
        {
            get => _GreySageParrotConfig ?? (_GreySageParrotConfig = new ViewConfig(Mage.GreySageParrot)
            {
                Name = Strings.GetLocalized("GreySageParrot"),
                //WatchFor = GameEvents.OnPlayerPlay,
                Condition = card => card.Type == "Spell" && card.Cost >= 5,
            });
        }
        private static ViewConfig _GreySageParrotConfig;

        internal static ViewConfig SunwingSquawkerConfig
        {
            get => _SunwingSquawkerConfig ?? (_SunwingSquawkerConfig = new ViewConfig(Paladin.SunwingSquawker)
            {
                Name = Strings.GetLocalized("SunwingSquawker"),
                //WatchFor = GameEvents.OnPlayerPlay,
                Condition = card => card.Type == "Spell" && LadyLiadrinView.SpellList.Contains(card.Id),
            });
        }
        private static ViewConfig _SunwingSquawkerConfig;

        internal static ViewConfig BrilliantMacawConfig
        {
            get => _BrilliantMacawConfig ?? (_BrilliantMacawConfig= new ViewConfig(Shaman.BrilliantMacaw)
            {
                Name = Strings.GetLocalized("BrilliantMacaw"),
                //WatchFor = GameEvents.OnPlayerPlay,
                Condition = card => card.Mechanics.Contains("Battlecry") && card.Id != Shaman.BrilliantMacaw,
            });
        }
        private static ViewConfig _BrilliantMacawConfig;

        internal static ViewConfig MonstrousParrotConfig
        {
            get => _MonstrousParrotConfig ?? (_MonstrousParrotConfig = new ViewConfig(Hunter.MonstrousParrot)
            {
                Name = Strings.GetLocalized("MonstrousParrot"),
                //WatchFor = GameEvents.OnPlayerPlayToGraveyard,
                Condition = card => card.Mechanics.Contains("Deathrattle") && card.Id != Rogue.UnearthedRaptor,
            });
        }
        private static ViewConfig _MonstrousParrotConfig;

        internal static ViewConfig VanessaVanCleefConfig
        {
            get => _VanessaVanCleefConfig ?? (_VanessaVanCleefConfig = new ViewConfig(Rogue.VanessaVancleefCore)
            {
                Name = Strings.GetLocalized("VanessaVanCleef"),
                //WatchFor = GameEvents.OnOpponentPlay,
                Condition = card => (card.Type == "Spell" || card.Type == "Minion"),
            });
        }
        private static ViewConfig _VanessaVanCleefConfig;

        public static bool IsValid()
        {
            return HasGreySageParrot || HasSunwingSquawker || HasBrilliantMacaw || HasMonstrousParrot || HasVanessaVanCleef;
        }

        public static bool HasGreySageParrot => Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == Mage.GreySageParrot) > -1;
        public static bool HasSunwingSquawker => Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == Paladin.SunwingSquawker) > -1;
        public static bool HasBrilliantMacaw => Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == Shaman.BrilliantMacaw) > -1;
        public static bool HasMonstrousParrot => Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == Hunter.MonstrousParrot) > -1;
        public static bool HasVanessaVanCleef => Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == Rogue.VanessaVancleefCore) > -1;

        readonly LastCardView GreySageParrot;
        readonly LastCardView SunwingSquawker;
        readonly LastCardView BrilliantMacaw;
        readonly LastCardView MonstrousParrot;
        readonly LastCardView VanessaVanCleef;

        public LastPlayedView()
        {
            CreateViewIf(HasGreySageParrot, ref GreySageParrot, GreySageParrotConfig.Name);
            CreateViewIf(HasSunwingSquawker, ref SunwingSquawker, SunwingSquawkerConfig.Name);
            CreateViewIf(HasBrilliantMacaw, ref BrilliantMacaw, BrilliantMacawConfig.Name);
            CreateViewIf(HasMonstrousParrot, ref MonstrousParrot, MonstrousParrotConfig.Name);
            CreateViewIf(HasVanessaVanCleef, ref VanessaVanCleef, VanessaVanCleefConfig.Name);
        }

        private void CreateViewIf(bool create, ref LastCardView view, string title)
        {
            if (create)
            {
                view = new LastCardView(title);
                Children.Add(view); 
            }
        }

        public override bool Update(Card card)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateGreySageParrot(Card card)
        {
            if (GreySageParrot != null && GreySageParrotConfig.Condition(card))
            {
                return GreySageParrot.Update(card);
            }
            return false;
        }

        public bool UpdateSunwingSquawker(Card card)
        {
            if (SunwingSquawker != null && SunwingSquawkerConfig.Condition(card))
            {
                return SunwingSquawker.Update(card);
            }
            return false;
        }

        public bool UpdateBrilliantMacaw(Card card)
        {
            if (BrilliantMacaw != null && BrilliantMacawConfig.Condition(card))
            {
                return BrilliantMacaw.Update(card);
            }
            return false;
        }

        public bool UpdateMonstrousParrot(Card card)
        {
            if (MonstrousParrot != null && MonstrousParrotConfig.Condition(card))
            {
                return MonstrousParrot.Update(card);
            }
            return false;
        }

        public bool UpdateVanessaVanCleef(Card card)
        {
            if (VanessaVanCleef != null && VanessaVanCleefConfig.Condition(card))
            {
                return VanessaVanCleef.Update(card); 
            }
            return false;
        }
    }
}
