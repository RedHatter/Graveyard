using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;

namespace HDT.Plugins.Graveyard
{
	public class DragoncallerAlannaView : NormalView
	{
		public static bool isValid()
		{
			return Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == HearthDb.CardIds.Collectible.Mage.DragoncallerAlanna) > -1;
		}

		public DragoncallerAlannaView()
		{
            Label.Text = Strings.GetLocalized("Alanna");
		}

		public bool Update(Card card)
		{
			return card.Type == "Spell" && card.Cost >= 5 && base.Update(card, true);
		}
	}
}
