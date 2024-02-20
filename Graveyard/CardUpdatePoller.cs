using Hearthstone_Deck_Tracker.Hearthstone;
using Hearthstone_Deck_Tracker.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDT.Plugins.Graveyard
{
    internal enum DefaultMode
    {
        None,
        First,
        Last,
        Custom,
    }

    internal class CardUpdatePoller
    {
        private readonly List<Func<Card,bool>> Registered = new List<Func<Card,bool>>();

        public DefaultMode DefaultMode { get; private set; }

        public Func<Card, bool> Default { get; private set; }

        public CardUpdatePoller(DefaultMode mode = DefaultMode.None)
        {
            DefaultMode = mode == DefaultMode.Custom ? DefaultMode.None : mode;
        }

        public CardUpdatePoller(Func<Card, bool> update)
        {
            SetDefault(update);
        }

        private void SetDefault(Func<Card, bool> update)
        {
            Default = update;
            DefaultMode = DefaultMode.Custom;
        }

        public void Register(Func<Card, bool> update, bool isDefault = false)
        {
            if (isDefault)
            {
                SetDefault(update);
            }
            else
            {
                Registered.Add(update);
            }
        }

        public void Poll(Card card)
        {
            var result = false;
            int startFor = 0, endFor = Registered.Count;
            if (DefaultMode == DefaultMode.First)
            {
                startFor += 1;
            }
            else if (DefaultMode == DefaultMode.Last)
            {
                endFor -= 1;
            }
            for (int i = startFor; i < endFor; i++)
            {
                try
                {
                    var update = Registered[i];
                    result |= update.Invoke(card);
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }
            }            
            if (!result && DefaultMode != DefaultMode.None)
            {
                result |= Default.Invoke(card);
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
