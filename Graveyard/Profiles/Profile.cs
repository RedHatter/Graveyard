using System.Windows.Controls;

namespace HDT.Plugins.Graveyard.Profiles
{
    public class DisplayProfile : ProfileSection
    {
        public double Left { get => _Left; set => SetValue(ref _Left, value); }
        private double _Left;
        public double Top { get => _Top; set => SetValue(ref _Top, value); }
        private double _Top;
        public double Scale { get => _Scale; set => SetValue(ref _Scale, value); }
        private double _Scale;
        public double Opacity { get => _Opacity; set => SetValue(ref _Opacity, value); }
        private double _Opacity;
        public Orientation? Orientation { get => _Orientation; set => SetValue(ref _Orientation, value); }
        private Orientation? _Orientation;
    }

    public class ViewProfile : ProfileSection
    {
        public bool Graveyard { get => _Graveyard; set => SetValue(ref _Graveyard, value); }
        private bool _Graveyard;
        public bool Questline { get => _Questline; set => SetValue(ref _Questline, value); }
        private bool _Questline;
        public bool Deathrattle { get => _Deathrattle; set => SetValue(ref _Deathrattle, value); }
        private bool _Deathrattle;
        public bool? Resurrect { get => _Resurrect; set => SetValue(ref _Resurrect, value); }
        private bool? _Resurrect;
    }

    public class PlayerProfile : ProfileSection
    {
        public DisplayProfile Display { get => _Display; set => SetValue(ref _Display, value); }
        private DisplayProfile _Display;
        public ViewProfile Views { get => _Views; set => SetValue(ref _Views, value); }
        private ViewProfile _Views;
        public string ExcludedCards { get => _ExcludedCards; set => SetValue(ref _ExcludedCards, value); }
        private string _ExcludedCards;

        public PlayerProfile()
        {

        }
    }

    public class Profile : ProfileSection
    {
        public PlayerProfile Player { get => _Player; set => SetValue(ref _Player, value); }
        private PlayerProfile _Player;
        public PlayerProfile Opponent { get => _Opponent; set => SetValue(ref _Opponent, value); }
        private PlayerProfile _Opponent;

        public Profile() 
        {
        }

        static internal Profile CreateDefault(Settings settings)
        {
            return new Profile
            {
                Player = new PlayerProfile
                {
                    Display = new DisplayProfile
                    {
                        Left = settings.PlayerLeft,
                        Top = settings.PlayerTop,
                        Scale = settings.FriendlyScale,
                        Opacity = settings.FriendlyOpacity,
                        Orientation = settings.FriendlyOrientation,
                    },
                    Views = new ViewProfile
                    {
                        Graveyard = settings.NormalEnabled,
                        Questline = settings.FriendlyQuestlineEnabled,
                        Deathrattle = settings.DeathrattleEnabled,
                        Resurrect = settings.ResurrectEnabled,
                    },
                    ExcludedCards = settings.ExcludedCards,
                },
                Opponent = new PlayerProfile
                {
                    Display = new DisplayProfile
                    {
                        Left = settings.EnemyLeft,
                        Top = settings.EnemyTop,
                        Scale = settings.EnemyScale,
                        Opacity = settings.EnemyOpacity,
                    },
                    Views = new ViewProfile
                    {
                        Graveyard = settings.EnemyEnabled,
                        Questline = settings.EnemyQuestlineEnabled,
                        Deathrattle = settings.OpponentDeathrattleEnabled,
                    }
                }
            };
        }
    }
}
