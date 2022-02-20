using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Hearthstone;
using System.Linq;
using static HearthDb.CardIds.Collectible;
using Core = Hearthstone_Deck_Tracker.Core;

namespace HDT.Plugins.Graveyard
{
    public class DeathrattleView : NormalView
    {
        private static ViewConfig _Config;
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new ViewConfig(
                Priest.XyrellaTheDevout, 
                Warlock.TamsinsPhylactery,
                Priest.AmuletOfUndying,
                Rogue.CounterfeitBlade,
                Hunter.JewelOfNzoth,
                Neutral.Vectus,
                Hunter.NineLives,
                Neutral.DaUndatakah,
                Priest.TwilightsCall)
            {
                Name = Strings.GetLocalized("Deathrattle"),
                Enabled = () => Settings.Default.DeathrattleEnabled,
                Condition = card => card.Mechanics.Contains("Deathrattle") && card.Id != Rogue.UnearthedRaptor,
                WatchFor = GameEvents.OnPlayerPlayToGraveyard,
                CreateView = () => new NormalView(),
            });
        }

        public static bool isValid()
        {
            return Core.Game.Player.PlayerCardList.FindIndex(card => Config.ShowOn.Contains(card.Id)) > -1;
        }

        public DeathrattleView()
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
