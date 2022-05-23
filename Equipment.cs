using System;
using System.Collections.Generic;

namespace DnD_CharSheet_5e
{
    // Why again serializable?
    [Serializable]
    public class Equipment
    {
        public Armor CharacterArmor { get; set; }
        public Armor LeftHand_Armor { get; set; }
       
        public Weapon RightHand_Weapon { get; set; }
        public Weapon LeftHand_Weapon { get; set; }

        public Item Head_Slot { get; set; }
        public Item Neck_Slot { get; set; }
        public Item Cape_Slot { get; set; }

        public Item Belt_Slot { get; set; }
        public Item Boots_Slot { get; set; }

        public Item LeftHand_Ring { get; set; }
        public Item RightHand_Ring { get; set; }

        public void Clear_Equipment()
        {
            CharacterArmor = null;
            LeftHand_Armor = null;

            RightHand_Weapon = null;
            LeftHand_Weapon = null;

            Head_Slot = null;
            Neck_Slot = null;
            Cape_Slot = null;

            Belt_Slot = null;
            Boots_Slot = null;
            
            LeftHand_Ring = null;
            RightHand_Ring = null;
        }
    }
}
