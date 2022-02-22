using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Hearthstone;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class CavernsView : NormalView
    {
        private static ViewConfig _Config;
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new ViewConfig(Rogue.TheCavernsBelow)
            {
                Name = Strings.GetLocalized("Caverns"),
                Enabled = () => Settings.Default.CavernsEnabled,
                Condition = card => card.Type == "Minion",
                WatchFor = GameEvents.OnPlayerPlay,
                CreateView = () => new NormalView(),
            });
        }

        public CavernsView()
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
