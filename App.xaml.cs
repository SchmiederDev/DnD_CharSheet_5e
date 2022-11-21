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
         * FIRST, it serves as a MEDIATOR BETWEEN THE ACTIVE CHARACTER AND DIFFERENT PARTS OF THE APP. 
         * SECOND, The SheetManager HANDLES ACTIONS OF THE CHARACTER and done to the character (= INPUT OF THE USER/ PLAYER).
         * THIRD, it serves as the CENTRAL BASE OF background KNOWLEDGE ABOUT THE GAME and its mechanics = DATA AND DATABASES.
         * 
         * For further explanation on the purpose and the functions of SheetManager see explanatory note at the head of the SheetManager-Class
         * 
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
            Init_ClassData();
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
            SheetManager.CS_Manager_Inst.theWeave.Load_SpellDataBase(FileManager.FM_Inst.SpellDataBase_JSON);
            SheetManager.CS_Manager_Inst.theWeave.Load_SpellCasterClassesData(FileManager.FM_Inst.SCCDB_JSON);
            SheetManager.CS_Manager_Inst.theWeave.Load_SpellListsDataBase(FileManager.FM_Inst.SpellListsDB_JSON);
            SheetManager.CS_Manager_Inst.theWeave.Load_SpellLists();
        }

        private void Init_ClassData()
        {
            SheetManager.CS_Manager_Inst.theWeave.Initialize_SpellCasterClasses();
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
