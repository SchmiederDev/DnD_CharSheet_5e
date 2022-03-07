using System;
using System.Collections.Generic;
using System.Windows;

namespace DnD_CharSheet_5e
{
    public class Character
    {
        public string PlayerName { get; set; }
        public string CharacterName { get; set; }

        public string CharacterRace { get; set; }
        public string CharacterSubrace { get; set; }

        public string CharacterClass { get; set; }
        public string Alignment { get; set; }

        public string Background { get; set; }

        public int Level { get; set; }

        public int ProficiencyBonus { get; set; } = 2;

        int[] profBonusIncrease = { 5, 9, 13, 17 };

        public int MaxHP { get; set; }
        public int CurrentHP { get; set; }
        public int TempHP { get; set; }

        public bool IsAlive { get; set; } = true;
        public bool IsConscious { get; set; } = true;

        public int HitDice { get; set; }
        public int CurrentHitDice { get; set; }

        public int InitiativeBonus { get; private set; }

        public int AC { get; set; } = 10;

        public const int ACBase = 10;

        public int ArmorBonus { get; set; } = 0;
        public int ShieldBonus { get; set; } = 0;

        public int StrScore { get; set; }
        public int DexScore { get; set; }
        public int ConScore { get; set; }
        public int IntScore { get; set; }
        public int WisScore { get; set; }
        public int ChaScore { get; set; }

        public int StrModifier { get; set; }
        public int DexModifier { get; set; }
        public int ConModifier { get; set; }
        public int IntModifier { get; set; }
        public int WisModifier { get; set; }
        public int ChaModifier { get; set; }


        public SavingThrow STR_Save { get; set; } = new SavingThrow();
        public SavingThrow DEX_Save { get; set; } = new SavingThrow();
        public SavingThrow CON_Save { get; set; } = new SavingThrow();
        public SavingThrow INT_Save { get; set; } = new SavingThrow();
        public SavingThrow  WIS_Save { get; set; } = new SavingThrow();
        public SavingThrow CHA_Save { get; set; } = new SavingThrow();

        public List<SavingThrow> Saves { get; private set; }


        public Skill Acrobatics { get; set; } = new Skill();
        public Skill AnimalHandling { get; set; } = new Skill();
        public Skill Arcana { get; set; } = new Skill();
        public Skill Athletics { get; set; } = new Skill();

        public Skill Deception { get; set; } = new Skill();

        public Skill History { get; set; } = new Skill();
        public Skill Insight { get; set; } = new Skill();
        public Skill Intimidation { get; set; } = new Skill();
        public Skill Investigation { get; set; } = new Skill();

        public Skill Medicine { get; set; } = new Skill();
        public Skill Nature { get; set; } = new Skill();

        public Skill Perception { get; set; } = new Skill();
        public Skill Performance { get; set; } = new Skill();
        public Skill Persuasion { get; set; } = new Skill();

        public Skill Religion { get; set; } = new Skill();

        public Skill SleightOfHand { get; set; } = new Skill();
        public Skill Stealth { get; set; } = new Skill();

        public Skill Survival { get; set; } = new Skill();

        public int Age { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }        

        public string Eyes { get; set; }
        public string Hair { get; set; }
        public string Skin { get; set; }

        public string CharApperance { get; set; }
        public string BackgroundStory { get; set; }
        public string AlliesAndOrgas { get; set; }

        public List<string> CharLanguages = new List<string>();

        public Inventory cInventory { get; set; } = new Inventory();                                         //cInventory = 'character Inventory'

        public Equipment CharEquipment = new Equipment();

        public delegate void OnHPChanged();
        public OnHPChanged hpChanged;

        public delegate void OnTempHPChanged();
        public OnTempHPChanged tempHPChanged;

        public delegate void OnACChanged();
        public OnACChanged acChanged;

        // IniBonus Calculation, LevelMax Control, Init_Basics -> Saves, Skills
        
        public void Init_Basics()
        {
            Init_Saves();
        }

        private void Init_Saves()
        {
            STR_Save.SaveName = "Strength Saving Throw";
            STR_Save.SaveKey = "STR";
            
            Saves.Add(STR_Save);

            DEX_Save.SaveName = "Dexterity Saving Throw";
            DEX_Save.SaveKey = "DEX";

            Saves.Add(DEX_Save);

            CON_Save.SaveName = "Constitution Saving Throw";
            CON_Save.SaveKey = "CON";

            Saves.Add(CON_Save);

            INT_Save.SaveName = "Intelligence Saving Throw";
            INT_Save.SaveKey = "INT";

            Saves.Add(INT_Save);

            WIS_Save.SaveName = "Wisdom Saving Throw";
            WIS_Save.SaveKey = "WIS";

            Saves.Add(WIS_Save);

            CHA_Save.SaveName = "Charisma Saving Throw";
            CHA_Save.SaveKey = "CHA";

            Saves.Add(CHA_Save);
        }

