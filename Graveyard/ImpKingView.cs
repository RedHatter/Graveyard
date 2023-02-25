using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Hearthstone;
using System.Collections.Generic;
using static HearthDb.CardIds;
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
				Condition = card => Imps.Contains(card.Id) || IsEponymousImp(card),
			});
		}

		internal static bool IsEponymousImp(Card card)
		{
			return card.Race == "Demon" && card.Name.Contains("Imp") && card.Id != Neutral.ImprisonedVilefiend;
        }
		
        internal static readonly List<string> Imps = new List<string>
        {
            NonCollectible.Warlock.DarkAlleyPact_FiendToken,
            NonCollectible.Neutral.RustswornCultist_RustedDevilToken,
			NonCollectible.Warlock.NofinsImpossible_ImplocToken, 
			Warlock.ImprisonedScrapImp,
            Warlock.TinyKnightOfEvilCore,
			Warlock.VulgarHomunculus,
			Neutral.StreetTrickster,
			Warlock.UnlicensedApothecary,
			Neutral.SneakyDevil,
			Warlock.EnvoyRustwix,
			Warlock.RingMatron,
        };
    }
}
