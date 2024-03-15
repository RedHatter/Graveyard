using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;
using System;
using System.Windows;
using System.Windows.Controls;

namespace HDT.Plugins.Graveyard
{
    public abstract class ViewBase : StackPanel
    {
        public void InitialiseDisplay()
        {
            Visibility = Visibility.Collapsed;
            Orientation = Orientation.Vertical;
        }
        
        public virtual HearthstoneTextBlock AddTitle(string text = "")
        {
            var title = new HearthstoneTextBlock
            {
                FontSize = 16,
                TextAlignment = TextAlignment.Center,
                Text = text,
                Margin = new Thickness(0, 20, 0, 0),
            };
            Children.Insert(0, title);
            return title;
        }
        public string Title 
        {
            get
            {
                if (_Title == null)
                {
                    _Title = AddTitle("<Not Set>");
                }
                return _Title.Text;
            }
            set
            {
                if (_Title == null)
                {
                    _Title = AddTitle(value);
                }
                else
                {
                    _Title.Text = value;
                }
            }
        }
        private HearthstoneTextBlock _Title;

        public Predicate<Card> Condition { get; set; }

        public abstract bool Update(Card card);
    }
}
