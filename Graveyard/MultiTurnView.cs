using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Card = Hearthstone_Deck_Tracker.Hearthstone.Card;

namespace HDT.Plugins.Graveyard
{
	public class MultiTurnView : ViewBase
    {
        internal class ViewConfig : Plugins.Graveyard.ViewConfig
        {
            public ViewConfig(params string[] showOn) : base(showOn)
            {

            }

            public override void RegisterView(ViewBase view, bool isDefault = false)
            {
                base.RegisterView(view, isDefault);
                var multiTurn = view as MultiTurnView;
                if (multiTurn != null)
                {
                    Plugin.Events.OnOpponentTurnStart.Register(multiTurn.TurnEnded);
                }
            }
        }

        public HearthstoneTextBlock Label;

        public List<TurnView> Views = new List<TurnView>();

        public List<List<Card>> CardLists = new List<List<Card>>();

        public readonly int Turns;

        public MultiTurnView(string title, int turns)
        {
            Turns = turns;

            Visibility = Visibility.Collapsed;
            Orientation = Orientation.Vertical;
            MinWidth = 250;

            // Turn Card Lists
            for (int i = 0; i < Turns + 1; i++)
            {
                Views.Add(new TurnView(i == 0 ? "#" : i.ToString()));
                Children.Add(Views[i]);
                CardLists.Add(new List<Card>());
            }
        }

        public override bool Update(Card card)
        {
            if (!Condition(card)) return false;

            CardLists[0].Add(card.Clone() as Card);
            Views[0].Cards.Update(CardLists[0], false);

            Visibility = Visibility.Visible;

            return true;
        }

        public async Task TurnEnded()
        {
            for (int i = Turns; i > 0; i--)
            {
                CardLists[i] = CardLists[i - 1];

            }
            CardLists[0] = new List<Card>();

            var tasks = new List<Task>();
            for (int i = 0; i < Turns + 1; i++)
            {
                tasks.Add(Views[i].Cards.UpdateAsync(CardLists[i], true));
            }

            await Task.WhenAll(tasks);
        }
    }

	public class TurnView : StackPanel
	{
		public HearthstoneTextBlock Title { get; private set; }
		public AnimatedCardList Cards { get; private set; }

		public TurnView(string name)
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