        public void Set_MaxHP_byText(string boxText)
        {
            MaxHP = int.Parse(boxText);
        }       

        public void Init_HP_HD()
        {
            CurrentHP = MaxHP;
            CurrentHitDice = HitDice;
        }

        public void Set_CurrHP_byText(string boxText)
        {
            CurrentHP = int.Parse(boxText);
        }

        public void Set_CurrHP(int hpCurrent)
        {
            CurrentHP = hpCurrent;

            if(hpChanged != null)
            {
                hpChanged.Invoke();
            }
        }

        public void Set_tempHP_byText(string boxText)
        {
            TempHP = int.Parse(boxText);
        }

        public void Set_tempHP(int hpTemp)
        {
            TempHP = hpTemp;

            if(tempHPChanged != null)
            {
                tempHPChanged.Invoke();
            }
        }

        public void Set_hitDice_byText(string boxText)
        {
            HitDice = int.Parse(boxText);
        }
        public void Update_HitDice()
        {
            HitDice = Level;
        }

        public void Update_ProfBonus()
        {
            for(int i = 0; i < profBonusIncrease.Length; i++)
            {
                if(profBonusIncrease[i] == Level)
                {
                    ProficiencyBonus++;
                }
            }
        }

        public void Set_currHitDice(string boxText)
        {
            CurrentHitDice = int.Parse(boxText);
        }

        public void Set_IniBonus()
        {
            InitiativeBonus = DexModifier;
        }

