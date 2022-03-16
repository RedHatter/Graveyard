using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDT.Plugins.Graveyard
{
    public class Setting
    {
        public Setting() { }
        public Setting(string name, string value)
        {
            Name = name;
            Value = value;
        }
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public sealed partial class Settings
    {
        private const string Filename = "Graveyard.xml";
        internal static string DataDir => Config.Instance.DataDir;
        private static string SettingsPath => Path.Combine(DataDir, Filename);

        public bool HasChanges { get; private set; } = false;
        public Settings()
        {
            var provider = Providers;

            SettingsLoaded += SettingsLoadedEventHandler;
            SettingsSaving += SettingsSavingEventHandler;
        }

        private void SettingsLoadedEventHandler(object sender, System.Configuration.SettingsLoadedEventArgs e)
        {
            try
            {
                Log.Debug($"Loading {SettingsPath}");

                if (File.Exists(SettingsPath))
                {
                    var actual = XmlManager<List<Setting>>.Load(SettingsPath);

                    foreach (var setting in actual)
                    {
                        try
                        {
                            if (Properties[setting.Name].PropertyType.IsEnum)
                                this[setting.Name] = Enum.Parse(Properties[setting.Name].PropertyType, setting.Value);
                            else
                                this[setting.Name] = Convert.ChangeType(setting.Value, Properties[setting.Name].PropertyType);
                        }
                        catch (Exception ex)
                        {
                            Log.Warn($"{setting.Name} loading error");
                            Log.Error(ex);
                        }
                    }
                    Log.Info($"{SettingsPath} loaded");
                }
                else
                {
                    Log.Warn($"{SettingsPath} does not exist");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            PropertyChanged += (s, a) => HasChanges = true;
            Log.Info($"Watching {SettingsPath} changes");
        }

        private void SettingsSavingEventHandler(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Log.Debug($"Saving {SettingsPath}");
            try
            {
                var saveFormat = PropertyValues.Cast<SettingsPropertyValue>()
                    .Where(p => p.SerializedValue.ToString() != p.Property.DefaultValue.ToString())
                    .Select(p => new Setting(p.Name, p.SerializedValue.ToString()))
                    .ToList();

                XmlManager<List<Setting>>.Save(SettingsPath, saveFormat);

                HasChanges = false;
               
                e.Cancel = true;

                Log.Info($"{SettingsPath} saved");
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        public override void Upgrade()
        {
            var upgradeVersion = new Version(1, 10);
            if (upgradeVersion.CompareTo(new Version(Default.Version)) > 0)
            {
                foreach (var config in GraveyardPlugin.GraveyardInstance.ConfigList)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(config.Enabled)
                                        || (bool)Default[config.Enabled]
                                        || config.ShowOnCards == null
                                        || config.ShowOnCards.Count() > 2) continue;

                        foreach (var configCard in config.ShowOnCards)
                        {
                            configCard.IsEnabled = false;
                            ViewConfigCards.Instance.Toggle(configCard);
                        }
                        Default[config.Enabled] = true;
                        Log.Info($"Upgraded {config.Name} setting to {upgradeVersion}");
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex);
                    }
                }
                Default.Version = upgradeVersion.ToString();
            }
        }
    }
}
