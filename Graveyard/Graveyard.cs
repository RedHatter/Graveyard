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

			if (Core.Game.CurrentGameMode == GameMode.Battlegrounds)
			{
				// don't show graveyard for Battlegrounds
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

			if (Settings.Default.ResurrectEnabled && ResurrectView.isValid())
			{
				Resurrect = new ResurrectView();
				_friendlyPanel.Children.Add(Resurrect);
				Normal = null;
			}
			else if (Settings.Default.NormalEnabled)
			{
				Normal = new NormalView();
				_friendlyPanel.Children.Add(Normal);
				Resurrect = null;
			}
			else
			{
				Normal = null;
				Resurrect = null;
			}

			if (Settings.Default.AnyfinEnabled && AnyfinView.isValid())
			{
				Anyfin = new AnyfinView();
				_friendlyPanel.Children.Add(Anyfin);
			}
			else
			{
				Anyfin = null;
			}

            if (Settings.Default.DeathrattleEnabled && DeathrattleView.isValid())
            {
                Deathrattle = new DeathrattleView();
                _friendlyPanel.Children.Add(Deathrattle);
            }
            else
            {
                Deathrattle = null;
            }

            if (Settings.Default.NZothEnabled && NZothView.isValid())
			{
				NZoth = new NZothView();
				_friendlyPanel.Children.Add(NZoth);
			}
			else
			{
				NZoth = null;
			}

			if (Settings.Default.HadronoxEnabled && HadronoxView.isValid())
			{
				Hadronox = new HadronoxView();
				_friendlyPanel.Children.Add(Hadronox);
			}
			else
			{
				Hadronox = null;
			}

			if (Settings.Default.DiscardEnabled && DiscardView.isValid())
			{
				Discard = new DiscardView();
				_friendlyPanel.Children.Add(Discard);
			}
			else
			{
				Discard = null;
			}

			if (Settings.Default.ShudderwockEnabled && ShudderwockView.isValid())
			{
				Shudderwock = new ShudderwockView();
				_friendlyPanel.Children.Add(Shudderwock);
			}
			else
			{
				Shudderwock = null;
			}

			if (Settings.Default.GuldanEnabled && GuldanView.isValid())
			{
				Guldan = new GuldanView();
				_friendlyPanel.Children.Add(Guldan);
			}
			else
			{
				Guldan = null;
			}
			if (Settings.Default.DragoncallerAlannaEnabled && DragoncallerAlannaView.isValid())
			{
				DragoncallerAlanna = new DragoncallerAlannaView();
				_friendlyPanel.Children.Add(DragoncallerAlanna);
			}
			else
			{
				DragoncallerAlanna = null;
			}
            if (Settings.Default.MulchmuncherEnabled && MulchmuncherView.isValid())
            {
                Mulchmuncher = new MulchmuncherView();
                _friendlyPanel.Children.Add(Mulchmuncher);
            }
            else
            {
                Mulchmuncher = null;
            }

            if (Settings.Default.CavernsEnabled && CavernsView.isValid())
            {
                Caverns = new CavernsView();
                _friendlyPanel.Children.Add(Caverns);
            }
            else
            {
                Caverns = null;
            }
            if (Settings.Default.KangorEnabled && KangorView.isValid())
            {
                Kangor = new KangorView();
                _friendlyPanel.Children.Add(Kangor);
            }
            else
            {
                Kangor = null;
            }
            if (Settings.Default.WitchingHourEnabled && WitchingHourView.isValid())
            {
                WitchingHour = new WitchingHourView();
                _friendlyPanel.Children.Add(WitchingHour);
            }
            else
            {
                WitchingHour = null;
            }
            if (Settings.Default.SoulwardenEnabled && SoulwardenView.isValid())
            {
                Soulwarden = new SoulwardenView();
                _friendlyPanel.Children.Add(Soulwarden);
            }
            else
            {
                Soulwarden = null;
            }
            if (Settings.Default.TessGreymaneEnabled && TessGreymaneView.isValid())
            {
                TessGreymane = new TessGreymaneView();
                _friendlyPanel.Children.Add(TessGreymane);
            }
            else
            {
                TessGreymane = null;
            }
			if (Settings.Default.ZuljinEnabled && ZuljinView.isValid())
			{
				Zuljin = new ZuljinView();
				_friendlyPanel.Children.Add(Zuljin);
			}
			else
			{
				Zuljin = null;	
			}
			if (Settings.Default.HoardPillagerEnabled && HoardPillagerView.isValid())
			{
				HoardPillager = new HoardPillagerView();
				_friendlyPanel.Children.Add(HoardPillager);
			}
			else
			{
				HoardPillager = null;
			}
			if (Settings.Default.LadyLiadrinEnabled && LadyLiadrinView.isValid())
            {
				LadyLiadrin = new LadyLiadrinView();
				_friendlyPanel.Children.Add(LadyLiadrin);
            }
            else
            {
				LadyLiadrin = null;
            }
            if (Settings.Default.NZothGotDEnabled && NZothGotDView.isValid())
            {
				NZothGotD = new NZothGotDView();
				_friendlyPanel.Children.Add(NZothGotD);
            }
			else
			{
				NZothGotD = null;
			}
			if (Settings.Default.RallyEnabled && RallyView.isValid())
            {
				Rally = new RallyView();
				_friendlyPanel.Children.Add(Rally);
            }
			else
			{
				Rally = null;
			}
			if (Settings.Default.SaurfangEnabled && SaurfangView.isValid())
            {
				Saurfang = new SaurfangView();
				_friendlyPanel.Children.Add(Saurfang);
            }
			else
			{
				Saurfang = null;
			}
			if (Settings.Default.YShaarjEnabled && YShaarjView.isValid())
            {
				YShaarj = new YShaarjView();
				_friendlyPanel.Children.Add(YShaarj);
            }
			else
			{
				YShaarj = null;
			}
			if (Settings.Default.ElwynnBoarEnabled && ElwynnBoarView.isValid())
            {
				ElwynnBoar = new ElwynnBoarView();
				_friendlyPanel.Children.Add(ElwynnBoar);
            }
			else
			{
				ElwynnBoar = null;
			}
			if (Settings.Default.KargalEnabled && KargalView.isValid())
            {
				Kargal = new KargalView();
				_friendlyPanel.Children.Add(Kargal);
			}
			else
			{
				Kargal = null;
			}
			if (Settings.Default.AntonidasEnabled && AntonidasView.isValid())
            {
				Antonidas = new AntonidasView();
				_friendlyPanel.Children.Add(Antonidas);
            }
            else
            {
				Antonidas = null;
            }
            if (Settings.Default.GrandFinaleEnabled && GrandFinaleView.isValid())
            {
				GrandFinale = new GrandFinaleView();
				_friendlyPanel.Children.Add(GrandFinale);
            }
            else
            {
				GrandFinale = null;
            }
			if (Settings.Default.LastPlayedEnabled && LastPlayedView.IsValid())
            {
				LastPlayed = new LastPlayedView();
				_friendlyPanel.Children.Add(LastPlayed);
            }
			else
            {
				LastPlayed = null;
            }
			if (Settings.Default.MulticasterEnabled && MulticasterView.IsValid())
            {
				Multicaster = new MulticasterView();
				_friendlyPanel.Children.Add(Multicaster);
            }
			else
            {
				Multicaster = null;
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