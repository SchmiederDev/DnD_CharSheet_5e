using System;
using System.Collections.Generic;
using System.Windows;

namespace DnD_CharSheet_5e
{
    public class Character
    {
        public Inventory cInventory { get; set; } = new Inventory();                                         //cInventory = 'character Inventory'

        public Equipment CharEquipment = new Equipment();

        string playerName;
        string charName;

        string charRace;
        string charSubrace;

        string charClass;
        string charAlignment;
        string charBackground;

        public List<string> CharLanguages = new List<string>();

        int charLevel;

        int proficiencyBonus = 2;

        int[] profBonusIncrease = {5, 9, 13, 17};

        int maxHP;
        int currHP;
        int tempHP;

        bool characterIsAlive = true;
        bool characterIsConscious = true;

        int hitDice;
        int currHitDice;

        int iniBonus;

        uint AC = 10;
        uint armorBonus = 0;
        uint shieldBonus = 0;

        int strValue;
        int dexValue;
        int conValue;
        int intValue;
        int wisValue;
        int chaValue;

        int strModifier;
        int dexModifier;
        int conModifier;
        int intModifier;
        int wisModifier;
        int chaModifier;

        SavingThrow STR_Save = new SavingThrow();
        SavingThrow DEX_Save = new SavingThrow();
        SavingThrow CON_Save = new SavingThrow();
        SavingThrow INT_Save = new SavingThrow();
        SavingThrow WIS_Save = new SavingThrow();
        SavingThrow CHA_Save = new SavingThrow();

        Skill Acrobatics = new Skill();
        Skill AnimalHandling = new Skill();
        Skill Arcana = new Skill();
        Skill Athletics = new Skill();

        Skill Deception = new Skill();

        Skill History = new Skill();
        Skill Insight = new Skill();
        Skill Intimidation = new Skill();
        Skill Investigation = new Skill();

        Skill Medicine = new Skill();
        Skill Nature = new Skill();

        Skill Perception = new Skill();
        Skill Performance = new Skill();
        Skill Persuasion = new Skill();

        Skill Religion = new Skill();

        Skill SleightOfHand = new Skill();
        Skill Stealth = new Skill();

        Skill Survival = new Skill();

        public int Age { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }        

        public string Eyes { get; set; }
        public string Hair { get; set; }
        public string Skin { get; set; }

        public string CharApperance { get; set; }
        public string BackgroundStory { get; set; }
        public string AlliesAndOrgas { get; set; }

        public delegate void OnHPChanged();
        public OnHPChanged hpChanged;

        public delegate void OnTempHPChanged();
        public OnTempHPChanged tempHPChanged;

        public delegate void OnACChanged();
        public OnACChanged acChanged;

        public void Set_playerName(string name)
        {
            playerName = name;
        }

        public string Get_playerName()
        {
            return playerName;
        }

        public void Set_charName(string name)
        {
            charName = name;
        }

        public string Get_charName()
        {
            return charName;
        }

        public void Set_Race(string race)
        {
            charRace = race;
        }

        public string Get_Race()
        {
            return charRace;
        }

        public void Set_Subrace(string subrace)
        {
            charSubrace = subrace;
        }

        public string Get_Subrace()
        {
            return charSubrace;
        }

       public void Set_charClass(string className)
       {
            charClass = className;
       }

        public string Get_charClass()
        {
            return charClass;
        }

        public void Set_Alignment(string alignment)
        {
            charAlignment = alignment;
        }

        public string Get_Alignment()
        {
            return charAlignment;
        }

        public void Set_Background(string background)
        {
            charBackground = background;
        }

        public string Get_Background()
        {
            return charBackground;
        }

        public void Set_charLvl(int level)
        {
            if(level <= 20)
            {
                charLevel = level;
            }
        }

        public int Get_charLvl()
        {
            return charLevel;
        }

        public void Increase_charLvl()
        {
            if(charLevel < 20)
            {
                charLevel ++;                
            }
            
            
        }

        public int Get_ProfBonus()
        {
            return proficiencyBonus;
        }

