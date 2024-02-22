using HearthDb.Enums;
using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Controls;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using static HearthDb.CardIds.Collectible;
using Card = Hearthstone_Deck_Tracker.Hearthstone.Card;

namespace HDT.Plugins.Graveyard
{
    public class TyrView : ViewBase
    {
        private static ViewConfig _Config;
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new ViewConfig(
                Paladin.Tyr)
            {
                CreateView = () => new TyrView(),
                UpdateOn = GameEvents.OnPlayerPlayToGraveyard,
                Condition = card => card.Type == "Minion"
                    && card.Attack >= 2 
                    && card.Attack <= 4,
            });
        }

        public List<TitledListView> Views = new List<TitledListView>();

        public List<List<Card>> CardLists = new List<List<Card>>();

        public TyrView()
        {
            Visibility = Visibility.Collapsed;
            Orientation = Orientation.Vertical;
            MinWidth = 250;

            for (int i = 0; i < 3; i++)
            {
                Views.Add(new TitledListView((i + 2).ToString()));
                Children.Add(Views[i]);
                CardLists.Add(new List<Card>());
            }
        }

        public override bool Update(Card card)
        {
            if (!Condition(card)) return false;

            var index = card.Attack - 2;

            CardLists[index].Add(card.Clone() as Card);
            Views[index].Cards.Update(CardLists[index], false);

            Visibility = Visibility.Visible;

            return true;
        }

        public class TitledListView : StackPanel
        {
            public HearthstoneTextBlock Title { get; private set; }
            public AnimatedCardList Cards { get; private set; }

            public TitledListView(string name)
            {
                Orientation = Orientation.Horizontal;

                Title = new HearthstoneTextBlock
                {
                    FontSize = 24,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top,
                    MinHeight = 30,
                    MinWidth = 30,
                    Text = name,
                };
                Children.Add(Title);

                Cards = new AnimatedCardList();
                Children.Add(Cards);
            }
        }
    }
}
