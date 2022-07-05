using System;
using System.Collections.Generic;

namespace DnD_CharSheet_5e
{
    /* The 'SpellList_Item-Class:
     * Very simple data container for deserialization from the 'SpellList'-Databases.
     * 
     * Certain 'Character-Classes' (in the game of D&D, not in C#) are able to 'cast' Spells. 
     * But, a specific 'Character-Class' like 'Wizard' or 'Druid' may only choose from a limited number Spells 
     * from all the hundreds of Spells that exist in the game - represented here by the 'SpellDataBase' and the 'Spell'-class data container.
     * 
     * Therefore each of these 'Character-Classes' has a list of Spells to choose from 
     * - represented here by the 'SpellList'-data bases related to a specific type of 'Character-Class' (e. g. 'Wizard')
     * and the 'SpellList_Item'-class data container.
     * 
     */
    public class SpellList_Item
    {
        public string SpellName { get; set; }
        public int SpellLevel { get; set; }
    }
}
