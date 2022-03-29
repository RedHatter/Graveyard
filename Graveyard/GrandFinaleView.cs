using Hearthstone_Deck_Tracker.API;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class GrandFinaleView
    {
        private static MultiTurnView.ViewConfig _Config;
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new MultiTurnView.ViewConfig(Mage.GrandFinale)
            {
                Name = "GrandFinale",
                Enabled = "GrandFinaleEnabled",
                CreateView = () => new MultiTurnView(1),
                UpdateOn = GameEvents.OnPlayerPlay,
                Condition = card => card.Race == "Elemental" || card.Race == "All",
            });
        }       
    }
}
