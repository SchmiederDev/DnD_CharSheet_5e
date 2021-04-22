using System;
using System.Collections.Generic;


namespace DnD_CharSheet_5e
{
    public class Inventory
    {
        public List<Item> cItems { get; } = new List<Item>();                                                 // c = 'character'
        public List<Weapon> cWeapons { get; } = new List<Weapon>();
        public List<Armor> cArmor { get; } = new List<Armor>();

        public void Add_Item(Item newItem)
        {
            cItems.Add(newItem);
        }

        public Item Find_Item_byID(string id)
        {
            Item tempItem = new Item();

            foreach (Item mItem in cItems)
            {
                if (mItem.Item_ID == id)
                {
                    tempItem = mItem;
                }
            }

            return tempItem;
        }

        public void Remove_Item(Item item_toRemove)
        {
            cItems.Remove(item_toRemove);
        }

        public void Remove_Item_byID(string id)                             // What if the Item exists in the Inventory several times + additional Button_ID?
        {
            foreach(Item cItem in cItems)
            {
                if(cItem.Item_ID == id)
                {
                    cItems.Remove(cItem);
                }
            }
        }

        public void Add_Weapon(Weapon newWeapon)
        {
            cWeapons.Add(newWeapon);
        }

        public Weapon Find_Weapon_byID(string id)
        {
            Weapon tempWeapon = new Weapon();

            foreach (Weapon cWeapon in cWeapons)
            {
                if (cWeapon.Item_ID == id)
                {
                    tempWeapon = cWeapon;
                }
            }

            return tempWeapon;
        }

        public void Remove_Weapon(Weapon weapon_toRemove)
        {
            cWeapons.Remove(weapon_toRemove);
        }

        public void Add_Armor(Armor newArmor)
        {
            cArmor.Add(newArmor);            
        }

        public Armor Find_Armor_byID(string id)
        {
            Armor tempArmor = new Armor();

            foreach(Armor armor in cArmor)
            {
                if(armor.Item_ID == id)
                {
                    tempArmor = armor;
                }
            }

            return tempArmor;
        }

        public void Remove_Armor(Armor armor_toRemove)
        {
            cArmor.Remove(armor_toRemove);
        }
    }
}
