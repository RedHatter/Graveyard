using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;

namespace HDT.Plugins.Graveyard
{
    public class YShaarjView : NormalView
	{
		public static bool isValid()
		{
			return Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == HearthDb.CardIds.Collectible.Neutral.YshaarjTheDefiler) > -1;
		}

		public YShaarjView()
		{
			Label.Text = Strings.GetLocalized("YShaarj");
		}

		public bool Update(Card card)
		{
			return (card.Text?.StartsWith("Corrupted") ?? false) && base.Update(card, true);
		}
	}
}
