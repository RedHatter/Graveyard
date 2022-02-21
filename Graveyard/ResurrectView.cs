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
			get => _Config ?? (_Config = new ViewConfig(
				Neutral.Rally, 
				Priest.RaiseDead, 
				Priest.Psychopomp, 
				Neutral.BodyWrapper, 
				Priest.MassResurrection, 
				Priest.CatrinaMuerte,
				Priest.Resurrect,
				Priest.OnyxBishop,
				Priest.EternalServitude,
				Priest.LesserDiamondSpellstone,
				Neutral.Kazakus)
            {
				Name = Strings.GetLocalized("Resurrect"),
				Enabled = () => Settings.Default.ResurrectEnabled,
				Condition = card => card.Type == "Minion",
			});
		}
		
		private ChancesTracker _chances = new ChancesTracker();

		public static bool isValid()
		{
			return Core.Game.Player.PlayerCardList.FindIndex(card => Config.ShowOn.Contains(card.Id)) > -1;
		}

		public ResurrectView()
		{
			// Section Label
			Label.Text = Config.Name;
		}

		public bool Update(Card card)
		{
			if (Config.Condition(card) && base.Update(card))
			{
				_chances.Update(card, Cards, View);

				return true; 
			}
			return false;
		}
	}
}
