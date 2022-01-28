using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDT.Plugins.Graveyard
{
    public class ElwynnBoarView : NormalView
    {
        public static bool isValid()
        {
            return Core.Game.Player.PlayerCardList.FindIndex(card =>
                card.Id == HearthDb.CardIds.Collectible.Neutral.ElwynnBoar) > -1;
        }

        public static bool IsAlwaysSeparate => Settings.Default.ElwynnBoarEnabled && Settings.Default.AlwaysBoarSeparately;

        public ElwynnBoarView()
        {
            // Section Label
            Label.Text = Strings.GetLocalized("ElwynnBoar");
        }

        public bool Update(Card card)
        {
            return card.Id == HearthDb.CardIds.Collectible.Neutral.ElwynnBoar && base.Update(card);
        }
    }
}
