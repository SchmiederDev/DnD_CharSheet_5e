using System;
using System.Collections.Generic;

namespace DnD_CharSheet_5e
{
    [Serializable]
    public class Item
    {
        public string ItemName { get; set; }
        public string Item_ID { get; set; }

        public bool IsEquipable { get; set; }
        public bool IsConsumable { get; set; }
        public string SlotReference { get; set; }

        public float ItemWeight { get; set; }
        public Coin Coin;

        public string ItemInfo { get; set; }
    }
}
