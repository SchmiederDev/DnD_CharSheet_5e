using System;
using System.Collections.Generic;
using System.Linq;

namespace DnD_CharSheet_5e
{
    [Serializable]
    public class CharacterClass
    {

        public string ClassName { get; set; }

        public int HitDie { get; set; }
        public int AverageHP { get; set; }

        public Proficiency[] Proficiencies { get; set; }

        public string SavingThrowProficiencyOne { get; set; }
        public string SavingThrowProficiencyTwo { get; set; }

        public int NumberOfSkillProficiencies { get; set; }
        public string[] SkillProficiencies { get; set; }

        public SpellSlot[] SpellSlots { get; set; }

        public ClassFeat[] FeatsOfClass { get; set; }


        public SubClassArchetype[] Archetype { get; set; }

    }

    [Serializable]
    public class ClassFeat : Feat
    {
        public int LevelRequirement { get; set; }
    }

    [Serializable]
    public struct SpellSlot
    {
        public int SpellLevel { get; set; }
        public int SpellNumber { get; set; }
    }

    [Serializable]
    public class SubClassArchetype
    {
        public string ArchetypeName { get; set; }

        public string ArchetypeIntroduction { get; set; }

        public ClassFeat[] ArchetypeFeats { get; set; }
    }
}
