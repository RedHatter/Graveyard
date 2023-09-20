using HDT.Plugins.Graveyard.Properties;
using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

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
        private static class Old
        {
            internal static readonly string Filename = Path.ChangeExtension(LibraryInfo.Name, ".xml");
            internal static string DataDir => Config.Instance.DataDir;
            internal static string SettingsPath => Path.Combine(DataDir, Filename);
        }

        private static readonly string Filename = Path.ChangeExtension(nameof(Settings), ".xml");
        internal static string DataDir => Path.Combine(Config.Instance.DataDir, $"Plugin.{LibraryInfo.Name}");
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

                if (!Directory.Exists(DataDir))
                    Directory.CreateDirectory(DataDir);

                try
                {
                    if (!File.Exists(SettingsPath) || File.Exists(Old.SettingsPath))
                    {
                        File.Move(Old.SettingsPath, SettingsPath);
                    }
                }
                catch (Exception ex) { Log.Error(ex); }

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
    }
}
