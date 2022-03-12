using Hearthstone_Deck_Tracker.API;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class ElwynnBoarView
    {
        private static ViewConfig _Config;
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new ViewConfig(Neutral.ElwynnBoar)
            {
                Name = "ElwynnBoar",
                Enabled = () => Settings.Default.ElwynnBoarEnabled,
                ShowFirst = () => true,
                CreateView = () => new NormalView(),
                UpdateOn = GameEvents.OnPlayerPlayToGraveyard,
                Condition = card => card.Id == Neutral.ElwynnBoar,
            });
        }       

        public static bool IsAlwaysSeparate => Settings.Default.ElwynnBoarEnabled && Settings.Default.AlwaysBoarSeparately;
    }
}
