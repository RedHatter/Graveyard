using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media;
using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Utility.Logging;
using Hearthstone_Deck_Tracker.API;
using Core = Hearthstone_Deck_Tracker.API.Core;
using Card = Hearthstone_Deck_Tracker.Hearthstone.Card;

namespace HDT.Plugins.Graveyard
{
	public class Graveyard
	{
		// The views
		public NormalView Normal;
		public ResurrectView Resurrect;
		public AnyfinView Anyfin;
		public NZothView NZoth;
		public NormalView Enemy;

		private StackPanel _vertical;
		private StackPanel _verticalEnemy;

    public Graveyard () {
			// Create container
			_verticalEnemy = new StackPanel();
			_verticalEnemy.Orientation = Orientation.Vertical;
			_verticalEnemy.RenderTransform = new ScaleTransform(
				Config.Instance.OverlayOpponentScaling / 100,
				Config.Instance.OverlayOpponentScaling / 100);
			Core.OverlayCanvas.Children.Add(_verticalEnemy);

			// Stick to the Right of the player panal
			var border = Core.OverlayCanvas.FindName("BorderStackPanelOpponent") as Border;
			DependencyPropertyDescriptor.FromProperty(Canvas.LeftProperty, typeof(Border))
				.AddValueChanged(border, Layout);
			DependencyPropertyDescriptor.FromProperty(Canvas.TopProperty, typeof(Border))
				.AddValueChanged(border, Layout);
			DependencyPropertyDescriptor.FromProperty(StackPanel.ActualWidthProperty, typeof(StackPanel))
				.AddValueChanged(_verticalEnemy, Layout);

			// Create container
			_vertical = new StackPanel();
			_vertical.Orientation = Orientation.Vertical;
			_vertical.RenderTransform = new ScaleTransform(
				Config.Instance.OverlayPlayerScaling / 100,
				Config.Instance.OverlayPlayerScaling / 100);
			Core.OverlayCanvas.Children.Add(_vertical);

			// Stick to the left of the player panal
			border = Core.OverlayCanvas.FindName("BorderStackPanelPlayer") as Border;
			DependencyPropertyDescriptor.FromProperty(Canvas.LeftProperty, typeof(Border))
				.AddValueChanged(border, Layout);
			DependencyPropertyDescriptor.FromProperty(Canvas.TopProperty, typeof(Border))
				.AddValueChanged(border, Layout);
			DependencyPropertyDescriptor.FromProperty(StackPanel.ActualWidthProperty, typeof(StackPanel))
				.AddValueChanged(_vertical, Layout);

			// Connect events
			GameEvents.OnGameStart.Add(Reset);
			DeckManagerEvents.OnDeckSelected.Add(d => Reset());
			GameEvents.OnPlayerPlayToGraveyard.Add(Update);

			GameEvents.OnOpponentPlayToGraveyard.Add(c => {
				Anyfin?.Update(c);
				Enemy?.Update(c);
			});
			GameEvents.OnPlayerPlay.Add(c => Anyfin?.UpdateDamage());
			GameEvents.OnOpponentPlay.Add(c => Anyfin?.UpdateDamage());
    }

		public void Dispose () {
			Core.OverlayCanvas.Children.Remove(_vertical);
			var border = Core.OverlayCanvas.FindName("BorderStackPanelPlayer") as Border;
			DependencyPropertyDescriptor.FromProperty(Canvas.LeftProperty, typeof(Border))
				.RemoveValueChanged(border, Layout);
			DependencyPropertyDescriptor.FromProperty(Canvas.TopProperty, typeof(Border))
				.RemoveValueChanged(border, Layout);
			DependencyPropertyDescriptor.FromProperty(StackPanel.ActualWidthProperty, typeof(StackPanel))
				.RemoveValueChanged(_vertical, Layout);

			Core.OverlayCanvas.Children.Remove(_verticalEnemy);
			border = Core.OverlayCanvas.FindName("BorderStackPanelOpponent") as Border;
			DependencyPropertyDescriptor.FromProperty(Canvas.LeftProperty, typeof(Border))
				.RemoveValueChanged(border, Layout);
			DependencyPropertyDescriptor.FromProperty(Canvas.TopProperty, typeof(Border))
				.RemoveValueChanged(border, Layout);
			DependencyPropertyDescriptor.FromProperty(StackPanel.ActualWidthProperty, typeof(StackPanel))
				.RemoveValueChanged(_vertical, Layout);
		}

		private void Layout (object obj, EventArgs e) {
			var border = Core.OverlayCanvas.FindName("BorderStackPanelPlayer") as Border;
			Canvas.SetLeft(_vertical, Canvas.GetLeft(border) - _vertical.ActualWidth * Config.Instance.OverlayPlayerScaling / 100 - 10);
			Canvas.SetTop(_vertical, Canvas.GetTop(border));

			border = Core.OverlayCanvas.FindName("BorderStackPanelOpponent") as Border;
			Canvas.SetLeft(_verticalEnemy, Canvas.GetLeft(border) + border.ActualWidth * Config.Instance.OverlayOpponentScaling / 100 + 10);
			Canvas.SetTop(_verticalEnemy, Canvas.GetTop(border));
		}

		/**
		 * Clear then recreate all Views.
		 */
		public void Reset () {
			_vertical.Children.Clear();

			if (Settings.Default.EnemyEnabled) {
				Enemy = new NormalView();
				_verticalEnemy.Children.Add(Enemy);
			}

			if (Settings.Default.ResurrectEnabled && ResurrectView.isValid()) {
				Resurrect = new ResurrectView();
				_vertical.Children.Add(Resurrect);
				Normal = null;
			} else if (Settings.Default.NormalEnabled) {
				Normal = new NormalView();
				_vertical.Children.Add(Normal);
				Resurrect = null;
			}

			if (Settings.Default.AnyfinEnabled && AnyfinView.isValid()) {
				Anyfin = new AnyfinView();
				_vertical.Children.Add(Anyfin);
			} else {
				Anyfin = null;
			}

			if (Settings.Default.NZothEnabled && NZothView.isValid()) {
				NZoth = new NZothView();
				_vertical.Children.Add(NZoth);
			} else {
				NZoth = null;
			}
		}

		public void Update (Card card) {
			var a = Anyfin?.Update(card) ?? false;
			var b = NZoth?.Update(card) ?? false;
			var c = Resurrect?.Update(card) ?? false;
			if (!(a || b || c))
				Normal?.Update(card);
		}
	}
}
