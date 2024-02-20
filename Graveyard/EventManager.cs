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
        // Remember to add a GameEvents.[GameEvent].Add(EventManager.[GameEvent])
        // call to GraveyardPlugin.OnLoad for new pollers
        public TurnUpdatePoller OnOpponentTurnStart { get; } = new TurnUpdatePoller(ActivePlayer.Opponent);
		public CardUpdatePoller OnPlayerPlayToGraveyard { get; } = new CardUpdatePoller();
		public CardUpdatePoller OnOpponentPlayToGraveyard { get; } = new CardUpdatePoller();
		public CardUpdatePoller OnPlayerPlay { get; } = new CardUpdatePoller();
		public CardUpdatePoller OnOpponentPlay { get; } = new CardUpdatePoller();
        public CardUpdatePoller OnPlayerCreateInPlay { get; } = new CardUpdatePoller();
        public CardUpdatePoller OnOpponentCreateInPlay { get; } = new CardUpdatePoller();
        public CardUpdatePoller OnPlayerHandDiscard { get; } = new CardUpdatePoller();
        public CardUpdatePoller OnPlayerDraw { get; } = new CardUpdatePoller();
        public CardUpdatePoller OnPlayerMulligan { get; } = new CardUpdatePoller();
        public CardUpdatePoller OnPlayerGet { get; } = new CardUpdatePoller();

        public void Clear()
        {
			OnPlayerPlayToGraveyard.Clear();
			OnOpponentPlayToGraveyard.Clear();

			OnPlayerPlay.Clear();
			OnOpponentPlay.Clear();

            OnPlayerCreateInPlay.Clear();
            OnOpponentCreateInPlay.Clear();

            OnPlayerHandDiscard.Clear();

            OnPlayerDraw.Clear();

            OnPlayerMulligan.Clear();

            OnPlayerGet.Clear();

            OnOpponentTurnStart.Clear();
		}

		public CardUpdatePoller MapCardEvent(ActionList<Card> actionList)
        {
            CardUpdatePoller cardUpdatePoller;
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
            else if (actionList == GameEvents.OnPlayerCreateInPlay)
            {
                cardUpdatePoller = OnPlayerCreateInPlay;
            }
            else if (actionList == GameEvents.OnOpponentCreateInPlay)
            {
                cardUpdatePoller = OnOpponentCreateInPlay;
            }
            else if (actionList == GameEvents.OnPlayerHandDiscard)
            {
                cardUpdatePoller = OnPlayerHandDiscard;
            }
            else if (actionList == GameEvents.OnPlayerDraw)
            {
                cardUpdatePoller = OnPlayerDraw;
            }
            else if (actionList == GameEvents.OnPlayerMulligan)
            {
                cardUpdatePoller = OnPlayerMulligan;
            }
            else if (actionList == GameEvents.OnPlayerGet)
            {
                cardUpdatePoller = OnPlayerGet;
            }
            else
            {
                throw new ArgumentException($"Requested game event mapping not supported by Graveyard");
            }
            return cardUpdatePoller;
        }
	}
}
