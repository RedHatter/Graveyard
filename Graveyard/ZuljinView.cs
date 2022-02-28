using Hearthstone_Deck_Tracker.API;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class ZuljinView : NormalView
    {
        private static ViewConfig _Config;
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new ViewConfig(Hunter.Zuljin)
            {
                Name = Strings.GetLocalized("Zuljin"),
                Enabled = () => Settings.Default.ZuljinEnabled,
                CreateView = () => new ZuljinView(),
                WatchFor = GameEvents.OnPlayerPlay,
                Condition = card => card.Type == "Spell",
            });
        }       
    }
}
