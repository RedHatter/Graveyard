using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;
using static HearthDb.CardIds.Collectible;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static HDT.Plugins.Graveyard.AntonidasView;

namespace HDT.Plugins.Graveyard
{
    public class GrandFinaleView : MultiTurnView
    {
        private static ViewConfig _Config;
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new ViewConfig(Mage.GrandFinale)
            {
                Name = Strings.GetLocalized("GrandFinale"),
                Enabled = () => Settings.Default.GrandFinaleEnabled,
                Condition = card => card.Race == "Elemental" || card.Race == "All",
            });
        }
        
        public static bool isValid()
        {
            return Core.Game.Player.PlayerCardList.FindIndex(card => Config.ShowOn.Contains(card.Id)) > -1;
        }
        public GrandFinaleView() 
            : base(Config.Name,1)
        {
        }

        public override bool Update(Card card)
        {
            if (Config.Condition(card))
            {
                return base.Update(card);
            }
            return false;
        }
    }
}
