using System;
using System.Collections.Generic;

namespace DnD_CharSheet_5e
{
    public class D20_System
    {
        Random dieSeed;

        int dieBase = 1;

        public int d4 { get; } = 4;
        public int d6 { get; } = 6;
        public int d8 { get; } = 8;
        public int d10 { get; } = 10;
        public int d12 { get; } = 12;
        public int d20 { get; } = 20;
        public int d100 { get; } = 100;

        public void InitializeRandom()
        {
            dieSeed = new Random();
        }

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

        public int Roll_Custom(int nominator, int denominator)
        {
            int result = nominator * dieSeed.Next(dieBase, denominator + 1);
            return result;
        }
    }
}
