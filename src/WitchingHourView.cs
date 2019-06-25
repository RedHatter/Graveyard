using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;

namespace HDT.Plugins.Graveyard
{
	public class WitchingHourView : NormalView
	{
		public static bool isValid()
		{
			return Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == HearthDb.CardIds.Collectible.Druid.WitchingHour) > -1;
		}

		public WitchingHourView()
		{
			// Section Label
			Label.Text = "Witching Hour";
		}

		new public bool Update(Card card)
		{
			return card.Race == "Beast" && base.Update(card);
		}
	}
}
