using System;

namespace DnD_CharSheet_5e
{
    /* SAVING THROW CLASS:
     * 
     * Saving Throws are dice rolls made to check whether a character suffers from a harmful effect such as a lightning bolt hurdled towards them,
     * the attempt of a creature to control their mind or if their are poisoned after drinking polluted water etc.
     * 
     * A 'Saving Throw' comes with a number of values determining how good a character is in fending off certain harmful effects.
     * 
     * This class is an abstract representation of these. It mainly stores the respective values, but can also calculate the modifier belonging to the 'Saving Throw'
     * (= a number added or substracted from the result of the corresponding die roll).
     * 
     * Finally, it allows to make the die roll (= getting a random number in the range of the corresponding values) for the respective Saving Throw.
     * 
     * In the game there are 6 types of Saving Throws which are stored as properties of the 'Character'-class from which also the here defined methods are called.
     * 
     */

    [Serializable]
    public class SavingThrow
    {
        #region PROPERTIES

        public string SaveName { get; set; }
        public string SaveKey { get; set; }

        public int AbilityBonus { get; set; }

        public int SaveModifier { get; set; }

        public bool IsProficient { get; set; } = false;

        #endregion

        #region METHODS ->  Setting the Ability Bonus, calculating modifiers and returning random int for die roll 

        public void Set_AbilityBonus(int bonus)
        {
            AbilityBonus = bonus;
        }

        public void Calculate_SaveModifier(int profBonus)
        {
            if(IsProficient)
            {
                SaveModifier = AbilityBonus + profBonus;
            }

            else
            {
                SaveModifier = AbilityBonus;
            }
        }

        public int Make_SavingThrow()
        {
            int result = SheetManager.CS_Manager_Inst.dSys.Roll_D20() + SaveModifier;

            return result;
        }

        #endregion
    }
}
