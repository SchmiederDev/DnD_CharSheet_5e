using System.Collections.Generic;
using System.Windows;

namespace DnD_CharSheet_5e
{
    #region PURPOSE AND FUNCTIONS OF SHEETMANAGER

    /*
     * The Sheet-Manager is the CENTRAL INSTANCE OF THE APP which RECEIVES AND PROCESSES DATA from ALL PARTS OF THE APP 
     * as well as PROVIDING IT TO THE DIFFERENT CLASSES, WINDOWS and PAGES. 
     * 
     * As the name suggests, it manages the whole 'character sheet' (that is the app).
     * It is comparable to 'GameManager'-Classes for (other) Computer-/Videogames.
     * 
     * It serves THREE PURPOSES: 
     */

    #region FIRST PURPOSE: MEDIATOR

    /* FIRST, it serves as a MEDIATOR BETWEEN THE ACTIVE CHARACTER AND DIFFERENT PARTS OF THE APP.   
    * 
    * It NOTIFIES DIFFERENT PARTS OF THE APP WHEN SOMETHING HAS CHANGED ABOUT THE CHARACTER - e. g. when hitpoints changed - 
    * AND VICE VERSA provides information about the character to the different Windows and Pages of the app: 
    * e. g. the inventory of the character which is used by CombatWindow and the Inventory Window is accessed via SheetManager.
    * 
    */

    #region SHEETMANAGER AND ITS INSTANCE OF THE CHARACTER-CLASS

    /* The active character (in opposition e. g. to those who populate the SaveGames) itself only exists as a single instance administered by the SheetManager.
    * This is for one much more handy than passing around data between several local instances of the character.
    * 
    * The reason why character isn't itself set up as a Singleton is there should be the possibility to have several instances of characters around.
    * One example would be the character creation pages - missing in this Basic Version - where a new character is created step by step (its members initialized) 
    * guiding the users through the process without having them to know about the rules of character creation. 
    * Here an instance of the character-class is created and populated over time but only in the end, when the process is completed
    * will it replace the active character. 
    * In the meantime, the app doesn't mess with the active character which might be an already existing character of the player held dearly.
    * Therefore ugly overrides are avoided. So the user is only able to override an existing character when he/she* consciously does so in the SaveWindow.
    */
    #endregion

    #endregion


    #region SECOND PURPOSE: DEALING WITH CHARACTER ACTIONS (= USER INPUT) BASED ON GAME MECHANICS

    /* SECOND, The SheetManager HANDLES ACTIONS OF THE CHARACTER and done to the character (= INPUT OF THE USER/ PLAYER). 
    * 
    * Whereas the character-class serves more as a container for data concerning the character and its current state
    * the SheetManager is the class which processes this data and does calculations based on it according to game mechanics.
    * This is related to the third function of the SheetManager as a base of knowledge about the game.
    * The SheetManager is supposed 'to know' which die to use for a specific check and which values are involved - e. g. for making an attack.
    */

    #region EXAMPLES

    /* EXAMPLE: The character(-class) has e. g. several values describing how effectively this character can attack hostile creatures in combat in general.
    * The SheetManager handles the actual attack (when the user has clicked on one of the relevant buttons in the CombatWindow).
    * It grabs all relevant values relevant for making an attack and calculates the outcome of the attempt of the character to make an attack and then provides feedback to the player visualized on UI.
    * 
    * ANOTHER EXAMPLE: The character(-class) has so called 'hitpoints' describing how much life force is within them. 
    * When the character suffers a blow and looses hitpoints this is handled by the SheetManager which also checks if the character is still alive and/ or unconscious.
    * It reports back the results of its state analysis of the character to the instance of the active character as well as to the UI and thereby the user in the CombatWindow.
    *      
    */

    #endregion

    #endregion


    #region THIRD PURPOSE: BASE OF KNOWLEDGE ABOUT GAME MECHANICS

    /* THIRD, it serves as the CENTRAL BASE OF background KNOWLEDGE ABOUT THE GAME and its mechanics - based on MEMBER METHODS AND DATABASES.
    * 
    * That means SheetManager holds instances of the different databases used by the app and allows them to be accessed at runtime by different parts of the app. 
    * Again, the reason for this is not have this knowledge about the game (here data/ databases) scattered accross the app and spares the need to pass this data around,
    * serialize and deserialize it over and over again and having an instance of a respective database in every Sub-Window.      
    */

    #region EXAMPLE:

    /* EXAMPLE: Some characters are able to use magic. But, usually characters know only a few spells whereas the SheetManager 'knows' them all.
    * It provides the 'SpellWindow' - and the user - with the information which spells a character can cast at a certain level.
    * When the chracter levels up, the Sheetmanager gets all the new spells a character can learn now and provides it to the user. 
    * Based on the users decision - that is interacting with UI - it registers which of the spells available was learned by the character - that is storing this data about the character for the user.
    *     
    */

    #endregion

