using HDT.Plugins.Graveyard.Profiles;
using Hearthstone_Deck_Tracker;
using System.IO;

namespace HDT.Plugins.Graveyard
{
    public sealed partial class Settings
    {
        internal const string ProfileFileName = "GraveyardProfiles.xml";
        internal static string DefaultProfilePath => Path.Combine(DataDir, ProfileFileName);

        internal ProfileFile ProfileFile
        {
            get => _ProfileFile ?? (_ProfileFile = LoadProfileFile());
            set => _ProfileFile = value;
        }
        private ProfileFile _ProfileFile;


        private ProfileFile LoadProfileFile()
        {
            return LoadProfileFile(string.IsNullOrEmpty(ProfilePath) ? DefaultProfilePath : ProfilePath);
        }

        private ProfileFile LoadProfileFile(string path)
        {
            if (File.Exists(path))
            {
                return XmlManager<ProfileFile>.Load(path);
            }
            else
            {
                return new ProfileFile();
            }
        }

        public Profile DefaultProfile 
        { 
            get => ProfileFile.Default ?? (ProfileFile.Default = Profile.CreateDefault(this)); 
            set => ProfileFile.Default = value; 
        }

        public Profile StandardProfile => DefaultProfile;
        public Profile WildProfile => DefaultProfile;            
    }
}
