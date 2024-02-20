using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Hearthstone;
using System.Linq;
using System.Windows;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    internal class FizzleView : NormalView
    {
        private static ViewConfig _Config;
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new ViewConfig(FizzleId)
            {
                CreateView = () => new FizzleView(),
                UpdateOn = GameEvents.OnPlayerPlay,
                Condition = card => card.Id == FizzleId || card.Id == SnapshotId,
            });
        }

        private static readonly string FizzleId = Neutral.PhotographerFizzle;
        private static readonly string SnapshotId = HearthDb.CardIds.NonCollectible.Neutral.PhotographerFizzle_FizzlesSnapshotToken;

        public override bool Update(Card card)
        {
            if (!(card.Id == FizzleId || card.Id == SnapshotId)) return false;

            if (card.Id == FizzleId)
            {
                Cards.AddRange(Core.Game.Player.Hand.Select(e => Database.GetCardFromId(e.CardId)));
            }
            else if (card.Id == SnapshotId)
            {
                Cards.Clear();
            }
            View.Update(Cards, false);
            
            Visibility = Visibility.Visible;

            return true;
        }
    }
}
