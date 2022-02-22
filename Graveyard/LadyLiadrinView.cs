using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Hearthstone;
using System.Collections.Generic;
using static HearthDb.CardIds.Collectible;

namespace HDT.Plugins.Graveyard
{
    public class LadyLiadrinView : NormalView
    {
        private static ViewConfig _Config;
        internal static ViewConfig Config
        {
            get => _Config ?? (_Config = new ViewConfig(Paladin.LadyLiadrin)
            {
                Name = Strings.GetLocalized("LadyLiadrin"),
                Enabled = () => Settings.Default.LadyLiadrinEnabled,
                WatchFor = GameEvents.OnPlayerPlay,
                Condition = card => card.Type == "Spell" && SpellList.Contains(card.Id),
            });
        }
        
        public static readonly List<string> SpellList = new List<string>
        {
            // 0
            Paladin.ForbiddenHealing,
            // 1
            Paladin.Adaptation,
            Paladin.BlessingOfMightLegacy,
            Paladin.BlessingOfWisdom,
            Paladin.DivineStrength,
            Paladin.HandOfProtectionLegacy,
            //Paladin.Humility
            Paladin.SandBreath,
            Paladin.ShieldOfHonor,
            // 2
            Paladin.DarkConviction,
            Paladin.DesperateStand,
            Paladin.FlashOfLight,
            Paladin.HandOfAdal,
            Paladin.LibramOfWisdom,
            Paladin.LightforgedBlessing,
            Paladin.NobleMount,
            Paladin.PotionOfHeroism,
            Paladin.SoundTheBells,
            //Paladin.Subdue
            // 3
            Paladin.GiftOfLuminance,
            Paladin.HoldTheBridge,
            //Paladin.RighteousDefense,
            Paladin.SealOfChampions,
            // 4
            Paladin.BlessingOfKingsCore,
            //Paladin.HammerOfWrath,
            Paladin.SilvermoonPortal,
            // 5
            Paladin.BlessedChampion,
            Paladin.BlessingOfAuthority,
            //Paladin.HolyWrath,
            // 6
            //?Paladin.Righteousness, all minions
            //?Paladin.ShrinkRay, all minions
            //?Paladin.LevelUp, all minions
            Paladin.PharaohsBlessing,
            Paladin.SpikeridgedSteed,
            // 8
            Paladin.Dinosize,
            Paladin.LayOnHands,
            // 9
            Paladin.LibramOfHope,
        };

        public LadyLiadrinView()
        {
            // Section Label
            Label.Text = Config.Name;
        }

        public bool Update(Card card)
        {
            return Config.Condition(card) && base.Update(card, true);
        }
    }
}
