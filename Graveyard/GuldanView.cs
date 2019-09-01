using HDT.Plugins.Graveyard.Resources;
using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;

namespace HDT.Plugins.Graveyard
{
	public class GuldanView : NormalView
	{
		public static bool isValid()
		{
			return Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == HearthDb.CardIds.Collectible.Warlock.BloodreaverGuldan) > -1;
		}

		public GuldanView()
		{
			// Section Label
			Label.Text = Strings.Guldan;
		}

		public bool Update(Card card)
		{
			return card.Race == "Demon" && base.Update(card);
		}
	}
}
