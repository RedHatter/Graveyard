using Hearthstone_Deck_Tracker.Hearthstone;
using System.Linq;
using static HearthDb.CardIds.Collectible;

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
				Enabled = () => Settings.Default.ShudderwockEnabled,
				Condition = card => card.Mechanics.Contains("Battlecry"),
			});
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
