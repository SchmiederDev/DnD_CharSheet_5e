using System;
using System.Collections.Generic;
using System.Windows;

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

        public bool charIsAlive { set; get; }
        public bool charIsConscious { get; set; }

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
            pName = character.PlayerName;
            cName = character.CharacterName;

            race = character.RaceName;
            subrace = character.SubraceName;

            charClass = character.ClassName;

            alignment = character.Alignment;
            background = character.Background;

            level = character.Level;

            maxHP = character.MaxHP;
            currHP = character.CurrentHP;

            tempHP = character.TempHP;

            charIsAlive = character.IsAlive;
            charIsConscious = character.IsConscious;

            HD = character.HitDice;
            currHD = character.CurrentHitDice;                       

            strength = character.Strength.Score;            
            dexerity = character.Dexterity.Score;
            constitution = character.Constitution.Score;
            intelligence = character.Intelligence.Score;
            wisdom = character.Wisdom.Score;
            charisma = character.Charisma.Score;
           
            str_ST = character.STR_Save.IsProficient;
            dex_ST = character.DEX_Save.IsProficient;
            con_ST = character.CON_Save.IsProficient;
            int_ST = character.INT_Save.IsProficient;
            wis_ST = character.WIS_Save.IsProficient;
            cha_ST = character.CHA_Save.IsProficient;

            acrobatics = character.Acrobatics.IsProficient;
            animalHandling = character.AnimalHandling.IsProficient;
            arcana = character.Arcana.IsProficient;
            athletics = character.Athletics.IsProficient;

            deception = character.Deception.IsProficient;

            history = character.History.IsProficient;
            insight = character.Insight.IsProficient;
            investigation = character.Investigation.IsProficient;

            medicine = character.Medicine.IsProficient;
            nature = character.Nature.IsProficient;

            perception = character.Perception.IsProficient;
            performance = character.Performance.IsProficient;
            persuasion = character.Persuasion.IsProficient;

            religion = character.Religion.IsProficient;

            sleightOfHand = character.SleightOfHand.IsProficient;
            stealth = character.Stealth.IsProficient;

            survival = character.Survival.IsProficient;

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
