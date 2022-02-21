using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;
using static HearthDb.CardIds.Collectible;
using System.Linq;

namespace HDT.Plugins.Graveyard
{
	public class SaurfangView : NormalView
	{
		private static ViewConfig _Config;
		internal static ViewConfig Config
		{
			get => _Config ?? (_Config = new ViewConfig(Warrior.OverlordSaurfang)
            {
				Name = Strings.GetLocalized("Saurfang"),
				Condition = card => card.EnglishText?.Contains("Frenzy:") ?? false
			});
		}
		
		private ChancesTracker _chances = new ChancesTracker();

		public static bool isValid()
		{
			return Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == Warrior.OverlordSaurfang) > -1;
		}

		public SaurfangView()
		{
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
