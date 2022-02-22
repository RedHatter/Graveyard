using Hearthstone_Deck_Tracker.Hearthstone;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class WitchingHourView : NormalView
    {
        private static ViewConfig _Config;
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new ViewConfig(Druid.WitchingHour, Hunter.RevivePet)
            {
                Name = Strings.GetLocalized("WitchingHour"),
                Enabled = () => Settings.Default.WitchingHourEnabled,
                Condition = card => card.Race == "Beast" || card.Race == "All",
            });
        }
        
        private ChancesTracker _chances = new ChancesTracker();

        public WitchingHourView()
        {
            // Section Label
            Label.Text = Config.Name;
        }

        public bool Update(Card card)
        {
            var update = Config.Condition(card) && base.Update(card);

            if (update)
                _chances.Update(card, Cards, View);

            return update;
        }
    }
}
