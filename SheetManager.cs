using System;
using System.Collections.Generic;
using System.Windows;

namespace DnD_CharSheet_5e
{
    public class SheetManager
    {        

        public Character character = new Character();
        public D20_System dSys = new D20_System();
        
        public void Load_Character(CharacterData charData)
        {
            character.Set_playerName(charData.pName);
            character.Set_charName(charData.cName);

            character.Set_Race(charData.race);
            character.Set_Subrace(charData.subrace);

            character.Set_charClass(charData.charClass);

            character.Set_Alignment(charData.alignment);
            character.Set_Background(charData.background);

            character.Set_charLvl(charData.level);

            character.Set_maxHP(charData.maxHP);
            character.Set_currHP(charData.currHP);

            character.Set_tempHP(charData.tempHP);

            character.Set_hitDice(charData.HD);
            character.Set_currHD(charData.currHD);

            character.Set_strValue(charData.strength);
            character.Set_dexValue(charData.dexerity);
            character.Set_conValue(charData.constitution);
            character.Set_intValue(charData.intelligence);
            character.Set_wisValue(charData.wisdom);
            character.Set_chaValue(charData.charisma);

            character.Set_SaveProficiencies(charData.str_ST, charData.dex_ST, charData.con_ST, charData.int_ST, charData.wis_ST, charData.cha_ST);

            character.Set_Proficiencies_strSkills(charData.athletics);
            character.Set_Proficiencies_dexSkills(charData.acrobatics, charData.sleightOfHand, charData.stealth);
            character.Set_Proficiencies_intSkills(charData.arcana, charData.history, charData.investigation, charData.nature, charData.religion);
            character.Set_Proficiencies_wisSkills(charData.animalHandling, charData.insight, charData.medicine, charData.perception, charData.survival);
            character.Set_Proficiencies_chaSkills(charData.deception, charData.intimidation, charData.performance, charData.persuasion);

        }

        public Character Get_Character()
        {
            return character;
        }

        public int Ability_Check(int modifier)
        {
            int result;

            do
            {
                result = dSys.Roll_D20() + modifier;
            } while (result < 1);
           
            return result;
        }
        
        public int Roll_for_Initiative()
        {
            int result;

            do
            {
                result = dSys.Roll_D20() + character.Get_iniBonus();
            } while (result < 1);
            
            return result;
        }

        public int Skill_Check(int skillModifier)
        {
            int result;

            do
            {
                result = dSys.Roll_D20() + skillModifier;
            } while (result < 1);
            
            return result;
        }

        public int SavingThrow(int saveModifier)
        {
            int result;

            do
            {
                result = dSys.Roll_D20() + saveModifier;
                
            } while (result < 1);

            return result;
        }

        public int Melee_Attack()
        {
            int result;
            result = dSys.Roll_D20() + character.Get_strModifier() + character.Get_ProfBonus();

            return result;
        }

        public int Ranged_Attack()
        {
            int result;
            result = dSys.Roll_D20() + character.Get_dexModifier() + character.Get_ProfBonus();

            return result;
        }

    }
}
