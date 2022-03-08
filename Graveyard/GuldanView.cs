using Hearthstone_Deck_Tracker.API;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class GuldanView
	{
		private static ViewConfig _Config;
		internal static ViewConfig Config
		{
			get => _Config ?? (_Config = new ViewConfig(Warlock.BloodreaverGuldan, Warlock.KanrethadEbonlocke)
            {
				Name = Strings.GetLocalized("Guldan"),
				Enabled = () => Settings.Default.GuldanEnabled,
				CreateView = () => new ChancesView(),
				UpdateOn = GameEvents.OnPlayerPlayToGraveyard,
				Condition = card => card.Race == "Demon",
			});
		}
	}
}
