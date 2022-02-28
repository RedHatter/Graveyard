using Hearthstone_Deck_Tracker.API;
using System.Linq;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class ShudderwockView : NormalView
	{
		private static ViewConfig _Config;
		internal static ViewConfig Config
		{
			get => _Config ?? (_Config = new ViewConfig(Shaman.Shudderwock)
            {
				Name = Strings.GetLocalized("Shudderwock"),
				Enabled = () => Settings.Default.ShudderwockEnabled,
				CreateView = () => new ShudderwockView(),
				WatchFor = GameEvents.OnPlayerPlay,
				Condition = card => card.Mechanics.Contains("Battlecry"),
			});
		}	
	}
}
