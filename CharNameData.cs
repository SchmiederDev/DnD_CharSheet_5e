using System;
using System.Collections.Generic;

namespace DnD_CharSheet_5e
{
    public class CharNameData
    {
        string charOne;
        string charTwo;
        string charThree;
        string charFour;
        string charFive;

        public void Transmit_CharName_Data(string nameOne, string nameTwo, string nameThree, string nameFour, string nameFive)
        {
            charOne = nameOne;
            charTwo = nameTwo;
            charThree = nameThree;
            charFour = nameFour;
            charFive = nameFive;
        }
    }
}
