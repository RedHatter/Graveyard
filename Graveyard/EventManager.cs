using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Enums;
using Hearthstone_Deck_Tracker.Hearthstone;
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

		public CardUpdatePoller MapCardEvent(ActionList<Card> actionList)
        {
            CardUpdatePoller cardUpdatePoller = null;
            if (actionList == GameEvents.OnPlayerPlayToGraveyard)
            {
                cardUpdatePoller = OnPlayerPlayToGraveyard;
            }
            else if (actionList == GameEvents.OnOpponentPlayToGraveyard)
            {
                cardUpdatePoller = OnOpponentPlayToGraveyard;
            }
            else if (actionList == GameEvents.OnPlayerPlay)
            {
                cardUpdatePoller = OnPlayerPlay;
            }
            else if (actionList == GameEvents.OnOpponentPlay)
            {
                cardUpdatePoller = OnOpponentPlay;
            }
            else if (actionList == GameEvents.OnPlayerHandDiscard)
            {
                cardUpdatePoller = OnPlayerHandDiscard;
            }
            return cardUpdatePoller;
        }
	}
}
