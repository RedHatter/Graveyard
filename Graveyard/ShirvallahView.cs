using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;

namespace HDT.Plugins.Graveyard
{
	public class ShirvallahView : NormalView
	{
		public static bool isValid()
		{
			return Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == HearthDb.CardIds.Collectible.Paladin.ShirvallahTheTiger) > -1;
		}

		public ShirvallahView()
		{
            Label.Text = Strings.GetLocalized("Shirvallah");
            int mana = 25;
		}

		public bool Update(Card card)
		{
			if (card.Type == "Spell" && card.Cost > 0 )
			{
				mana =- card.Cost;
				ShirvallahTheTiger = Database.GetCardFromId("TRL_300");
				Card ShirvallahTheTiger { get; };

				var match = Cards.FirstOrDefault(c => c.Name == "Shirvallah, the Tiger");
				if (match != null)
				{
					match.Count = mana;
				}
				else
				{
					Cards.Add(ShirvallahTheTiger as Card);
				}
				View.Update(Cards, false);

				Label.Visibility = Visibility.Visible;

				return true;
		}
	}
}
