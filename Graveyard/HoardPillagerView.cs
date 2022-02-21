using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class HoardPillagerView : NormalView
    {
        private static ViewConfig _Config;
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new ViewConfig());
        }
        
        private ChancesTracker _chances = new ChancesTracker();

        public static bool isValid()
        {
            return Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == Neutral.HoardPillager
            || card.Id == Neutral.RummagingKobold) > -1;
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
