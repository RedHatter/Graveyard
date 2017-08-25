using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;

namespace HDT.Plugins.Graveyard
{
	public class DKGuldanView : NormalView
	{
		public static bool isValid()
		{
			return Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == HearthDb.CardIds.Collectible.Warlock.BloodreaverGuldan) > -1;
		}

		public DKGuldanView()
		{
			// Section Label
			Label.Text = "Gul'dan";
		}

		new public bool Update(Card card)
		{
			return (card.Race != null && card.Race.Contains("Demon")) && base.Update(card);
		}
	}
}
