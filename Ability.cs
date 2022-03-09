using System;
using System.Collections.Generic;
using System.Windows;

namespace DnD_CharSheet_5e
{
    public class Ability
    {
        public string AbilityName { get; set; }
        public string ReferenceKey { get; set; }

        public int Score { get; set; }

        public int Modifier { get; private set; }

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
