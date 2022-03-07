using System;
using System.Collections.Generic;

namespace DnD_CharSheet_5e
{
    [Serializable]
    public class Armor : Item
    {
        public int ArmorBonus { get; set; }

        public bool DexAdd { get; set; } = true;
        public bool HasMax { get; set; } = false;
        public bool HasDisadvantage { get; set; }
        public int StrMax { get; set; }
    }
}
