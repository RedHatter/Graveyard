using Hearthstone_Deck_Tracker.Utility.Logging;
using System;
using System.Collections.Generic;

namespace HDT.Plugins.Graveyard
{
    internal class UpdatePoller
    {
        private readonly List<Action> Actions = new List<Action>();

        public void Register(Action action)
        {
            Actions.Add(action);
        }

        public void Poll()
        {
            foreach (var action in Actions)
            {
                try
                {
                    action.Invoke();
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }
            }
        }

        public void Clear()
        {
            Actions.Clear();
        }
    }

    internal class UpdatePoller<T>
    {
        private readonly List<Action<T>> Actions = new List<Action<T>>();

        public void Register(Action<T> action)
        {
            Actions.Add(action);
        }

        public void Poll(T obj)
        {
            foreach (var action in Actions)
            {
                try
                {
                    action.Invoke(obj);
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }
            }
        }

        public void Clear()
        {
            Actions.Clear();
        }
    }
}
