using Hearthstone_Deck_Tracker.API;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class WitchingHourView
    {
        private static ViewConfig _Config;
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new ViewConfig(Druid.WitchingHour, Hunter.RevivePet)
            {
                Name = "WitchingHour",
                Enabled = "WitchingHourEnabled",
                CreateView = () => new ChancesView(),
                UpdateOn = GameEvents.OnPlayerPlayToGraveyard,
                Condition = card => card.Race == "Beast" || card.Race == "All",
            });
        }       
    }
}
