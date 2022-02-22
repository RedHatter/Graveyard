using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Hearthstone;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class DiscardView : NormalView
	{
		private static ViewConfig _Config;
		internal static ViewConfig Config
		{
			get => _Config ?? (_Config = new ViewConfig(Warlock.CruelDinomancer)
			{
				Name = Strings.GetLocalized("DiscardTitle"),
				Enabled = () => Settings.Default.DiscardEnabled,
				WatchFor = GameEvents.OnPlayerHandDiscard,
				Condition = card => card.Type == "Minion",
				CreateView = () => new NormalView(),
			});
		}

		private ChancesTracker _chances = new ChancesTracker();

		public DiscardView()
		{
			// Section Label
			Label.Text = Config.Name;
		}

		public bool Update(Card card)
		{
			if (Config.Condition(card) && base.Update(card))
			{
				// Silverware Golem and Clutchmother Zaras are still counted as discarded, even when their effects trigger
				_chances.Update(card, Cards, View);
				return true;
			}
			return false;
		}
	}
}
