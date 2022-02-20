using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Hearthstone;
using System.Linq;
using static HearthDb.CardIds.Collectible;
using Core = Hearthstone_Deck_Tracker.Core;

namespace HDT.Plugins.Graveyard
{
    public class CavernsView : NormalView
    {
        private static ViewConfig _Config;
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new ViewConfig(Rogue.TheCavernsBelow)
            {
                Name = Strings.GetLocalized("Caverns"),
                Enabled = () => Settings.Default.CavernsEnabled,
                Condition = card => card.Type == "Minion",
                WatchFor = GameEvents.OnPlayerPlay,
                CreateView = () => new NormalView(),
            });
        }
        public static bool isValid()
        {
            return Core.Game.Player.PlayerCardList.FindIndex(card => Config.ShowOn.Contains(card.Id)) > -1;
        }

        public CavernsView()
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
