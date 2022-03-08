using Hearthstone_Deck_Tracker.API;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class MulchmuncherView
    {
        private static ViewConfig _Config;
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new ViewConfig(Druid.Mulchmuncher)
            {
                Name = Strings.GetLocalized("Mulchmuncher"),
                Enabled = () => Settings.Default.MulchmuncherEnabled,
                ShowFirst = () => true,
                CreateView = () => new NormalView(),
                UpdateOn = GameEvents.OnPlayerPlayToGraveyard,
                Condition = card => card.Name == "Treant",
            });
        }       
    }
}
