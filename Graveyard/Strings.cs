using WPFLocalizeExtension.Engine;

namespace HDT.Plugins.Graveyard
{
    public class Strings
    {
        public static string GetLocalized(string key) => LocalizeDictionary.Instance.GetLocalizedObject("Graveyard", "Strings", key, LocalizeDictionary.Instance.Culture)?.ToString();
    }
}
