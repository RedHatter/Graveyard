using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDT.Plugins.Graveyard.SettingsUpgrades
{
    internal interface ISettingsUpgrade
    {
        Version Version { get; }

        bool Upgrade(Settings settings);
    }
}
