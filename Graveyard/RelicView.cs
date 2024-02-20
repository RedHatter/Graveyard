using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Hearthstone;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    internal class RelicView : NormalView
    {
        private static ViewConfig _Config;
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new ViewConfig(ShowOn.ToArray())
            {
                Name = "Relic",
                CreateView = () => new RelicView(),
                UpdateOn = GameEvents.OnPlayerPlay,
                Condition = card => Relics.Contains(card.Id),
            });
        }

        private readonly string RelicsofOldId = "REV_942e2";
        private bool RelicsofOldWaiting = false;
        private bool RelicsofOldCheck(Card card)
        {
            if (card.Id == RelicsofOldId)
            {
                RelicsofOldWaiting = true;
                return true;
            }
            return false;
        }

        private bool RelicsofOldUncheck(Card card)
        {
            if (card.Id == RelicsofOldId)
            {
                RelicsofOldWaiting = false;
                return true;
            }
            return false;
        }

        private bool RelicsCheck(Card card)
        {
            if (RelicsofOldCheck(card)) return true;
            if (Config.Condition(card))
            {
                UpdateList(card);
                return true;
            }
            return false;
        }

        public override bool Update(Card card)
        {
            if (!Condition(card)) return false;

            UpdateList(card, RelicsofOldWaiting ? 2 : 1);

            return true;
        }

        private void UpdateList(Card card, int countToAdd = 1)
        {
            var match = Cards.FirstOrDefault(c => c.Name == card.Name);
            if (match != null)
            {
                match.Count += countToAdd;
            }
            else
            {
                var clone = card.Clone() as Card;
                clone.Count = countToAdd;
                Cards.Add(clone);
            }
            View.Update(Cards, false);

            Visibility = Visibility.Visible;
        }

        internal static readonly List<string> Relics = new List<string>
        {
            Demonhunter.RelicOfExtinction,
            Demonhunter.RelicOfPhantasms,
            Demonhunter.RelicOfDimensions,
        };

        internal static readonly List<string> ShowOn = Relics.Append(Demonhunter.ArtificerXymox).ToList();               

        internal class ViewConfig : Plugins.Graveyard.ViewConfig
        {
            public ViewConfig(params string[] showOn) : base(showOn)
            {

            }

            public override void RegisterView(ViewBase view, bool isDefault = false)
            {
                base.RegisterView(view, isDefault);
                if (view is RelicView relicView)
                {
                    Plugin.Events.OnPlayerCreateInPlay.Register(relicView.RelicsCheck);
                    Plugin.Events.OnPlayerPlayToGraveyard.Register(relicView.RelicsofOldUncheck);
                }
            }
        }

        
    }
}
