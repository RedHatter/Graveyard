using System;
using System.Linq;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Collections.Generic;
using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Controls;
using Card = Hearthstone_Deck_Tracker.Hearthstone.Card;
using Hearthstone_Deck_Tracker.Utility.Logging;

namespace HDT.Plugins.Graveyard
{
	public class ChancesTracker
	{
        private readonly Dictionary<Card, HearthstoneTextBlock> Chances = new Dictionary<Card, HearthstoneTextBlock>();

		public void Update(Card card, List<Card> Cards, AnimatedCardList View)
		{
			var count = (double)Cards.Aggregate(0, (total, c) => total + c.Count);
			for (var i = 0; i < Cards.Count(); i++)
			{
				if (!Chances.ContainsKey(Cards[i]))
				{
                    var chance = new HearthstoneTextBlock
                    {
                        FontSize = 18,
                        TextAlignment = TextAlignment.Left
                    };
					if (View.Items.GetItemAt(i) is UserControl control && control.Content is Grid grid)
					{
						grid.Width = 260;
						if (grid.Children[0] is Hearthstone_Deck_Tracker.Controls.Card cardControl)
							cardControl.HorizontalAlignment = HorizontalAlignment.Right; 
						else
                            Log.Warn("Expected Hearthstone_Deck_Tracker.Controls.Card, check AnimatedCard for layout changes");
						if (grid.Children[2] is Rectangle rectangle)
                            rectangle.Width = 260;
						else
                            Log.Warn("Expected Rectangle, check AnimatedCard for layout changes");
                        grid.Children.Add(chance); 
					}
					else
					{
						Log.Warn("Expected UserControl and Grid, check AnimatedCard for layout changes");
					}
					Chances.Add(Cards[i], chance);
				}

				Chances[Cards[i]].Text = $"{Math.Round(Cards[i].Count / count * 100)}%";
			}
		}
	}
}