        public void Set_maxHP_byText(string boxText)
        {
            maxHP = int.Parse(boxText);
        }

        public void Set_maxHP(int hpMaximum)
        {
            maxHP = hpMaximum;
        }

        public int Get_maxHP()
        {
            return maxHP;
        }

        public void Set_AliveStatus(bool isAlive)
        {
            characterIsAlive = isAlive;
        }

        public bool Get_AliveStatus()
        {
            return characterIsAlive;
        }

        public void Set_ConsciousnessStatus(bool isConscious)
        {
            characterIsConscious = isConscious;
        }

        public bool Get_ConsciousnessStatus()
        {
            return characterIsConscious;
        }

        public void Init_HP_HD()
        {
            currHP = maxHP;
            currHitDice = hitDice;
        }

        public void Set_currHP_byText(string boxText)
        {
            currHP = int.Parse(boxText);
        }

        public void Set_currHP(int hpCurrent)
        {
            currHP = hpCurrent;

            if(hpChanged != null)
            {
                hpChanged.Invoke();
            }
        }

        public int Get_currHP()
        {
            return currHP;
        }

        public void Set_tempHP_byText(string boxText)
        {
            tempHP = int.Parse(boxText);
        }

        public void Set_tempHP(int hpTemp)
        {
            tempHP = hpTemp;

            if(tempHPChanged != null)
            {
                tempHPChanged.Invoke();
            }
        }

        public int Get_tempHP()
        {
            return tempHP;
        }

        public void Set_hitDice_byText(string boxText)
        {
            hitDice = int.Parse(boxText);
        }

        public void Set_hitDice(int dice)
        {
            hitDice = dice;
        }

        public void Update_hitDice()
        {
            Set_hitDice(charLevel);
        }

        public int Get_hitDice()
        {
            return hitDice;
        }

        public void Update_ProfBonus()
        {
            foreach (int i in profBonusIncrease)
            {
                if (i == charLevel)
                {
                    proficiencyBonus++;
                }
            }
        }

        public void Set_currHitDice(string boxText)
        {
            currHitDice = int.Parse(boxText);
        }

        public void Set_currHD(int hdCurrent)
        {
            currHitDice = hdCurrent;
        }

        public int Get_currHitDice()
        {
            return currHitDice;
        }

        public void Set_iniBonus()
        {
            iniBonus = dexModifier;
        }

        public int Get_iniBonus()
        {
            return iniBonus;
        }

        public bool Check_STR_Requirement(Armor armor)
        {
            if(armor.StrMax <= strValue)
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
                armorBonus = CharEquipment.CharacterArmor.ArmorBonus;
                shieldBonus = CharEquipment.LeftHand_Armor.ArmorBonus;

                if (CharEquipment.CharacterArmor.DexAdd == true && CharEquipment.CharacterArmor.HasMax == false)
                {
                    AC = 10 + armorBonus + shieldBonus + (uint)dexModifier;
                }

                else if (CharEquipment.CharacterArmor.DexAdd == true && CharEquipment.CharacterArmor.HasMax == true)
                {
                    if (dexModifier <= 2)
                    {
                        AC = 10 + armorBonus + shieldBonus + (uint)dexModifier;
                    }

                    else
                    {
                        AC = 12 + armorBonus + shieldBonus;
                    }
                }

                else if (CharEquipment.CharacterArmor.DexAdd == false)
                {
                    AC = 10 + armorBonus + shieldBonus;
                }
            }

            else if(CharEquipment.CharacterArmor == null && CharEquipment.LeftHand_Armor != null)
            {
                shieldBonus = CharEquipment.LeftHand_Armor.ArmorBonus;
                AC = 10 + shieldBonus + (uint)dexModifier;
            }

