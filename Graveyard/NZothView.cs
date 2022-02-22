using Hearthstone_Deck_Tracker.Hearthstone;
using System.Linq;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class NZothView : NormalView
	{
		private static ViewConfig _Config;
		internal static ViewConfig Config
		{
			get => _Config ?? (_Config = new ViewConfig(Neutral.NzothTheCorruptor)
            {
				Name = Strings.GetLocalized("NZoth"),
				Enabled = () => Settings.Default.NZothEnabled,
				Condition = card => card.Mechanics.Contains("Deathrattle") && card.Id != Rogue.UnearthedRaptor,
			});
		}
		
		public NZothView()
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
