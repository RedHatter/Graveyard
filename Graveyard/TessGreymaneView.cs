using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class TessGreymaneView : NormalView
    {
        private static ViewConfig _Config;
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new ViewConfig(Rogue.TessGreymane, Rogue.ContrabandStash)
            {
                Name = Strings.GetLocalized("TessGreymane"),
                Condition = card => !card.IsClass("Rogue") && !card.IsClass("Neutral"),
            });
        }
        
        public static bool isValid()
        {
            return Core.Game.Player.PlayerCardList.FindIndex(card => 
                card.Id == Rogue.TessGreymane ||
                card.Id == Rogue.ContrabandStash
                ) > -1;
        }

        public TessGreymaneView()
        {
            Label.Text = Config.Name;
        }

        public bool Update(Card card)
        {
            return Config.Condition(card) && base.Update(card, true);
        }
    }
}
