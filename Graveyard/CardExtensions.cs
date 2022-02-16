using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Card = Hearthstone_Deck_Tracker.Hearthstone.Card;
using DbCard = HearthDb.Card;

namespace HDT.Plugins.Graveyard
{
    public enum SpellSchool
    {
        Unknown = -1,
        General = 0,
        Arcane = 1,
        Fire = 2,
        Frost = 3,
        Nature = 4,
        Holy = 5,
        Shadow = 6,
        Fel = 7,
    }

    internal static class CardExtensions
    {
        private static readonly Dictionary<string, SpellSchool> SpellSchoolCache = new Dictionary<string, SpellSchool>();

        public static SpellSchool GetSpellSchool(this Card card)
        {
            if (card.Type == "Spell")
            {
                if (SpellSchoolCache.ContainsKey(card.Id))
                {
                    return SpellSchoolCache[card.Id];
                }

                HearthDb.Cards.All.TryGetValue(card.Id, out DbCard dbCard);

                if (dbCard != null)
                {
                    var spellSchool = SpellSchool.General;
                    var tryGetSpellSchool = (SpellSchool?)dbCard.SpellSchool;
                    if (tryGetSpellSchool.HasValue)
                    {
                        spellSchool = tryGetSpellSchool.Value;
                    }
                    SpellSchoolCache.Add(card.Id, spellSchool);
                    return spellSchool;
                };               
            }
            return SpellSchool.Unknown;
        }
    }
}
