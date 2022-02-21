using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;
using static HearthDb.CardIds.Collectible;
using System.Linq;

namespace HDT.Plugins.Graveyard
{
	public class HadronoxView : NormalView
	{
		private static ViewConfig _Config;
		internal static ViewConfig Config
		{
			get => _Config ?? (_Config = new ViewConfig());
		}
		
		public static bool isValid()
		{
			return Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == Druid.Hadronox) > -1;
		}

		public HadronoxView()
		{
			// Section Label
			Label.Text = Strings.GetLocalized("Hadronox");
		}

		public bool Update(Card card)
		{
			return card.Mechanics.Contains("Taunt") && base.Update(card);
		}
	}
}
