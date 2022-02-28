using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Card = Hearthstone_Deck_Tracker.Hearthstone.Card;

namespace HDT.Plugins.Graveyard
{
	public class NormalView : ViewBase
	{
		public List<Card> Cards;
		public HearthstoneTextBlock Label;
		public AnimatedCardList View;

		public NormalView()
		{
			Visibility = Visibility.Collapsed;
			Orientation = Orientation.Vertical;

			// Card View
			View = new AnimatedCardList();
			Children.Add(View);
			Cards = new List<Card>();
		}

		public override bool Update(Card card)
        {
			if (!Condition(card)) return false;

			var match = Cards.FirstOrDefault(c => c.Name == card.Name);
			if (match != null)
			{
				match.Count++;
			}
			else
			{
				Cards.Add(card.Clone() as Card);
			}
			View.Update(Cards, false);

			Visibility = Visibility.Visible;

			return true;
		}

		[Obsolete("Use update without isSpell parameter, Condition predicate should be updated instead")]
		public bool Update(Card card, bool isSpell)
		{		
			return Update(card);
		}
	}
}
