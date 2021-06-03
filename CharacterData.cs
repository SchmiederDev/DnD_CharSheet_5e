using System;
using System.Collections.Generic;

namespace DnD_CharSheet_5e
{
    [Serializable]
    public class CharacterData
    {
        public string pName { set; get; }
        public string cName { set; get; }
        public string race { set; get; }
        public string subrace { set; get; }

        public string charClass { set; get; }
        public string alignment { set; get; }
        public string background { set; get; }

        public int level { set; get; }

        public int maxHP { set; get; }
        public int currHP { set; get; }

        public int tempHP { set; get; }

        public int HD { set; get; }
        public int currHD { set; get; }

        public int strength { set; get; }
        public int dexerity { set; get; }
        public int constitution { set; get; }
        public int intelligence { set; get; }
        public int wisdom { set; get; }
        public int charisma { set; get; }

        public bool str_ST { set; get; }
        public bool dex_ST { set; get; }
        public bool con_ST { set; get; }
        public bool int_ST { set; get; }
        public bool wis_ST { set; get; }
        public bool cha_ST { set; get; }

        public bool acrobatics { set; get; }
        public bool animalHandling { set; get; }
        public bool arcana { set; get; }
        public bool athletics { set; get; }

        public bool deception { set; get; }

        public bool history { set; get; }
        public bool insight { set; get; }
        public bool intimidation { set; get; }
        public bool investigation { set; get; }

        public bool medicine { set; get; }
        public bool nature { set; get; }

        public bool perception { set; get; }
        public bool performance { set; get; }
        public bool persuasion { set; get; }

        public bool religion { set; get; }

        public bool sleightOfHand { set; get; }
        public bool stealth { set; get; }

        public bool survival { set; get; }

        public string CD_charAppearance { get; set; } 

        public string CD_backgroundStory { get; set; }

        public string CD_AlliesAndOrgas { get; set; }


        public int CD_Age { get; set; }
        public float CD_Height { get; set; }
        public float CD_Weight { get; set; }

        public string CD_Eyes { get; set; }
        public string CD_Skin { get; set; }
        public string CD_Hair { get; set; }


        public Inventory CD_Inventory { get; set; }
        public Equipment CD_Equipment { get; set; }
        

        public void Transfer_CharData(Character character)
        {
            pName = character.Get_playerName();
            cName = character.Get_charName();

            race = character.Get_Race();
            subrace = character.Get_Subrace();

            charClass = character.Get_charClass();

            alignment = character.Get_Alignment();
            background = character.Get_Background();

            level = character.Get_charLvl();

            maxHP = character.Get_maxHP();
            currHP = character.Get_currHP();

            HD = character.Get_hitDice();
            currHD = character.Get_currHitDice();

            strength = character.Get_strValue();
            dexerity = character.Get_dexValue();
            constitution = character.Get_conValue();
            intelligence = character.Get_intValue();
            wisdom = character.Get_wisValue();
            charisma = character.Get_chaValue();

            str_ST = character.Get_STR_Prof();
            dex_ST = character.Get_DEX_Prof();
            con_ST = character.Get_CON_Prof();
            int_ST = character.Get_INT_Prof();
            wis_ST = character.Get_WIS_Prof();
            cha_ST = character.Get_CHA_Prof();

            acrobatics = character.Get_Acrobatics_Prof();
            animalHandling = character.Get_AnimalHandling_Prof();
            arcana = character.Get_Arcana_Prof();
            athletics = character.Get_Athletics_Prof();

            deception = character.Get_Deception_Prof();

            history = character.Get_History_Prof();
            insight = character.Get_Insight_Prof();
            intimidation = character.Get_Intimidation_Prof();
            investigation = character.Get_Investigation_Prof();

            medicine = character.Get_Medicine_Prof();
            nature = character.Get_Nature_Prof();

            perception = character.Get_Perception_Prof();
            performance = character.Get_Performance_Prof();
            persuasion = character.Get_Persuasion_Prof();

            religion = character.Get_Religion_Prof();

            sleightOfHand = character.Get_SleightOfHand_Prof();
            stealth = character.Get_Stealth_Prof();

            survival = character.Get_Survival_Prof();

            CD_Age = character.Age;
            CD_Height = character.Height;
            CD_Weight = character.Weight;

            CD_Eyes = character.Eyes;
            CD_Skin = character.Skin;
            CD_Hair = character.Hair;

            CD_charAppearance = character.CharApperance;
            CD_backgroundStory = character.BackgroundStory;
            CD_AlliesAndOrgas = character.AlliesAndOrgas;            

            CD_Inventory = character.cInventory;
            CD_Equipment = character.CharEquipment;
        }
    }
}
