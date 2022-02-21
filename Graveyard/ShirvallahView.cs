using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;
using Hearthstone_Deck_Tracker.Utility.Logging;
using System;
using System.Windows;
using static Hearthstone_Deck_Tracker.API.Core;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
	public class ShirvallahView : NormalView
	{
		private static ViewConfig _Config;
		internal static ViewConfig Config
		{
			get => _Config ?? (_Config = new ViewConfig(Paladin.ShirvallahTheTiger)
            {
				Name = Strings.GetLocalized("Shirvallah"),
				Enabled = () => Settings.Default.ShirvallahEnabled,
			});
		}
		
		public static bool isValid()
		{
			return Core.Game.Player.PlayerCardList.FindIndex(card => Config.ShowOn.Contains(card.Id)) > -1;
		}

		private readonly Card ShirvallahCard = Database.GetCardFromId(Paladin.ShirvallahTheTiger);

		public ShirvallahView()
		{
            Label.Text = Config.Name;
			Cards.Add(ShirvallahCard);
		}

        public bool Update(Card card)
		{
			if (card.Type == "Spell" && ActualCardCost(card) > 0)
			{
				ShirvallahCard.Cost = Math.Max(ShirvallahCard.Cost - ActualCardCost(card), 0);
				ShirvallahCard.Count = ShirvallahCard.Cost;			

				View.Update(Cards, false);

				Visibility = Visibility.Visible;

				return true;
			}
			return false;
		}

		private int ActualCardCost(Card card)
        {
            switch (card.Id)
            {
				case Paladin.LibramOfHope:
				case Paladin.LibramOfJudgment:
				case Paladin.LibramOfJustice:
				case Paladin.LibramOfWisdom:
					return card.Cost - Game.Player.LibramReductionCount;
                default:
					return card.Cost;
            }
        }
	}
}
