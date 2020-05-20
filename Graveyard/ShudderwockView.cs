using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;
using System.Linq;

namespace HDT.Plugins.Graveyard
{
	public class ShudderwockView : NormalView
	{
		public static bool isValid()
		{
			return Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == HearthDb.CardIds.Collectible.Shaman.Shudderwock) > -1;
		}

		public ShudderwockView()
		{
			Label.Text = Strings.GetLocalized("Shudderwock");
		}

		public bool Update(Card card)
		{
			return card.Mechanics.Contains("Battlecry") && base.Update(card, true);
		}
	}
}
