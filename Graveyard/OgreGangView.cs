using Hearthstone_Deck_Tracker.API;
using System.Collections.Generic;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    internal class OgreGangView
    {
        private static ViewConfig _Config;
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new ViewConfig(Neutral.KingpinPud)
            {
                CreateView = () => new NormalView(),
                UpdateOn = GameEvents.OnPlayerPlayToGraveyard,
                Condition = card => GangMembers.Contains(card.Id),
            });
        }

        internal static readonly List<string> GangMembers = new List<string>
        {
            Neutral.OgreGangAce,
            Neutral.OgreGangOutlaw,
            Neutral.OgreGangRider,
        };
    }
}
