using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Hearthstone;
using System.Linq;
using static HearthDb.CardIds.Collectible;
using Core = Hearthstone_Deck_Tracker.Core;

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
				Condition = card => card.Type == "Minion",
				WatchFor = GameEvents.OnPlayerHandDiscard,
				CreateView = () => new NormalView(),
			});
		}

		private ChancesTracker _chances = new ChancesTracker();

		public static bool isValid()
		{
			return Core.Game.Player.PlayerCardList.FindIndex(card => Config.ShowOn.Contains(card.Id)) > -1;
		}

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
