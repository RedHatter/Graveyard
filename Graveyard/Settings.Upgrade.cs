using HDT.Plugins.Graveyard.SettingsUpgrades;
using Hearthstone_Deck_Tracker.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDT.Plugins.Graveyard
{
	public sealed partial class Settings
	{
		internal static IEnumerable<ISettingsUpgrade> Upgrades = new List<ISettingsUpgrade>()
		{
			new SettingsUpgradev0110(),
        };

		public override void Upgrade()
		{
			foreach (var upgrade in Upgrades.OrderBy(u => u.Version))
			{
				if (upgrade.Version.CompareTo(new Version(Version)) > 0)
				{
					if (upgrade.Upgrade(this))
                        Version = upgrade.ToString();
					else
						break;
				}
			}
		}
	}
}
