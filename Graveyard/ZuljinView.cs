using Hearthstone_Deck_Tracker.API;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class ZuljinView
    {
        private static ViewConfig _Config;
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new ViewConfig(Hunter.Zuljin)
            {
                Name = "Zuljin",
                Enabled = "ZuljinEnabled",
                CreateView = () => new NormalView(),
                UpdateOn = GameEvents.OnPlayerPlay,
                Condition = card => card.Type == "Spell",
            });
        }       
    }
}
