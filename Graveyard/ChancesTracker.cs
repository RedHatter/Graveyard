using System;
using System.Linq;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Collections.Generic;
using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Controls;
using Card = Hearthstone_Deck_Tracker.Hearthstone.Card;

namespace HDT.Plugins.Graveyard
{
	public class ChancesTracker
	{
		private Dictionary<Card, HearthstoneTextBlock> _chances = new Dictionary<Card, HearthstoneTextBlock>();

		public void Update(Card card, List<Card> Cards, AnimatedCardList View)
		{
			var count = (double)Cards.Aggregate(0, (total, c) => total + c.Count);
			for (var i = 0; i < Cards.Count(); i++)
			{
				if (!_chances.ContainsKey(Cards[i]))
				{
					var chance = new HearthstoneTextBlock();
					chance.FontSize = 18;
					chance.TextAlignment = TextAlignment.Left;
					var grid = (View.Items.GetItemAt(i) as UserControl).Content as Grid;
					grid.Width = 260;
					(grid.Children[0] as Hearthstone_Deck_Tracker.Controls.Card).HorizontalAlignment = HorizontalAlignment.Right;
					(grid.Children[1] as Rectangle).Width = 260;
					grid.Children.Add(chance);
					_chances.Add(Cards[i], chance);
				}

				_chances[Cards[i]].Text = $"{Math.Round(Cards[i].Count / count * 100)}%";
			}
		}
	}
}
