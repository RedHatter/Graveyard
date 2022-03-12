using Hearthstone_Deck_Tracker.Hearthstone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDT.Plugins.Graveyard
{
    internal class ViewBuilder
    {
        private readonly ViewConfig Config;
        private readonly IEnumerable<Card> PlayerCards;

        private List<Card> ActiveCards => _ActiveCards  ?? (_ActiveCards = GetActiveCards());
        private List<Card> _ActiveCards;

        public ViewBuilder(ViewConfig config, IEnumerable<Card> playerCards)
        {
            Config = config;
            PlayerCards = playerCards;
        }

        private List<Card> GetActiveCards()
        {
            if (Config.ShowOn == null) return new List<Card>();

            return new List<Card>(from playerCard in PlayerCards
                                  join cardId in Config.ShowOn
                                  on playerCard.Id equals cardId
                                  select playerCard);
        }

        public ViewBase BuildView()
        {
            if (!Config.Enabled()) return null;

            if (Config.ShowOn == null || ActiveCards.Count() > 0)
            {
                var view = Config.CreateView();
                view.Title = ActiveCards.Count == 1 ? ActiveCards.First().LocalizedName : Config.Name;
                view.Condition = Config.Condition;
                return view;
            }

            return null;
        }
    }
}
