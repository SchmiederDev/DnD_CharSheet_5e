using System;

namespace DnD_CharSheet_5e
{
    /* FUNCTION OF THE 'Proficiency'-Class
     * 
     * There are a multitude of proficiencies in D&D: meaning capabilities of characters to do something or do sth. with something.
     * 
     * For Example, characters can be 'proficient' in the 'Perception'-Skill so the get a bonus to their dice roll results for an attempt to see or hear something.
     * They can be 'proficient' with a weapon - again meaning they get a bonus for using this kind of weapon.
     * They can be 'proficient' with 'Alchemist's supplies' meaning they are capable of brewing magical potions etc.
     *
     * This data container here serves as the most abstract representation of those 'proficiencies'.
     * In a future version this data container will mostly be used to hold this kind of data upon Character creation and 'leveling up' when characters learn new proficiencies.
    */

    [Serializable]
    public class Proficiency
    {
        public string ProficiencyType { get; set; }
        public string ProficiencyName { get; set; }
    }
}
