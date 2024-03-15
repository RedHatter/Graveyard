using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;
using System.Windows;

namespace HDT.Plugins.Graveyard
{
    public class ChancesView : NormalView
    {
        private readonly ChancesTracker Chances = new ChancesTracker();

        public ChancesView() : base()
        {
            Margin = new Thickness(Margin.Left + Settings.Default.ChancesViewLeft, Margin.Top, Margin.Right, Margin.Right);
        }

        public override HearthstoneTextBlock AddTitle(string text = "")
        {
            var label = base.AddTitle(text);
            label.Margin = new Thickness(label.Margin.Left - Settings.Default.ChancesViewLeft, label.Margin.Top, label.Margin.Right, label.Margin.Right);
            return label;
        }

        public override bool Update(Card card)
        {
            if (base.Update(card))
            {
                Chances.Update(card, Cards, View);
                return true;
            }
            return false;
        }
    }
}
