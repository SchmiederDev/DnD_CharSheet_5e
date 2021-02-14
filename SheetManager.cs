using System;
using System.Collections.Generic;
using System.Windows;

namespace DnD_CharSheet_5e
{
    public class SheetManager
    {
        public static SheetManager managerInst;

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
