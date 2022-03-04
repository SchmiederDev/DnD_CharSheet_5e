using System;

namespace DnD_CharSheet_5e
{
    [Serializable]
    public class MagicWeapon : Weapon
    {
        public string ItemAttribute_ReferenceKey { get; set; }
        public int ItemBonus { get; set; }

        public bool AdditionalDamage { get; set; }
        public int AdditionalDamageNominator { get; set; }
        public int AdditionalDamageDenominator { get; set; }
        public string AdditionalDamageType { get; set; }
    }
}
