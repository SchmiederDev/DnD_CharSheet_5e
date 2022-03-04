﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DnD_CharSheet_5e
{
    // Mystra is the Goddess of magic in the Forgotten Realms campaign setting for D&D. The class for handling spells is therefore named after her.
    public class Mystra
    {
        public List<Spell> SpellDataBase { get; set; }
        public List<SpellList_Item> BardSpellList { get; set; }
        public List<SpellList_Item> WizardSpellList { get; set; }

        public Mystra()
        {
            SpellDataBase = new List<Spell>();
            BardSpellList = new List<SpellList_Item>();
            WizardSpellList = new List<SpellList_Item>();
        }

        public void Read_SpellDataBase(string jsonSDB)
        {
            SpellDataBase = JsonConvert.DeserializeObject<List<Spell>>(jsonSDB);
        }

        public void Read_BardSpellList(string jsonBSL)
        {
            BardSpellList = JsonConvert.DeserializeObject<List<SpellList_Item>>(jsonBSL);
        }

        public void Read_WizardSpellList(string jsonWSL)
        {
            WizardSpellList = JsonConvert.DeserializeObject<List<SpellList_Item>>(jsonWSL);
        }

        public Spell Find_Spell_in_Database_byName(string spellName)
        {
            Spell tempSpell = new Spell();

            foreach(Spell spell in SpellDataBase)
            {
                if(spell.SpellName == spellName)
                {
                    tempSpell = spell;
                }
            }

            return tempSpell;
        }
    }
}