using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DnD_CharSheet_5e
{
    /* NAME AND PURPOSE OF THE 'MYSTRA'-CLASS:
     * 
     * Mystra is the Goddess of magic in the Forgotten Realms campaign setting for D&D. This class for handling spells is therefore named after her.
     * 
     * 'Mystra' is a class that serves mainly the purpose of giving other parts of the app (foremost 'SpellsWindow') acccess to the Spell Data Bases.
     * 
     * There is one large data base - simply called 'SpellDataBase' - which contains all the data about 'Spells' (= all Spells) 
     * which are available in D&D under the Open Licence Agreement for the game.
     * 
     * Character-Classes (here the character classes of the game are meant) like 'Bard' or 'Wizard' may only cast/ learn a limited number of spells according to their character class.
     * The so called 'Spell Lists' (= 'SpellList_Item'-Class and JSON Data Bases) contain the data which specific spells (of all the spells) are available for a specific character class like 'Wizard'.
    */

    public class Mystra
    {
        #region PROPERTIES AND CONSTRUCTOR
        public List<Spell> SpellDataBase { get; set; }
        public List<SpellList_Item> BardSpellList { get; set; }
        public List<SpellList_Item> WizardSpellList { get; set; }

        public Mystra()
        {
            SpellDataBase = new List<Spell>();
            BardSpellList = new List<SpellList_Item>();
            WizardSpellList = new List<SpellList_Item>();
        }
        #endregion

        #region DESERIALIZATION METHODS FOR SPELL DATA BASES

        public void Load_SpellDataBase(string jsonSDB)
        {
            SpellDataBase = JsonConvert.DeserializeObject<List<Spell>>(jsonSDB);
        }

        public void Load_BardSpellList(string jsonBSL)
        {
            BardSpellList = JsonConvert.DeserializeObject<List<SpellList_Item>>(jsonBSL);
        }

        public void Load_WizardSpellList(string jsonWSL)
        {
            WizardSpellList = JsonConvert.DeserializeObject<List<SpellList_Item>>(jsonWSL);
        }

        #endregion

        #region METHODS FOR SEARCHING AND RETRIEVING SPELL OBJECTS FROM DATA BASES

        public Spell Find_Spell_in_Database_byName(string spellName)
        {
            Spell tempSpell = SpellDataBase.Find(spellElement => spellElement.SpellName == spellName);

            return tempSpell;
        }

        #endregion
    }
}
