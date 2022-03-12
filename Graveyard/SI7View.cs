using Hearthstone_Deck_Tracker.API;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
	internal class SI7View
    {
		private static ViewConfig _Config;
		internal static ViewConfig Config
		{
			get => _Config ?? (_Config = new ViewConfig(
                Rogue.Si7Assassin,
                Rogue.Si7Informant,
                Rogue.Si7Smuggler)
			{
				Name = "SI7",
				Enabled = () => Settings.Default.SI7Enabled,
				CreateView = () => new NormalView(),
				UpdateOn = GameEvents.OnPlayerPlay,
				Condition = card => card.Name.StartsWith("SI:7"),
			});
		}		
	}
}
