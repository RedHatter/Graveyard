using Hearthstone_Deck_Tracker.Enums;
using Hearthstone_Deck_Tracker.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDT.Plugins.Graveyard
{
    internal class TurnUpdatePoller
    {
        private readonly List<Func<Task>> Registered = new List<Func<Task>>();
        private readonly ActivePlayer Player;

        public TurnUpdatePoller(ActivePlayer player)
        {
            Player = player;
        }

        public void Register(Func<Task> task)
        {
            Registered.Add(task);
        }

        public async void Poll(ActivePlayer player)
        {
            if (Player != player) return;

            foreach (var task in Registered)
            {
                try
                {
                    await task();
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }
            }
        }

        public void Clear()
        {
            Registered.Clear();
        }
    }
}
