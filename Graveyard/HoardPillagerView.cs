using Hearthstone_Deck_Tracker.API;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class HoardPillagerView 
    {
        private static ViewConfig _Config;
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new ViewConfig(Neutral.HoardPillager, Neutral.RummagingKobold)
            {
                Name = "HoardPillager",
                Enabled = "HoardPillagerEnabled",
                CreateView = () => new ChancesView(),
                UpdateOn = GameEvents.OnPlayerPlayToGraveyard,
                Condition = card => card.Type == "Weapon"
            });
        } 
    }
}
