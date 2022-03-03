using Hearthstone_Deck_Tracker.API;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class GrandFinaleView
    {
        private static ViewConfig _Config;
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new ViewConfig(Mage.GrandFinale)
            {
                Name = Strings.GetLocalized("GrandFinale"),
                Enabled = () => Settings.Default.GrandFinaleEnabled,
                CreateView = () => new MultiTurnView(Config.Name, 1),
                WatchFor = GameEvents.OnPlayerPlay,
                Condition = card => card.Race == "Elemental" || card.Race == "All",
            });
        }       
    }
}
