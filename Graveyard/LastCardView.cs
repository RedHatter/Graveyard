using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Controls;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Card = Hearthstone_Deck_Tracker.Hearthstone.Card;

namespace HDT.Plugins.Graveyard
{
    public class LastCardView : ViewBase
    {
        public readonly HearthstoneTextBlock Label;
        public readonly AnimatedCardList Cards;

        public LastCardView(string title = "Last Card")
        {
            Visibility = Visibility.Collapsed;
            Orientation = Orientation.Vertical;

            Cards = new AnimatedCardList();
            Children.Add(Cards);
        }

        public override bool Update(Card card)
        {
            if (!Condition(card)) return false;

            Cards.Update(new List<Card> { card.Clone() as Card }, false);

            Visibility = Visibility.Visible;

            return true;
        }
    }
}
