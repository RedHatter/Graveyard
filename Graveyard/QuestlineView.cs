using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Hearthstone;
using System;
using Coll = HearthDb.CardIds.Collectible;
using Non = HearthDb.CardIds.NonCollectible;

namespace HDT.Plugins.Graveyard
{
    public class QuestlineView : NormalView
    {
        private static readonly string SharedName = "Questline";
        private static readonly Func<ViewBase> SharedCreateView = () => new QuestlineView();
        private static readonly Predicate<Card> SharedCondition = card => true; 

        internal static ViewConfig FriendlyConfig
        {
            get => _FriendlyConfig ?? (_FriendlyConfig = new ViewConfig()
            {
                Name = SharedName,
                Enabled = "FriendlyQuestlineEnabled",
                ShowFirst = () => true,
                CreateView = SharedCreateView,
                UpdateOn = GameEvents.OnPlayerPlay,
                Condition = SharedCondition,
            });
        }
        private static ViewConfig _FriendlyConfig;
        
        internal static ViewConfig EnemyConfig
        {
            get => _EnemyConfig ?? (_EnemyConfig = new ViewConfig()
            {
                Name = SharedName,
                Enabled = "EnemyQuestlineEnabled",
                CreateView = SharedCreateView, 
                UpdateOn = GameEvents.OnOpponentPlay,
                Condition = SharedCondition,
            });
        }
        private static ViewConfig _EnemyConfig;

        internal class ViewConfig : Plugins.Graveyard.ViewConfig
        {
            public ViewConfig() : base()
            {

            }

            public override void RegisterView(ViewBase view, bool isDefault = false)
            {
                base.RegisterView(view, isDefault);
                if (view is QuestlineView questlineView)
                {
                    if (UpdateOn == GameEvents.OnPlayerPlay)
                    {
                        Plugin.Events.OnPlayerCreateInPlay.Register(questlineView.HunterShortcut);
                    }
                    else if (UpdateOn == GameEvents.OnOpponentPlay)
                    {
                        Plugin.Events.OnOpponentCreateInPlay.Register(questlineView.HunterShortcut);
                    }
                }
            }
        }

        private bool HunterShortcut(Card card)
        {
            if (card.Id == "SW_322e3")
            {
                return base.Update(Database.GetCardFromId(Non.Hunter.DefendtheDwarvenDistrict_TakeTheHighGroundToken));
            }
            else if (card.Id == "SW_322e")
            {
                return base.Update(Database.GetCardFromId(Non.Hunter.DefendtheDwarvenDistrict_KnockEmDownToken));
            }
            return true;
        }

        public override bool Update(Card card)
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
                    return base.Update(card);
                default:
                    return false;
            }

        }
    }
}
