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
			get => _Config ?? (_Config = new ViewConfig(Shaman.Shudderwock)
            {
				Name = Strings.GetLocalized("Shudderwock"),
				Condition = card => card.Mechanics.Contains("Battlecry"),
			});
		}
		
		public static bool isValid()
		{
			return Core.Game.Player.PlayerCardList.FindIndex(card => Config.ShowOn.Contains(card.Id)) > -1;
		}

		public ShudderwockView()
		{
			Label.Text = Config.Name;
		}

		public bool Update(Card card)
		{
			return Config.Condition(card) && base.Update(card, true);
		}
	}
}