    #region NOTES ON THE CURRENT VERSION REGARDING DATABASES
    /* 
     * In the current version of the app game knowledge (= data and databases) brokered by SheetManager includes mainly the 'Mystra'-Class - handling 'magic' in the game,
     * spells in general and spells that can be cast by the character - and the 'd20'-System (see the respective class).
     * In a future version - already in progress - SheetManager will also provide information (data) about several other types of game knowledge such as the 'Races' (Elves, Dwarves etc.) which
     * populate the worlds of D&D and 'Classes' of characters (Fighters, Wizards, Rogues etc.).
     * These are relevant for automatized character creation as well as 'Leveling Up' the character.
     * 
     */

    #endregion

    #endregion

    #endregion

    public class SheetManager
    {
        #region SHEETMANAGER SINGLETON AND CONSTRUCTOR

        public static SheetManager CS_Manager_Inst;

        public SheetManager()
        {
            if (CS_Manager_Inst == null)
            {
                CS_Manager_Inst = this;
            }
        }

        #endregion

        #region MEMBER PROPERTIES

        public Character character { get; set; } = new Character();        

        public D20_System dSys { get; set; } = new D20_System();


        bool IsTempHPactive = false;

        public static uint DeathSaveDC { get; } = 10;

        // Mystra is the Goddess of magic in the Forgotten Realms campaign setting for D&D. The class for handling magic is therefore named after her.
        //The magical energetical field that surrounds and permeates the worlds of D&D is called 'The Weave', also in the 'Forgotten Realms' campaign setting.
        //The instance of Mystra is therefore called 'theWeave'.

        public Mystra theWeave = new Mystra();

        public string[] StandardRaces { get; } = { "Dragonborn", "Dwarf", "Elf", "Gnome", "Half-Elf", "Halfling", "Half-Orc", "Human", "Tiefling" };
        public string[] StandardSubraces { get; } = { "Drow", "Hill Dwarf", "Mountain Dwarf", "High Elf", "Wood Elf", "Forest Gnome", "Rock Gnome", "Lightfood Halfling", "Stout Halfling" };
        public string[] StandardClasses { get; } = { "Barbarian", "Bard", "Cleric", "Druid", "Fighter", "Monk", "Paladin", "Ranger", "Rogue", "Sorcerer", "Warlock", "Wizard" };

        public List<DnDLanguage> Languages { get; set; } = new List<DnDLanguage>();

        #endregion

        #region MEMBER METHODS

        #region COMBAT METHODS
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
                result = dSys.Roll_Custom(character.CharEquipment.RightHand_Weapon.DamageNumerator, character.CharEquipment.RightHand_Weapon.DamageDenominator) + character.Strength.Modifier;
                return result;
            }

            else if (character.CharEquipment.RightHand_Weapon != null && character.CharEquipment.RightHand_Weapon.IsRanged == true)
            {
                result = dSys.Roll_Custom(character.CharEquipment.RightHand_Weapon.DamageNumerator, character.CharEquipment.RightHand_Weapon.DamageDenominator) + character.Dexterity.Modifier;
                return result;
            }

            else
            {
                MessageBox.Show($"You have no weapon equiped. Damage roll will be counted as 'Unarmed Strike'");
                result = 1 + character.Strength.Modifier;
                return result;
            }
        }

        public int Roll_for_Damage(Weapon attackingWeapon)
        {
            int result = 0;

            if (attackingWeapon.IsRanged == false)
            {
                if (attackingWeapon.IsFinesse == false)
                {
                    result = dSys.Roll_Custom(attackingWeapon.DamageNumerator, attackingWeapon.DamageDenominator) + character.Strength.Modifier;
                }

                else
                {
                    if (character.Dexterity.Modifier >= character.Strength.Modifier)
                    {
                        result = dSys.Roll_Custom(attackingWeapon.DamageNumerator, attackingWeapon.DamageDenominator) + character.Dexterity.Modifier;
                    }

                    else
                    {
                        result = dSys.Roll_Custom(attackingWeapon.DamageNumerator, attackingWeapon.DamageDenominator) + character.Strength.Modifier;
                    }
                }
            }

            else
            {
                result = dSys.Roll_Custom(attackingWeapon.DamageNumerator, attackingWeapon.DamageDenominator) + character.Dexterity.Modifier;
            }

            return result;
        }

        #endregion

        #region METHODS FOR HANDLING HITPOINTS
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

        #endregion

        #region DEATH SAVING THROWS
        public int Roll_DeathSave()
        {
            int DS_Result = dSys.Roll_D20();
            return DS_Result;
        }

        public bool DeathSave(int result)
        {
            // The declaration of this boolean ('DS' = Death Save -> Death Save is Success/Failure) isn't necessary, but to make it more obvious what is happening here - I declare it nonetheless.
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

        #endregion

        #endregion
    }

}

public struct DnDLanguage
{
    public string Language { get; set; }
    public string TypicalSpeakers { get; set; }
    public string Script { get; set; }
}
