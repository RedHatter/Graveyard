using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Hearthstone;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class GuldanView : NormalView
	{
		private static ViewConfig _Config;
		internal static ViewConfig Config
		{
			get => _Config ?? (_Config = new ViewConfig(Warlock.BloodreaverGuldan, Warlock.KanrethadEbonlocke)
            {
				Name = Strings.GetLocalized("Guldan"),
				Enabled = () => Settings.Default.GuldanEnabled,
				WatchFor = GameEvents.OnPlayerPlayToGraveyard,
				Condition = card => card.Race == "Demon",
			});
		}
		
		private ChancesTracker _chances = new ChancesTracker();

		public GuldanView()
		{
			// Section Label
			Label.Text = Config.Name;
		}

		public bool Update(Card card)
		{
			var update = Config.Condition(card) && base.Update(card);

			if (update)
				_chances.Update(card, Cards, View);

			return update;
		}
	}
}
