using Hearthstone_Deck_Tracker.Hearthstone;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class YShaarjView : NormalView
	{
		private static ViewConfig _Config;
		internal static ViewConfig Config
		{
			get => _Config ?? (_Config = new ViewConfig(Neutral.YshaarjTheDefiler)
			{
				Name = Strings.GetLocalized("YShaarj"),
				Enabled = () => Settings.Default.YShaarjEnabled,
				Condition = card => (card.EnglishText?.StartsWith("Corrupted") ?? false),
			});
		}	

		public YShaarjView()
		{
			Label.Text = Config.Name;
		}

		public bool Update(Card card)
		{
			return Config.Condition(card) && base.Update(card, true);
		}
	}
}
