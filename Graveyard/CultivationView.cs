using Hearthstone_Deck_Tracker.API;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class CultivationView
    {
        private static ViewConfig _Config;
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new ViewConfig(Druid.Cultivation)
            {
                ShowFirst = () => true,
                CreateView = () => new NormalView(),
                UpdateOn = GameEvents.OnPlayerPlay,
                Condition = card => card.Name.Contains("Treant"),
            });
        }

        internal class ViewConfig : Plugins.Graveyard.ViewConfig
        {
            public ViewConfig(params string[] showOn) : base(showOn) { }
            public override void RegisterView(ViewBase view, bool isDefault = false)
            {
                base.RegisterView(view, isDefault);
                Plugin.Events.OnPlayerCreateInPlay.Register(view.Update);
            }
        }
    }
}
