using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;
using System.Linq;

namespace HDT.Plugins.Graveyard
{
    public class NZothGotDView : NormalView
	{
		private ChancesTracker _chances = new ChancesTracker();

		public static bool isValid()
		{
			return Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == HearthDb.CardIds.Collectible.Neutral.NzothGodOfTheDeep) > -1;
		}

		public NZothGotDView()
		{
			// Section Label
			Label.Text = Strings.GetLocalized("NZothGotD");
		}

		public bool Update(Card card)
		{
			if ((card.Race != null || card.Type == "Minion" && WitchingHourView.ChooseOne.Contains(card.Id)) && base.Update(card))
			{
				_chances.Update(card, Cards, View);

				return true;
			}
			return false;
		}
	}
}
