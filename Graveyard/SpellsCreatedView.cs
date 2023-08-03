﻿using Hearthstone_Deck_Tracker.API;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    internal class SpellsCreatedView
    {
        private static ViewConfig _Config;
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new ViewConfig(Mage.GrandMagisterRommath)
            {
                CreateView = () => new NormalView(),
                UpdateOn = GameEvents.OnPlayerGet,
                Condition = card => card.Type == "Spell",
            });
        }
    }
}
