using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;
using static HearthDb.CardIds.Collectible;
using System.Linq;

namespace HDT.Plugins.Graveyard
{
	public class NZothView : NormalView
	{
		private static ViewConfig _Config;
		internal static ViewConfig Config
		{
			get => _Config ?? (_Config = new ViewConfig());
		}
		
		public static bool isValid()
		{
			return Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == Neutral.NzothTheCorruptor) > -1;
		}

		public NZothView()
		{
			// Section Label
			Label.Text = Strings.GetLocalized("NZoth");
		}

		public bool Update(Card card)
		{
			return card.Mechanics.Contains("Deathrattle") && card.Id != Rogue.UnearthedRaptor && base.Update(card);
		}
	}
}
