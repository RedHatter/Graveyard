using Hearthstone_Deck_Tracker.API;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class YShaarjView
	{
		private static ViewConfig _Config;
		internal static ViewConfig Config
		{
			get => _Config ?? (_Config = new ViewConfig(Neutral.YshaarjTheDefiler)
			{
				Name = "YShaarj",
				Enabled = "YShaarjEnabled",
				CreateView = () => new NormalView(),
				UpdateOn = GameEvents.OnPlayerPlay,
				Condition = card => (card.EnglishText?.StartsWith("Corrupted") ?? false),
			});
		}	
	}
}
