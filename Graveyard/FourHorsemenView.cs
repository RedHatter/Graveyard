using Hearthstone_Deck_Tracker.API;
using System.Collections.Generic;
using Collectible = HearthDb.CardIds.Collectible.Neutral;
using NonCollectible = HearthDb.CardIds.NonCollectible.Neutral;

namespace HDT.Plugins.Graveyard
{
    internal class FourHorsemenView
    {        
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new ViewConfig(Collectible.RivendareWarrider)
            {
                Name = "FourHorsemen",
                CreateView = () => new NormalView(),
                UpdateOn = GameEvents.OnPlayerPlayToGraveyard,
                Condition = card => Horsemen.Contains(card.Id),
                UseSoloCardNameAsTitle = false,
            });
        }
        private static ViewConfig _Config;

        internal static readonly List<string> Horsemen = new List<string>
        {
            Collectible.RivendareWarrider,
            NonCollectible.RivendareWarrider_BlaumeuxFamineriderToken,
            NonCollectible.RivendareWarrider_KorthazzDeathriderToken,
            NonCollectible.RivendareWarrider_ZeliekConquestriderToken,
        };
    }
}
