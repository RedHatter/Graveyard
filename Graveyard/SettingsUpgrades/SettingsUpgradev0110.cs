using Hearthstone_Deck_Tracker.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDT.Plugins.Graveyard.SettingsUpgrades
{
    internal class SettingsUpgradev0110 : ISettingsUpgrade
    {
        private Settings Settings;

        public Version Version { get; } = new Version(1, 10);

        public bool Upgrade(Settings settings)
        {
            Settings = settings;

            if (Version.CompareTo(new Version(Settings.Version)) <= 0) return false;

            // Process config list for disabled settings and convert to excluded cards
            // Exclude "Last Played" views to process separately
            foreach (var config in Plugin.Graveyard.ConfigList
                .Where(c => !string.IsNullOrEmpty(c.Enabled)
                && !(bool)Settings[c.Enabled]
                && c.Enabled != "LastPlayedEnabled"
                && c.ShowOnCards != null
                && c.ShowOnCards.Count() <= 2))
            {
                TryUpdateConfig(config, true);
            }

            // "Last Played" cards all use single LastPlayedEnabled setting
            var lastPlayedConfigs = Plugin.Graveyard.ConfigList
                .Where(c => c.Enabled == "LastPlayedEnabled"
                && !(bool)Settings[c.Enabled])
                .ToList();
            for (int i = 0; i < lastPlayedConfigs.Count(); i++)
            {
                var config = lastPlayedConfigs[i];
                TryUpdateConfig(config, i == lastPlayedConfigs.Count() - 1);
            }

            // Kazakus has his own ResurrectKazakus setting
            if (!Settings.ResurrectKazakus)
            {
                var kazakusConfig = ResurrectView.Config.ShowOnCards
                    .Where(c => c.CardId == HearthDb.CardIds.Collectible.Neutral.Kazakus)
                    .FirstOrDefault();
                if (kazakusConfig != null)
                {
                    kazakusConfig.IsEnabled = false;
                    ViewConfigCards.Instance.Toggle(kazakusConfig);
                }
                Settings.ResurrectKazakus = true;
            }

            return true;
        }

        bool TryUpdateConfig(ViewConfig config, bool updateSetting)
        {
            try
            {
                foreach (var configCard in config.ShowOnCards)
                {
                    configCard.IsEnabled = false;
                    ViewConfigCards.Instance.Toggle(configCard);
                }
                if (updateSetting) Settings[config.Enabled] = true;
                Log.Info($"Upgraded {config.Name} setting to {Version}");
                return true;
            }
            catch (Exception ex)
            {
                Log.Warn($"Upgrade {config.Name} setting to {Version} failed");
                Log.Error(ex);
                return false;
            }
        }
    }
}
