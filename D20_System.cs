using System;

namespace DnD_CharSheet_5e
{
    public class D20_System
    {
        #region PROPERTIES

        Random dieSeed;

        const int dieBase = 1;

        public int d4 { get; } = 4;
        public int d6 { get; } = 6;
        public int d8 { get; } = 8;
        public int d10 { get; } = 10;
        public int d12 { get; } = 12;
        public int d20 { get; } = 20;
        public int d100 { get; } = 100;

        #endregion

        public void InitializeRandom()
        {
            dieSeed = new Random();
        }

        /* The 'Next'- Method returns a maximum value which is lower than 'MaxValue' for the second parameter/ argument by 1. 
         * So 'd4' e. g. could have been initiated with '5' instead of '4'.  
         * But it seemed somehow illogical to me to represent a 4-sided die with a number greater than the actual sides of the die.
         * This is why I kept the real number of sides for the dice and added '1' in the dice roll methods.
        */

        #region METHODS (DICE ROLLS METHODS)
        public int Roll_D4()
        {
            int result = dieSeed.Next(dieBase, d4 + 1);
            return result;
        }

        public int Roll_D6()
        {
            int result = dieSeed.Next(dieBase, d6 + 1);
            return result;
        }

        public int Roll_D8()
        {
            int result = dieSeed.Next(dieBase, d8 + 1);
            return result;
        }

        public int Roll_D10()
        {
            int result = dieSeed.Next(dieBase, d10 + 1);
            return result;
        }

        public int Roll_D12()
        {
            int result = dieSeed.Next(dieBase, d12 + 1);
            return result;
        }

        public int Roll_D20()
        {
            int result = dieSeed.Next(dieBase, d20 + 1);
            return result;
        }

        public int Roll_D100()
        {
            int result = dieSeed.Next(dieBase, d100 + 1);
            return result;
        }

        public int Roll_Custom(int numerator, int denominator)
        {
            int result = numerator * dieSeed.Next(dieBase, denominator + 1);
            return result;
        }
        #endregion
    }
}
