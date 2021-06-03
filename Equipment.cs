using System;
using System.Collections.Generic;

namespace DnD_CharSheet_5e
{
    [Serializable]
    public class Equipment
    {
        public Armor CharacterArmor { get; set; }
        public Armor LeftHand_Armor { get; set; }
       
        public Weapon RightHand_Weapon { get; set; }
        public Weapon LeftHand_Weapon { get; set; }
    }
}
