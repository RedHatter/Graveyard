using Hearthstone_Deck_Tracker;
using Card = Hearthstone_Deck_Tracker.Hearthstone.Card;

namespace HDT.Plugins.Graveyard
{
    public class AntonidasView : MultiTurnView
    {
		public static bool isValid()
		{
			return Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == HearthDb.CardIds.Collectible.Mage.GrandMagusAntonidas) > -1;
		}

		public AntonidasView() 
			: base(Strings.GetLocalized("Antonidas"), 3)
		{
		}

		public override bool Update(Card card)
		{
			if (card.GetSchool() == School.Fire)
			{
				return base.Update(card);
			}
			return false;
		}
	}
}
