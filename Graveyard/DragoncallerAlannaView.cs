using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
	public class DragoncallerAlannaView : NormalView
	{
		private static ViewConfig _Config;
		internal static ViewConfig Config
		{
			get => _Config ?? (_Config = new ViewConfig(Mage.DragoncallerAlanna)
            {
				Name = Strings.GetLocalized("Alanna"),
				Enabled = () => Settings.Default.DragoncallerAlannaEnabled,
				Condition = card => card.Type == "Spell" && card.Cost >= 5,
			});
		}

		public static bool isValid()
		{
			return Core.Game.Player.PlayerCardList.FindIndex(card => Config.ShowOn.Contains(card.Id)) > -1;
		}

		public DragoncallerAlannaView()
		{
            Label.Text = Config.Name;
		}

		public bool Update(Card card)
		{
			return Config.Condition(card) && base.Update(card, true);
		}
	}
}
