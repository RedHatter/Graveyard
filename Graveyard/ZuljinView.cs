using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Hearthstone;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class ZuljinView : NormalView
    {
        private static ViewConfig _Config;
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new ViewConfig(Hunter.Zuljin)
            {
                Name = Strings.GetLocalized("Zuljin"),
                Enabled = () => Settings.Default.ZuljinEnabled,
                WatchFor = GameEvents.OnPlayerPlay,
                Condition = card => card.Type == "Spell",
            });
        }       

        public ZuljinView()
        {
            Label.Text = Config.Name;
        }

        public bool Update(Card card)
        {
            return Config.Condition(card) && base.Update(card, true);
        }
    }
}
