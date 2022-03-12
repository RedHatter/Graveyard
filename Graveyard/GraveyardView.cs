using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Hearthstone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDT.Plugins.Graveyard
{
    internal class GraveyardView
    {
		private static readonly string SharedName = "Graveyard";
		private static readonly Func<ViewBase> SharedCreateView = () => new NormalView();
		private static readonly Predicate<Card> SharedCondition = card => card.Type == "Minion";

		internal static ViewConfig FriendlyConfig
		{
			get => _FriendlyConfig ?? (_FriendlyConfig = new ViewConfig()
			{
				Name = SharedName,
				Enabled = () => Settings.Default.NormalEnabled,
				CreateView = SharedCreateView,
				UpdateOn = GameEvents.OnPlayerPlayToGraveyard,
				Condition = SharedCondition,
			});
		}
		private static ViewConfig _FriendlyConfig;

		internal static ViewConfig EnemyConfig
		{
			get => _EnemyConfig ?? (_EnemyConfig = new ViewConfig()
			{
				Name = SharedName,
				Enabled = () => Settings.Default.EnemyEnabled,
				CreateView = SharedCreateView,
				UpdateOn = GameEvents.OnOpponentPlayToGraveyard,
				Condition = SharedCondition,
			});
		}
		private static ViewConfig _EnemyConfig;
	}
}
