using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;

namespace HDT.Plugins.Graveyard
{
    public class CavernsView : NormalView
    {
        public static bool isValid()
        {
            return Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == HearthDb.CardIds.Collectible.Rogue.TheCavernsBelow) > -1;
        }

        public CavernsView()
        {
            // Section Label
            Label.Text = Strings.GetLocalized("Caverns");
        }

        public bool Update(Card card)
        {
            return base.Update(card);
        }
    }
}
