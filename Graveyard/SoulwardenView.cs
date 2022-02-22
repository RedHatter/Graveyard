using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Hearthstone;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class SoulwardenView : NormalView
    {
        private static ViewConfig _Config;
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new ViewConfig(Warlock.Soulwarden)
            {
                Name = Strings.GetLocalized("Soulwarden"),
                Enabled = () => Settings.Default.SoulwardenEnabled,
                WatchFor = GameEvents.OnPlayerHandDiscard,
                Condition = card => true,
            });
        }
        
        private ChancesTracker _chances = new ChancesTracker();

        public SoulwardenView()
        {
            // Section Label
            Label.Text = Config.Name;
        }

        public bool Update(Card card)
        {
            if (!base.Update(card, card.Type == "Spell"))
            {
                return false;
            }

            // Silverware Golem and Clutchmother Zaras are still counted as discarded, even when their effects trigger
            _chances.Update(card, Cards, View);

            return true;
        }
    }
}
