using System.Linq;
using HDT.Plugins.Graveyard.Resources;
using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;

namespace HDT.Plugins.Graveyard
{
	public class HadronoxView : NormalView
	{
		public static bool isValid()
		{
			return Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == HearthDb.CardIds.Collectible.Druid.Hadronox) > -1;
		}

		public HadronoxView()
		{
			// Section Label
			Label.Text = Strings.Hadronox;
		}

		public bool Update(Card card)
		{
			return card.Mechanics.Contains("Taunt") && base.Update(card);
		}
	}
}