            else if(CharEquipment.CharacterArmor != null && CharEquipment.LeftHand_Armor == null)
            {
                armorBonus = CharEquipment.CharacterArmor.ArmorBonus;

                if (CharEquipment.CharacterArmor.DexAdd == true && CharEquipment.CharacterArmor.HasMax == false)
                {
                    AC = 10 + armorBonus + (uint)dexModifier;
                }

                else if (CharEquipment.CharacterArmor.DexAdd == true && CharEquipment.CharacterArmor.HasMax == true)
                {
                    if (dexModifier <= 2)
                    {
                        AC = 10 + armorBonus + (uint)dexModifier;
                    }

                    else
                    {
                        AC = 12 + armorBonus;
                    }
                }

                else if (CharEquipment.CharacterArmor.DexAdd == false)
                {
                    AC = 10 + armorBonus;
                }
            }

            else if(CharEquipment.CharacterArmor == null && CharEquipment.LeftHand_Armor == null)
            {                
                AC = 10 + (uint)dexModifier;
            }

            if(acChanged != null)
            {
                acChanged.Invoke();
            }

        }

        public uint Get_AC()
        {
            return AC;
        }

        public void Set_strValue_byText(string boxText)
        {
            strValue = int.Parse(boxText);
        }

        public void Set_strValue(int strength)
        {
            strValue = strength;
        }

        public int Get_strValue()
        {
            return strValue;
        }

        public void Set_strModifier(int score)
        {
            strModifier = calculateModifier(score);
        }

        public int Get_strModifier()
        {
            return strModifier;
        }

        public void Set_dexValue_byText(string boxText)
        {
            dexValue = int.Parse(boxText);
        }

        public void Set_dexValue(int dexterity)
        {
            dexValue = dexterity;
        }

        public int Get_dexValue()
        {
            return dexValue;
        }

        public void Set_dexModifier(int score)
        {
            dexModifier = calculateModifier(score);
        }

        public int Get_dexModifier()
        {
            return dexModifier;
        }

        public void Set_conValue_byText(string boxText)
        {
            conValue = int.Parse(boxText);
        }

        public void Set_conValue(int constitution)
        {
            conValue = constitution;
        }

        public int Get_conValue()
        {
            return conValue;
        }

        public void Set_conModifier(int score)
        {
            conModifier = calculateModifier(score);
        }

        public int Get_conModifier()
        {
            return conModifier;
        }

        public void Set_intValue_byText(string boxText)
        {
            intValue = int.Parse(boxText);
        }

        public void Set_intValue(int intelligence)
        {
            intValue = intelligence;
        }

        public int Get_intValue()
        {
            return intValue;
        }

        public void Set_intModifier(int score)
        {
            intModifier = calculateModifier(score);
        }

        public int Get_intModifier()
        {
            return intModifier;
        }

        public void Set_wisValue_byText(string boxText)
        {
            wisValue = int.Parse(boxText);
        }

        public void Set_wisValue(int wisdom)
        {
            wisValue = wisdom;
        }

        public int Get_wisValue()
        {
            return wisValue;
        }

        public void Set_wisModifier(int score)
        {
            wisModifier = calculateModifier(score);
        }

        public int Get_wisModifier()
        {
            return wisModifier;
        }

        public void Set_chaValue_byText(string boxText)
        {
            chaValue = int.Parse(boxText);
        }

        public void Set_chaValue(int charisma)
        {
            chaValue = charisma;
        }

        public int Get_chaValue()
        {
            return chaValue;
        }

        public void Set_chaModifier(int score)
        {
            chaModifier = calculateModifier(score);
        }

        public int Get_chaModifier()
        {
            return chaModifier;
        }

        public void SetAllAbilityScores(int[] abilityScores)
        {
            if(abilityScores.Length <= 6)
            {
                strValue = abilityScores[0];
                dexValue = abilityScores[1];
                conValue = abilityScores[2];
                intValue = abilityScores[3];
                wisValue = abilityScores[4];
                chaValue = abilityScores[5];
            }

            MessageBox.Show("STR: " + strValue + " DEX: " + dexValue + " CON: " + conValue + " INT: " + intValue + " WIS " + wisValue + " CHA: " + chaValue);
        }

        public int calculateModifier(int score)
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

