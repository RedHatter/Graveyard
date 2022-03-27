using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using Card = Hearthstone_Deck_Tracker.Hearthstone.Card;
using Core = Hearthstone_Deck_Tracker.API.Core;

namespace HDT.Plugins.Graveyard
{
    public class Graveyard
	{
		// The views
		internal List<ViewConfig> ConfigList { get; } = new List<ViewConfig>
		{
			QuestlineView.FriendlyConfig,
			ResurrectView.Config,
			DeathrattleView.Config,
			NZothView.Config,
			HadronoxView.Config,
			DiscardView.Config,
			GuldanView.Config,
			ShudderwockView.Config,
			DragoncallerAlannaView.Config,
			CavernsView.Config,
			MulchmuncherView.Config,
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
			ElwynnBoarView.Config,
			KargalView.Config,
			AntonidasView.Config,
			GrandFinaleView.Config,
			LastPlayedView.BrilliantMacawConfig,
			LastPlayedView.GreySageParrotConfig,
			LastPlayedView.MonstrousParrotConfig,
			LastPlayedView.SunwingSquawkerConfig,
			LastPlayedView.VanessaVanCleefConfig,
			MulticasterView.Config,
			ShirvallahView.Config,
			JaceDarkweaverView.Config,
			SI7View.Config,
		};

		private readonly StackPanel FriendlyPanel;
		private readonly StackPanel EnemyPanel;

		private StackPanel FirstPanel;

		public static InputManager Input;

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

		public void Clear()
        {
			FriendlyPanel.Children.Clear();
			EnemyPanel.Children.Clear();

			Plugin.Events.Clear();
		}

		public void Update()
        {
			var visibility = (Core.Game.IsInMenu && Hearthstone_Deck_Tracker.Config.Instance.HideInMenu)
				|| Core.Game.IsBattlegroundsMatch
				|| Core.Game.IsMercenariesMatch ? System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;

			FriendlyPanel.Visibility = visibility;
			EnemyPanel.Visibility = visibility;
		}

		/**
		* Clear then recreate all Views.
		*/
		public void Reset()
		{
			Clear();

			if ((Core.Game.IsInMenu && Hearthstone_Deck_Tracker.Config.Instance.HideInMenu) || Core.Game.IsBattlegroundsMatch || Core.Game.IsMercenariesMatch)
			{
				// don't initialize in menu unless the overlay is visible
				// don't show graveyard for Battlegrounds or Mercenaries
				// this should include spectating
				return;
			}
			
			InitializeView(EnemyPanel, QuestlineView.EnemyConfig);
			InitializeView(EnemyPanel, GraveyardView.EnemyConfig);
		
			FirstPanel = new StackPanel();
			FriendlyPanel.Children.Add(FirstPanel);
            
			InitializeView(FriendlyPanel, GraveyardView.FriendlyConfig, true);
			
			var anyfinView = InitializeView(FriendlyPanel, AnyfinView.Config);
			if (anyfinView != null)
			{
				RegisterView(GameEvents.OnOpponentPlayToGraveyard, anyfinView);
			}

			foreach (var config in ConfigList)
            {
                if (config.ShowFirst())
                {
					InitializeView(FirstPanel, config);
				}
				else
                {
					InitializeView(FriendlyPanel, config);
				}
			}

            // Show "demo mode" when overlay is visible in menu
			if (Core.Game.IsInMenu)
            {
                foreach (var card in Core.Game.Player.PlayerCardList)
                {
					if (card != null)
                    {
						Plugin.Events.OnOpponentPlay.Poll(card);
						Plugin.Events.OnOpponentPlayToGraveyard.Poll(card);
						Plugin.Events.OnPlayerHandDiscard.Poll(card);
						Plugin.Events.OnPlayerPlay.Poll(card);
						Plugin.Events.OnPlayerPlayToGraveyard.Poll(card);						
					}
                }
            }
		}

		private ViewBase InitializeView(Panel parent, ViewConfig config, bool isDefault = false)
        {
			var view = new ViewBuilder(config, Core.Game.Player.PlayerCardList).BuildView();
			if (view == null) return null;
			
			RegisterView(config, view, isDefault);
			ShowView(parent, view);
			return view;
		}

		private bool ShowView(Panel parent, ViewBase view)
		{
			if (view == null) return false;
			
			parent.Children.Add(view);

			return true;
		}

		private void RegisterView(ViewConfig config, ViewBase view, bool isDefault = false)
        {
			RegisterView(config.UpdateOn, view, isDefault);
			var multiTurn = view as MultiTurnView;
			if (multiTurn != null)
			{
				Plugin.Events.OnOpponentTurnStart.Register(multiTurn.TurnEnded);
			}
		}

		private void RegisterView(ActionList<Card> actionList, ViewBase view, bool isDefault = false)
        {
			if (actionList == GameEvents.OnPlayerPlayToGraveyard)
			{
				Plugin.Events.OnPlayerPlayToGraveyard.Register(view.Update, isDefault);
			}
			else if (actionList == GameEvents.OnOpponentPlayToGraveyard)
			{
				Plugin.Events.OnOpponentPlayToGraveyard.Register(view.Update, isDefault);
			}
			else if (actionList == GameEvents.OnPlayerPlay)
			{
				Plugin.Events.OnPlayerPlay.Register(view.Update, isDefault);
			}
			else if (actionList == GameEvents.OnOpponentPlay)
			{
				Plugin.Events.OnOpponentPlay.Register(view.Update, isDefault);
			}
			else if (actionList == GameEvents.OnPlayerHandDiscard)
			{
				Plugin.Events.OnPlayerHandDiscard.Register(view.Update, isDefault);
			}
		}
	}
}