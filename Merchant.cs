using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;


namespace DnD_CharSheet_5e
{
    public class Merchant
    {        
        public List<Item> merchItems = new List<Item>();
        public List<Weapon> merchWeapons = new List<Weapon>();
        public List<Armor> merchArmor = new List<Armor>();

        public void Load_ItemDataBase(string jsonIDB)
        {            
            merchItems = JsonConvert.DeserializeObject<List<Item>>(jsonIDB);            
        }
        
        public Item Find_Item_byID(string id)
        {
            Item tempItem = merchItems.Find(ItemElement => ItemElement.Item_ID == id);

            return tempItem;
        }

        public void Load_WeaponDataBase(string jsonWDB)
        {
            merchWeapons = JsonConvert.DeserializeObject<List<Weapon>>(jsonWDB);
        }

        public Weapon Find_Weapon_byID(string id)
        {
            Weapon tempWeapon = merchWeapons.Find(WeaponElement => WeaponElement.Item_ID == id);

            return tempWeapon;
        }

        public void Load_ArmorDataBase(string jsonADB)
        {
            merchArmor = JsonConvert.DeserializeObject<List<Armor>>(jsonADB);
        }

        public Armor Find_Armor_byID(string id)
        {
            Armor tempArmor = merchArmor.Find(ArmorElement => ArmorElement.Item_ID == id);

            return tempArmor;
        }

    }
}
