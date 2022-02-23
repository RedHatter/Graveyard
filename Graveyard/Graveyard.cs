using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Card = Hearthstone_Deck_Tracker.Hearthstone.Card;
using Core = Hearthstone_Deck_Tracker.API.Core;
using GameMode = Hearthstone_Deck_Tracker.Enums.GameMode;


namespace HDT.Plugins.Graveyard
{
    public class Graveyard
	{
		// The views
		public NormalView Normal;
		public NormalView Enemy;

		public QuestlineView FriendlyQuestline;
		public QuestlineView EnemyQuestline;

		public ResurrectView Resurrect;
		public AnyfinView Anyfin;
        public DeathrattleView Deathrattle;
        public NZothView NZoth;
		public HadronoxView Hadronox;
		public DiscardView Discard;
		public GuldanView Guldan;
		public ShudderwockView Shudderwock;
		public DragoncallerAlannaView DragoncallerAlanna;
        public CavernsView Caverns;
        public MulchmuncherView Mulchmuncher;
        public KangorView Kangor;
        public WitchingHourView WitchingHour;
        public TessGreymaneView TessGreymane;
        public SoulwardenView Soulwarden;
		public ZuljinView Zuljin;
		public HoardPillagerView HoardPillager;
		public LadyLiadrinView LadyLiadrin;
		public NZothGotDView NZothGotD;
		public RallyView Rally;
		public SaurfangView Saurfang;
		public YShaarjView YShaarj;
		public ElwynnBoarView ElwynnBoar;
		public KargalView Kargal;
		public AntonidasView Antonidas;
		public GrandFinaleView GrandFinale;
		public LastPlayedView LastPlayed;
		public MulticasterView Multicaster;
		public ShirvallahView Shirvallah;

		private StackPanel _friendlyPanel;
		private StackPanel _enemyPanel;

		public static InputManager Input;

        public Graveyard()
		{

			// Create container
			_enemyPanel = new StackPanel();
			_enemyPanel.Orientation = Orientation.Vertical;
			Core.OverlayCanvas.Children.Add(_enemyPanel);
			Canvas.SetTop(_enemyPanel, Settings.Default.EnemyTop);
			Canvas.SetLeft(_enemyPanel, Settings.Default.EnemyLeft);

			// Create container
			_friendlyPanel = new StackPanel();
			_friendlyPanel.Orientation = Settings.Default.FriendlyOrientation;
			Core.OverlayCanvas.Children.Add(_friendlyPanel);
			Canvas.SetTop(_friendlyPanel, Settings.Default.PlayerTop);
			Canvas.SetLeft(_friendlyPanel, Settings.Default.PlayerLeft);

			Input = new InputManager(_friendlyPanel, _enemyPanel);

			Settings.Default.PropertyChanged += SettingsChanged;
			SettingsChanged(null, null);
		}

        //on year change clear out the grid and update the data
        private void SettingsChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			_friendlyPanel.Orientation = Settings.Default.FriendlyOrientation;
			_friendlyPanel.RenderTransform = new ScaleTransform(Settings.Default.FriendlyScale / 100, Settings.Default.FriendlyScale / 100);
			_friendlyPanel.Opacity = Settings.Default.FriendlyOpacity / 100;
			_enemyPanel.RenderTransform = new ScaleTransform(Settings.Default.EnemyScale / 100, Settings.Default.EnemyScale / 100);
			_enemyPanel.Opacity = Settings.Default.EnemyOpacity / 100;
		}

		public void Dispose()
		{
			Core.OverlayCanvas.Children.Remove(_friendlyPanel);
			Core.OverlayCanvas.Children.Remove(_enemyPanel);
			Input.Dispose();
		}

