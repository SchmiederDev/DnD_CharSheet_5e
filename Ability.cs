using System;
using System.Collections.Generic;
using System.Windows;

namespace DnD_CharSheet_5e
{
    #region EXPLANATION OF THE GAME MECHANIC BEHIND 'ABILITIES'

    /* 'Abilities' describe the basic physical and mental capabilities of a D&D-character and are equivalent to different types of 'Stats' in other games.
    * This class is an abstract representation of these.
    * Each 'Ability' - such as 'Strength' or 'Intelligence' - is represented by two values: a score and a modifier.
    * The 'Score' describes the potential of a character regardinG the specific ability in a range from 1 to 20.
    * Based on this value a so called 'modifier' is calculated which may than be added (or substracted) from dice rolls related to the ability.
    * For example: Pushing a heavy bolder is related to 'Strength' - analyzing a complicated magical formula is related to 'Intelligence'.
    * A die roll determines the success or failure of the attempt to push a heavy bolder or analyze a magical formula based on the values of the related 'Abilities'.
    */
    #endregion

    [Serializable]
    public class Ability
    {
        public string AbilityName { get; set; }
        public string ReferenceKey { get; set; }

        public int Score { get; set; }

        public int Modifier { get; private set; }

        // The formula for the calculation of an ability modifier - expressed here in C# - is to be found in the 'Player's Handbook' of the D&D Source Books.
        public void Calculate_Modifier()
        {            

            int tempVal01 = Score - 10;
            decimal tempVal02 = tempVal01 / 2m;

            tempVal01 = (int)Math.Floor(tempVal02);

            Modifier = tempVal01;
        }

        public int Ability_Check()
        {
            int result = SheetManager.CS_Manager_Inst.dSys.Roll_D20() + Modifier;
            return result;
        }
    }
}
