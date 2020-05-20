using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;

namespace HDT.Plugins.Graveyard
{
    public class TessGreymaneView : NormalView
    {
        public static bool isValid()
        {
            return Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == HearthDb.CardIds.Collectible.Rogue.TessGreymane) > -1;
        }

        public TessGreymaneView()
        {
            Label.Text = Strings.GetLocalized("TessGreymane");
        }

        public bool Update(Card card)
        {
            return !card.IsClass("Rogue") && !card.IsClass("Neutral") && base.Update(card, true);
        }
    }
}
