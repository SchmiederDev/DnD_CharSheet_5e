using System.Collections.Generic;
using Newtonsoft.Json;


namespace DnD_CharSheet_5e
{
    // Helper class for 'MerchantWindow' providing Item Data Bases to it. It also allows searching for items in these data bases.
    // It serves the purpose of 'selling' items to the Character of the Player/ user via the MerchantWindow (which will be added to the Inventory of the Character-class instance). 
    public class Merchant
    {  
        
        public List<Item> merchItems = new List<Item>();
        public List<Weapon> merchWeapons = new List<Weapon>();
        public List<Armor> merchArmor = new List<Armor>();

        #region LOAD DATABASE(S) METHODS

        public void Load_ItemDataBase(string jsonIDB)
        {            
            merchItems = JsonConvert.DeserializeObject<List<Item>>(jsonIDB);            
        }       
        

        public void Load_WeaponDataBase(string jsonWDB)
        {
            merchWeapons = JsonConvert.DeserializeObject<List<Weapon>>(jsonWDB);
        }

        public void Load_ArmorDataBase(string jsonADB)
        {
            merchArmor = JsonConvert.DeserializeObject<List<Armor>>(jsonADB);
        }

        #endregion

        #region METHODS FOR SEARCHING ITEMS IN ITEM DATA BASES (the respective List<>'s)

        public Item Find_Item_byID(string id)
        {
            Item tempItem = merchItems.Find(ItemElement => ItemElement.Item_ID == id);

            return tempItem;
        }

        public Weapon Find_Weapon_byID(string id)
        {
            Weapon tempWeapon = merchWeapons.Find(WeaponElement => WeaponElement.Item_ID == id);

            return tempWeapon;
        }      

        public Armor Find_Armor_byID(string id)
        {
            Armor tempArmor = merchArmor.Find(ArmorElement => ArmorElement.Item_ID == id);

            return tempArmor;
        }

        #endregion

    }
}
