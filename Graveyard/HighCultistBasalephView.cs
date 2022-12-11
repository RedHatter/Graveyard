using Hearthstone_Deck_Tracker.API;
using System.Threading.Tasks;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class HighCultistBasalephView : NormalView
    {
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new ViewConfig(Priest.HighCultistBasaleph)
            {
                Name = "HighCultistBasaleph",
                CreateView = () => new HighCultistBasalephView(),
                UpdateOn = GameEvents.OnPlayerPlayToGraveyard,
                Condition = card => card.Race == "Undead" || card.Race == "All",
            });
        }
        private static ViewConfig _Config;

        internal class ViewConfig : Plugins.Graveyard.ViewConfig
        {
            public ViewConfig(params string[] showOn) : base(showOn)
            {

            }

            public override void RegisterView(ViewBase view, bool isDefault = false)
            {
                base.RegisterView(view, isDefault);
                if (view is HighCultistBasalephView basalephView)
                {
                    Plugin.Events.OnOpponentTurnStart.Register(basalephView.PlayerTurnEnded);
                }
            }
        }

        private async Task PlayerTurnEnded()
        {
            Cards.Clear();
            View.Items.Clear();
            await Task.CompletedTask;
            return;
        }
    }
}
