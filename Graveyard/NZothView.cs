using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;
using System.Linq;

namespace HDT.Plugins.Graveyard
{
	public class NZothView : NormalView
	{
		public static bool isValid()
		{
			return Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == HearthDb.CardIds.Collectible.Neutral.NzothTheCorruptor) > -1;
		}

		public NZothView()
		{
			// Section Label
			Label.Text = Strings.GetLocalized("NZoth");
		}

		public bool Update(Card card)
		{
			return card.Mechanics.Contains("Deathrattle") && card.Id != HearthDb.CardIds.Collectible.Rogue.UnearthedRaptor && base.Update(card);
		}
	}
}
