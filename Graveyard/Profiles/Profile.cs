using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HDT.Plugins.Graveyard.Profiles
{
    internal class DisplaySettings
    {
        public double Left { get; set; }
        public double Top { get; set; }
        public double Scale { get; set; }
        public double Opacity { get; set; }
        public Orientation? Orientation { get; set; }
    }

    internal class ViewSettings
    {
        public bool GraveyardEnabled { get; set; }
        public bool QuestlineEnabled { get; set; }
        public bool DeathrattleEnabled { get; set; }
        public bool? ResurrectEnabled { get; set; }
    }

    internal class PlayerSettings
    {
        public DisplaySettings Display { get; set; }
        public ViewSettings Views { get; set; }
        public string ExcludedCards { get; set; }
    }

    internal class Profile
    {
        public PlayerSettings Player { get; set; }
        public PlayerSettings Opponent { get; set; }

        public Profile() {}

        public Profile(Settings settings)
        {
            Player = new PlayerSettings
            {
                Display = new DisplaySettings
                {
                    Left = settings.PlayerLeft,
                    Top = settings.PlayerTop,
                    Scale = settings.FriendlyScale,
                    Opacity = settings.FriendlyOpacity,
                    Orientation = settings.FriendlyOrientation,
                },
                Views = new ViewSettings
                {
                    GraveyardEnabled = settings.NormalEnabled,
                    QuestlineEnabled = settings.FriendlyQuestlineEnabled,
                    DeathrattleEnabled = settings.DeathrattleEnabled,
                    ResurrectEnabled = settings.ResurrectEnabled,
                },
                ExcludedCards = settings.ExcludedCards,
            };
            Opponent = new PlayerSettings
            {
                Display = new DisplaySettings
                {
                    Left = settings.EnemyLeft,
                    Top = settings.EnemyTop,
                    Scale = settings.EnemyScale,
                    Opacity = settings.EnemyOpacity,
                },
                Views = new ViewSettings
                {
                    GraveyardEnabled = settings.EnemyEnabled,
                    QuestlineEnabled = settings.EnemyQuestlineEnabled,
                    DeathrattleEnabled = settings.OpponentDeathrattleEnabled,
                }
            };
    }
}
