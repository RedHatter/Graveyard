using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;
using System;
using System.Collections.Generic;

namespace HDT.Plugins.Graveyard
{
    public class WitchingHourView : NormalView
    {
        private ChancesTracker _chances = new ChancesTracker();

        public static bool isValid()
        {
            return Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == HearthDb.CardIds.Collectible.Druid.WitchingHour) > -1;
        }

        public WitchingHourView()
        {
            // Section Label
            Label.Text = Strings.GetLocalized("WitchingHour");
        }

        public bool Update(Card card)
        {
            var update = (card.Race == "Beast" || card.Race == "All") && base.Update(card);

            if (update)
                _chances.Update(card, Cards, View);

            return update;
        }
    }
}
