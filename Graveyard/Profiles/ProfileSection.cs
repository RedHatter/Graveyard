using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HDT.Plugins.Graveyard.Profiles
{
    public class ProfileSection : INotifyPropertyChanged
    {
        protected bool SetValue<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (!ApplyValueToStore(ref backingStore, value)) return false;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        private bool ApplyValueToStore<T>(ref T backingStore, T value)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value)) return false;

            backingStore = value;
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null) return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
