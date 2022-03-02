using Hearthstone_Deck_Tracker.Hearthstone;
using Hearthstone_Deck_Tracker.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDT.Plugins.Graveyard
{
    internal class CardUpdatePoller
    {
        private readonly List<Func<Card,bool>> Registered = new List<Func<Card,bool>>();

        public void Register(Func<Card, bool> update)
        {
            Registered.Add(update);
        }

        public void Poll(Card card)
        {
            var result = false;
            foreach (var update in Registered)
            {
                try
                {
                    result |= update.Invoke(card);
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }
            }
            _LastResult = result;
        }

        public bool? LastResult => _LastResult;
        private bool? _LastResult;
        

        public void Clear()
        {
            Registered.Clear();
        }
    }
}
