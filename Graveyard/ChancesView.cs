using Hearthstone_Deck_Tracker.Hearthstone;

namespace HDT.Plugins.Graveyard
{
    public class ChancesView : NormalView
    {
        private readonly ChancesTracker Chances = new ChancesTracker();

        public override bool Update(Card card)
        {
            if (base.Update(card))
            {
                Chances.Update(card, Cards, View);
                return true;
            }
            return false;
        }
    }
}
