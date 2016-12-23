using System.Linq;
using System.Windows;
using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Utility.Logging;
using Hearthstone_Deck_Tracker.Hearthstone;

namespace HDT.Plugins.Graveyard
{
	public class NZothView : NormalView
	{
    public static bool isValid () {
      return Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == "OG_133") > -1;
    }

    public NZothView () {
      // Section Label
      Label.Text = "N'Zoth, the Corruptor";
    }

		new public bool Update (Card card) {
			return card.Mechanics.Contains("Deathrattle") && card.Id != "LOE_019" && base.Update(card);
		}
	}
}
