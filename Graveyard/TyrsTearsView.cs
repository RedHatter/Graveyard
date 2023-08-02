using HearthDb.Enums;
using Hearthstone_Deck_Tracker.API;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    internal class TyrsTearsView
    {
        private static ViewConfig _Config;
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new ViewConfig(
                Paladin.TyrsTears_TyrsTearsToken)
            {
                CreateView = () => new ChancesView(),
                UpdateOn = GameEvents.OnPlayerPlayToGraveyard,
                Condition = card => card.CardClass == CardClass.PALADIN 
                    && card.Type == "Minion",
            });
        }
    }
}
