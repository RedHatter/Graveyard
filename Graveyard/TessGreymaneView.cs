using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;

namespace HDT.Plugins.Graveyard
{
	public class TessGreymaneView : NormalView
	{
		public static bool isValid()
		{
			return Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == HearthDb.CardIds.Collectible.Rogue.TessGreymane) > -1;
		}

		public TessGreymaneView()
		{
			Label.Text = "Tess Greymane";
		}

		new public bool Update(Card card)
		{
			return !card.IsClass("Rogue") && !card.IsClass("Neutral") && base.Update(card, false, true);
		}

	}
}
