using Hearthstone_Deck_Tracker.API;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class NZothGotDView
	{
		private static ViewConfig _Config;
		internal static ViewConfig Config
		{
			get => _Config ?? (_Config = new ViewConfig(Neutral.NzothGodOfTheDeep)
            {
				Name = Strings.GetLocalized("NZothGotD"),
				Enabled = () => Settings.Default.NZothGotDEnabled,
				CreateView = () => new ChancesView(),
				UpdateOn = GameEvents.OnPlayerPlayToGraveyard,
				Condition = card => card.Race != null,
			});
		}	
	}
}
