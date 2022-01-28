using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static HDT.Plugins.Graveyard.AntonidasView;

namespace HDT.Plugins.Graveyard
{
    public class GrandFinaleView : MultiTurnView
    {
        public static bool isValid()
        {
            return Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == HearthDb.CardIds.Collectible.Mage.GrandFinale) > -1;
        }
        public GrandFinaleView() 
            : base(Strings.GetLocalized("GrandFinale"),1)
        {
        }

        public override bool Update(Card card)
        {
            if (card.Race == "Elemental" || card.Race == "All")
            {
                return base.Update(card);
            }
            return false;
        }
    }
}
