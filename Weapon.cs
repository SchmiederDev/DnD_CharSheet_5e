using System;

namespace DnD_CharSheet_5e
{
    [Serializable]
    public class Weapon : Item
    {
        public int DamageNumerator { get; set; }
        public int DamageDenominator { get; set; }

        public bool IsRanged { get; set; }
        public uint NormRange { get; set; }
        public uint MaxRange { get; set; }
        public bool IsFinesse { get; set; }
        public bool IsLight { get; set; }
        public bool IsHeavy { get; set; }
        public bool IsTwoHanded { get; set; }
        public bool IsVersatile { get; set; }
    }
}
