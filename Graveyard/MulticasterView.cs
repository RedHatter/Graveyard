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
    public class MulticasterView : ViewBase
    {
        private static ViewConfig _Config;
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new ViewConfig(Mage.MagisterDawngrasp, Neutral.Multicaster)
            {
                Name = "Multicaster",
                Enabled = () => Settings.Default.MulticasterEnabled,
                CreateView = () => new MulticasterView(),
                UpdateOn = GameEvents.OnPlayerPlay,
            });
        }       

        public readonly HearthstoneTextBlock Label;
        public readonly AnimatedCardList Cards;

        public Dictionary<School, Card> SchoolList = new Dictionary<School, Card>();

        public MulticasterView()
        {
            Visibility = Visibility.Collapsed;
            Orientation = Orientation.Vertical;

            Cards = new AnimatedCardList();
            Children.Add(Cards);
        }

        public override bool Update(Card card)
        {
            var school = card.GetSchool();

            if (school > School.General)
            {
                if (SchoolList.ContainsKey(school))
                {
                    SchoolList[school] = card.Clone() as Card;
                }
                else
                {
                    SchoolList.Add(school, card.Clone() as Card);                        
                }

                Cards.Update(SchoolList.Values.ToList(), true);

                Visibility = Visibility.Visible;

                return true;
            }
            return false;
        }
    }
}