        public void calculateAbilityModifiers()
        {
            strModifier = calculateModifier(strValue);
            dexModifier = calculateModifier(dexValue);
            conModifier = calculateModifier(conValue);
            intModifier = calculateModifier(intValue);
            wisModifier = calculateModifier(wisValue);
            chaModifier = calculateModifier(chaValue);
        }

        public void Set_SaveBaseValues()
        {
            STR_Save.Set_AbilityBonus(strModifier);
            DEX_Save.Set_AbilityBonus(dexModifier);
            CON_Save.Set_AbilityBonus(conModifier);
            INT_Save.Set_AbilityBonus(intModifier);
            WIS_Save.Set_AbilityBonus(wisModifier);
            CHA_Save.Set_AbilityBonus(chaModifier);
        }

        public void Set_SaveProficiencies(bool strProf, bool dexProf, bool conProf, bool intProf, bool wisProf, bool chaProf)
        {
            STR_Save.Set_Proficiency(strProf);
            DEX_Save.Set_Proficiency(dexProf);
            CON_Save.Set_Proficiency(conProf);
            INT_Save.Set_Proficiency(intProf);
            WIS_Save.Set_Proficiency(wisProf);
            CHA_Save.Set_Proficiency(chaProf);
        }

        public void Calculate_SaveModifiers()
        {
            STR_Save.Set_SaveModifier(proficiencyBonus);
            DEX_Save.Set_SaveModifier(proficiencyBonus);
            CON_Save.Set_SaveModifier(proficiencyBonus);
            INT_Save.Set_SaveModifier(proficiencyBonus);
            WIS_Save.Set_SaveModifier(proficiencyBonus);
            CHA_Save.Set_SaveModifier(proficiencyBonus);
        }

        public int Get_STR_Save()
        {
            return STR_Save.Get_SaveModifier();
        }

        public bool Get_STR_Prof()
        {
            return STR_Save.Get_Proficiency();
        }

        public int Get_DEX_Save()
        {
            return DEX_Save.Get_SaveModifier();
        }

        public bool Get_DEX_Prof()
        {
            return DEX_Save.Get_Proficiency();
        }

        public int Get_CON_Save()
        {
            return CON_Save.Get_SaveModifier();
        }

        public bool Get_CON_Prof()
        {
            return CON_Save.Get_Proficiency();
        }

        public int Get_INT_Save()
        {
            return INT_Save.Get_SaveModifier();
        }

        public bool Get_INT_Prof()
        {
            return INT_Save.Get_Proficiency();
        }

        public int Get_WIS_Save()
        {
            return WIS_Save.Get_SaveModifier();
        }

        public bool Get_WIS_Prof()
        {
            return WIS_Save.Get_Proficiency();
        }

        public int Get_CHA_Save()
        {
            return CHA_Save.Get_SaveModifier();
        }

        public bool Get_CHA_Prof()
        {
            return CHA_Save.Get_Proficiency();
        }

        public void Set_SkillBaseValues()
        {            
            Acrobatics.Set_abilityBonus(dexModifier);
            AnimalHandling.Set_abilityBonus(wisModifier);
            Arcana.Set_abilityBonus(intModifier);
            Athletics.Set_abilityBonus(strModifier);

            Deception.Set_abilityBonus(chaModifier);

            History.Set_abilityBonus(intModifier);
            Insight.Set_abilityBonus(wisModifier);
            Intimidation.Set_abilityBonus(chaModifier);
            Investigation.Set_abilityBonus(intModifier);

            Medicine.Set_abilityBonus(wisModifier);
            Nature.Set_abilityBonus(intModifier);

            Perception.Set_abilityBonus(wisModifier);
            Performance.Set_abilityBonus(chaModifier);
            Persuasion.Set_abilityBonus(chaModifier);

            Religion.Set_abilityBonus(intModifier);

            SleightOfHand.Set_abilityBonus(dexModifier);
            Stealth.Set_abilityBonus(dexModifier);

            Survival.Set_abilityBonus(wisModifier);
        }

