using System;
using System.Collections.Generic;

namespace DnD_CharSheet_5e
{
    public class Armor : Item
    {
        public uint ArmorBonus { get; set; }

        public bool DexAdd { get; set; } = true;
        public bool HasMax { get; set; } = false;
        
        public uint DexMax { get; } = 2;

        public bool HasDisadvantage { get; set; }
    }
}
