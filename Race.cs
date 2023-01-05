using System;
using System.Collections.Generic;

namespace DnD_CharSheet_5e
{
    [Serializable]
    public class Race
    {
        public string RaceName { get; set; }

        // use class or struct instead? array/ list
        public AbilityScoreIncrease[] AbilityScoreIncreases;
        public uint AgeMax { get; set; }
        public string AgeBackground { get; set; }

        public string AlignmentBackground { get; set; }

        public string Size { get; set; }
        public string SizeBackground { get; set; }

        public uint Speed { get; set; }

        public RaceFeat[] RaceFeats { get; set; }

        public Proficiency[] RaceProficiencies { get; set; }

        public Race_SpellAbility[] Race_SpellAbilities { get; set; }

        // At the moment only the languages spoken - not their respektive description - here with a key or own database?
        public string[] Languages;

        public Subrace[] SubracesOfRace { get; set; }

    }

    [Serializable]
    public struct RaceFeat
    {
        public string FeatName { get; set; }
        public string FeatDescription { get; set; }
    }

    [Serializable]
    public struct Race_SpellAbility
    {
        public string SpellName {get; set;}
        public uint ClassLevel { get; set; }
    }

    [Serializable]
    public struct AbilityScoreIncrease
    {
        public string AbilityKey;
        public uint AbilityBonus;
    }

    [Serializable]
    public struct Height
    {
        public uint feet { get; set; }
        public uint inches { get; set; }
    }

    [Serializable]
    public class Subrace
    {
        public string SubraceName { get; set; }        

        public AbilityScoreIncrease SubraceIncrease { get; set; }

        public Height HeightMax { get; set; }
        public Height HeightMin { get; set; }

        public float WeightMax { get; set; }
        public float WeightMin { get; set; }

        public RaceFeat[] SubraceFeats { get; set; }

        public Proficiency[] SubraceProficiencies { get; set; }

        public Race_SpellAbility[] Subrace_SpellAbilities { get; set; }
    }
}
