namespace DnD_CharSheet_5e
{

    /* THE SPELL-CLASS:
     * 
     * This is a simple data container for deserialization from the 'SpellDataBase'
     * containing all of the data needed during a game session to describe/ represent a specific 'Spell' in the game.
     * 
     * Certain 'Character-Classes' (in the game of D&D, not in C#) are able to 'cast' Spells. 
     * But, a specific 'Character-Class' like 'Wizard' or 'Druid' may only choose from a limited number Spells 
     * from all the hundreds of Spells that exist in the game - represented here by the 'SpellDataBase' and the 'Spell'-class data container.
     * 
     * Therefore each of these 'Character-Classes' has a list of Spells to choose from 
     * - represented here by the 'SpellList'-data bases related to a specific type of 'Character-Class' (e. g. 'Wizard')
     * and the 'SpellList_Item'-class data container.
     */

    public class Spell
    {
        public string SpellName { set; get; }

        public int SpellLevel { set; get; }

        public string School { set; get; }

        public string CastingTime { set; get; }

        public string Range { set; get; }

        public string Components { set; get; }

        public string Duration { set; get; }

        public string Description { set; get; }
    }
}
