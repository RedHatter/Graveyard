using Hearthstone_Deck_Tracker.API;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    internal class MixtapeView
    {
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new ViewConfig(
                Rogue.Mixtape)
            {
                CreateView = () => new ChancesView(),
                UpdateOn = GameEvents.OnOpponentPlay,
                Condition = card => card.Type == "Spell",
            });
        }
        private static ViewConfig _Config;
    }
}
