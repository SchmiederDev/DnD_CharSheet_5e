using System;

namespace DnD_CharSheet_5e
{
    // Base class to represent Items in the Characters (= player's/ user's) Inventory
    [Serializable]
    public class Item
    {
        public string ItemName { get; set; } = "Default Item";
        public string Item_ID { get; set; } = "i_00";

        public bool IsEquipable { get; set; } = false;
        public bool IsConsumable { get; set; } = false;
        public string SlotReference { get; set; } = "none";

        public float ItemWeight { get; set; } = 0.0f;

        public Coin Coin;

        public string ItemInfo { get; set; } = "Place Holder Item";
        
    }
}
