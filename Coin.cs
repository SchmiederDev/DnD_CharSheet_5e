using System;
using System.Collections.Generic;

namespace DnD_CharSheet_5e
{
    /* EXPLANATORY NOTE ON CURRENCY IN D&D AND ITS REPRESENTATION IN THE CODE OF THE APP
         * 
         *  In D&D there are fore types of coin determined by their material: Platinum, Gold, Silver and Copper.
         *  The 'Key' of a 'Coin'-Object defines which type of coin a number (value/ price) represents.
         *  
         *  Because there a 4 types of coin (and not only 'Dollar' and 'Cent' e. g.) and for reasons of gameplay authenticity 
         *  I decided against the option to represent currency/ a price as a decimal number in this app.
         */

    // The class is made serializable to be deserialized from (or serialized to) the Item Data Bases.

    [Serializable]
    public class Coin
    {         
        public int Price { get; set; }
        public string CoinKey { get; set;  }          
        
    }
}
