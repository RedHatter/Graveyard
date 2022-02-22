using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Hearthstone;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class MulchmuncherView : NormalView
    {
        private static ViewConfig _Config;
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new ViewConfig(Druid.Mulchmuncher)
            {
                Name = Strings.GetLocalized("Mulchmuncher"),
                Enabled = () => Settings.Default.MulchmuncherEnabled,
                WatchFor = GameEvents.OnPlayerPlayToGraveyard,
                Condition = card => card.Name == "Treant",
            });
        }
        
        public MulchmuncherView()
        {
            // Section Label
            Label.Text = Config.Name;
        }

        public bool Update(Card card)
        {
            return Config.Condition(card) && base.Update(card);
        }
    }
}
