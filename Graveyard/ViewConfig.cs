using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Hearthstone;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HDT.Plugins.Graveyard
{
    internal class ViewConfig
    {
        public string Name { get; set; }
        public Func<bool> Enabled { get; set; }
        public Func<bool> ShowFirst { get; set; } = () => false;
        public ActionList<Card> UpdateOn { get; set; }       
        public IEnumerable<string> ShowOn => ShowOnCards?.Where(c => c.IsEnabled).Select(c => c.CardId);
        public Predicate<Card> Condition { get; set; }        
        public Func<ViewBase> CreateView { get; set; }

        private List<ViewConfigCard> ShowOnCards;

        public ViewConfig() { }
        public ViewConfig(params string[] showOn)
        {
            ShowOnCards = new List<ViewConfigCard>(showOn.Select(s => ViewConfigCards.Instance.Factory(s)));
        }
    }
}
