using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class MulchmuncherView : NormalView
    {
        private static ViewConfig _Config;
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new ViewConfig());
        }
        
        public static bool isValid()
        {
            return Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == Druid.Mulchmuncher) > -1;
        }

        public MulchmuncherView()
        {
            // Section Label
            Label.Text = Strings.GetLocalized("Mulchmuncher");
        }

        public bool Update(Card card)
        {
            return card.Name == "Treant" && base.Update(card);
        }
    }
}
