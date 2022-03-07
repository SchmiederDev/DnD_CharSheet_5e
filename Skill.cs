using System;
using System.Collections.Generic;

namespace DnD_CharSheet_5e
{
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
    }
}
