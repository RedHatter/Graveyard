using Hearthstone_Deck_Tracker.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    internal class ImpKingView
    {
		private static ViewConfig _Config;
		internal static ViewConfig Config
		{
			get => _Config ?? (_Config = new ViewConfig(Warlock.ImpKingRafaam)
			{
				Name = "ImpKing",
				CreateView = () => new ChancesView(),
				UpdateOn = GameEvents.OnPlayerPlayToGraveyard,
				Condition = card => card.Race == "Demon" && card.Name.Contains("Imp") && card.Id != Neutral.ImprisonedVilefiend,
			});
		}
	}
}
