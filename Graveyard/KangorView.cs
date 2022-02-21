using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class KangorView : NormalView
    {
        private static ViewConfig _Config;
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new ViewConfig());
        }
        
        public static bool isValid()
        {
            return Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == Paladin.KangorsEndlessArmy) > -1;
        }

        public KangorView()
        {
            // Section Label
            Label.Text = Strings.GetLocalized("Kangor");
        }

        public bool Update(Card card)
        {
            return (card.Race == "Mech" || card.Race == "All") && base.Update(card);
        }
    }
}
