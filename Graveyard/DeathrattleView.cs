using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Hearthstone;
using System;
using System.Linq;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class DeathrattleView
    {
        private static readonly string SharedName = "Deathrattle";
        private static readonly Func<ViewBase> SharedCreateView = () => new NormalView();
        private static readonly Predicate<Card> SharedCondition = card => card.Mechanics.Contains("Deathrattle") && card.Id != Rogue.UnearthedRaptor;

        private static ViewConfig _PlayerConfig;
        internal static ViewConfig PlayerConfig
        {
            get => _PlayerConfig ?? (_PlayerConfig = new ViewConfig(
                Priest.XyrellaTheDevout, 
                Warlock.TamsinsPhylactery,
                Priest.AmuletOfUndying,
                Rogue.CounterfeitBlade,
                Hunter.JewelOfNzoth,
                Neutral.Vectus,
                Hunter.NineLives,
                Neutral.DaUndatakah,
                Priest.TwilightsCall)
            {
                Name = SharedName,
                Enabled = "DeathrattleEnabled",
                Condition = SharedCondition,
                UpdateOn = GameEvents.OnPlayerPlayToGraveyard,
                CreateView = SharedCreateView,
            });
        }

        private static ViewConfig _OpponentConfig;
        internal static ViewConfig OpponentConfig
        {
            get => _OpponentConfig ?? (_OpponentConfig = new ViewConfig()
            {
                Name = "Deathrattle",
                Enabled = "OpponentDeathrattleEnabled",
                Condition = SharedCondition,
                UpdateOn = GameEvents.OnOpponentPlayToGraveyard,
                CreateView = SharedCreateView,
            });
        }
    }
}
