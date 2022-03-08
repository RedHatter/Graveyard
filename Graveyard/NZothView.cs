using Hearthstone_Deck_Tracker.API;
using System.Linq;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class NZothView
	{
		private static ViewConfig _Config;
		internal static ViewConfig Config
		{
			get => _Config ?? (_Config = new ViewConfig(Neutral.NzothTheCorruptor)
            {
				Name = Strings.GetLocalized("NZoth"),
				Enabled = () => Settings.Default.NZothEnabled,
				CreateView = () => new NormalView(),
				UpdateOn = GameEvents.OnPlayerPlayToGraveyard,
				Condition = card => card.Mechanics.Contains("Deathrattle") && card.Id != Rogue.UnearthedRaptor,
			});
		}
	
	}
}
