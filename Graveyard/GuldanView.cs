using Hearthstone_Deck_Tracker.API;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class GuldanView
	{
		private static ViewConfig _Config;
		internal static ViewConfig Config
		{
			get => _Config ?? (_Config = new ViewConfig(
				Demonhunter.AllFelBreaksLoose,
				Warlock.BloodreaverGuldanCore,
				Warlock.KanrethadEbonlocke)
            {
				Name = "Guldan",
				Enabled = "GuldanEnabled",
				CreateView = () => new ChancesView(),
				UpdateOn = GameEvents.OnPlayerPlayToGraveyard,
				Condition = card => card.Race == "Demon",
			});
		}
	}
}
