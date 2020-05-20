using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;

namespace HDT.Plugins.Graveyard
{
	public class DiscardView : NormalView
	{
		private ChancesTracker _chances = new ChancesTracker();

		public static bool isValid()
		{
			return Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == HearthDb.CardIds.Collectible.Warlock.CruelDinomancer) > -1;
		}

		public DiscardView()
		{
			// Section Label
			Label.Text = Strings.GetLocalized("DiscardTitle");
		}

		public bool Update(Card card)
		{
			if (!base.Update(card))
			{
				return false;
			}

			// Silverware Golem and Clutchmother Zaras are still counted as discarded, even when their effects trigger
			_chances.Update(card, Cards, View);

			return true;
		}
	}
}
