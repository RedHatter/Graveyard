using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;
using static HearthDb.CardIds.Collectible;
using System.Linq;

namespace HDT.Plugins.Graveyard
{
    public class NZothGotDView : NormalView
	{
		private static ViewConfig _Config;
		internal static ViewConfig Config
		{
			get => _Config ?? (_Config = new ViewConfig());
		}
		
		private ChancesTracker _chances = new ChancesTracker();

		public static bool isValid()
		{
			return Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == Neutral.NzothGodOfTheDeep) > -1;
		}

		public NZothGotDView()
		{
			// Section Label
			Label.Text = Strings.GetLocalized("NZothGotD");
		}

		public bool Update(Card card)
		{
			if (card.Race != null && base.Update(card))
			{
				_chances.Update(card, Cards, View);

				return true;
			}
			return false;
		}
	}
}
