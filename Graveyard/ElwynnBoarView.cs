using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;
using static HearthDb.CardIds.Collectible;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hearthstone_Deck_Tracker.API;

namespace HDT.Plugins.Graveyard
{
    public class ElwynnBoarView : NormalView
    {
        private static ViewConfig _Config;
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new ViewConfig(Neutral.ElwynnBoar)
            {
                Name = Strings.GetLocalized("ElwynnBoar"),
                Enabled = () => Settings.Default.ElwynnBoarEnabled,
                WatchFor = GameEvents.OnPlayerPlayToGraveyard,
                Condition = card => card.Id == Neutral.ElwynnBoar,
            });
        }       

        public static bool IsAlwaysSeparate => Settings.Default.ElwynnBoarEnabled && Settings.Default.AlwaysBoarSeparately;

        public ElwynnBoarView()
        {
            // Section Label
            Label.Text = Config.Name;
        }

        public bool Update(Card card)
        {
            return Config.Condition(card) && base.Update(card);
        }
    }
}
