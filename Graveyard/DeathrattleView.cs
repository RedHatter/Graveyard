using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;
using System.Linq;
using Collectible = HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class DeathrattleView : NormalView
    {
        public static bool isValid()
        {
            return Core.Game.Player.PlayerCardList.FindIndex(card =>
                card.Id == Collectible.Priest.XyrellaTheDevout ||
                card.Id == Collectible.Warlock.TamsinsPhylactery ||
                card.Id == Collectible.Priest.AmuletOfUndying ||
                card.Id == Collectible.Rogue.CounterfeitBlade ||
                card.Id == Collectible.Hunter.JewelOfNzoth ||
                card.Id == Collectible.Neutral.Vectus ||
                card.Id == Collectible.Hunter.NineLives ||
                card.Id == Collectible.Neutral.DaUndatakah ||
                card.Id == Collectible.Priest.TwilightsCall                
                ) > -1;
        }

        public DeathrattleView()
        {
            // Section Label
            Label.Text = Strings.GetLocalized("Deathrattle");
        }

        public bool Update(Card card)
        {
            return card.Mechanics.Contains("Deathrattle") && card.Id != Collectible.Rogue.UnearthedRaptor && base.Update(card);
        }
    }
}
