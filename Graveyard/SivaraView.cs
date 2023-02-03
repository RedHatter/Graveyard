using static HDT.Plugins.Graveyard.HoldingEffectView;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    internal class SivaraView : NormalView
    {
        private static ViewConfig _Config;
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new HoldingEffectConfig(Mage.CommanderSivara)
            {
                Name = "Sivara",
                CardLimit = 3,
                Condition = card => card.Type == "Spell",
            });
        }            
    }
}
