using System;
using System.Collections.Generic;

namespace DnD_CharSheet_5e
{
    public class Skill
    {
        int abilityBonus;
        int skillModifier;
        bool isProficient = false;

        public void Set_abilityBonus(int bonus)
        {
            abilityBonus = bonus;
        }

        public void Set_Proficiency(bool proficiency)
        {
            isProficient = proficiency;
        }

        public bool Get_Proficiency()
        {
            return isProficient;
        }

        public void Set_skillModifier(int profBonus)
        {
            if(isProficient)
            {
                skillModifier = abilityBonus + profBonus;
            }

            else
            {
                skillModifier = abilityBonus;
            }
            
        }

        public int Get_skillModifier()
        {
            return skillModifier;
        }        
    }
}
