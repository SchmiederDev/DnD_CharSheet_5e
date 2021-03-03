using System;
using System.Collections.Generic;

namespace DnD_CharSheet_5e
{
    public class Weapon : Item
    {
        public uint DamageNominator { get; set; }
        public uint DamageDenominator { get; set; }

        public bool IsRanged { get; set; }
        public bool IsFinesse { get; set; }
    }
}
