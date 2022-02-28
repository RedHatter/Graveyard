using Hearthstone_Deck_Tracker.API;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class NZothGotDView : ChancesView
	{
		private static ViewConfig _Config;
		internal static ViewConfig Config
		{
			get => _Config ?? (_Config = new ViewConfig(Neutral.NzothGodOfTheDeep)
            {
				Name = Strings.GetLocalized("NZothGotD"),
				Enabled = () => Settings.Default.NZothGotDEnabled,
				CreateView = () => new NZothGotDView(),
				WatchFor = GameEvents.OnPlayerPlayToGraveyard,
				Condition = card => card.Race != null,
			});
		}	
	}
}
