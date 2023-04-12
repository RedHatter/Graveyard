using Hearthstone_Deck_Tracker.API;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    internal class StranglethornHeartView
    {
        private static ViewConfig _Config;
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new ViewConfig(
                Hunter.StranglethornHeart)
            {
                CreateView = () => new ChancesView(),
                UpdateOn = GameEvents.OnPlayerPlayToGraveyard,
                Condition = card => card.Cost >= 5 && (card.Race == "Beast" || card.Race == "All"),
            });
        }
    }
}