        public void Set_Proficiencies_strSkills(bool athletics)
        {
            Athletics.Set_Proficiency(athletics);
        }

        public void Set_Proficiencies_dexSkills(bool acrobatics, bool sleightOfHand, bool stealth)
        {
            Acrobatics.Set_Proficiency(acrobatics);
            SleightOfHand.Set_Proficiency(sleightOfHand);
            Stealth.Set_Proficiency(stealth);
        }

        public void Set_Proficiencies_intSkills(bool arcana, bool history, bool investigation, bool nature, bool religion)
        {
            Arcana.Set_Proficiency(arcana);
            History.Set_Proficiency(history);
            Investigation.Set_Proficiency(investigation);
            Nature.Set_Proficiency(nature);
            Religion.Set_Proficiency(religion);
        }

        public void Set_Proficiencies_wisSkills(bool animalHandling, bool insight, bool medicine, bool perception, bool survival)
        {
            AnimalHandling.Set_Proficiency(animalHandling);
            Insight.Set_Proficiency(insight);
            Medicine.Set_Proficiency(medicine);
            Perception.Set_Proficiency(perception);
            Survival.Set_Proficiency(survival);
        }

        public void Set_Proficiencies_chaSkills(bool deception, bool intimidation, bool performance, bool persuasion)
        {
            Deception.Set_Proficiency(deception);
            Intimidation.Set_Proficiency(intimidation);
            Performance.Set_Proficiency(performance);
            Persuasion.Set_Proficiency(persuasion);
        }

        public void Calculate_SkillModifiers()
        {
            Acrobatics.Set_skillModifier(proficiencyBonus);
            AnimalHandling.Set_skillModifier(proficiencyBonus);
            Arcana.Set_skillModifier(proficiencyBonus);
            Athletics.Set_skillModifier(proficiencyBonus);

            Deception.Set_skillModifier(proficiencyBonus);

            History.Set_skillModifier(proficiencyBonus);
            Insight.Set_skillModifier(proficiencyBonus);
            Intimidation.Set_skillModifier(proficiencyBonus);
            Investigation.Set_skillModifier(proficiencyBonus);

            Medicine.Set_skillModifier(proficiencyBonus);
            Nature.Set_skillModifier(proficiencyBonus);

            Perception.Set_skillModifier(proficiencyBonus);
            Performance.Set_skillModifier(proficiencyBonus);
            Persuasion.Set_skillModifier(proficiencyBonus);

            Religion.Set_skillModifier(proficiencyBonus);

            SleightOfHand.Set_skillModifier(proficiencyBonus);
            Stealth.Set_skillModifier(proficiencyBonus);

            Survival.Set_skillModifier(proficiencyBonus);
        }

        public int Get_Acrobatics()
        {
            return Acrobatics.Get_skillModifier();
        }

        public bool Get_Acrobatics_Prof()
        {
            return Acrobatics.Get_Proficiency();
        }

        public int Get_AnimalHandling()
        {
            return AnimalHandling.Get_skillModifier();
        }

        public bool Get_AnimalHandling_Prof()
        {
            return AnimalHandling.Get_Proficiency();
        }

        public int Get_Arcana()
        {
            return Arcana.Get_skillModifier();
        }

        public bool Get_Arcana_Prof()
        {
            return Arcana.Get_Proficiency();
        }

        public int Get_Athletics()
        {
            return Athletics.Get_skillModifier();
        }

        public bool Get_Athletics_Prof()
        {
            return Athletics.Get_Proficiency();
        }

        public int Get_Deception()
        {
            return Deception.Get_skillModifier();
        }

        public bool Get_Deception_Prof()
        {
            return Deception.Get_Proficiency();
        }

        public int Get_History()
        {
            return History.Get_skillModifier();
        }

        public bool Get_History_Prof()
        {
            return History.Get_Proficiency();
        }

        public int Get_Insight()
        {
            return Insight.Get_skillModifier();
        }
        public bool Get_Insight_Prof()
        {
            return Insight.Get_Proficiency();
        }
        public int Get_Intimidation()
        {
            return Intimidation.Get_skillModifier();
        }
        public bool Get_Intimidation_Prof()
        {
            return Intimidation.Get_Proficiency();
        }

