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
            get => _Config ?? (_Config = new ViewConfig(Neutral.HoardPillager, Neutral.RummagingKobold)
            {
                Name = Strings.GetLocalized("HoardPillager"),
                Enabled = () => Settings.Default.HoardPillagerEnabled,
                Condition = card => card.Type == "Weapon"
            });
        }
        
        private ChancesTracker _chances = new ChancesTracker();

        public HoardPillagerView()
        {
            Label.Text = Config.Name;
        }

        public bool Update(Card card)
        {
            var update = Config.Condition(card) && base.Update(card, true);

            if (update)
                _chances.Update(card, Cards, View);

            return update;
        }
    }
}
