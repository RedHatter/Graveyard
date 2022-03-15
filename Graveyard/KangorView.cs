using Hearthstone_Deck_Tracker.API;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class KangorView
    {
        private static ViewConfig _Config;
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new ViewConfig(Paladin.KangorsEndlessArmy)
            {
                Name = "Kangor",
                Enabled = "KangorEnabled",
                CreateView = () => new NormalView(),
                UpdateOn = GameEvents.OnPlayerPlayToGraveyard,
                Condition = card => card.Race == "Mech" || card.Race == "All",
            });
        }      
    }
}
