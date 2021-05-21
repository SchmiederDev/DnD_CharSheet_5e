using System;
using System.Collections.Generic;

namespace DnD_CharSheet_5e
{
    public class Armor : Item
    {
        public uint ArmorBonus { get; set; }

        public bool DexAdd { get; set; } = true;
        public bool HasMax { get; set; } = false;
        public bool HasDisadvantage { get; set; }
        public uint StrMax { get; set; }
    }
}
