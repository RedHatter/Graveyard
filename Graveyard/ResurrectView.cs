using Hearthstone_Deck_Tracker.API;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class ResurrectView : ChancesView
	{
		private static ViewConfig _Config;
		internal static ViewConfig Config
		{
			get => _Config ?? (_Config = new ViewConfig(
				Neutral.Rally, 
				Priest.RaiseDead, 
				Priest.Psychopomp, 
				Neutral.BodyWrapper, 
				Priest.MassResurrection, 
				Priest.CatrinaMuerte,
				Priest.Resurrect,
				Priest.OnyxBishop,
				Priest.EternalServitude,
				Priest.LesserDiamondSpellstone,
				Neutral.Kazakus)
            {
				Name = Strings.GetLocalized("Resurrect"),
				Enabled = () => Settings.Default.ResurrectEnabled,
				CreateView = () => new ResurrectView(),
				WatchFor = GameEvents.OnPlayerPlayToGraveyard,
				Condition = card => card.Type == "Minion",
			});
		}
	}
}
