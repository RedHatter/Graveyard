using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;
using static HearthDb.CardIds.Collectible;
using System.Linq;

namespace HDT.Plugins.Graveyard
{
	public class ShudderwockView : NormalView
	{
		private static ViewConfig _Config;
		internal static ViewConfig Config
		{
			get => _Config ?? (_Config = new ViewConfig());
		}
		
		public static bool isValid()
		{
			return Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == Shaman.Shudderwock) > -1;
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
