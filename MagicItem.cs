using System;
using System.Collections.Generic;

namespace DnD_CharSheet_5e
{
    [Serializable]
    public class MagicItem : Item
    {
        public string ItemTypeKey { get; set; }
        public string ItemAttribute_ReferenceKey { get; set; }
        public int ItemBonus { get; set; }

        // Array?
        public List<Spell> SpellsCastOnItem { get; set; }
    }
}
