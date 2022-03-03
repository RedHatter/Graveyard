using Hearthstone_Deck_Tracker.API;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class AntonidasView
    {
		private static ViewConfig _Config;
		internal static ViewConfig Config
        {
			get => _Config ?? (_Config = new ViewConfig(Mage.GrandMagusAntonidas)
			{
				Name = Strings.GetLocalized("Antonidas"),
				Enabled = () => Settings.Default.AntonidasEnabled,
				CreateView = () => new MultiTurnView(Config.Name, 3),
				WatchFor = GameEvents.OnPlayerPlay,
				Condition = card => card.GetSchool() == School.Fire,				
			});
        }
	}
}
