using System;
using System.Collections.Generic;


namespace DnD_CharSheet_5e
{
    [Serializable]
    public class SpellList
    {
        public string SpellCasterClassName { set; get; }
        public List<SpellList_Item> SpellListItems { set; get; }
    }
}
