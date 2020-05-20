using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;

namespace HDT.Plugins.Graveyard
{
    public class MulchmuncherView : NormalView
    {
        public static bool isValid()
        {
            return Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == HearthDb.CardIds.Collectible.Druid.Mulchmuncher) > -1;
        }

        public MulchmuncherView()
        {
            // Section Label
            Label.Text = Strings.GetLocalized("Mulchmuncher");
        }

        public bool Update(Card card)
        {
            return card.Name == "Treant" && base.Update(card);
        }
    }
}
