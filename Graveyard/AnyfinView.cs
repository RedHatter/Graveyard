using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace HDT.Plugins.Graveyard
{
	public class AnyfinView : NormalView
	{
		private HearthstoneTextBlock _dmg;
		private HearthstoneTextBlock _secondDmg;

		public static bool isValid()
		{
			return Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == HearthDb.CardIds.Collectible.Paladin.AnyfinCanHappen) > -1;
		}

		public AnyfinView()
		{
			// Section Label
			Label.Text = Strings.GetLocalized("Anyfin");

			// Damage Label
			_dmg = new HearthstoneTextBlock();
			_dmg.FontSize = 24;
			_dmg.TextAlignment = TextAlignment.Center;
			_dmg.Text = "0";
			Children.Add(_dmg);
			_dmg.Visibility = Visibility.Hidden;

			_secondDmg = new HearthstoneTextBlock();
			_secondDmg.FontSize = 24;
			_secondDmg.TextAlignment = TextAlignment.Center;
			_secondDmg.Text = "0";
			Children.Add(_secondDmg);
			_secondDmg.Visibility = Visibility.Hidden;
		}

		public bool Update(Card card)
		{
			if (card.Race != "Murloc" || !base.Update(card))
			{
				return false;
			}

			UpdateDamage();
			return true;
		}

		public void UpdateDamage()
		{
			// Update damage counter
			Range<int> damage = AnyfinCalculator.CalculateDamageDealt(Cards);
			_dmg.Text = damage.Minimum == damage.Maximum ? damage.Maximum.ToString() : $"{damage.Minimum} - {damage.Maximum}";
			_dmg.Visibility = Cards.Count > 0 ? Visibility.Visible : Visibility.Hidden;

			List<Card> moreCards = Cards.Select(c => c.Clone() as Card).ToList();
			moreCards.AddRange(Cards);
			Range<int> secondDamage = AnyfinCalculator.CalculateDamageDealt(moreCards);
			_secondDmg.Text = damage.Minimum == damage.Maximum ? secondDamage.Maximum.ToString() : $"{secondDamage.Minimum} - {secondDamage.Maximum}";
			_secondDmg.Visibility = Cards.Count > 0 ? Visibility.Visible : Visibility.Hidden;
		}
	}
}
