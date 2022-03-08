using Hearthstone_Deck_Tracker.API;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class DiscardView
	{
		private static ViewConfig _Config;
		internal static ViewConfig Config
		{
			get => _Config ?? (_Config = new ViewConfig(Warlock.CruelDinomancer)
			{
				Name = Strings.GetLocalized("DiscardTitle"),
				Enabled = () => Settings.Default.DiscardEnabled,
				CreateView = () => new ChancesView(),
				UpdateOn = GameEvents.OnPlayerHandDiscard,
				Condition = card => card.Type == "Minion",				
			});
		}
	}
}
