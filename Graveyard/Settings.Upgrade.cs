using HDT.Plugins.Graveyard.SettingsUpgrades;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HDT.Plugins.Graveyard
{
    public sealed partial class Settings
	{
		internal static IEnumerable<ISettingsUpgrade> Upgrades = new List<ISettingsUpgrade>()
		{
			new SettingsUpgradev0110(),
            new SettingsUpgradev0111(),
        };

		public override void Upgrade()
		{
			foreach (var upgrade in Upgrades.OrderBy(u => u.Version))
			{
				if (upgrade.Version.CompareTo(new Version(Version)) > 0)
				{
					if (upgrade.Upgrade(this))
                        Version = upgrade.Version.ToString(); 
					else
						break;
				}
			}
		}
	}
}
