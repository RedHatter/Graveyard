using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
	public class RallyView : NormalView
	{
		private static ViewConfig _Config;
		internal static ViewConfig Config
		{
			get => _Config ?? (_Config = new ViewConfig(Neutral.Rally)
            {
				Name = Strings.GetLocalized("Rally"),
				Enabled = () => Settings.Default.RallyEnabled,
				Condition = card => card.Type == "Minion" && card.Cost >= 1 && card.Cost <= 3,
			});
		}
		
		private ChancesTracker _chances = new ChancesTracker();

		public static bool isValid()
		{
			return Core.Game.Player.PlayerCardList.FindIndex(card => Config.ShowOn.Contains(card.Id)) > -1;
		}

		public static bool IsAlwaysSeparate => Settings.Default.RallyEnabled && Settings.Default.AlwaysRallySeparately;

		public RallyView()
		{
			// Section Label
			Label.Text = Config.Name;
		}

		public bool Update(Card card)
		{
			var update = Config.Condition(card) && base.Update(card);

			if (update)
				_chances.Update(card, Cards, View);

			return update;
		}
	}
}
