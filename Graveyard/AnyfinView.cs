using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Hearthstone;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
	public class AnyfinView : NormalView
	{
		private static AnyfinViewConfig _Config;
		internal static ViewConfig Config
		{
			get => _Config ?? (_Config = new AnyfinViewConfig(Paladin.AnyfinCanHappen)
			{
				Name = "Anyfin",
				Enabled = "AnyfinEnabled",
				UpdateOn = GameEvents.OnPlayerPlayToGraveyard,
				CreateView = () => new AnyfinView(),
				Condition = card => card.Race == "Murloc" || card.Race == "All",
			});
		}				

		internal class AnyfinViewConfig : ViewConfig
        {
            public AnyfinViewConfig(params string[] showOn) : base(showOn)
            {

            }

            public override void RegisterView(ViewBase view, bool isDefault = false)
            {
                base.RegisterView(view, isDefault);
				RegisterForCardEvent(GameEvents.OnOpponentPlayToGraveyard, view.Update, isDefault);
			}
        }
	}
}
