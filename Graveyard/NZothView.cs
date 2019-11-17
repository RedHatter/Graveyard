using System.Linq;
using HDT.Plugins.Graveyard.Resources;
using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;

namespace HDT.Plugins.Graveyard
{
	public class NZothView : NormalView
	{
		public static bool isValid()
		{
			return Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == HearthDb.CardIds.Collectible.Neutral.NzothTheCorruptorOG 
            || card.Id == HearthDb.CardIds.Collectible.Neutral.NzothTheCorruptorWILD_EVENT) > -1;
		}

		public NZothView()
		{
			// Section Label
			Label.Text = Strings.NZoth;
		}

		public bool Update(Card card)
		{
			return card.Mechanics.Contains("Deathrattle") && card.Id != HearthDb.CardIds.Collectible.Rogue.UnearthedRaptor && base.Update(card);
		}
	}
}
