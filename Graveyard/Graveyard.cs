using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Enums;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Card = Hearthstone_Deck_Tracker.Hearthstone.Card;
using Core = Hearthstone_Deck_Tracker.API.Core;
using GameMode = Hearthstone_Deck_Tracker.Enums.GameMode;


namespace HDT.Plugins.Graveyard
{
    public class Graveyard
	{
		private static readonly string SharedName = Strings.GetLocalized("Graveyard");
		private static readonly Func<ViewBase> SharedCreateView = () => new NormalView();
		private static readonly Predicate<Card> SharedCondition = card => card.Type == "Minion";

		internal static ViewConfig FriendlyConfig
        {
			get => _FriendlyConfig ?? (_FriendlyConfig = new ViewConfig()
            {
				Name = SharedName,
				Enabled = () => Settings.Default.NormalEnabled,
				CreateView = SharedCreateView,
				WatchFor = GameEvents.OnPlayerPlayToGraveyard,
				Condition = SharedCondition,
			});
        }
		private static ViewConfig _FriendlyConfig;

		internal static ViewConfig EnemyConfig
        {
			get => _EnemyConfig ?? (_EnemyConfig = new ViewConfig()
            {
				Name = SharedName,
				Enabled = () => Settings.Default.EnemyEnabled,
				CreateView = SharedCreateView,
				WatchFor = GameEvents.OnOpponentPlayToGraveyard,
				Condition = SharedCondition,
            });
        }
		private static ViewConfig _EnemyConfig;

		// The views
		public ViewBase Normal;
		public ViewBase Enemy;

		public ViewBase FriendlyQuestline;
		public ViewBase EnemyQuestline;

		internal List<ViewConfig> ConfigList = new List<ViewConfig>
		{ 
			ResurrectView.Config,
			AnyfinView.Config,
			DeathrattleView.Config,
			NZothView.Config,
			HadronoxView.Config,
			DiscardView.Config,
			GuldanView.Config,
			ShudderwockView.Config,
			DragoncallerAlannaView.Config,
			CavernsView.Config,
			//MulchmuncherView.Config,
			KangorView.Config,
			WitchingHourView.Config,
			TessGreymaneView.Config,
			SoulwardenView.Config,
			ZuljinView.Config,
			HoardPillagerView.Config,
			LadyLiadrinView.Config,
			NZothGotDView.Config,
			RallyView.Config,
			SaurfangView.Config,
			YShaarjView.Config,
			//ElwynnBoarView.Config,
			KargalView.Config,
			AntonidasView.Config,
			GrandFinaleView.Config,
			//LastPlayedView.BrilliantMacawConfig,
			//LastPlayedView.GreySageParrotConfig,
			//LastPlayedView.MonstrousParrotConfig,
			//LastPlayedView.SunwingSquawkerConfig,
			//LastPlayedView.VanessaVanCleefConfig,
			MulticasterView.Config,
			ShirvallahView.Config,
			new ViewConfig(HearthDb.CardIds.Collectible.Demonhunter.JaceDarkweaver)
            {
				Name = Strings.GetLocalized("Jace"),
				Enabled = () => true,
				CreateView = () => new NormalView(),
				WatchFor = GameEvents.OnPlayerPlay,
				Condition = card => card.GetSchool() == School.Fel,
            },
			new ViewConfig(HearthDb.CardIds.Collectible.Rogue.Si7Assassin,
				HearthDb.CardIds.Collectible.Rogue.Si7Informant,
				HearthDb.CardIds.Collectible.Rogue.Si7Smuggler)
			{
				Name = Strings.GetLocalized("SI7"),
				Enabled = () => true,
				CreateView = () => new NormalView(),
				WatchFor = GameEvents.OnPlayerPlay,
				Condition = card => card.Name.StartsWith("SI:7"),
			}
		};

		internal List<ViewConfig> ConfigSinglesList = new List<ViewConfig>
		{
			MulchmuncherView.Config,
			ElwynnBoarView.Config,
			LastPlayedView.BrilliantMacawConfig,
			LastPlayedView.GreySageParrotConfig,
			LastPlayedView.MonstrousParrotConfig,
			LastPlayedView.SunwingSquawkerConfig,
			LastPlayedView.VanessaVanCleefConfig,
		};

		private readonly StackPanel FriendlyPanel;
		private readonly StackPanel EnemyPanel;

		private StackPanel SinglesPanel;

		public static InputManager Input;

		internal TurnUpdatePoller OnOpponentTurnStart { get; } = new TurnUpdatePoller(ActivePlayer.Opponent);
		internal CardUpdatePoller OnPlayerPlayToGraveyard { get; } = new CardUpdatePoller();
		internal CardUpdatePoller OnOpponentPlayToGraveyard { get; } = new CardUpdatePoller();
		internal CardUpdatePoller OnPlayerPlay { get; } = new CardUpdatePoller();
		internal CardUpdatePoller OnOpponentPlay { get; } = new CardUpdatePoller();
		internal CardUpdatePoller OnPlayerHandDiscard { get; } = new CardUpdatePoller();

