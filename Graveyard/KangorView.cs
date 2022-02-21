using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class KangorView : NormalView
    {
        private static ViewConfig _Config;
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new ViewConfig(Paladin.KangorsEndlessArmy)
            {
                Name = Strings.GetLocalized("Kangor"),
                Enabled = () => Settings.Default.KangorEnabled,
                Condition = card => card.Race == "Mech" || card.Race == "All",
            });
        }
        
        public static bool isValid()
        {
            return Core.Game.Player.PlayerCardList.FindIndex(card => Config.ShowOn.Contains(card.Id)) > -1;
        }

        public KangorView()
        {
            // Section Label
            Label.Text = Config.Name;
        }

        public bool Update(Card card)
        {
            return Config.Condition(card) && base.Update(card);
        }
    }
}
