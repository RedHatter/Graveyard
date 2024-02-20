using System;
using System.Windows.Controls;

namespace HDT.Plugins.Graveyard
{
    public sealed partial class Settings
    {
        private int GetDefaultIntValue(string key) => int.Parse(Properties[key].DefaultValue.ToString());
        private double GetDefaultDblValue(string key) => double.Parse(Properties[key].DefaultValue.ToString());
        public double DefaultPlayerLeft => GetDefaultDblValue(nameof(PlayerLeft));
        public double DefaultPlayerTop => GetDefaultDblValue(nameof(PlayerTop));
        public double DefaultEnemyLeft => GetDefaultDblValue(nameof(EnemyLeft));
        public double DefaultEnemyTop => GetDefaultDblValue(nameof(EnemyTop));
        public double DefaultFriendlyOpacity => GetDefaultDblValue(nameof(FriendlyOpacity));
        public double DefaultFriendlyScale => GetDefaultDblValue(nameof(FriendlyScale));
        public Orientation DefaultFriendlyOrientation => (Orientation)Enum.Parse(typeof(Orientation), Properties[nameof(FriendlyOrientation)].DefaultValue.ToString());
        public double DefaultEnemyOpacity => GetDefaultDblValue(nameof(EnemyOpacity));
        public double DefaultEnemyScale => GetDefaultDblValue(nameof(EnemyScale));

        public void ResetPlayerPosition()
        {
            PlayerLeft = DefaultPlayerLeft;
            PlayerTop = DefaultPlayerTop;
        }
        public void ResetOpponentPosition()
        {
            EnemyLeft = DefaultEnemyLeft;
            EnemyTop = DefaultEnemyTop;
        }
        public void ResetPlayerDisplay()
        {
            FriendlyOpacity = DefaultFriendlyOpacity;
            FriendlyScale = DefaultFriendlyScale;
            FriendlyOrientation = DefaultFriendlyOrientation;
        }
        public void ResetOpponentDisplay()
        {
            EnemyOpacity = DefaultEnemyOpacity;
            EnemyScale = DefaultEnemyScale;
        }
    }
}
