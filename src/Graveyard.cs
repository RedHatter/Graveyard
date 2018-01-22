using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.Generic;
using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.API;
using Core = Hearthstone_Deck_Tracker.API.Core;
using Card = Hearthstone_Deck_Tracker.Hearthstone.Card;

namespace HDT.Plugins.Graveyard
{
	public class Graveyard
	{
		// The views
		public NormalView Normal;
		public NormalView Enemy;

		public ResurrectView Resurrect;
		public AnyfinView Anyfin;
		public NZothView NZoth;
		public DiscardView Discard;
		public GuldanView Guldan;

		private StackPanel _vertical;
		private StackPanel _verticalEnemy;

		public static InputManager Input;

		public Graveyard()
		{
			// Create container
			_verticalEnemy = new StackPanel();
			_verticalEnemy.Orientation = Orientation.Vertical;
			// _verticalEnemy.RenderTransform = new ScaleTransform(Config.Instance.OverlayOpponentScaling / 100, Config.Instance.OverlayOpponentScaling / 100);
			Core.OverlayCanvas.Children.Add(_verticalEnemy);
			Canvas.SetTop(_verticalEnemy, Settings.Default.EnemyTop);
			Canvas.SetLeft(_verticalEnemy, Settings.Default.EnemyLeft);

			// Create container
			_vertical = new StackPanel();
			_vertical.Orientation = Orientation.Vertical;
			// _vertical.RenderTransform = new ScaleTransform(Config.Instance.OverlayPlayerScaling / 100, Config.Instance.OverlayPlayerScaling / 100);
			Core.OverlayCanvas.Children.Add(_vertical);
			Canvas.SetTop(_vertical, Settings.Default.PlayerTop);
			Canvas.SetLeft(_vertical, Settings.Default.PlayerLeft);

			Input = new InputManager(_vertical, _verticalEnemy);

			// Connect events
			GameEvents.OnGameStart.Add(Reset);
			GameEvents.OnGameEnd.Add(Reset);
			DeckManagerEvents.OnDeckSelected.Add(d => Reset());

			GameEvents.OnPlayerPlayToGraveyard.Add(PlayerGraveyardUpdate);
			GameEvents.OnOpponentPlayToGraveyard.Add(EnemyGraveyardUpdate);

			GameEvents.OnPlayerPlay.Add(c => Anyfin?.UpdateDamage());
			GameEvents.OnOpponentPlay.Add(c => Anyfin?.UpdateDamage());

			GameEvents.OnPlayerHandDiscard.Add(PlayerDiscardUpdate);
		}

		public void Dispose()
		{
			Core.OverlayCanvas.Children.Remove(_vertical);
			Core.OverlayCanvas.Children.Remove(_verticalEnemy);
			Input.Dispose();
		}

		/**
		* Clear then recreate all Views.
		*/
		public void Reset()
		{
			_vertical.Children.Clear();
			_verticalEnemy.Children.Clear();

			if (Settings.Default.EnemyEnabled)
			{
				Enemy = new NormalView();
				_verticalEnemy.Children.Add(Enemy);
			}
			else
			{
				Enemy = null;
			}

			if (Settings.Default.ResurrectEnabled && ResurrectView.isValid())
			{
				Resurrect = new ResurrectView();
				_vertical.Children.Add(Resurrect);
				Normal = null;
			}
			else if (Settings.Default.NormalEnabled)
			{
				Normal = new NormalView();
				_vertical.Children.Add(Normal);
				Resurrect = null;
			}
			else
			{
				Normal = null;
				Resurrect = null;
			}

			if (Settings.Default.AnyfinEnabled && AnyfinView.isValid())
			{
				Anyfin = new AnyfinView();
				_vertical.Children.Add(Anyfin);
			}
			else
			{
				Anyfin = null;
			}

			if (Settings.Default.NZothEnabled && NZothView.isValid())
			{
				NZoth = new NZothView();
				_vertical.Children.Add(NZoth);
			}
			else
			{
				NZoth = null;
			}

			if (Settings.Default.DiscardEnabled && DiscardView.isValid())
			{
				Discard = new DiscardView();
				_vertical.Children.Add(Discard);
			}
			else
			{
				Discard = null;
			}

			if (Settings.Default.GuldanEnabled && GuldanView.isValid())
			{
				Guldan = new GuldanView();
				_vertical.Children.Add(Guldan);
			}
			else
			{
				Guldan= null;
			}
		}

		public void PlayerGraveyardUpdate(Card card)
		{
			var any = Anyfin?.Update(card) ?? false;
			var nzoth = NZoth?.Update(card) ?? false;
			var rez = Resurrect?.Update(card) ?? false;
			var guldan = Guldan?.Update(card) ?? false;
			if (!(any || nzoth || rez || guldan))
			{
				Normal?.Update(card);
			}
		}

		public void EnemyGraveyardUpdate(Card card)
		{
			Anyfin?.Update(card);
			Enemy?.Update(card);
		}

		public void PlayerDiscardUpdate(Card card)
		{
			Discard?.Update(card);
		}
	}
}
