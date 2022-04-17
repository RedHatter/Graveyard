using Hearthstone_Deck_Tracker.API;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class AntonidasView
    {
		private static MultiTurnView.ViewConfig _Config;
		internal static ViewConfig Config
        {
			get => _Config ?? (_Config = new MultiTurnView.ViewConfig(Mage.GrandMagusAntonidas)
			{
				Name = "Antonidas",
				Enabled = "AntonidasEnabled",
				CreateView = () => new MultiTurnView(3),
				UpdateOn = GameEvents.OnPlayerPlay,
				Condition = card => card.GetSchool() == School.Fire,				
			});
        }
	}
}
