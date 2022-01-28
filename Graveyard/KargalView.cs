using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDT.Plugins.Graveyard
{
    public class KargalView : NormalView
    {
		public static bool isValid()
		{
			return Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == HearthDb.CardIds.Collectible.Neutral.KargalBattlescar) > -1;
		}

		internal static readonly List<string> Posts = new List<string>
		{
			HearthDb.CardIds.Collectible.Neutral.CrossroadsWatchPost,
			HearthDb.CardIds.Collectible.Neutral.FarWatchPost,
			HearthDb.CardIds.Collectible.Neutral.MorshanWatchPost,
        };

		public KargalView()
		{
			Label.Text = Strings.GetLocalized("Kargal");
		}

		public bool Update(Card card)
		{
			return Posts.Contains(card.Id) && base.Update(card);
		}
	}
}
