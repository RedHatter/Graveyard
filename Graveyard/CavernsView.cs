using Hearthstone_Deck_Tracker.API;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class CavernsView
    {
        private static ViewConfig _Config;
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new ViewConfig(Rogue.TheCavernsBelow)
            {
                Name = Strings.GetLocalized("Caverns"),
                Enabled = () => Settings.Default.CavernsEnabled,
                CreateView = () => new NormalView(),
                WatchFor = GameEvents.OnPlayerPlay,
                Condition = card => card.Type == "Minion",                
            });
        }
    }
}
