using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Controls;
using Hearthstone_Deck_Tracker.Hearthstone;
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
    public class AntonidasView : StackPanel
    {
		public HearthstoneTextBlock Label;

		public List<TurnView> Views = new List<TurnView>();

		public List<List<Card>> CardLists = new List<List<Card>>();

		public static bool isValid()
		{
			return Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == HearthDb.CardIds.Collectible.Mage.GrandMagusAntonidas) > -1;
		}

		public AntonidasView()
		{
			// Title
			Label = new HearthstoneTextBlock
			{
				FontSize = 16,
				TextAlignment = TextAlignment.Center,
				Text = Strings.GetLocalized("Antonidas"),
				Margin = new Thickness(0, 20, 0, 0),
				Visibility = Visibility.Hidden,
			};
			Children.Add(Label);

			// Turn Card Lists
			for (int i = 0; i < 4; i++)
            {
				Views.Add(new TurnView(i == 0 ? "#" : i.ToString()));
				Children.Add(Views[i]);
				CardLists.Add(new List<Card>());
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
					Visibility = Visibility.Hidden,
					Text = name,
				};
				Children.Add(Title);

				Cards = new AnimatedCardList();
				Children.Add(Cards);
			}
        }

		public bool Update(Card card)
		{
			if (card.Type == "Spell")
			{
                HearthDb.Card dbCard;
                HearthDb.Cards.All.TryGetValue(card.Id, out dbCard);
                if (dbCard?.SpellSchool == 2)
                {
					CardLists[0].Add(card.Clone() as Card);
					Views[0].Cards.Update(CardLists[0], false);
					Views[0].Title.Visibility = Visibility.Visible;
					Label.Visibility = Visibility.Visible;
				}
				return true;
			}
			return false;
		}

		public async Task TurnEnded()
        {
			for (int i = 3; i > 0; i--)
            {
				CardLists[i] = CardLists[i - 1];

			}
			CardLists[0] = new List<Card>();

			var tasks = new List<Task>();
            for (int i = 0; i < 4; i++)
            {
				Views[i].Title.Visibility = Label.Visibility;
				tasks.Add(Views[i].Cards.UpdateAsync(CardLists[i], true));
			}

			await Task.WhenAll(tasks);
		}
	}
}
