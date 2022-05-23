using System;
using System.Collections.Generic;
using System.Windows;

namespace DnD_CharSheet_5e
{
    public class Character
    {
        public string PlayerName { get; set; }
        public string CharacterName { get; set; }

        public string RaceName { get; set; }
        public string SubraceName { get; set; }

        public string ClassName { get; set; }
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

        public Ability Strength { get; set; } = new Ability();
        public Ability Dexterity { get; set; } = new Ability();
        public Ability  Constitution { get; set; } = new Ability();
        public Ability Intelligence { get; set; } = new Ability();
        public Ability Wisdom { get; set; } = new Ability();
        public Ability Charisma { get; set; } = new Ability();

        public List<Ability> Abilities = new List<Ability>();


        public SavingThrow STR_Save { get; set; } = new SavingThrow();
        public SavingThrow DEX_Save { get; set; } = new SavingThrow();
        public SavingThrow CON_Save { get; set; } = new SavingThrow();
        public SavingThrow INT_Save { get; set; } = new SavingThrow();
        public SavingThrow WIS_Save { get; set; } = new SavingThrow();
        public SavingThrow CHA_Save { get; set; } = new SavingThrow();

        public List<SavingThrow> Saves { get; private set; } = new List<SavingThrow>();


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


        public List<Skill> Skills { get; set; } = new List<Skill>();

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
            Init_Abilities();
            Init_Saves();
            Init_Skills();
        }

