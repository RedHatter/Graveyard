using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;

namespace HDT.Plugins.Graveyard
{
    public class HoardPillagerView : NormalView
    {
        private ChancesTracker _chances = new ChancesTracker();

        public static bool isValid()
        {
            return Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == HearthDb.CardIds.Collectible.Neutral.HoardPillager
            || card.Id == HearthDb.CardIds.Collectible.Neutral.RummagingKobold) > -1;
        }

        public HoardPillagerView()
        {
            Label.Text = Strings.GetLocalized("HoardPillager");
        }

        public bool Update(Card card)
        {
            var update = card.Type == "Weapon"  && base.Update(card, true);

            if (update)
                _chances.Update(card, Cards, View);

            return update;
        }
    }
}
