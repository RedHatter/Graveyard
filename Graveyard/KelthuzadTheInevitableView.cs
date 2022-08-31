using Hearthstone_Deck_Tracker.API;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class KelthuzadTheInevitableView
    {
        private static ViewConfig _Config;
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new ViewConfig(Mage.KelthuzadTheInevitable)
            {
                Name = "KelthuzadTheInevitable",
                ShowFirst = () => true,
                CreateView = () => new NormalView(),
                UpdateOn = GameEvents.OnPlayerPlayToGraveyard,
                Condition = card => card.Id == Neutral.VolatileSkeleton,
            });
        }
    }
}