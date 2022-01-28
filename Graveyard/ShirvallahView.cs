using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;
using Hearthstone_Deck_Tracker.Utility.Logging;
using System;
using System.Windows;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
	public class ShirvallahView : NormalView
	{
        public static bool isValid()
		{
			return Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == Paladin.ShirvallahTheTiger) > -1;
		}

		private readonly Card ShirvallahCard = Database.GetCardFromId(Paladin.ShirvallahTheTiger);

		public ShirvallahView()
		{
            Label.Text = Strings.GetLocalized("Shirvallah");
		}

        private void ShirvallahCard_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
			Log.Info($"Shirvallah changed {e.PropertyName}");
        }

        public bool Update(Card card)
		{
			if (card.Type == "Spell" && card.Cost > 0)
			{
				if (Cards.Count == 0)
				{
					Cards.Add(ShirvallahCard);
				}

				ShirvallahCard.Cost = Math.Max(ShirvallahCard.Cost - card.Cost, 0);
				ShirvallahCard.Count = ShirvallahCard.Cost;			

				View.Update(Cards, false);

				Label.Visibility = Visibility.Visible;

				return true;
			}
			return false;
		}
	}
}
