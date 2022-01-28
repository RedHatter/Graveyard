using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Controls;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Card = Hearthstone_Deck_Tracker.Hearthstone.Card;

namespace HDT.Plugins.Graveyard
{
    public class LastCardView : StackPanel
    {
        public readonly HearthstoneTextBlock Label;
        public readonly AnimatedCardList Cards;

        public LastCardView(string title = "Last Card")
        {
            Visibility = Visibility.Collapsed;
            Orientation = Orientation.Vertical;

            // Title
            Label = new HearthstoneTextBlock
            {
                FontSize = 16,
                TextAlignment = TextAlignment.Center,
                Text = title,
                Margin = new Thickness(0, 20, 0, 0),
            };
            Children.Add(Label);

            Cards = new AnimatedCardList();
            Children.Add(Cards);
        }

        public virtual bool Update(Card card)
        {
            Cards.Update(new List<Card> { card.Clone() as Card }, false);

            Visibility = Visibility.Visible;

            return true;
        }
    }
}
