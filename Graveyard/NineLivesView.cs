using System.Linq;
using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;

namespace HDT.Plugins.Graveyard
{
	public class NineLivesView : NormalView
	{
		public static bool isValid()
		{
			return Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == HearthDb.CardIds.Collectible.Hunter.NineLives) > -1;
		}

		public NineLivesView()
		{
			// Section Label
			Label.Text = "Nine Lives";
		}

		new public bool Update(Card card)
		{
			return card.Mechanics.Contains("Deathrattle") && base.Update(card);
		}
	}
}
