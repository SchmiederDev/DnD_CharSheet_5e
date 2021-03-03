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
        public FileManager fManager = new FileManager();
        
        public List<Item> merchItems = new List<Item>();

        public void Load_ItemDataBase(string jsonIDB)
        {            
            merchItems = JsonConvert.DeserializeObject<List<Item>>(jsonIDB);            
        }
        
        public Item Find_Item_byID(string id)
        {
            Item tempItem = new Item();

            foreach(Item mItem in merchItems)
            {
                if(mItem.Item_ID == id)
                {
                    tempItem = mItem;
                }
            }

            return tempItem;
        }
    }
}
