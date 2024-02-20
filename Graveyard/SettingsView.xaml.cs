using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;
using Hearthstone_Deck_Tracker.Utility;
using MahApps.Metro.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Navigation;
using ResourceStrings = HDT.Plugins.Graveyard.Resources.Strings;

namespace HDT.Plugins.Graveyard
{
    public partial class SettingsView : Grid
	{
		private static Flyout _flyout;
		public static Flyout Flyout => _flyout ?? (_flyout = CreateSettingsFlyout());

		private static Flyout CreateSettingsFlyout()
		{
            var settings = new Flyout
            {
                Position = Position.Left
            };
            SetZIndex(settings, 100);
			settings.Header = Strings.GetLocalized(nameof(ResourceStrings.SettingsTitle));
			settings.Content = new SettingsView();
			Core.MainWindow.Flyouts.Items.Add(settings);
			return settings;
		}
		public static Uri PluginReadmeUrl = new Uri(Settings.Default.GitHubProjectUrl + "#readme");
		public static Uri PluginReleasesUrl = new Uri(Settings.Default.GitHubProjectUrl + "/releases");
		public static Uri PluginIssuesUrl = new Uri(Settings.Default.GitHubProjectUrl + "/issues");
	
		private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e) => Helper.TryOpenUrl(e.Uri.AbsoluteUri);

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

        public class GroupHeader
        {
            public string Title { get; set; }
            public ICommand Command { get; set; }
        }
        public GroupHeader PlayerPositionGroup { get; } = new GroupHeader
        {
            Title = Strings.GetLocalized(nameof(ResourceStrings.SettingsPositionTitle)).ToUpper(),
            Command = new Command(Settings.Default.ResetPlayerPosition)
        };
        public GroupHeader OpponentPositionGroup { get; } = new GroupHeader
        {
            Title = Strings.GetLocalized(nameof(ResourceStrings.SettingsPositionTitle)).ToUpper(),
            Command = new Command(Settings.Default.ResetOpponentPosition)
        };
        public GroupHeader PlayerDisplayGroup { get; } = new GroupHeader
        {
            Title = Strings.GetLocalized(nameof(ResourceStrings.SettingsDisplayTitle)).ToUpper(),
            Command = new Command(Settings.Default.ResetPlayerDisplay)
        };
        public GroupHeader OpponentDisplayGroup { get; } = new GroupHeader
        {
            Title = Strings.GetLocalized(nameof(ResourceStrings.SettingsDisplayTitle)).ToUpper(),
            Command = new Command(Settings.Default.ResetOpponentDisplay)
        };

        public IEnumerable<Orientation> OrientationTypes => Enum.GetValues(typeof(Orientation)).Cast<Orientation>();

		private void BtnUnlock_Click (object sender, RoutedEventArgs e) {
			BtnUnlock.Content = Graveyard.Input.Toggle() 
				? Strings.GetLocalized(nameof(ResourceStrings.Lock)) 
				: Strings.GetLocalized(nameof(ResourceStrings.Unlock));
		}

		internal class CardComparer : IComparer
		{
			public int Compare(object x, object y)
			{
				if (x is SettingsCard settingsCardX && y is SettingsCard settingsCardY)
				{
					Card cardX = settingsCardX.Card;
					Card cardY = settingsCardY.Card;
					// workaround to put neutral cards last
					bool xIsNeutral = cardX.CardClass == HearthDb.Enums.CardClass.NEUTRAL;
					bool yIsNeutral = cardY.CardClass == HearthDb.Enums.CardClass.NEUTRAL;
					if (xIsNeutral && !yIsNeutral)
						return 1;
					if (!xIsNeutral && yIsNeutral)
						return -1;
					int cardClassCompare = settingsCardX.CardClass.CompareTo(settingsCardY.CardClass);
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
