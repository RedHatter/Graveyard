using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Hearthstone;
using System.Windows;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    internal class AzeriteRatView : NormalView
    {
        private static ViewConfig _Config;
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new ViewConfig( 
                Deathknight.ReapWhatYouSow,
                Deathknight.SkeletonCrew)
            {
                Name = Database.GetCardFromId(HearthDb.CardIds.NonCollectible.Deathknight.KoboldMiner_TheAzeriteRatToken)?.LocalizedName ?? "AzeriteRat",
                CreateView = () => new AzeriteRatView(),
                UpdateOn = GameEvents.OnPlayerPlayToGraveyard,
                Condition = card => card.Type == "Minion",
            });
        }

        public override bool Update(Card card)
        {
            if (!Condition(card)) return false;

            var removed = Cards.RemoveAll(c => c.Cost < card.Cost);
            if (removed > 0 || Cards.Count == 0)
            {
                Cards.Add(card.Clone() as Card);
                View.Update(Cards, true);
                Visibility = Visibility.Visible;
            }
#warning Always return false to pass card onto GraveyardView, other unwanted side effects?
            return false; 
        }
    }
}
