using System.Linq;
using System.Windows;
using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;

namespace HDT.Plugins.Graveyard
{
	public class AnyfinView : NormalView
	{
		private HearthstoneTextBlock _dmg;

    public static bool isValid () {
      return Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == "LOE_026") > -1;
    }

    public AnyfinView () {
      // Section Label
      Label.Text = "Anyfin Can Happen";

      // Damage Label
      _dmg = new HearthstoneTextBlock();
      _dmg.FontSize = 24;
      _dmg.TextAlignment = TextAlignment.Center;
      _dmg.Text = "0";
      Children.Add(_dmg);
			_dmg.Visibility = Visibility.Hidden;
    }

		new public bool Update (Card card) {
			if (card.Race != "Murloc" || !base.Update(card)) return false;
			UpdateDamage();
      return true;
		}

		public void UpdateDamage () {
			// Update damage counter
			Range<int> damage = AnyfinCalculator.CalculateDamageDealt(Cards);

			_dmg.Text = damage.Minimum == damage.Maximum ?
				damage.Maximum.ToString() : $"{damage.Minimum} - {damage.Maximum}";
			_dmg.Visibility = Cards.Count > 0 ? Visibility.Visible : Visibility.Hidden;
		}
	}
}
