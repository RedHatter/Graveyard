using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Hearthstone;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
	public class AnyfinView : NormalView
	{
		private static AnyfinViewConfig _Config;
		internal static ViewConfig Config
		{
			get => _Config ?? (_Config = new AnyfinViewConfig(Paladin.AnyfinCanHappen)
			{
				Name = "Anyfin",
				Enabled = "AnyfinEnabled",
				UpdateOn = GameEvents.OnPlayerPlayToGraveyard,
				CreateView = () => new AnyfinView(),
				Condition = card => card.IsMurloc(),
			});
		}				

		internal class AnyfinViewConfig : ViewConfig
        {
            public AnyfinViewConfig(params string[] showOn) : base(showOn)
            {

            }

            public override void RegisterView(ViewBase view, bool isDefault = false)
            {
                base.RegisterView(view, isDefault);
				RegisterForCardEvent(GameEvents.OnOpponentPlayToGraveyard, view.Update, isDefault);
			}
        }

		private HearthstoneTextBlock _dmg;
		private HearthstoneTextBlock _secondDmg;

		public AnyfinView()
		{
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

		public override bool Update(Card card)
		{
			if (base.Update(card))
			{
				UpdateDamage();
				return true; 
			}
			return false;
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
