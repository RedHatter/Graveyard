using HDT.Plugins.Graveyard.Properties;
using Hearthstone_Deck_Tracker.Utility;
using Hearthstone_Deck_Tracker.Utility.Logging;
using System;
using WPFLocalizeExtension.Engine;

namespace HDT.Plugins.Graveyard
{
    public class Strings
    {
        public static string GetLocalized(string key)
        {
            var value = LocalizeDictionary.Instance.GetLocalizedObject(LibraryInfo.Name, "Strings", key, LocalizeDictionary.Instance.Culture)?.ToString();

            if (value == null)
            {
                try
                {
                    Log.Info($"Local {key} string not available, trying to use HDT string instead");
                    value = LocUtil.Get(key);
                }
                catch (MissingMethodException ex)
                {
                    Log.Error(ex);
                }
            }

            return string.IsNullOrEmpty(value) ? key : value;
        }
            
    }
}
