using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;
using System.Collections.Generic;

namespace HDT.Plugins.Graveyard
{
    public class WitchingHourView : NormalView
    {
        private ChancesTracker _chances = new ChancesTracker();

        internal static readonly List<string> ChooseOne = new List<string>
        {
            HearthDb.CardIds.Collectible.Druid.DruidOfTheSaber,
            HearthDb.CardIds.Collectible.Druid.DruidOfTheSwarm,
            HearthDb.CardIds.Collectible.Druid.DruidOfTheFlame,
            HearthDb.CardIds.Collectible.Druid.DruidOfTheScythe,
            HearthDb.CardIds.Collectible.Druid.WardruidLoti,
            HearthDb.CardIds.Collectible.Druid.Shellshifter,
            HearthDb.CardIds.Collectible.Druid.DruidOfTheClaw,
            HearthDb.CardIds.Collectible.Druid.DruidOfTheFang, // Honorary "choose one" for beast purposes
        };

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
            var update = (card.Race == "Beast" || card.Type == "Minion" && card.Race == null && ChooseOne.Contains(card.Id)) && base.Update(card);

            if (update)
                _chances.Update(card, Cards, View);

            return update;
        }
    }
}
