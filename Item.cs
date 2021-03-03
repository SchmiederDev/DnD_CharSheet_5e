using System;
using System.Collections.Generic;

namespace DnD_CharSheet_5e
{
    public class Item
    {
        public string ItemName { get; set; }

        public string Item_ID { get; set; }

        public int ItemWeight { get; set; }

        public Coin Coin;

        public string ItemInfo { get; set; }
    }
}
