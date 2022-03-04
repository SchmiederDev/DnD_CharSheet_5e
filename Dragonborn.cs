using System;
using System.Collections.Generic;

namespace DnD_CharSheet_5e
{
    public class Dragonborn : CharacterRace
    {
        public string DragonType { get; set; }

        public BreathWeapon breathWeapon { get; set; }

        public string BreathWeaponSaveKey { get; set; }

        public class BreathWeapon
        {
            public string DamageType { get; set; }

            public string Reach { get; set; }

            public int dieNumber { get; set; } = 2;
            public int dieBase { get; } = 6;

            public int[] increaseLevels { get; } = { 6, 11, 17 };

            public void CheckLevelThreshold(int classLevel)
            {
                bool hasReachedLevelThreshold = false;

                for(int i = 0; i < increaseLevels.Length; i++)
                {
                    if(classLevel == increaseLevels[i])
                    {
                        hasReachedLevelThreshold = true;
                    }
                }

                if (hasReachedLevelThreshold)
                    dieNumber++;
            }

        }

        
    }
}
