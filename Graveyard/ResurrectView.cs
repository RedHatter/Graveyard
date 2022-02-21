using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Hearthstone;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
	public class ResurrectView : NormalView
	{
		private static ViewConfig _Config;
		internal static ViewConfig Config
		{
			get => _Config ?? (_Config = new ViewConfig());
		}
		
		private ChancesTracker _chances = new ChancesTracker();

		public static bool isValid()
		{
			return Core.Game.Player.PlayerCardList.FindIndex(card =>
				(card.Id == Neutral.Rally && !RallyView.IsAlwaysSeparate) ||
				card.Id == Priest.RaiseDead ||
                card.Id == Priest.Psychopomp ||
                card.Id == Neutral.BodyWrapper ||
                card.Id == Priest.MassResurrection ||
                card.Id == Priest.CatrinaMuerte ||                
				card.Id == Priest.Resurrect ||
				card.Id == Priest.OnyxBishop ||
				card.Id == Priest.EternalServitude ||
				card.Id == Priest.LesserDiamondSpellstone ||
				(Settings.Default.ResurrectKazakus && card.Id == Neutral.Kazakus)
                ) > -1;
		}

		public ResurrectView()
		{
			// Section Label
			Label.Text = Strings.GetLocalized("Resurrect");
		}

		public bool Update(Card card)
		{
			if (!base.Update(card))
			{
				return false;
			}

			_chances.Update(card, Cards, View);

			return true;
		}
	}
}
