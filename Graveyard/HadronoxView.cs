using Hearthstone_Deck_Tracker.Hearthstone;
using System.Linq;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class HadronoxView : NormalView
	{
		private static ViewConfig _Config;
		internal static ViewConfig Config
		{
			get => _Config ?? (_Config = new ViewConfig(Druid.Hadronox)
            {
				Name = Strings.GetLocalized("Hadronox"),
				Enabled = () => Settings.Default.HadronoxEnabled,
				Condition = card => card.Mechanics.Contains("Taunt"),
			});
		}
		
		public HadronoxView()
		{
			// Section Label
			Label.Text = Config.Name;
		}

		public bool Update(Card card)
		{
			return Config.Condition(card) && base.Update(card);
		}
	}
}
