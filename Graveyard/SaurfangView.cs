using Hearthstone_Deck_Tracker.API;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class SaurfangView
	{
		private static ViewConfig _Config;
		internal static ViewConfig Config
		{
			get => _Config ?? (_Config = new ViewConfig(Warrior.OverlordSaurfang)
            {
				Name = "Saurfang",
				Enabled = "SaurfangEnabled",
				CreateView = () => new ChancesView(),
				UpdateOn = GameEvents.OnPlayerPlayToGraveyard,
				Condition = card => card.EnglishText?.Contains("Frenzy:") ?? false
			});
		}
	}
}
