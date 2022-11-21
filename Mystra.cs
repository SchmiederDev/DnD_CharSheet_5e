using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Windows;

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

        public List<SpellCasterData> SpellCasterClasses_Data {get; set;}
        public List<SpellCasterClass> SpellCasterClasses { get; set; }

        public List<SpellList> SpellLists { get; set; }

        public List<SpellList_Item> BardSpellList { get; set; }
        public List<SpellList_Item> ClericSpellList { get; set; }
        public List<SpellList_Item> DruidSpellList { get; set; }
        public List<SpellList_Item> PaladinSpellList { get; set; }
        public List<SpellList_Item> RangerSpellList { get; set; }
        public List<SpellList_Item> SorcererSpellList { get; set; }
        public List<SpellList_Item> WarlockSpellList { get; set; }
        public List<SpellList_Item> WizardSpellList { get; set; }


        public Mystra()
        {
            SpellDataBase = new List<Spell>();

            SpellCasterClasses_Data = new List<SpellCasterData>();
            SpellCasterClasses = new List<SpellCasterClass>();
            SpellLists = new List<SpellList>();

            BardSpellList = new List<SpellList_Item>();
            ClericSpellList = new List<SpellList_Item>();
            DruidSpellList = new List<SpellList_Item>();
            PaladinSpellList = new List<SpellList_Item>();
            RangerSpellList = new List<SpellList_Item>();
            SorcererSpellList = new List<SpellList_Item>();
            WarlockSpellList = new List<SpellList_Item>();
            WizardSpellList = new List<SpellList_Item>();
        }
        #endregion

        #region DESERIALIZATION METHODS FOR SPELL DATA BASES

        public void Load_SpellDataBase(string jsonSDB)
        {
            try
            {
                SpellDataBase = JsonConvert.DeserializeObject<List<Spell>>(jsonSDB);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void Load_SpellCasterClassesData(string jsonSCCDB)
        {            
            try
            {
                SpellCasterClasses_Data = JsonConvert.DeserializeObject<List<SpellCasterData>>(jsonSCCDB);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void Load_SpellListsDataBase(string jsonSLDB)
        {
            try
            {
                SpellLists = JsonConvert.DeserializeObject<List<SpellList>>(jsonSLDB);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

       
        public void Load_SpellLists()
        {
            BardSpellList = Load_SpellList(FileManager.FM_Inst.BardSpellList_JSON);
            ClericSpellList = Load_SpellList(FileManager.FM_Inst.ClericSpellList_JSON);
            DruidSpellList = Load_SpellList(FileManager.FM_Inst.DruidSpellList_JSON);
            PaladinSpellList = Load_SpellList(FileManager.FM_Inst.ClericSpellList_JSON);
            RangerSpellList = Load_SpellList(FileManager.FM_Inst.RangerSpellList_JSON);
            SorcererSpellList = Load_SpellList(FileManager.FM_Inst.SorcererSpellList_JSON);
            WarlockSpellList = Load_SpellList(FileManager.FM_Inst.WarlockSpellList_JSON);
            WizardSpellList = Load_SpellList(FileManager.FM_Inst.WizardSpellList_JSON);
        }

        public List<SpellList_Item> Load_SpellList(string jsonSpellList)
        {
            try
            {
                List<SpellList_Item> SpellList = JsonConvert.DeserializeObject<List<SpellList_Item>>(jsonSpellList);
                return SpellList;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        #endregion

        #region METHODS FOR HANDLING SPELL CASTER CLASSES

        public void Initialize_SpellCasterClasses()
        {
            foreach(SpellCasterData SpellCasterData in SpellCasterClasses_Data)
            {
                SpellCasterClass CasterClass = new SpellCasterClass();
                CasterClass.SCC_Name = SpellCasterData.SCC_Name;
                CasterClass.SpellCastingAbility_Name = SpellCasterData.SC_Ability;
                CasterClass.Generate_AbilityKey();
                SpellCasterClasses.Add(CasterClass);
            }
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
