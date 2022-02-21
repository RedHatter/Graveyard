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
			get => _Config ?? (_Config = new ViewConfig(Neutral.KargalBattlescar)
            {
				Name = Strings.GetLocalized("Kargal"),
				Enabled = () => Settings.Default.KargalEnabled,
				Condition = card => Posts.Contains(card.Id),
			});
		}
		
		public static bool isValid()
		{
			return Core.Game.Player.PlayerCardList.FindIndex(card => Config.ShowOn.Contains(card.Id)) > -1;
		}

		internal static readonly List<string> Posts = new List<string>
		{
            Neutral.CrossroadsWatchPost,
            Neutral.FarWatchPost,
            Neutral.MorshanWatchPost,
        };

		public KargalView()
		{
			Label.Text = Config.Name;
		}

		public bool Update(Card card)
		{
			return Config.Condition(card) && base.Update(card);
		}
	}
}
