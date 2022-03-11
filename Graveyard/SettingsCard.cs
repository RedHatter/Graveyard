using Hearthstone_Deck_Tracker.Hearthstone;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HDT.Plugins.Graveyard
{
    internal class SettingsCard : INotifyPropertyChanged
    {
        public ViewConfigCard Config { get; private set; }
        public Card Card { get; private set; }
        public bool IsEnabled { get => _IsEnabled; set => SetProperty(ref _IsEnabled, value, onChanged: () => Config.IsEnabled = value); }
        private bool _IsEnabled;
        public string CardClass => Card == null ? string.Empty : Card.GetPlayerClass;

        public SettingsCard(ViewConfigCard config)
        {
            Config = config;
            Card = Database.GetCardFromId(config.CardId);
            _IsEnabled = config.IsEnabled;
        }

        #region INotifyPropertyChanged
        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion       
    }
}
