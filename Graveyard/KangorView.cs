using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;

namespace HDT.Plugins.Graveyard
{
    public class KangorView : NormalView
    {
        public static bool isValid()
        {
            return Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == HearthDb.CardIds.Collectible.Paladin.KangorsEndlessArmy) > -1;
        }

        public KangorView()
        {
            // Section Label
            Label.Text = Strings.GetLocalized("Kangor");
        }

        public bool Update(Card card)
        {
            return (card.Race == "Mech" || card.Id == HearthDb.CardIds.Collectible.Neutral.NightmareAmalgam) && base.Update(card);
        }
    }
}
