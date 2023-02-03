using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Hearthstone;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HDT.Plugins.Graveyard
{
    // For cards with holding (i.e. in hand) effects
    // such as Commander Sivara and Hedra the Heretic
    // Single card per instance
    internal class HoldingEffectView : NormalView
    {
        public string CardId { get; set; }
        public int? CardLimit { get; set; }

        private bool IsCard(Card card) => card.Id == CardId;
        private bool CardInHand = false;

        private bool CardLimitReached => CardLimit.HasValue && 
            Cards.Sum(c => c.Count) >= CardLimit.Value;

        private bool ClearOnNextTurn = false;

        public HoldingEffectView(string cardId) 
        { 
            CardId = cardId;
        }

        private void CardOutbound(Card card, Action outboundAction = null)
        {
            if (CardInHand && IsCard(card))
            {
                CardInHand = false;
                outboundAction?.Invoke();
            }
        }

        private void CardInbound(Card card, Action inboundAction = null)
        {
            if (!CardInHand && IsCard(card))
            {
                CardInHand = true;
                inboundAction?.Invoke();
            }
        }

        public override bool Update(Card card)
        {
            CardOutbound(card, () => ClearOnNextTurn = true);
            return CardInHand && !CardLimitReached && base.Update(card);
        }

        private bool PlayerDraw(Card card)
        {
            CardInbound(card);
            return CardInHand;
        }

        private bool PlayerMulligan(Card card)
        {
            CardOutbound(card);
            return CardInHand;
        }

        private async Task TurnEnded()
        {
            if (ClearOnNextTurn)
            {
                Cards.Clear();
                await View.UpdateAsync(Cards, true);
                ClearOnNextTurn = false;
            }
        }

        internal class HoldingEffectConfig : ViewConfig
        {
            public int? CardLimit { get; set; }

            public HoldingEffectConfig(string cardId) : base(cardId)
            {
                CreateView = () => new HoldingEffectView(cardId)
                {
                    CardLimit = CardLimit,
                };
                UpdateOn = GameEvents.OnPlayerPlay;
            }

            public override void RegisterView(ViewBase view, bool isDefault = false)
            {
                base.RegisterView(view, isDefault);
                if (view is HoldingEffectView holdingEffectView)
                {
                    RegisterForCardEvent(GameEvents.OnPlayerDraw, holdingEffectView.PlayerDraw);
                    RegisterForCardEvent(GameEvents.OnPlayerMulligan, holdingEffectView.PlayerMulligan);
                    Plugin.Events.OnOpponentTurnStart.Register(holdingEffectView.TurnEnded);
                }
            }
        }
    }
}
