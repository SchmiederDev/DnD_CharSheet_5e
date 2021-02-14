using System;
using System.Collections.Generic;

namespace DnD_CharSheet_5e
{
    public class SavingThrow
    {
        int abilityBonus;
        int saveModifier;
        bool IsProficient = false;

        public void Set_AbilityBonus(int bonus)
        {
            abilityBonus = bonus;
        }

        public void Set_Proficiency(bool proficiency)
        {
            IsProficient = proficiency;
        }

        public bool Get_Proficiency()
        {
            return IsProficient;
        }

        public void Set_SaveModifier(int profBonus)
        {
            if(IsProficient)
            {
                saveModifier = abilityBonus + profBonus;
            }

            else
            {
                saveModifier = abilityBonus;
            }
        }

        public int Get_SaveModifier()
        {
            return saveModifier;
        }
    }
}
