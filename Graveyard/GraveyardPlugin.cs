using Hearthstone_Deck_Tracker.Plugins;
using System;
using System.Reflection;
using System.Windows.Controls;

namespace HDT.Plugins.Graveyard
{
    public class GraveyardPlugin : IPlugin
	{
		public Graveyard GraveyardInstance;
        public string Author => "RedHatter";
        public string ButtonText => Strings.GetLocalized("Settings");

        public string Description => Strings.GetLocalized("GraveyardDescription");

        public MenuItem MenuItem => null;
        public string Name => "Graveyard";

        public void OnButtonPress() => SettingsView.Flyout.IsOpen = true;
        public void OnLoad() => GraveyardInstance = new Graveyard();
        public void OnUnload()
        {
            Settings.Default.Save();

            GraveyardInstance?.Dispose();
            GraveyardInstance = null;
        }
        public void OnUpdate() { }

        public static readonly Version AssemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;
        public static readonly Version PluginVersion = new Version(AssemblyVersion.Major, AssemblyVersion.Minor, AssemblyVersion.Build);
        public Version Version => PluginVersion;
    }
}
