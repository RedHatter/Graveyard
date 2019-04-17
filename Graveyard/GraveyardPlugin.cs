using System;
using System.Reflection;
using System.Windows.Controls;
using Hearthstone_Deck_Tracker.Plugins;

namespace HDT.Plugins.Graveyard
{
	public class GraveyardPlugin : IPlugin
	{
		public Graveyard GraveyardInstance;
        public string Author => "RedHatter";
        public string ButtonText => "Settings";


        public string Description => @"Displays minions that have died this game. Includes specialized displays:
- Deathrattle minions (Nine Lives/Da Undatakah/Twilight's Call/N'Zoth)
- Taunt minions (Hadronox)
- Demons (Bloodreaver Gul'dan)
- Resurrection chance (Catrina Muerte/Mass Resurrection/Wild Priest cards)
- Murloc minions with a damage calculator for Anyfin Can Happen
- Discard retrieve chance (Soulwarden/Cruel Dinomancer) 
- Treant deaths (Mulchmuncher)
- Mech deaths (Kangor's Endless Army)
- 5-cost spells (Dragoncaller Alanna)
- Beast minions resummon chance (Witching Hour)
- Minions played count (The Caverns Below)

This version built from https://github.com/batstyx/Graveyard 
";

        public MenuItem MenuItem => null;
        public string Name => "Graveyard";

        public void OnButtonPress() => SettingsView.Flyout.IsOpen = true;
        public void OnLoad() => GraveyardInstance = new Graveyard();
        public void OnUnload()
        {
            Settings.Default.Save();

            GraveyardInstance.Dispose();
            GraveyardInstance = null;
        }
        public void OnUpdate() { }

        public static readonly Version AssemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;
        public static readonly Version PluginVersion = new Version(AssemblyVersion.Major, AssemblyVersion.Minor, AssemblyVersion.Build);
        public Version Version => PluginVersion;
    }
}
