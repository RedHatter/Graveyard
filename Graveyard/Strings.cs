using Hearthstone_Deck_Tracker.Utility;
using WPFLocalizeExtension.Engine;

namespace HDT.Plugins.Graveyard
{
    public class Strings
    {
        public static string GetLocalized(string key)
        {
            var value = LocalizeDictionary.Instance.GetLocalizedObject("Graveyard", "Strings", key, LocalizeDictionary.Instance.Culture)?.ToString()
            ?? LocUtil.Get(key);

            return string.IsNullOrEmpty(value) ? key : value;
        }
            
    }
}
