using Hearthstone_Deck_Tracker.API;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    internal class MinionsCreatedView
    {
        private static ViewConfig _Config;
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new ViewConfig(Priest.RaDen)
            {
                CreateView = () => new NormalView(),
                UpdateOn = GameEvents.OnPlayerGet,
                Condition = card => card.Type == "Minion",
            });
        }
    }
}
