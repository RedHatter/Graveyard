using Hearthstone_Deck_Tracker.API;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class TessGreymaneView 
    {
        private static ViewConfig _Config;
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new ViewConfig(Rogue.TessGreymaneCore, Rogue.ContrabandStash)
            {
                Name = "TessGreymane",
                Enabled = "TessGreymaneEnabled",
                CreateView = () => new NormalView(),
                UpdateOn = GameEvents.OnPlayerPlay,
                Condition = card => !card.IsClass("Rogue") && !card.IsClass("Neutral"),
            });
        }       
    }
}
