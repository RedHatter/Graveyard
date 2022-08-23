using HDT.Plugins.Graveyard;
using HDT.Plugins.Graveyard.Profiles;
using Hearthstone_Deck_Tracker.Hearthstone;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDT.Plugins.Graveyard.SettingsUpgrades
{
    internal class SettingsUpgradev0111 : ISettingsUpgrade
    {
        public Version Version { get; } = new Version("1.11");

        public bool Upgrade(Settings settings)
        {
            settings.DefaultProfile = Profile.CreateDefault(settings);
            ProfileFile.Save(Settings.DefaultProfilePath, settings.ProfileFile);
            return false;
        }

        
    }
}