        private void Init_Abilities()
        {
            Strength.AbilityName = "Strength";
            Strength.ReferenceKey = "STR";
            Abilities.Add(Strength);

            Dexterity.AbilityName = "Dexterity";
            Dexterity.ReferenceKey = "DEX";
            Abilities.Add(Dexterity);

            Constitution.AbilityName = "Constitution";
            Constitution.ReferenceKey = "CON";
            Abilities.Add(Constitution);

            Intelligence.AbilityName = "Intelligence";
            Intelligence.ReferenceKey = "INT";
            Abilities.Add(Intelligence);

            Wisdom.AbilityName = "Wisdom";
            Wisdom.ReferenceKey = "WIS";
            Abilities.Add(Wisdom);

            Charisma.AbilityName = "Charisma";
            Charisma.ReferenceKey = "CHA";
            Abilities.Add(Charisma);
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

        private void Init_Skills()
        {
            Acrobatics.SkillName = "Acrobatics";
            Acrobatics.AbilityReferenceKey = "DEX";
            Skills.Add(Acrobatics);

            AnimalHandling.SkillName = "Animal Handling";
            AnimalHandling.AbilityReferenceKey = "WIS";
            Skills.Add(AnimalHandling);

            Arcana.SkillName = "Arcana";
            Arcana.AbilityReferenceKey = "INT";
            Skills.Add(Arcana);

            Athletics.SkillName = "Athletics";
            Athletics.AbilityReferenceKey = "STR";
            Skills.Add(Athletics);

            Deception.SkillName = "Deception";
            Deception.AbilityReferenceKey = "CHA";
            Skills.Add(Deception);

            History.SkillName = "History";
            History.AbilityReferenceKey = "INT";
            Skills.Add(History);

            Insight.SkillName = "Insight";
            Insight.AbilityReferenceKey = "WIS";
            Skills.Add(Insight);

            Intimidation.SkillName = "Intimidation";
            Intimidation.AbilityReferenceKey = "CHA";
            Skills.Add(Intimidation);

            Investigation.SkillName = "Investigation";
            Investigation.AbilityReferenceKey = "INT";
            Skills.Add(Investigation);

            Medicine.SkillName = "Medicine";
            Medicine.AbilityReferenceKey = "WIS";
            Skills.Add(Medicine);

            Nature.SkillName = "Nature";
            Nature.AbilityReferenceKey = "INT";
            Skills.Add(Nature);

            Perception.SkillName = "Perception";
            Perception.AbilityReferenceKey = "WIS";
            Skills.Add(Perception);

            Performance.SkillName = "Performance";
            Performance.AbilityReferenceKey = "CHA";
            Skills.Add(Performance);

            Persuasion.SkillName = "Persuasion";
            Persuasion.AbilityReferenceKey = "CHA";
            Skills.Add(Persuasion);

            Religion.SkillName = "Religion";
            Religion.AbilityReferenceKey = "INT";
            Skills.Add(Religion);

            SleightOfHand.SkillName = "Sleight of Hand";
            SleightOfHand.AbilityReferenceKey = "DEX";
            Skills.Add(SleightOfHand);

            Stealth.SkillName = "Stealth";
            Stealth.AbilityReferenceKey = "DEX";
            Skills.Add(Stealth);

            Survival.SkillName = "Survival";
            Survival.AbilityReferenceKey = "WIS";
            Skills.Add(Survival);
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
            InitiativeBonus = Dexterity.Modifier;
        }

        public bool Check_STR_Requirement(Armor armor)
        {
            if(armor.StrMax <= Strength.Score)
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
                    AC = ACBase + ArmorBonus + ShieldBonus + Dexterity.Modifier;
                }

                else if (CharEquipment.CharacterArmor.DexAdd == true && CharEquipment.CharacterArmor.HasMax == true)
                {
                    if (Dexterity.Modifier <= 2)
                    {
                        AC = ACBase + ArmorBonus + ShieldBonus + Dexterity.Modifier;
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
                AC = ACBase + ShieldBonus + Dexterity.Modifier;
            }

            else if(CharEquipment.CharacterArmor != null && CharEquipment.LeftHand_Armor == null)
            {
                ArmorBonus = CharEquipment.CharacterArmor.ArmorBonus;

                if (CharEquipment.CharacterArmor.DexAdd == true && CharEquipment.CharacterArmor.HasMax == false)
                {
                    AC = ACBase + ArmorBonus + Dexterity.Modifier;
                }

                else if (CharEquipment.CharacterArmor.DexAdd == true && CharEquipment.CharacterArmor.HasMax == true)
                {
                    if(Dexterity.Modifier <= 2)
                    {
                        AC = ACBase + ArmorBonus + Dexterity.Modifier;
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
                AC = ACBase + Dexterity.Modifier;
            }

            if(acChanged != null)
            {
                acChanged.Invoke();
            }

        }

        public void Set_StrScore_byText(string boxText)
        {
            Strength.Score = int.Parse(boxText);
        }


        public void Set_DexScore_byText(string boxText)
        {
            Dexterity.Score = int.Parse(boxText);
        }

        public void Set_ConScore_byText(string boxText)
        {
            Constitution.Score = int.Parse(boxText);
        }

        public void Set_IntScore_byText(string boxText)
        {
            Intelligence.Score = int.Parse(boxText);
        }

        public void Set_WisScore_byText(string boxText)
        {
            Wisdom.Score = int.Parse(boxText);
        }

        public void Set_ChaScore_byText(string boxText)
        {
            Charisma.Score = int.Parse(boxText);
        }

        public void SetAllAbilityScores(int[] abilityScores)
        {
            if(abilityScores.Length <= 6)
            {
                Strength.Score = abilityScores[0];
                Dexterity.Score = abilityScores[1];
                Constitution.Score = abilityScores[2];
                Intelligence.Score = abilityScores[3];
                Wisdom.Score = abilityScores[4];
                Charisma.Score = abilityScores[5];
            }
        }

        public void CalculateAbilityModifiers()
        {
            foreach(Ability ability in Abilities)
            {
                ability.Calculate_Modifier();
            }
        }

        public void CalculateSavingThrowModifiers()
        {
            foreach(SavingThrow savingThrow in Saves)
            {
                savingThrow.Calculate_SaveModifier(ProficiencyBonus);
            }
        }

        public void CalculateSkillModifiers()
        {
            foreach(Skill skill in Skills)
            {
                skill.Calculate_SkillModifier(ProficiencyBonus);
            }
        }

        public void Set_SaveBaseValues()
        {
            STR_Save.AbilityBonus = Strength.Modifier;
            DEX_Save.AbilityBonus = Dexterity.Modifier;
            CON_Save.AbilityBonus = Constitution.Modifier;
            INT_Save.AbilityBonus = Intelligence.Modifier;
            WIS_Save.AbilityBonus = Wisdom.Modifier;
            CHA_Save.AbilityBonus = Charisma.Modifier;
                        
        }

        public void Set_SaveProficiencies(bool strProf, bool dexProf, bool conProf, bool intProf, bool wisProf, bool chaProf)
        {
            STR_Save.IsProficient = strProf;
            DEX_Save.IsProficient = dexProf;
            CON_Save.IsProficient = conProf;
            INT_Save.IsProficient = intProf;
            WIS_Save.IsProficient = wisProf;
            CHA_Save.IsProficient = chaProf;
        }

        public void Set_SkillBaseValues()
        {
            Acrobatics.AbilityBonus = Dexterity.Modifier;
            AnimalHandling.AbilityBonus = Wisdom.Modifier;
            Arcana.AbilityBonus = Intelligence.Modifier;
            Athletics.AbilityBonus = Strength.Modifier;

            Deception.AbilityBonus = Charisma.Modifier;

            History.AbilityBonus = Intelligence.Modifier;
            Insight.AbilityBonus = Wisdom.Modifier;
            Intimidation.AbilityBonus = Charisma.Modifier;
            Investigation.AbilityBonus = Intelligence.Modifier;

            Medicine.AbilityBonus = Wisdom.Modifier;
            Nature.AbilityBonus = Intelligence.Modifier;

            Perception.AbilityBonus = Wisdom.Modifier;
            Performance.AbilityBonus = Charisma.Modifier;
            Persuasion.AbilityBonus = Charisma.Modifier;

            Religion.AbilityBonus = Intelligence.Modifier;

            SleightOfHand.AbilityBonus = Dexterity.Modifier;
            Stealth.AbilityBonus = Dexterity.Modifier;

            Survival.AbilityBonus = Dexterity.Modifier;
        }

        public void Set_Proficiencies_strSkills(bool athletics)
        {
            Athletics.IsProficient = athletics;
        }

        public void Set_Proficiencies_dexSkills(bool acrobatics, bool sleightOfHand, bool stealth)
        {
            Acrobatics.IsProficient = acrobatics;
            SleightOfHand.IsProficient =sleightOfHand;
            Stealth.IsProficient = stealth;
        }

        public void Set_Proficiencies_intSkills(bool arcana, bool history, bool investigation, bool nature, bool religion)
        {
            Arcana.IsProficient = arcana;
            History.IsProficient = history;
            Investigation.IsProficient = investigation;
            Nature.IsProficient = nature;
            Religion.IsProficient = religion;
        }

        public void Set_Proficiencies_wisSkills(bool animalHandling, bool insight, bool medicine, bool perception, bool survival)
        {
            AnimalHandling.IsProficient = animalHandling;
            Insight.IsProficient = insight;
            Medicine.IsProficient = medicine;
            Perception.IsProficient = perception;
            Survival.IsProficient = survival;
        }

        public void Set_Proficiencies_chaSkills(bool deception, bool intimidation, bool performance, bool persuasion)
        {
            Deception.IsProficient = deception;
            Intimidation.IsProficient = intimidation;
            Performance.IsProficient = performance;
            Persuasion.IsProficient = persuasion;
        }

        public void Level_Up()
        {
            Level++;
            Update_HitDice();
            Update_ProfBonus();
        }

        public void Load_Character(CharacterData charData)
        {
            Init_Basics();
            PlayerName = charData.pName;
            CharacterName = charData.cName;

            RaceName = charData.race;
            SubraceName = charData.subrace;

            ClassName = charData.charClass;

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

            Strength.Score = charData.strength;           
            Dexterity.Score = charData.dexerity;
            Constitution.Score = charData.constitution;
            Intelligence.Score = charData.intelligence;
            Wisdom.Score = charData.wisdom;
            Charisma.Score = charData.charisma;

            CalculateAbilityModifiers();
           
            Set_SaveBaseValues();            

            Set_SaveProficiencies(charData.str_ST, charData.dex_ST, charData.con_ST, charData.int_ST, charData.wis_ST, charData.cha_ST);
            
            CalculateSavingThrowModifiers();

            Set_Proficiencies_strSkills(charData.athletics);
            Set_Proficiencies_dexSkills(charData.acrobatics, charData.sleightOfHand, charData.stealth);
            Set_Proficiencies_intSkills(charData.arcana, charData.history, charData.investigation, charData.nature, charData.religion);
            Set_Proficiencies_wisSkills(charData.animalHandling, charData.insight, charData.medicine, charData.perception, charData.survival);
            Set_Proficiencies_chaSkills(charData.deception, charData.intimidation, charData.performance, charData.persuasion);
            
            Set_SkillBaseValues();
            CalculateSkillModifiers();                       

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

        public void Reset_Character()
        {
            Age = 0;
            Height = 0;
            Weight = 0;

            Eyes = null;
            Skin = null;
            Hair = null;

            CharApperance = null;
            BackgroundStory = null;
            AlliesAndOrgas = null;

            cInventory.Clear_Inventory();
            CharEquipment.Clear_Equipment();
        }
    }
}
