using Hearthstone_Deck_Tracker.API;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class HoardPillagerView : ChancesView
    {
        private static ViewConfig _Config;
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new ViewConfig(Neutral.HoardPillager, Neutral.RummagingKobold)
            {
                Name = Strings.GetLocalized("HoardPillager"),
                Enabled = () => Settings.Default.HoardPillagerEnabled,
                CreateView = () => new HoardPillagerView(),
                WatchFor = GameEvents.OnPlayerPlayToGraveyard,
                Condition = card => card.Type == "Weapon"
            });
        } 
    }
}
