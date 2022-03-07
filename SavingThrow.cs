using System;
using System.Collections.Generic;

namespace DnD_CharSheet_5e
{
    public class SavingThrow
    {
        public string SaveName { get; set; }
        public string SaveKey { get; set; }

        public int AbilityBonus { get; set; }

        public int SaveModifier { get; set; }

        public bool IsProficient { get; set; } = false;

        public void Set_AbilityBonus(int bonus)
        {
            AbilityBonus = bonus;
        }

        public void Calculate_SaveModifier(int profBonus)
        {
            if(IsProficient)
            {
                SaveModifier = AbilityBonus + profBonus;
            }

            else
            {
                SaveModifier = AbilityBonus;
            }
        }
    }
}
