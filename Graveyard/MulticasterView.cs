using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Card = Hearthstone_Deck_Tracker.Hearthstone.Card;
using DbCard = HearthDb.Card;

namespace HDT.Plugins.Graveyard
{
    public class MulticasterView : StackPanel
    {
        public static bool IsValid()
        {
            return Core.Game.Player.PlayerCardList.FindIndex(card => 
                card.Id == HearthDb.CardIds.Collectible.Mage.MagisterDawngrasp ||
                card.Id == HearthDb.CardIds.Collectible.Neutral.Multicaster
                ) > -1;
        }

        public readonly HearthstoneTextBlock Label;
        public readonly AnimatedCardList Cards;

        public Dictionary<int, Card> SchoolList = new Dictionary<int, Card>();

        public MulticasterView()
        {
            Visibility = Visibility.Collapsed;
            Orientation = Orientation.Vertical;

            Label = new HearthstoneTextBlock
            {
                FontSize = 16,
                TextAlignment = TextAlignment.Center,
                Text = Strings.GetLocalized("Multicaster"),
                Margin = new Thickness(0, 20, 0, 0),
            };
            Children.Add(Label);

            Cards = new AnimatedCardList();
            Children.Add(Cards);
        }

        public virtual bool Update(Card card)
        {
            if (card.Type == "Spell")
            {
                HearthDb.Cards.All.TryGetValue(card.Id, out DbCard dbCard);

                if (dbCard?.SpellSchool > 0)
                {
                    if (SchoolList.ContainsKey(dbCard.SpellSchool))
                    {
                        SchoolList[dbCard.SpellSchool] = card.Clone() as Card;
                    }
                    else
                    {
                        SchoolList.Add(dbCard.SpellSchool, card.Clone() as Card);
                        
                    }

                    Cards.Update(SchoolList.Values.ToList(), true);

                    Visibility = Visibility.Visible;

                    return true;
                }
            }
            return false;
        }
    }
}
