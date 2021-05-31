using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;
using System.Linq;

namespace HDT.Plugins.Graveyard
{
	public class SaurfangView : NormalView
	{
		private ChancesTracker _chances = new ChancesTracker();

		public static bool isValid()
		{
			return Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == HearthDb.CardIds.Collectible.Warrior.OverlordSaurfang) > -1;
		}

		public SaurfangView()
		{
			Label.Text = Strings.GetLocalized("Saurfang");
		}

		public bool Update(Card card)
		{
			if (!((card.Text?.Contains("Frenzy:") ?? false) && base.Update(card)))
			{
				return false;
			}

			_chances.Update(card, Cards, View);

			return true;
		}
	}
}
