using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Hearthstone;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class TessGreymaneView : NormalView
    {
        private static ViewConfig _Config;
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new ViewConfig(Rogue.TessGreymane, Rogue.ContrabandStash)
            {
                Name = Strings.GetLocalized("TessGreymane"),
                Enabled = () => Settings.Default.TessGreymaneEnabled,
                WatchFor = GameEvents.OnPlayerPlay,
                Condition = card => !card.IsClass("Rogue") && !card.IsClass("Neutral"),
            });
        }
        
        public TessGreymaneView()
        {
            Label.Text = Config.Name;
        }

        public bool Update(Card card)
        {
            return Config.Condition(card) && base.Update(card, true);
        }
    }
}