        public int Get_Investigation()
        {
            return Investigation.Get_skillModifier();
        }

        public bool Get_Investigation_Prof()
        {
            return Investigation.Get_Proficiency();
        }

        public int Get_Medicine()
        {
            return Medicine.Get_skillModifier();
        }

        public bool Get_Medicine_Prof()
        {
            return Medicine.Get_Proficiency();
        }

        public int Get_Nature()
        {
            return Nature.Get_skillModifier();
        }

        public bool Get_Nature_Prof()
        {
            return Nature.Get_Proficiency();
        }

        public int Get_Perception()
        {
            return Perception.Get_skillModifier();
        }

        public bool Get_Perception_Prof()
        {
            return Perception.Get_Proficiency();
        }

        public int Get_Performance()
        {
            return Performance.Get_skillModifier();
        }

        public bool Get_Performance_Prof()
        {
            return Performance.Get_Proficiency();
        }

        public int Get_Persuasion()
        {
            return Persuasion.Get_skillModifier();
        }

        public bool Get_Persuasion_Prof()
        {
            return Persuasion.Get_Proficiency();
        }

        public int Get_Religion()
        {
            return Religion.Get_skillModifier();
        }

        public bool Get_Religion_Prof()
        {
            return Religion.Get_Proficiency();
        }

        public int Get_SleightOfHand()
        {
            return SleightOfHand.Get_skillModifier();
        }

        public bool Get_SleightOfHand_Prof()
        {
            return SleightOfHand.Get_Proficiency();
        }

        public int Get_Stealth()
        {
            return Stealth.Get_skillModifier();
        }

        public bool Get_Stealth_Prof()
        {
            return Stealth.Get_Proficiency();
        }

        public int Get_Survival()
        {
            return Survival.Get_skillModifier();
        }

        public bool Get_Survival_Prof()
        {
            return Survival.Get_Proficiency();
        }        

        public void Level_Up()
        {
            Increase_charLvl();
            Update_hitDice();
            Update_ProfBonus();
        }

        public void Load_Character(CharacterData charData)
        {
            Set_playerName(charData.pName);
            Set_charName(charData.cName);

            Set_Race(charData.race);
            Set_Subrace(charData.subrace);

            Set_charClass(charData.charClass);

            Set_Alignment(charData.alignment);
            Set_Background(charData.background);

            Set_charLvl(charData.level);
            Update_ProfBonus();

            Set_maxHP(charData.maxHP);
            Set_currHP(charData.currHP);

            Set_tempHP(charData.tempHP);

            Set_AliveStatus(charData.charIsAlive);
            Set_ConsciousnessStatus(charData.charIsConscious);

            Set_hitDice(charData.HD);
            Set_currHD(charData.currHD);

            Set_strValue(charData.strength);
            Set_dexValue(charData.dexerity);
            Set_conValue(charData.constitution);
            Set_intValue(charData.intelligence);
            Set_wisValue(charData.wisdom);
            Set_chaValue(charData.charisma);

            calculateAbilityModifiers();
            Set_SaveBaseValues();
            Set_SkillBaseValues();

            Set_SaveProficiencies(charData.str_ST, charData.dex_ST, charData.con_ST, charData.int_ST, charData.wis_ST, charData.cha_ST);
            
            Calculate_SaveModifiers();

            Set_Proficiencies_strSkills(charData.athletics);
            Set_Proficiencies_dexSkills(charData.acrobatics, charData.sleightOfHand, charData.stealth);
            Set_Proficiencies_intSkills(charData.arcana, charData.history, charData.investigation, charData.nature, charData.religion);
            Set_Proficiencies_wisSkills(charData.animalHandling, charData.insight, charData.medicine, charData.perception, charData.survival);
            Set_Proficiencies_chaSkills(charData.deception, charData.intimidation, charData.performance, charData.persuasion);

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
