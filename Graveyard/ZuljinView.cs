using HDT.Plugins.Graveyard.Resources;
using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDT.Plugins.Graveyard
{
    public class ZuljinView : NormalView
    {
        public static bool isValid()
        {
            return Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == HearthDb.CardIds.Collectible.Hunter.Zuljin) > -1;
        }

        public ZuljinView()
        {
            Label.Text = Strings.Zuljin;
        }

        public bool Update(Card card)
        {
            return card.Type == "Spell" && base.Update(card, true);
        }
    }
}
