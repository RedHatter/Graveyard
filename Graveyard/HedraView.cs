using static HDT.Plugins.Graveyard.HoldingEffectView;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
	internal class HedraView
    {
		private static ViewConfig _Config;
		internal static ViewConfig Config
		{
			get => _Config ?? (_Config = new HoldingEffectConfig(Druid.HedraTheHeretic)
			{
				Name = "Hedra",				
				Condition = card => card.Type == "Spell",
			});
		}
	}
}
