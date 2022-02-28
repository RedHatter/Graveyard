using Hearthstone_Deck_Tracker.API;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class DiscardView : ChancesView
	{
		private static ViewConfig _Config;
		internal static ViewConfig Config
		{
			get => _Config ?? (_Config = new ViewConfig(Warlock.CruelDinomancer)
			{
				Name = Strings.GetLocalized("DiscardTitle"),
				Enabled = () => Settings.Default.DiscardEnabled,
				CreateView = () => new DiscardView(),
				WatchFor = GameEvents.OnPlayerHandDiscard,
				Condition = card => card.Type == "Minion",				
			});
		}
	}
}