		/**
		* Clear then recreate all Views.
		*/
		public void Reset()
		{
			_friendlyPanel.Children.Clear();
			_enemyPanel.Children.Clear();

			if (Core.Game.IsBattlegroundsMatch || Core.Game.IsMercenariesMatch)
			{
				// don't show graveyard for Battlegrounds or Mercenaries
				// this should include spectating
				return;
			}

			if (Settings.Default.EnemyQuestlineEnabled)
			{
				EnemyQuestline = new QuestlineView();
				_enemyPanel.Children.Add(EnemyQuestline);
			}
			else
			{
				EnemyQuestline = null;
			}
			if (Settings.Default.EnemyEnabled)
			{
				Enemy = new NormalView();
				_enemyPanel.Children.Add(Enemy);
			}
			else
			{
				Enemy = null;
			}
			if (Settings.Default.FriendlyQuestlineEnabled)
			{
				FriendlyQuestline = new QuestlineView();
				_friendlyPanel.Children.Add(FriendlyQuestline);
			}
			else
			{
				FriendlyQuestline = null;
			}
			if (!ShowFriendlyView(ResurrectView.Config, ref Resurrect) && Settings.Default.NormalEnabled)
			{
				Normal = new NormalView();
				_friendlyPanel.Children.Add(Normal);
			}
			else
			{
				Normal = null;
			}
			ShowFriendlyView(AnyfinView.Config, ref Anyfin);
			ShowFriendlyView(DeathrattleView.Config, ref Deathrattle);
			ShowFriendlyView(NZothView.Config, ref NZoth);
			ShowFriendlyView(HadronoxView.Config, ref Hadronox);
			ShowFriendlyView(DiscardView.Config, ref Discard);
			ShowFriendlyView(ShudderwockView.Config, ref Shudderwock);
			ShowFriendlyView(GuldanView.Config, ref Guldan);
			ShowFriendlyView(DragoncallerAlannaView.Config, ref DragoncallerAlanna);
			ShowFriendlyView(MulchmuncherView.Config, ref Mulchmuncher);
			ShowFriendlyView(CavernsView.Config, ref Caverns);
			ShowFriendlyView(KangorView.Config, ref Kangor);
			ShowFriendlyView(WitchingHourView.Config, ref WitchingHour);
			ShowFriendlyView(SoulwardenView.Config, ref Soulwarden);
			ShowFriendlyView(TessGreymaneView.Config, ref TessGreymane);
			ShowFriendlyView(ZuljinView.Config, ref Zuljin);
			ShowFriendlyView(HoardPillagerView.Config, ref HoardPillager);
			ShowFriendlyView(LadyLiadrinView.Config, ref LadyLiadrin);
			ShowFriendlyView(NZothGotDView.Config, ref NZothGotD);
			ShowFriendlyView(RallyView.Config, ref Rally);
			ShowFriendlyView(SaurfangView.Config, ref Saurfang);
			ShowFriendlyView(YShaarjView.Config, ref YShaarj);
			ShowFriendlyView(ElwynnBoarView.Config, ref ElwynnBoar);
			ShowFriendlyView(KargalView.Config, ref Kargal);
			ShowFriendlyView(AntonidasView.Config, ref Antonidas);
			ShowFriendlyView(GrandFinaleView.Config, ref GrandFinale);
			ShowFriendlyView(LastPlayedView.Config, ref LastPlayed);
			ShowFriendlyView(MulticasterView.Config, ref Multicaster);
			ShowFriendlyView(ShirvallahView.Config, ref Shirvallah);
		}

		private bool ShowFriendlyView<T>(ViewConfig config, ref T view) where T : UIElement, new()
        {
			if(config.Enabled() && Core.Game.Player.PlayerCardList.FindIndex(card => config.ShowOn.Contains(card.Id)) > -1)
            {
				view = new T();
				_friendlyPanel.Children.Add(view);
				return true;
			}
            else
            {
				view = null;
				return false;
            }
        }

		public void PlayerGraveyardUpdate(Card card)
		{
			var any = Anyfin?.Update(card) ?? false;
            var deathrattle = Deathrattle?.Update(card) ?? false;
			LastPlayed?.UpdateMonstrousParrot(card);
            var nzoth = NZoth?.Update(card) ?? false;
			var hadr = Hadronox?.Update(card) ?? false;
			var guldan = Guldan?.Update(card) ?? false;
			var rez = Resurrect?.Update(card) ?? false;
            var mulch = Mulchmuncher?.Update(card) ?? false;
            var kangor = Kangor?.Update(card) ?? false;
            var witching = WitchingHour?.Update(card) ?? false;
			var hoardpillager = HoardPillager?.Update(card) ?? false;
			var nzothgotd = NZothGotD?.Update(card) ?? false;
			var rally = ((rez && RallyView.IsAlwaysSeparate) || !rez) && (Rally?.Update(card) ?? false);
			var saurfang = Saurfang?.Update(card) ?? false;
			var elwynnboar = ((deathrattle && ElwynnBoarView.IsAlwaysSeparate) || !deathrattle) && (ElwynnBoar?.Update(card) ?? false);
            if (!(any || deathrattle || nzoth || hadr || guldan || rez || mulch || kangor || witching || hoardpillager || nzothgotd || rally || saurfang || elwynnboar))
			{
				Normal?.Update(card);
			}
		}

		public void EnemyDamageUpdate(Card card)
		{
			Anyfin?.Update(card);
		}

		public void PlayerDamageUpdate(Card card)
		{
			Anyfin?.Update(card);
		}

		public void EnemyGraveyardUpdate(Card card)
		{
			Anyfin?.Update(card);
			Enemy?.Update(card);
		}

		public void PlayerDiscardUpdate(Card card)
		{
			Discard?.Update(card);
            Soulwarden?.Update(card);
		}

		public void PlayerPlayUpdate(Card card)
		{
			FriendlyQuestline?.Update(card);
			Shudderwock?.Update(card);
			LastPlayed?.UpdateBrilliantMacaw(card);
			DragoncallerAlanna?.Update(card);
			LastPlayed?.UpdateGreySageParrot(card);
            Caverns?.Update(card);
            TessGreymane?.Update(card);
			Zuljin?.Update(card);
			LadyLiadrin?.Update(card);
			LastPlayed?.UpdateSunwingSquawker(card);
			YShaarj?.Update(card);
			Kargal?.Update(card);
			Antonidas?.Update(card);
			GrandFinale?.Update(card);
			Multicaster?.Update(card);
			Shirvallah?.Update(card);
		}

		public void OpponentPlayUpdate(Card card)
        {
			EnemyQuestline?.Update(card);
			LastPlayed?.UpdateVanessaVanCleef(card);
        }

		public async void TurnStartUpdate(Hearthstone_Deck_Tracker.Enums.ActivePlayer player)
		{
			if (player == Hearthstone_Deck_Tracker.Enums.ActivePlayer.Opponent)
            {
				if (Antonidas != null) await Antonidas.TurnEnded();
				if (GrandFinale != null) await GrandFinale.TurnEnded();
			}
		}
	}
}