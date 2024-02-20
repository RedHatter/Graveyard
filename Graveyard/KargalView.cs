using Hearthstone_Deck_Tracker.API;
using System.Collections.Generic;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class KargalView
    {
		private static ViewConfig _Config;
		internal static ViewConfig Config
		{
			get => _Config ?? (_Config = new ViewConfig(Neutral.KargalBattlescar)
            {
				Name = "Kargal",
				Enabled = "KargalEnabled",
				CreateView = () => new NormalView(),
				UpdateOn = GameEvents.OnPlayerPlay,
				Condition = card => Posts.Contains(card.Id),
			});
		}
		
		internal static readonly List<string> Posts = new List<string>
		{
            Neutral.CrossroadsWatchPost,
            Neutral.FarWatchPost,
            Neutral.MorshanWatchPost,
        };
	}
}
