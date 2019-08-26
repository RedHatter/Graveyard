using System;
using System.IO;
using System.Xml;
using System.Configuration;
using System.Windows.Controls;
using Hearthstone_Deck_Tracker.Plugins;

namespace HDT.Plugins.Graveyard
{
    public class GraveyardPlugin : IPlugin
    {
        public Graveyard GraveyardInstance;

        public string Author
        {
            get { return "RedHatter"; }
        }

        public string ButtonText
        {
            get { return "Settings"; }
        }

        public string Description
        {
            get { return @"Displays minions that have died this game. Includes specialized displays:
deathrattle minions for N'Zoth,
taunt minions for Hadronox,
demons for Bloodreaver Gul'dan,
resurrect chance for priest resurrect cards,
Murloc minions with a damage calculator for Anyfin Can Happen,
resurrect chance for Cruel Dinomancer
possible minions for Nine Lives and Witching Hour."; }
        }

        public MenuItem MenuItem
        {
            get { return null; }
        }

        public string Name
        {
            get { return "Graveyard"; }
        }

        public void OnButtonPress()
        {
            SettingsView.Flyout.IsOpen = true;
        }

        public void OnLoad()
        {
            FetchSettings();
            GraveyardInstance = new Graveyard();
        }

        public void OnUnload()
        {
            GraveyardInstance.Dispose();
            GraveyardInstance = null;
        }

        public void OnUpdate() { }

        public static void StoreSettings()
        {
            string path = Environment.ExpandEnvironmentVariables(@"%appdata%\HearthstoneDeckTracker\Graveyard\");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            XmlDocument Xml = new XmlDocument();
            Xml.AppendChild(Xml.CreateXmlDeclaration("1.0", "UTF-8", null));

            XmlNode Root = Xml.CreateElement("settings");
            Xml.AppendChild(Root);

            SettingsPropertyCollection props = Settings.Default.Properties;
            foreach (SettingsProperty setting in props)
            {
                XmlNode CurrentNode = Xml.CreateElement(setting.Name);
                CurrentNode.InnerText = Settings.Default.PropertyValues[setting.Name].PropertyValue.ToString();
                Root.AppendChild(CurrentNode);
            }

            path += "Settings.xml";

            Xml.Save(path);
        }

        public static void FetchSettings()
        {
            string path = Environment.ExpandEnvironmentVariables(@"%appdata%\HearthstoneDeckTracker\Graveyard\Settings.xml");
            if (File.Exists(path))
            {
                XmlDocument Xml = new XmlDocument();
                Xml.Load(path);
                XmlNode Root = Xml.SelectSingleNode("settings");

                if (Root != null)
                {
                    SettingsPropertyCollection props = Settings.Default.Properties;
                    foreach (SettingsProperty setting in props)
                    {
                        try
                        {
                            if (bool.TryParse(Root.SelectSingleNode(setting.Name).InnerText, out bool value))
                            {
                                Settings.Default[setting.Name] = value;
                            }
                        }
                        catch
                        {
                            if (double.TryParse(Root.SelectSingleNode(setting.Name).InnerText, out double value))
                            {
                                Settings.Default[setting.Name] = value;
                            }
                        }
                    }              
                }
            }
        }

		public Version Version
		{
			get { return new Version(1, 3, 0); }
		}
	}
}
