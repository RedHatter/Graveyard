using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Hearthstone;
using Hearthstone_Deck_Tracker.Plugins;
using System;
using System.Reflection;
using System.Windows.Controls;

namespace HDT.Plugins.Graveyard
{
    public class GraveyardPlugin : IPlugin
	{
        private Settings Settings;
        public Graveyard GraveyardInstance;
        public string Author => "RedHatter";
        public string ButtonText => Strings.GetLocalized("Settings");

        public string Description => Strings.GetLocalized("GraveyardDescription");

        public MenuItem MenuItem { get; set; }
        public string Name => "Graveyard";

        public void OnButtonPress() => SettingsView.Flyout.IsOpen = true;
        public void OnLoad()
        {
            Settings = Settings.Default;

            MenuItem = new MenuItem { Header = Name };
            MenuItem.Click += (sender, args) => OnButtonPress();

            GraveyardInstance = new Graveyard();

            GameEvents.OnInMenu.Add(GraveyardInstance.Reset);
            GameEvents.OnGameStart.Add(GraveyardInstance.Reset);
            GameEvents.OnGameEnd.Add(GraveyardInstance.Reset);
            DeckManagerEvents.OnDeckSelected.Add(UpdateSelectedDeck);

            GameEvents.OnPlayerPlayToGraveyard.Add(GraveyardInstance.OnPlayerPlayToGraveyard.Poll);
            GameEvents.OnOpponentPlayToGraveyard.Add(GraveyardInstance.OnOpponentPlayToGraveyard.Poll);

            GameEvents.OnPlayerHandDiscard.Add(GraveyardInstance.OnPlayerHandDiscard.Poll);
            
            GameEvents.OnPlayerPlay.Add(GraveyardInstance.OnPlayerPlay.Poll);
            GameEvents.OnOpponentPlay.Add(GraveyardInstance.OnOpponentPlay.Poll);

            GameEvents.OnTurnStart.Add(GraveyardInstance.OnOpponentTurnStart.Poll);

            UpdateSelectedDeck(DeckList.Instance.ActiveDeck);
        }

        private Deck SelectedDeck;
        private void UpdateSelectedDeck(Deck deck)
        {
            if (deck == DeckList.Instance.ActiveDeck && deck != SelectedDeck)
            {
                SelectedDeck = deck;
                GraveyardInstance.Reset();
            }
        }

        public void OnUnload()
        {
            if (Settings?.HasChanges ?? false) Settings?.Save();
            Settings = null;

            GraveyardInstance?.Dispose();
            GraveyardInstance = null;
        }
        public void OnUpdate() { }

        public static readonly Version AssemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;
        public static readonly Version PluginVersion = new Version(AssemblyVersion.Major, AssemblyVersion.Minor, AssemblyVersion.Build);
        public Version Version => PluginVersion;
    }
}
