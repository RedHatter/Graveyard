using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;
using MahApps.Metro.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WPFLocalizeExtension.Engine;

namespace HDT.Plugins.Graveyard
{
	public partial class SettingsView : Grid
	{
		private static Flyout _flyout;

		public static Flyout Flyout
		{
			get
			{
				if (_flyout == null)
				{
					_flyout = CreateSettingsFlyout();
				}
				return _flyout;
			}
		}

		private static Flyout CreateSettingsFlyout()
		{
			var settings = new Flyout();
			settings.Position = Position.Left;
			Panel.SetZIndex(settings, 100);
			settings.Header = Strings.GetLocalized("SettingsTitle");
			settings.Content = new SettingsView();
			Core.MainWindow.Flyouts.Items.Add(settings);
			return settings;
		}

		public SettingsView()
		{
			InitializeComponent();

			var cards = new ObservableCollection<SettingsCard>(
				ViewConfigCards.Instance.Enumerable
				.Select(v => new SettingsCard(v)));

			CardView.ItemsSource = cards;

			var view = (ListCollectionView)CollectionViewSource.GetDefaultView(cards);
			view.GroupDescriptions.Add(new PropertyGroupDescription("CardClass"));
			view.CustomSort = new CardComparer();
		}

		public IEnumerable<Orientation> OrientationTypes => Enum.GetValues(typeof(Orientation)).Cast<Orientation>();

		private void BtnUnlock_Click (object sender, RoutedEventArgs e) {
			BtnUnlock.Content = Graveyard.Input.Toggle() ? Strings.GetLocalized("Lock") : Strings.GetLocalized("Unlock");
		}

		internal class CardComparer : IComparer
		{
			public int Compare(object x, object y)
			{
				if (x is SettingsCard && y is SettingsCard)
				{
					Card cardX = ((SettingsCard)x).Card;
					Card cardY = ((SettingsCard)y).Card;
					// workaround to put neutral cards last
					bool xIsNeutral = cardX.CardClass == HearthDb.Enums.CardClass.NEUTRAL;
					bool yIsNeutral = cardY.CardClass == HearthDb.Enums.CardClass.NEUTRAL;
					if (xIsNeutral && !yIsNeutral)
						return 1;
					if (!xIsNeutral && yIsNeutral)
						return -1;
					int cardClassCompare = cardX.CardClass.CompareTo(cardY.CardClass);
					if (cardClassCompare != 0)
						return cardClassCompare;
					int manaCostCompare = cardX.Cost.CompareTo(cardY.Cost);
					if (manaCostCompare != 0)
						return manaCostCompare;
					return cardX.LocalizedName.CompareTo(cardY.LocalizedName);
				}
				else
				{
					return 1;
				}
			}
		}
	}
}
