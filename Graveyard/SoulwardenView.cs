using Hearthstone_Deck_Tracker.API;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class SoulwardenView
    {
        private static ViewConfig _Config;
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new ViewConfig(Warlock.Soulwarden)
            {
                Name = "Soulwarden",
                Enabled = "SoulwardenEnabled",
                CreateView = () => new ChancesView(),
                UpdateOn = GameEvents.OnPlayerHandDiscard,
                Condition = card => true,
            });
        }
    }
}
