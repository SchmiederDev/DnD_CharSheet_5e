using System;
using System.Collections.Generic;

namespace DnD_CharSheet_5e
{
    /* THE SKILL-CLASS:
     * 
     * Technically, 'Skills' represent again nothing more than a number - the 'SkillModifier' - added or substracted from a die roll result made in certain situations (see below).
     * This class is meant to store and calculate this number for a specific Skill - see the list of Skills visualized on UI in MainWindow or the properties of the 'Character'-class -
     * and also allows to make such a die roll for a specific Skill - where the 'SkillModifier' is added to or substracted from a random number between 1 and 20 - representing a 20-sided die or 'd20'.
     * 
     * Skills are based and expand on a player's characters Abilities. 
     * Each Skill is related to one of the six abilities - see the list of Abilities visualized on UI in MainWindow or the properties of the 'Character'-class -
     * stored here in the 'AbilityReferenceKey'. 
     * The 'Acrobatics'-Skill for example, is related to the 'Dexterity'-Ability. 
     * 
     * Whereas Abilities describe the characters basic physical and mental cababilities (see abilities)
     * Skills describe more precisely how good - or bad - a character is in taking specific actions 
     * like climbing a wall, understanding ancient magical symbols, follow the trail of a monster or convincing 
     * a city guard to let a party of characters into town although they don't have the pass demanded by the city council.     *      
     * 
     * The success (or failure) of all of the endeavors mentioned above will (again) be determined by a die roll.
     * The result has to be higher than or equal to a certain number determining the difficulty of the endeavor - the so called Difficulty Class or 'DC'.  
     * 
     * When characters are especially good in a specific type of action they are 'proficient' in this type of Skill ('IsProficient' == true).
     * Then they may add their 'Proficiency Bonus' to the die roll result depending on the 'Level' of a character: see 'Chracater'-Class.
     * If they aren't especially versed in this type of action, then only the bonus of the related ability is added to - or substracted from(!) - the die roll result.    
     * 
     * Climbing a wall calls for an 'Athletics' or 'Acrobatics'-Check (= die roll for an instance of this class).
     * Understandig the ancient magical symbols calls for an 'Arcana'-Check, following the trail of a monster calls for a 'Survival'-Check,
     * convincing the city guard calls for a 'Persuasion'-Check and so on.
     * 
     * Most of the skill names are actually pretty self explanatory - you might guess what 'Medicine' or 'Animal Handling' is good for. 
     */

    [Serializable]
    public class Skill
    {
        public string SkillName { get; set; } 
        public string AbilityReferenceKey { get; set; }

        public int AbilityBonus { get; set; }

        public int SkillModifier { get; set; }

        public bool IsProficient { get; set; } = false;

        public void Calculate_SkillModifier(int profBonus)
        {
            if(IsProficient)
            {
                SkillModifier = AbilityBonus + profBonus;
            }

            else
            {
                SkillModifier = AbilityBonus;
            }            
        }

        public int SkillCheck()
        {
            int result = SheetManager.CS_Manager_Inst.dSys.Roll_D20() + SkillModifier;
            return result;
        }
    }
}
