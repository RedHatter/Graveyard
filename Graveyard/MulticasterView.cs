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

        public Dictionary<SpellSchool, Card> SchoolList = new Dictionary<SpellSchool, Card>();

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
            var spellSchool = card.GetSpellSchool();

            if (spellSchool > SpellSchool.General)
            {
                if (SchoolList.ContainsKey(spellSchool))
                {
                    SchoolList[spellSchool] = card.Clone() as Card;
                }
                else
                {
                    SchoolList.Add(spellSchool, card.Clone() as Card);
                        
                }

                Cards.Update(SchoolList.Values.ToList(), true);

                Visibility = Visibility.Visible;

                return true;
            }
            return false;
        }
    }
}
