using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Hearthstone;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class KangorView : NormalView
    {
        private static ViewConfig _Config;
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new ViewConfig(Paladin.KangorsEndlessArmy)
            {
                Name = Strings.GetLocalized("Kangor"),
                Enabled = () => Settings.Default.KangorEnabled,
                WatchFor = GameEvents.OnPlayerPlayToGraveyard,
                Condition = card => card.Race == "Mech" || card.Race == "All",
            });
        }
        
        public KangorView()
        {
            // Section Label
            Label.Text = Config.Name;
        }

        public bool Update(Card card)
        {
            return Config.Condition(card) && base.Update(card);
        }
    }
}
