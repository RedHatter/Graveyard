using Hearthstone_Deck_Tracker.Utility.Logging;
using System;
using System.Windows;

namespace HDT.Plugins.Graveyard.SettingsUpgrades
{
    internal class SettingsUpgradev0111 : ISettingsUpgrade
    {
        private Settings Settings;

        public Version Version { get; } = new Version(1, 11);

        public bool Upgrade(Settings settings)
        {
            Settings = settings;

            if (Version.CompareTo(new Version(Settings.Version)) <= 0) return false;

            return TryUpdateConfig(nameof(Settings.PlayerLeft), SystemParameters.PrimaryScreenWidth) 
                & TryUpdateConfig(nameof(Settings.PlayerTop), SystemParameters.PrimaryScreenHeight)
                & TryUpdateConfig(nameof(Settings.EnemyLeft), SystemParameters.PrimaryScreenWidth)
                & TryUpdateConfig(nameof(Settings.EnemyTop), SystemParameters.PrimaryScreenHeight);
        }

        bool TryUpdateConfig(string setting, double size, bool updateSetting = true)
        {
            try
            {
                double position = (double)Settings[setting];
                double percentage = position.PixelsToPercentage(size);
                if (updateSetting) Settings[setting] = percentage;                
                Log.Info($"Upgraded {setting} setting to {Version}");
                Log.Info($"value {position}px->{percentage:#,##0}% for size {size}");
                return updateSetting;
            }
            catch (Exception ex)
            {
                Log.Warn($"Upgrade {setting} setting to {Version} failed");
                Log.Error(ex);
                return false;
            }
        }
    }
}
