using Hearthstone_Deck_Tracker.API;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class SoulwardenView : ChancesView
    {
        private static ViewConfig _Config;
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new ViewConfig(Warlock.Soulwarden)
            {
                Name = Strings.GetLocalized("Soulwarden"),
                Enabled = () => Settings.Default.SoulwardenEnabled,
                CreateView = () => new SoulwardenView(),
                WatchFor = GameEvents.OnPlayerHandDiscard,
                Condition = card => true,
            });
        }
    }
}
