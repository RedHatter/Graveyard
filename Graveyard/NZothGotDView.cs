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
			get => _Config ?? (_Config = new ViewConfig(Neutral.NzothGodOfTheDeep)
            {
				Name = Strings.GetLocalized("NZothGotD"),
				Condition = card => card.Race != null,
			});
		}
		
		private ChancesTracker _chances = new ChancesTracker();

		public static bool isValid()
		{
			return Core.Game.Player.PlayerCardList.FindIndex(card => Config.ShowOn.Contains(card.Id)) > -1;
		}

		public NZothGotDView()
		{
			// Section Label
			Label.Text = Config.Name;
		}

		public bool Update(Card card)
		{
			if (Config.Condition(card) && base.Update(card))
			{
				_chances.Update(card, Cards, View);

				return true;
			}
			return false;
		}
	}
}
