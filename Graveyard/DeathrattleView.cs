using Hearthstone_Deck_Tracker.API;
using System.Linq;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class DeathrattleView
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
                Name = "Deathrattle",
                Enabled = "DeathrattleEnabled",
                Condition = card => card.Mechanics.Contains("Deathrattle") && card.Id != Rogue.UnearthedRaptor,
                UpdateOn = GameEvents.OnPlayerPlayToGraveyard,
                CreateView = () => new NormalView(),
            });
        }
    }
}
