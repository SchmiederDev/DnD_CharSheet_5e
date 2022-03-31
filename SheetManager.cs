using System;
using System.Collections.Generic;
using System.Windows;
using Newtonsoft.Json;

namespace DnD_CharSheet_5e
{
    public class SheetManager
    {
        public static SheetManager CS_Manager_Inst;

        public SheetManager()
        {
            if (CS_Manager_Inst == null)
            {
                CS_Manager_Inst = this;
            }
        }

        public Character character { get; set; } = new Character();

        public Character CharGenCharacter { get; set; } = new Character();
        

        public D20_System dSys { get; set; } = new D20_System();

        public List<Race> CharacterRaces { get; set; } = new List<Race>();
        public List<Dragonborn> Dragonborns { get; set; } = new List<Dragonborn>();

        public List<DnDLanguage> Languages { get; set; } = new List<DnDLanguage>();

        //The magical energetical field that surrounds and permeates the worlds of D&D is called 'The Weave' in the 'Forgotten Realms' campaign setting.
        //The instance of Mystra is therefore called 'theWeave'

        public Mystra theWeave = new Mystra();

        bool IsTempHPactive = false;

        public static uint DeathSaveDC { get; } = 10;

        // consider to load ALL relevant databases here instead of partially mainwindow

        public void Init_DataBases()
        {
            FileManager.FM_Inst.Set_RDBPath();

            try
            {
                Load_RaceDataBase(FileManager.FM_Inst.Read_RaceDataBase());
                //MessageBox.Show("Loaded successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            FileManager.FM_Inst.Set_LanguageDBPath();

            try
            {
                Load_LanguageDataBase(FileManager.FM_Inst.Read_LanguageDataBase());
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            FileManager.FM_Inst.Set_DragonbornDBPath();

            try
            {
                Load_Dragonborn(FileManager.FM_Inst.Read_DragonbornDB());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public void Load_RaceDataBase(string jsonRDB)
        {
            CharacterRaces = JsonConvert.DeserializeObject<List<Race>>(jsonRDB);
        }

        public void Load_Dragonborn(string jsonDDB)
        {
            Dragonborns = JsonConvert.DeserializeObject<List<Dragonborn>>(jsonDDB);
        }

        public void Load_LanguageDataBase(string jsonLDB)
        {
            Languages = JsonConvert.DeserializeObject<List<DnDLanguage>>(jsonLDB);
        }

        public void Init_TempHPCallback()
        {
            character.tempHPChanged += Activate_Deactive_TempHP;
        }

        public void Activate_Deactive_TempHP()
        {
            if (character.TempHP > 0)
            {
                IsTempHPactive = true;
            }

            else
            {
                IsTempHPactive = false;
            }
        }

        public int Roll_for_Initiative()
        {
            int result;

            result = dSys.Roll_D20() + character.InitiativeBonus;
            
            return result;
        }

        public int Melee_Attack()
        {
            int result;
            result = dSys.Roll_D20() + character.Strength.Modifier + character.ProficiencyBonus;

            return result;
        }

        public int Ranged_Attack()
        {
            int result;
            result = dSys.Roll_D20() + character.Dexterity.Modifier + character.ProficiencyBonus;

            return result;
        }

        public int Damage_Roll()
        {
            int result;

            if (character.CharEquipment.RightHand_Weapon != null && character.CharEquipment.RightHand_Weapon.IsRanged == false)
            {
                result = dSys.Roll_Custom((int)character.CharEquipment.RightHand_Weapon.DamageNominator, (int)character.CharEquipment.RightHand_Weapon.DamageDenominator) + character.Strength.Modifier;
                return result;
            }

            else if (character.CharEquipment.RightHand_Weapon != null && character.CharEquipment.RightHand_Weapon.IsRanged == true)
            {
                result = dSys.Roll_Custom((int)character.CharEquipment.RightHand_Weapon.DamageNominator, (int)character.CharEquipment.RightHand_Weapon.DamageDenominator) + character.Dexterity.Modifier;
                return result;
            }

            else
            {
                MessageBox.Show($"You have no weapon equiped. Damage roll will be counted as 'Unarmed Strike'");
                result = 1 + character.Strength.Modifier;
                return result;
            }
        }

        public void Get_Hit(int damage)
        {
            if (IsTempHPactive == false)
            {
                int tempCurrHP = character.CurrentHP;
                tempCurrHP -= damage;
                character.Set_CurrHP(tempCurrHP);
            }

            else
            {
                int tempCurrHP = character.CurrentHP;

                int tempTempHP = character.TempHP;
                int tempHP_Excess = tempTempHP;

                tempHP_Excess -= damage;

                if (tempHP_Excess >= 0)
                {
                    character.Set_tempHP(tempHP_Excess);
                }

                else
                {
                    character.Set_tempHP(0);

                    tempCurrHP += tempHP_Excess;
                    character.Set_CurrHP(tempCurrHP);
                }
            }
        }

        public void Heal_Amount(int hpHealed)
        {
            int tempHP = character.CurrentHP;
            tempHP += hpHealed;

            if (tempHP > character.MaxHP)
            {
                tempHP = character.MaxHP;
                character.Set_CurrHP(tempHP);
            }

            else
            {
                character.Set_CurrHP(tempHP);
            }
        }

        public int Heal_withDice(int numerator, int denominator)
        {
            int tempHP = character.CurrentHP;

            int hpHealed = dSys.Roll_Custom(numerator, denominator);

            tempHP += hpHealed;

            if (tempHP > character.MaxHP)
            {
                tempHP = character.MaxHP;
                character.Set_CurrHP(tempHP);
            }

            else
            {
                character.Set_CurrHP(tempHP);
            }

            return hpHealed;
        }

        public void Add_TempHP_withDice(int numerator, int denominator)
        {
            int tempTempHP = dSys.Roll_Custom(numerator, denominator);
            character.Set_tempHP(tempTempHP);
        }

        public int Roll_DeathSave()
        {
            int DS_Result = dSys.Roll_D20();
            return DS_Result;
        }

        public bool DeathSave(int result)
        {
            // The declaration of this boolean ('DS' = Death Save -> Death Save is Success/Failure) isn't necessary, but to make it more obvious what is happening here - I declare its nonetheless.
            bool DS_IsSuccess;
            int DSresult = result;

            if (DSresult >= DeathSaveDC)
            {
                DS_IsSuccess = true;
                return DS_IsSuccess;
            }

            else
            {
                DS_IsSuccess = false;
                return DS_IsSuccess;
            }
        }

        public void CharGen_SetLanguages()
        {            

           for(int i = 0; i < CharGenCharacter.CharRace.RaceBackground.Languages.Length; i++)
           {
                CharGenCharacter.CharLanguages.Add(CharGenCharacter.CharRace.RaceBackground.Languages[i]);
           }
        }

    }    

}

public struct DnDLanguage
{
    public string Language { get; set; }
    public string TypicalSpeakers { get; set; }
    public string Script { get; set; }
}