        public bool Check_STR_Requirement(Armor armor)
        {
            if(armor.StrMax <= StrScore)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        public void Calculate_AC()
        {                       
            if(CharEquipment.CharacterArmor != null && CharEquipment.LeftHand_Armor != null)
            {
                ArmorBonus = CharEquipment.CharacterArmor.ArmorBonus;
                ShieldBonus = CharEquipment.LeftHand_Armor.ArmorBonus;

                if (CharEquipment.CharacterArmor.DexAdd == true && CharEquipment.CharacterArmor.HasMax == false)
                {
                    AC = ACBase + ArmorBonus + ShieldBonus + DexModifier;
                }

                else if (CharEquipment.CharacterArmor.DexAdd == true && CharEquipment.CharacterArmor.HasMax == true)
                {
                    if (DexModifier <= 2)
                    {
                        AC = ACBase + ArmorBonus + ShieldBonus + DexModifier;
                    }

                    else
                    {
                        AC = 12 + ArmorBonus + ShieldBonus;
                    }
                }

                else if (CharEquipment.CharacterArmor.DexAdd == false)
                {
                    AC = ACBase + ArmorBonus + ShieldBonus;
                }
            }

            else if(CharEquipment.CharacterArmor == null && CharEquipment.LeftHand_Armor != null)
            {
                ShieldBonus = CharEquipment.LeftHand_Armor.ArmorBonus;
                AC = ACBase + ShieldBonus + DexModifier;
            }

            else if(CharEquipment.CharacterArmor != null && CharEquipment.LeftHand_Armor == null)
            {
                ArmorBonus = CharEquipment.CharacterArmor.ArmorBonus;

                if (CharEquipment.CharacterArmor.DexAdd == true && CharEquipment.CharacterArmor.HasMax == false)
                {
                    AC = ACBase + ArmorBonus + DexModifier;
                }

                else if (CharEquipment.CharacterArmor.DexAdd == true && CharEquipment.CharacterArmor.HasMax == true)
                {
                    if(DexModifier <= 2)
                    {
                        AC = ACBase + ArmorBonus + DexModifier;
                    }

                    else
                    {
                        AC = 12 + ArmorBonus;
                    }
                }

                else if (CharEquipment.CharacterArmor.DexAdd == false)
                {
                    AC = ACBase + ArmorBonus;
                }
            }

            else if(CharEquipment.CharacterArmor == null && CharEquipment.LeftHand_Armor == null)
            {                
                AC = ACBase + DexModifier;
            }

            if(acChanged != null)
            {
                acChanged.Invoke();
            }

        }

        public void Set_StrScore_byText(string boxText)
        {
            StrScore = int.Parse(boxText);
        }

        public void Calculate_StrModifier()
        {
            StrModifier = CalculateModifier(StrScore);
        }

        public void Set_DexScore_byText(string boxText)
        {
            DexScore = int.Parse(boxText);
        }

        public void Calculate_DexModifier()
        {
            DexModifier = CalculateModifier(DexScore);
        }

        public void Set_ConScore_byText(string boxText)
        {
            ConScore = int.Parse(boxText);
        }

        public void Calculate_ConModifier()
        {
            ConModifier = CalculateModifier(ConScore);
        }

        public void Set_IntScore_byText(string boxText)
        {
            IntScore = int.Parse(boxText);
        }

        public void Calculate_IntModifier()
        {
            IntModifier = CalculateModifier(IntScore);
        }

        public void Set_WisScore_byText(string boxText)
        {
            WisScore = int.Parse(boxText);
        }

        public void Calculate_WisModifier()
        {
            WisModifier = CalculateModifier(WisScore);
        }

        public void Set_ChaScore_byText(string boxText)
        {
            ChaScore = int.Parse(boxText);
        }

        public void Calculate_ChaModifier()
        {
            ChaModifier = CalculateModifier(ChaScore);
        }

        public void SetAllAbilityScores(int[] abilityScores)
        {
            if(abilityScores.Length <= 6)
            {
                StrScore = abilityScores[0];
                DexScore = abilityScores[1];
                ConScore = abilityScores[2];
                IntScore = abilityScores[3];
                WisScore = abilityScores[4];
                ChaScore = abilityScores[5];
            }
        }

        public int CalculateModifier(int score)
        {
            int tempScore;
            int tempMod;
            int tempVal_01;
            decimal tempVal_02;

            tempScore = score;
            tempVal_01 = tempScore - 10;
            tempVal_02 = tempVal_01 / 2m;

            tempVal_01 = (int)Math.Floor(tempVal_02);

            tempMod = tempVal_01;

            return tempMod;
        }

        public void CalculateAbilityModifiers()
        {
            Calculate_StrModifier();
            Calculate_DexModifier();
            Calculate_ConModifier();
            Calculate_IntModifier();
            Calculate_WisModifier();
            Calculate_ChaModifier();
        }

        public void Set_SaveBaseValues()
        {
            STR_Save.AbilityBonus = StrModifier;
            DEX_Save.AbilityBonus = DexModifier;
            CON_Save.AbilityBonus = ConModifier;
            INT_Save.AbilityBonus = IntModifier;
            WIS_Save.AbilityBonus = WisModifier;
            CHA_Save.AbilityBonus = ChaModifier;
        }

        public void UI_Set_SaveProficiencies(bool strProf, bool dexProf, bool conProf, bool intProf, bool wisProf, bool chaProf)
        {
            STR_Save.IsProficient = strProf;
            DEX_Save.IsProficient = dexProf;
            CON_Save.IsProficient = conProf;
            INT_Save.IsProficient = intProf;
            WIS_Save.IsProficient = wisProf;
            CHA_Save.IsProficient = chaProf;
        }

        public void Calculate_SaveModifiers()
        {
            STR_Save.Calculate_SaveModifier(ProficiencyBonus);
            DEX_Save.Calculate_SaveModifier(ProficiencyBonus);
            CON_Save.Calculate_SaveModifier(ProficiencyBonus);
            INT_Save.Calculate_SaveModifier(ProficiencyBonus);
            WIS_Save.Calculate_SaveModifier(ProficiencyBonus);
            CHA_Save.Calculate_SaveModifier(ProficiencyBonus);
        }

        public void Set_SkillBaseValues()
        {
            Acrobatics.AbilityBonus = DexModifier;
            AnimalHandling.AbilityBonus = WisModifier;
            Arcana.AbilityBonus = IntModifier;
            Athletics.AbilityBonus = StrModifier;

            Deception.AbilityBonus = ChaModifier;

            History.AbilityBonus = IntModifier;
            Insight.AbilityBonus = WisModifier;
            Intimidation.AbilityBonus = ChaModifier;
            Investigation.AbilityBonus = IntModifier;

            Medicine.AbilityBonus = WisModifier;
            Nature.AbilityBonus = IntModifier;

            Perception.AbilityBonus = WisModifier;
            Performance.AbilityBonus = ChaModifier;
            Persuasion.AbilityBonus = ChaModifier;

            Religion.AbilityBonus = IntModifier;

            SleightOfHand.AbilityBonus = DexModifier;
            Stealth.AbilityBonus = DexModifier;

            Survival.AbilityBonus = DexModifier;
        }

        public void UI_Set_Proficiencies_strSkills(bool athletics)
        {
            Athletics.IsProficient = athletics;
        }

        public void UI_Set_Proficiencies_dexSkills(bool acrobatics, bool sleightOfHand, bool stealth)
        {
            Acrobatics.IsProficient = acrobatics;
            SleightOfHand.IsProficient =sleightOfHand;
            Stealth.IsProficient = stealth;
        }

        public void UI_Set_Proficiencies_intSkills(bool arcana, bool history, bool investigation, bool nature, bool religion)
        {
            Arcana.IsProficient = arcana;
            History.IsProficient = history;
            Investigation.IsProficient = investigation;
            Nature.IsProficient = nature;
            Religion.IsProficient = religion;
        }

        public void UI_Set_Proficiencies_wisSkills(bool animalHandling, bool insight, bool medicine, bool perception, bool survival)
        {
            AnimalHandling.IsProficient = animalHandling;
            Insight.IsProficient = insight;
            Medicine.IsProficient = medicine;
            Perception.IsProficient = perception;
            Survival.IsProficient = survival;
        }

        public void UI_Set_Proficiencies_chaSkills(bool deception, bool intimidation, bool performance, bool persuasion)
        {
            Deception.IsProficient = deception;
            Intimidation.IsProficient = intimidation;
            Performance.IsProficient = performance;
            Persuasion.IsProficient = persuasion;
        }

        public void Calculate_SkillModifiers()
        {
            Acrobatics.Calculate_SkillModifier(ProficiencyBonus);
            AnimalHandling.Calculate_SkillModifier(ProficiencyBonus);
            Arcana.Calculate_SkillModifier(ProficiencyBonus);
            Athletics.Calculate_SkillModifier(ProficiencyBonus);

            Deception.Calculate_SkillModifier(ProficiencyBonus);

            History.Calculate_SkillModifier(ProficiencyBonus);
            Insight.Calculate_SkillModifier(ProficiencyBonus);
            Intimidation.Calculate_SkillModifier(ProficiencyBonus);
            Investigation.Calculate_SkillModifier(ProficiencyBonus);

            Medicine.Calculate_SkillModifier(ProficiencyBonus);
            Nature.Calculate_SkillModifier(ProficiencyBonus);

            Perception.Calculate_SkillModifier(ProficiencyBonus);
            Performance.Calculate_SkillModifier(ProficiencyBonus);
            Persuasion.Calculate_SkillModifier(ProficiencyBonus);

            Religion.Calculate_SkillModifier(ProficiencyBonus);

            SleightOfHand.Calculate_SkillModifier(ProficiencyBonus);
            Stealth.Calculate_SkillModifier(ProficiencyBonus);

            Survival.Calculate_SkillModifier(ProficiencyBonus);
        }

        public void Level_Up()
        {
            Level++;
            Update_HitDice();
            Update_ProfBonus();
        }

        public void Load_Character(CharacterData charData)
        {
            PlayerName = charData.pName;
            CharacterName = charData.cName;

            CharacterRace = charData.race;
            CharacterSubrace = charData.subrace;

            CharacterClass = charData.charClass;

            Alignment = charData.alignment;
            Background = charData.background;

            Level = charData.level;
            Update_ProfBonus();

            MaxHP = charData.maxHP;
            CurrentHP = charData.currHP;

            TempHP = charData.tempHP;

            IsAlive = charData.charIsAlive;
            IsConscious = charData.charIsConscious;

            HitDice = charData.HD;
            CurrentHitDice = charData.currHD;

            StrScore = charData.strength;
            DexScore = charData.dexerity;
            ConScore = charData.constitution;
            IntScore = charData.intelligence;
            WisScore = charData.wisdom;
            ChaScore = charData.charisma;

            CalculateAbilityModifiers();
            Set_SaveBaseValues();
            Set_SkillBaseValues();

            UI_Set_SaveProficiencies(charData.str_ST, charData.dex_ST, charData.con_ST, charData.int_ST, charData.wis_ST, charData.cha_ST);
            
            Calculate_SaveModifiers();

            UI_Set_Proficiencies_strSkills(charData.athletics);
            UI_Set_Proficiencies_dexSkills(charData.acrobatics, charData.sleightOfHand, charData.stealth);
            UI_Set_Proficiencies_intSkills(charData.arcana, charData.history, charData.investigation, charData.nature, charData.religion);
            UI_Set_Proficiencies_wisSkills(charData.animalHandling, charData.insight, charData.medicine, charData.perception, charData.survival);
            UI_Set_Proficiencies_chaSkills(charData.deception, charData.intimidation, charData.performance, charData.persuasion);

            Calculate_SkillModifiers();                       

            Age = charData.CD_Age;
            Height = charData.CD_Height;
            Weight = charData.CD_Weight;

            Eyes = charData.CD_Eyes;
            Skin = charData.CD_Skin;
            Hair = charData.CD_Hair;

            CharApperance = charData.CD_charAppearance;
            BackgroundStory = charData.CD_backgroundStory;
            AlliesAndOrgas = charData.CD_AlliesAndOrgas;

            cInventory = charData.CD_Inventory;
            CharEquipment = charData.CD_Equipment;
        }
    }
}
