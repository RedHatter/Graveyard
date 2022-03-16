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
        public string Enabled { get; set; }
        public Func<bool> ShowFirst { get; set; } = () => false;
        public ActionList<Card> UpdateOn { get; set; }       
        public IEnumerable<string> ShowOn => _ShowOnCards?.Where(c => c.IsEnabled).Select(c => c.CardId);
        public Predicate<Card> Condition { get; set; }        
        public Func<ViewBase> CreateView { get; set; }

        public IEnumerable<ViewConfigCard> ShowOnCards => _ShowOnCards;
        private readonly List<ViewConfigCard> _ShowOnCards;

        public ViewConfig() { }
        public ViewConfig(params string[] showOn)
        {
            _ShowOnCards = new List<ViewConfigCard>(showOn.Select(s => ViewConfigCards.Instance.Factory(s)));
        }
    }
}
