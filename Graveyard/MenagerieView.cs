using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static HearthDb.CardIds.Collectible;
using Card = Hearthstone_Deck_Tracker.Hearthstone.Card;

namespace HDT.Plugins.Graveyard
{
    internal class MenagerieView : ViewBase
    {
        private static ViewConfig _Config;
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new ViewConfig(Warrior.PowerSlider, Neutral.TheOneAmalgamBand)
            {
                Name = "Menagerie",
                CreateView = () => new MenagerieView(),
                UpdateOn = GameEvents.OnPlayerPlay,
            });
        }

        public readonly HearthstoneTextBlock Label;
        public readonly AnimatedCardList Cards;

        public Dictionary<string, Card> RaceList = new Dictionary<string, Card>();
        public int AllCount = 0;

        public MenagerieView()
        {
            Visibility = Visibility.Collapsed;
            Orientation = Orientation.Vertical;

            Cards = new AnimatedCardList();
            Children.Add(Cards);
        }

        public override bool Update(Card card)
        {
            var race = card.Race;

            if (string.IsNullOrEmpty(race)) return false;

            if (race == "All")
            {
                AllCount += 1;
                race = $"All{AllCount}";
            }

            if (RaceList.ContainsKey(race))
            {
                RaceList[race] = card.Clone() as Card;
            }
            else
            {
                RaceList.Add(race, card.Clone() as Card);
            }

            Cards.Update(RaceList.Values.ToList(), true);

            Visibility = Visibility.Visible;

            return true;
        }
    }
}
