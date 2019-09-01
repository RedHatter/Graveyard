using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HDT.Plugins.Graveyard.Resources;
using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;

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
			Label.Text = Strings.Shudderwock;
		}

		public bool Update(Card card)
		{
			return card.Mechanics.Contains("Battlecry") && base.Update(card, true);
		}
	}
}
