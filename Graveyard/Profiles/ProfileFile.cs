using Hearthstone_Deck_Tracker;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HDT.Plugins.Graveyard.Profiles
{
    [XmlRoot(ElementName ="Profiles")]
    public class ProfileFile
    {
        public static ProfileFile Load(string path)
        {
            return XmlManager<ProfileFile>.Load(path);
        }

        public static void Save(string path, ProfileFile profileFile)
        {
            XmlManager<ProfileFile>.Save(path, profileFile);
        }
        
        public Profile Default { get; set; }
        public Profile Standard { get; set; }
        public Profile Wild { get; set; }
    }


}
