using Hearthstone_Deck_Tracker.Hearthstone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coll = HearthDb.CardIds.Collectible;
using Non = HearthDb.CardIds.NonCollectible;

namespace HDT.Plugins.Graveyard
{
    public class QuestlineView : NormalView
    {
        public QuestlineView()
        {
            Label.Text = Strings.GetLocalized("Questline");
        }

        public bool Update(Card card)
        {
            switch (card.Id)
            {
                case Coll.Demonhunter.FinalShowdown:
                case Non.Demonhunter.FinalShowdown_GainMomentumToken:
                case Non.Demonhunter.FinalShowdown_CloseThePortalToken:
                case Non.Demonhunter.FinalShowdown_DemonslayerKurtrusToken:
                case Coll.Druid.LostInThePark:
                case Non.Druid.LostinthePark_DefendTheSquirrelsToken:
                case Non.Druid.LostinthePark_FeralFriendsyToken:
                case Non.Druid.LostinthePark_GuffTheToughToken:
                case Coll.Hunter.DefendTheDwarvenDistrict:
                case Non.Hunter.DefendtheDwarvenDistrict_TakeTheHighGroundToken:
                case Non.Hunter.DefendtheDwarvenDistrict_KnockEmDownToken:
                case Non.Hunter.DefendtheDwarvenDistrict_TavishMasterMarksmanToken:
                case Coll.Mage.SorcerersGambit:
                case Non.Mage.SorcerersGambit_StallForTimeToken:
                case Non.Mage.SorcerersGambit_ReachThePortalRoomToken:
                case Non.Mage.SorcerersGambit_ArcanistDawngraspToken:
                case Coll.Paladin.RiseToTheOccasion:
                case Non.Paladin.RisetotheOccasion_PaveTheWayToken:
                case Non.Paladin.RisetotheOccasion_AvengeTheFallenToken:
                case Non.Paladin.RisetotheOccasion_LightbornCarielToken:
                case Coll.Priest.SeekGuidance:
                case Non.Priest.SeekGuidance_DiscoverTheVoidShardToken:
                case Non.Priest.SeekGuidance_IlluminateTheVoidToken:
                case Non.Priest.SeekGuidance_XyrellaTheSanctifiedToken:
                case Coll.Rogue.FindTheImposter:
                case Non.Rogue.FindtheImposter_LearnTheTruthToken:
                case Non.Rogue.FindtheImposter_MarkedATraitorToken:
                case Non.Rogue.FindtheImposter_SpymasterScabbsToken:
                case Coll.Shaman.CommandTheElements:
                case Non.Shaman.CommandtheElements_StirTheStonesToken:
                case Non.Shaman.CommandtheElements_TameTheFlamesToken:
                case Non.Shaman.CommandtheElements_StormcallerBrukanToken:
                case Coll.Warlock.TheDemonSeed:
                case Non.Warlock.TheDemonSeed_EstablishTheLinkToken:
                case Non.Warlock.TheDemonSeed_CompleteTheRitualToken:
                case Non.Warlock.TheDemonSeed_BlightbornTamsinToken:
                case Coll.Warrior.RaidTheDocks:
                case Non.Warrior.RaidtheDocks_CreateADistractionToken:
                case Non.Warrior.RaidtheDocks_SecureTheSuppliesToken:
                case Non.Warrior.RaidtheDocks_CapnRokaraToken:
                    return Update(card, true);
                default:
                    return false;
            }

        }
    }
}
