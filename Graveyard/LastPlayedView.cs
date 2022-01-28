using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;
using System.Linq;
using System.Windows.Controls;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class LastPlayedView : StackPanel
    {
        public static bool IsValid()
        {
            return HasGreySageParrot || HasSunwingSquawker || HasBrilliantMacaw || HasMonstrousParrot || HasVanessaVanCleef;
        }

        public static bool HasGreySageParrot => Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == Mage.GreySageParrot) > -1;
        public static bool HasSunwingSquawker => Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == Paladin.SunwingSquawker) > -1;
        public static bool HasBrilliantMacaw => Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == Shaman.BrilliantMacaw) > -1;
        public static bool HasMonstrousParrot => Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == Hunter.MonstrousParrot) > -1;
        public static bool HasVanessaVanCleef => Core.Game.Player.PlayerCardList.FindIndex(card => card.Id == Rogue.VanessaVancleefCore) > -1;

        readonly LastCardView GreySageParrot;
        readonly LastCardView SunwingSquawker;
        readonly LastCardView BrilliantMacaw;
        readonly LastCardView MonstrousParrot;
        readonly LastCardView VanessaVanCleef;

        public LastPlayedView()
        {
            CreateViewIf(HasGreySageParrot, ref GreySageParrot, Strings.GetLocalized("GreySageParrot"));
            CreateViewIf(HasSunwingSquawker, ref SunwingSquawker, Strings.GetLocalized("SunwingSquawker"));
            CreateViewIf(HasBrilliantMacaw, ref BrilliantMacaw, Strings.GetLocalized("BrilliantMacaw"));
            CreateViewIf(HasMonstrousParrot, ref MonstrousParrot, Strings.GetLocalized("MonstrousParrot"));
            CreateViewIf(HasVanessaVanCleef, ref VanessaVanCleef, Strings.GetLocalized("VanessaVanCleef"));
        }

        private void CreateViewIf(bool create, ref LastCardView view, string title)
        {
            if (create)
            {
                view = new LastCardView(title);
                Children.Add(view); 
            }
        }

        public bool UpdateGreySageParrot(Card card)
        {
            if (GreySageParrot != null && card.Type == "Spell" && card.Cost >= 5)
            {
                return GreySageParrot.Update(card);
            }
            return false;
        }

        public bool UpdateSunwingSquawker(Card card)
        {
            if (SunwingSquawker != null && card.Type == "Spell" && LadyLiadrinView.SpellList.Contains(card.Id))
            {
                return SunwingSquawker.Update(card);
            }
            return false;
        }

        public bool UpdateBrilliantMacaw(Card card)
        {
            if (BrilliantMacaw != null && card.Mechanics.Contains("Battlecry") && card.Id != Shaman.BrilliantMacaw)
            {
                return BrilliantMacaw.Update(card);
            }
            return false;
        }

        public bool UpdateMonstrousParrot(Card card)
        {
            if (MonstrousParrot != null && card.Mechanics.Contains("Deathrattle") && card.Id != Rogue.UnearthedRaptor)
            {
                return MonstrousParrot.Update(card);
            }
            return false;
        }

        public bool UpdateVanessaVanCleef(Card card)
        {
            if (VanessaVanCleef != null && (card.Type == "Spell" || card.Type == "Minion"))
            {
                return VanessaVanCleef.Update(card); 
            }
            return false;
        }
    }
}