		public Graveyard()
		{

			// Create container
			EnemyPanel = new StackPanel();
			EnemyPanel.Orientation = Orientation.Vertical;
			Core.OverlayCanvas.Children.Add(EnemyPanel);
			Canvas.SetTop(EnemyPanel, Settings.Default.EnemyTop);
			Canvas.SetLeft(EnemyPanel, Settings.Default.EnemyLeft);

			// Create container
			FriendlyPanel = new StackPanel();
			FriendlyPanel.Orientation = Settings.Default.FriendlyOrientation;
			Core.OverlayCanvas.Children.Add(FriendlyPanel);
			Canvas.SetTop(FriendlyPanel, Settings.Default.PlayerTop);
			Canvas.SetLeft(FriendlyPanel, Settings.Default.PlayerLeft);

			Input = new InputManager(FriendlyPanel, EnemyPanel);

			Settings.Default.PropertyChanged += SettingsChanged;
			SettingsChanged(null, null);
		}

        //on year change clear out the grid and update the data
        private void SettingsChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			FriendlyPanel.Orientation = Settings.Default.FriendlyOrientation;
			FriendlyPanel.RenderTransform = new ScaleTransform(Settings.Default.FriendlyScale / 100, Settings.Default.FriendlyScale / 100);
			FriendlyPanel.Opacity = Settings.Default.FriendlyOpacity / 100;
			EnemyPanel.RenderTransform = new ScaleTransform(Settings.Default.EnemyScale / 100, Settings.Default.EnemyScale / 100);
			EnemyPanel.Opacity = Settings.Default.EnemyOpacity / 100;
		}

		public void Dispose()
		{
			Core.OverlayCanvas.Children.Remove(FriendlyPanel);
			Core.OverlayCanvas.Children.Remove(EnemyPanel);
			Input.Dispose();
		}

		/**
		* Clear then recreate all Views.
		*/
		public void Reset()
		{
			FriendlyPanel.Children.Clear();
			EnemyPanel.Children.Clear();

			OnPlayerPlayToGraveyard.Clear();
			OnOpponentPlayToGraveyard.Clear();
			
			OnPlayerPlay.Clear();
			OnOpponentPlay.Clear();

			OnPlayerHandDiscard.Clear();

			OnOpponentTurnStart.Clear();

			if ((Core.Game.IsInMenu && !Core.OverlayWindow.IsVisible) || Core.Game.IsBattlegroundsMatch || Core.Game.IsMercenariesMatch)
			{
				// don't initialize in menu unless the overlay is visible
				// don't show graveyard for Battlegrounds or Mercenaries
				// this should include spectating
				return;
			}

			ShowEnemyView(QuestlineView.EnemyConfig, ref EnemyQuestline);
			ShowEnemyView(EnemyConfig, ref Enemy);

			ShowFriendlyView(QuestlineView.FriendlyConfig, ref FriendlyQuestline);
			
			SinglesPanel = new StackPanel();
			FriendlyPanel.Children.Add(SinglesPanel);
            
			foreach (var config in ConfigSinglesList)
            {
				ShowView(config, SinglesPanel.Children);
            }

			ShowFriendlyView(FriendlyConfig, ref Normal);

            foreach (var config in ConfigList)
            {
				ShowView(config, FriendlyPanel.Children);
			}

            // Show "demo mode" when overlay is visible in menu
			if (Core.Game.IsInMenu)
            {
                foreach (var card in Core.Game.Player.PlayerCardList)
                {
					if (card != null)
                    {
						OnOpponentPlay.Poll(card);
						OnOpponentPlayToGraveyard.Poll(card);
						OnPlayerHandDiscard.Poll(card);
						OnPlayerPlay.Poll(card);
						OnPlayerPlayToGraveyard.Poll(card);						
					}
                }
            }
		}

		private bool ShowFriendlyView(ViewConfig config, ref ViewBase view)
        {
			view = ShowView(config, FriendlyPanel.Children);
			return view != null;
		}

		private bool ShowEnemyView(ViewConfig config, ref ViewBase view)
		{
			view = ShowView(config, EnemyPanel.Children);
			return view != null;
		}

		private ViewBase ShowView(ViewConfig config, UIElementCollection parent)
		{
			var view = CreateView(config);
            if (view != null)
            {
				RegisterView(config, view);
				parent.Add(view);
			}			
			return view;
		}

		private ViewBase CreateView(ViewConfig config)
        {
			if (config.Enabled() && (config.ShowOn == null || Core.Game.Player.PlayerCardList.FindIndex(card => config.ShowOn.Contains(card.Id)) > -1))
			{
				var view = config.CreateView();
				view.Title = config.Name;
				view.Condition = config.Condition;
				return view;
			}
			return null;
		}

		private void RegisterView(ViewConfig config, ViewBase view)
        {
			if (config.WatchFor == GameEvents.OnPlayerPlayToGraveyard)
			{
				OnPlayerPlayToGraveyard.Register(view.Update);
			}
			else if (config.WatchFor == GameEvents.OnOpponentPlayToGraveyard)
			{
				OnOpponentPlayToGraveyard.Register(view.Update);
			}
			else if (config.WatchFor == GameEvents.OnPlayerPlay)
			{
				OnPlayerPlay.Register(view.Update);
			}
			else if (config.WatchFor == GameEvents.OnOpponentPlay)
			{
				OnOpponentPlay.Register(view.Update);
			}
			else if (config.WatchFor == GameEvents.OnPlayerHandDiscard)
			{
				OnPlayerHandDiscard.Register(view.Update);
			}
			var multiTurn = view as MultiTurnView;
			if (multiTurn != null)
			{
				OnOpponentTurnStart.Register(multiTurn.TurnEnded);
            }
		}
	}
}