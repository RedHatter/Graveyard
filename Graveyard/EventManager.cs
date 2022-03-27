using Hearthstone_Deck_Tracker.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDT.Plugins.Graveyard
{
    internal class EventManager
    {
		public TurnUpdatePoller OnOpponentTurnStart { get; } = new TurnUpdatePoller(ActivePlayer.Opponent);
		public CardUpdatePoller OnPlayerPlayToGraveyard { get; } = new CardUpdatePoller();
		public CardUpdatePoller OnOpponentPlayToGraveyard { get; } = new CardUpdatePoller();
		public CardUpdatePoller OnPlayerPlay { get; } = new CardUpdatePoller();
		public CardUpdatePoller OnOpponentPlay { get; } = new CardUpdatePoller();
		public CardUpdatePoller OnPlayerHandDiscard { get; } = new CardUpdatePoller();

		public void Clear()
        {
			OnPlayerPlayToGraveyard.Clear();
			OnOpponentPlayToGraveyard.Clear();

			OnPlayerPlay.Clear();
			OnOpponentPlay.Clear();

			OnPlayerHandDiscard.Clear();

			OnOpponentTurnStart.Clear();
		}
	}
}
