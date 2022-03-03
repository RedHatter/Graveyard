using Hearthstone_Deck_Tracker.API;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class DragoncallerAlannaView
	{
		private static ViewConfig _Config;
		internal static ViewConfig Config
		{
			get => _Config ?? (_Config = new ViewConfig(Mage.DragoncallerAlanna)
            {
				Name = Strings.GetLocalized("Alanna"),
				Enabled = () => Settings.Default.DragoncallerAlannaEnabled,
				CreateView = () => new NormalView(),
				WatchFor = GameEvents.OnPlayerPlay,
				Condition = card => card.Type == "Spell" && card.Cost >= 5,
			});
		}
	}
}
