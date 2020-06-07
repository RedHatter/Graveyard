using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
	public class GuldanView : NormalView
	{
		private ChancesTracker _chances = new ChancesTracker();

		public static bool isValid()
		{
			return Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == Warlock.BloodreaverGuldan || card.Id == Warlock.KanrethadEbonlocke) > -1;
		}

		public GuldanView()
		{
			// Section Label
			Label.Text = Strings.GetLocalized("Guldan");
		}

		public bool Update(Card card)
		{
			var update = card.Race == "Demon" && base.Update(card);

			if (update)
				_chances.Update(card, Cards, View);

			return update;
		}
	}
}
