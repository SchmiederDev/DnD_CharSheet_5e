using System;
using System.Collections.Generic;
using System.Windows;
using Newtonsoft.Json;

namespace DnD_CharSheet_5e
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        // The three most important classes of this app are the 'Character'-class, the 'SheetManager'-Class & the 'FileManager'-class which are initialized here and explained below.

        // Explanatory notes (comments) on the central classes of the app

        #region THE CHARACTER-CLASS

        /* The Character-Class handles the actual character the player assumes the role of during the game. 
         * 
         * It is mostly a container for the numerous values related to the character used in the game - which in this app of course come in the form of variables.
         * It also offers member functions to calculate these values because one is often based another.
         * Furthermore, other functions of this class parse or process information from input fields to create the character or when values related to character change over the course of the game/ using the app.
         * Finally, it processes data from savegame-files to initialize the character on load.
         * 
         * Nothing is done with the character class here but it is still mentioned to guide you through the functionality of the app.
        */

        #endregion

        #region THE SHEETMANAGER

        /*  The Sheet-Manager is the one central instance of this app which processes information from all parts of the app as well as providing it to the different classes, windows and pages.
         *  
         *  It serves two purposes: First, it handles actions of the character and done to the character (handling input of the user/ player)
         *  and second, it serves as the central base of background knowledge about the game and its mechanics. 
         *  
         *  To explain this first point: The character(-class) has e. g. several values describing how effectively this character can attack hostile creatures in combat in general.
         *  The SheetManager handles the actual attack (when the user has clicked on one of the relevant buttons in the CombatWindow). 
         *  It grabs all relevant values relevant for making an attack and calculates the outcome of the attempt of the character to make an attack and then provides feedback to the player visualized on UI.
         *  Another example: The character(-class) has so called 'hitpoints' describing how much life force is within them. 
         *  When the character suffers a blow and looses hitpoints this is handled by the SheetManager which also checks if the character is still alive and/ or unconscious.
         *  It reports back the results of its state analysis of the character to the character themselves as well as to the UI and thereby the user.
         *  
         *  The second function of the Sheetmanager as a source of knowledge:
         *  The SheetManager also processes relevant information from the databases relevant fpr the game.
         *  To give you an example: Some character are able to use magic. But, usually characters know only a few spells whereas the SheetManager 'knows' them all.
         *  It provides the 'SpellWindow' - and the user - with the information which spells a character can cast at a certain level.
         *  When the chracter levels up, the Sheetmanager gets all the new spells a character can learn now and provides it to the user.
         *  Based on the users decision - that is interacting with UI - it registers which of the spells available was learned by the character - that is storing this data about the character for the user. 
         */
        #endregion

        #region THE FILEMANAGER

        /* 
         * The 'FileManager' probably doesn't need much of an explanation. It is the class which handles File Operations.
         * It is initialized here to provide the app with all necessary folder paths for accessing data at runtime. 
         * This includes images, sound effects and databases - some of them - which are globally needed - are loaded here.
        */
        #endregion

        App()
        {
            Init_Sheetmanager();
            Init_Character();
            Init_FileSystem();
            Load_DataBases();
            Init_ImageHandler();                        
        }

        #region INITIALIZE  SHEETMANAGER
        private void Init_Sheetmanager()
        {
            SheetManager.CS_Manager_Inst = new SheetManager();
            SheetManager.CS_Manager_Inst.dSys.InitializeRandom();
        }
        #endregion

        #region INITIALIZE CHARACTER
        private void Init_Character()
        {
            SheetManager.CS_Manager_Inst.character.Init_Basics();
        }

        #endregion

        #region INITIALIZE FILE SYSTEM
        private void Init_FileSystem()
        {
            FileManager.FM_Inst.Init_FileSystem();
        }

        #endregion

        #region LOAD DATABASES
        private void Load_DataBases()
        {
            Load_Languages();
            Load_SpellDataBases();
        }

        private void Load_LanguageDataBase(string jsonLDB)
        {
            SheetManager.CS_Manager_Inst.Languages = JsonConvert.DeserializeObject<List<DnDLanguage>>(jsonLDB);
        }

        private void Load_Languages()
        {
            try
            {
                Load_LanguageDataBase(FileManager.FM_Inst.Read_DataBase(FileManager.FM_Inst.LDB_Path));
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Load_SpellDataBases()
        {
            try
            {
                SheetManager.CS_Manager_Inst.theWeave.Load_SpellDataBase(FileManager.FM_Inst.jsonSDB);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            try
            {
                SheetManager.CS_Manager_Inst.theWeave.Load_BardSpellList(FileManager.FM_Inst.jsonBSL);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            try
            {
                SheetManager.CS_Manager_Inst.theWeave.Load_WizardSpellList(FileManager.FM_Inst.jsonWSL);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Init_ImageHandler()
        {
            ImageHandler.ImgHandlerInst = new ImageHandler();
            ImageHandler.ImgHandlerInst.ImageFileNames = FileManager.FM_Inst.ImageFileNames;
            ImageHandler.ImgHandlerInst.Set_Uris();

            Load_ControlImages();
        }

        private void Load_ControlImages()
        {
            ImageHandler.ImgHandlerInst.Load_DieImage();
        }
        #endregion
    }
}
