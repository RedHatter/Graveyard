using Hearthstone_Deck_Tracker.API;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    internal class JaceDarkweaverView
    {
		private static ViewConfig _Config;
		internal static ViewConfig Config
		{
			get => _Config ?? (_Config = new ViewConfig(Demonhunter.JaceDarkweaver)
			{
				Name = Strings.GetLocalized("Jace"),
				Enabled = () => Settings.Default.JaceEnabled,
				CreateView = () => new NormalView(),
				UpdateOn = GameEvents.OnPlayerPlay,
				Condition = card => card.GetSchool() == School.Fel,
			});
		}
	}
}
