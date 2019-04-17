using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDT.Plugins.Graveyard
{
    public class DeathrattleView : NormalView
    {
        public static bool isValid()
        {
            return Core.Game.Player.PlayerCardList.FindIndex(card =>
                card.Id == HearthDb.CardIds.Collectible.Hunter.NineLives ||
                card.Id == HearthDb.CardIds.Collectible.Neutral.DaUndatakah ||
                card.Id == HearthDb.CardIds.Collectible.Priest.TwilightsCall                
                ) > -1;
        }

        public DeathrattleView()
        {
            // Section Label
            Label.Text = "Deathrattle";
        }

        public bool Update(Card card)
        {
            return card.Mechanics.Contains("Deathrattle") && card.Id != HearthDb.CardIds.Collectible.Rogue.UnearthedRaptor && base.Update(card);
        }
    }
}
