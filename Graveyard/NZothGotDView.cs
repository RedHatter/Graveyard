using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;
using static HearthDb.CardIds.Collectible;
using System.Linq;
using Hearthstone_Deck_Tracker.API;

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
				Enabled = () => Settings.Default.NZothGotDEnabled,
				WatchFor = GameEvents.OnPlayerPlayToGraveyard,
				Condition = card => card.Race != null,
			});
		}
		
		private ChancesTracker _chances = new ChancesTracker();

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
