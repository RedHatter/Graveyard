using Hearthstone_Deck_Tracker.API;
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

        public MenuItem MenuItem { get; set; }
        public string Name => "Graveyard";

        public void OnButtonPress() => SettingsView.Flyout.IsOpen = true;
        public void OnLoad()
        {
            MenuItem = new MenuItem { Header = Name };
            MenuItem.Click += (sender, args) => OnButtonPress();

            GraveyardInstance = new Graveyard();

            GameEvents.OnGameStart.Add(GraveyardInstance.Reset);
            GameEvents.OnGameEnd.Add(GraveyardInstance.Reset);
            DeckManagerEvents.OnDeckSelected.Add(d => GraveyardInstance.Reset());

            GameEvents.OnPlayerPlayToGraveyard.Add(GraveyardInstance.PlayerGraveyardUpdate);
            GameEvents.OnOpponentPlayToGraveyard.Add(GraveyardInstance.EnemyGraveyardUpdate);

            GameEvents.OnPlayerPlay.Add(GraveyardInstance.PlayerDamageUpdate);
            GameEvents.OnOpponentPlay.Add(GraveyardInstance.EnemyDamageUpdate);

            GameEvents.OnPlayerHandDiscard.Add(GraveyardInstance.PlayerDiscardUpdate);
            GameEvents.OnPlayerPlay.Add(GraveyardInstance.PlayerPlayUpdate);

            GameEvents.OnTurnStart.Add(GraveyardInstance.TurnStartUpdate);
        }

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
