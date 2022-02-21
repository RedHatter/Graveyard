using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;
using static HearthDb.CardIds.Collectible;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDT.Plugins.Graveyard
{
    public class KargalView : NormalView
    {
		private static ViewConfig _Config;
		internal static ViewConfig Config
		{
			get => _Config ?? (_Config = new ViewConfig());
		}
		
		public static bool isValid()
		{
			return Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == Neutral.KargalBattlescar) > -1;
		}

		internal static readonly List<string> Posts = new List<string>
		{
            Neutral.CrossroadsWatchPost,
            Neutral.FarWatchPost,
            Neutral.MorshanWatchPost,
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
