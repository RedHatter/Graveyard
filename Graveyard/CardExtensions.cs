using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Card = Hearthstone_Deck_Tracker.Hearthstone.Card;
using DbCard = HearthDb.Card;

namespace HDT.Plugins.Graveyard
{
    public enum School
    {
        NotASpell = -1,
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
        private static readonly Dictionary<string, School> SchoolCache = new Dictionary<string, School>();

        public static School GetSchool(this Card card)
        {
            if (card.Type == "Spell")
            {
                if (SchoolCache.ContainsKey(card.Id))
                {
                    return SchoolCache[card.Id];
                }

                HearthDb.Cards.All.TryGetValue(card.Id, out DbCard dbCard);

                if (dbCard != null)
                {
                    var school = School.General;
                    var tryGetSchool = (School?)dbCard.SpellSchool;
                    if (tryGetSchool.HasValue)
                    {
                        school = tryGetSchool.Value;
                    }
                    SchoolCache.Add(card.Id, school);
                    return school;
                };               
            }
            return School.NotASpell;
        }
    }
}
