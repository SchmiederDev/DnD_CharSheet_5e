using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Windows;

namespace DnD_CharSheet_5e
{
    public class FileManager
    {
        #region SINGLETON AND CONSTRUCTOR

        public static FileManager FM_Inst = new FileManager();

        FileManager()
        {
            if (FM_Inst == null)
            {
                FM_Inst = this;
            }
        }

        #endregion

        #region PROPERTIES

        #region BASIC FOLDER PATHS

        string rootPath;

        string ResourceFolderName = @"\Resources";
        string ResourceFolderPath;
        
        string saveGameFolderPath = @"\SaveGames";
        string SoundEffectsFolderPath = @"\SoundEffects";
        string ImagesFolderPath = @"\Images";
        string DataBasesFolderName = @"\DataBases";
        string DataBasesPath;


        public string saveGameFolder { get; private set; }
        public string SoundEffectsFolder { get; private set; }
        
        public string ImagesFolder { get; private set; }        
        public string[] ImageFileNames { get; private set; }
        #endregion

        #region SAVE GAME PATHS

        string saveSlot_01 = @"\character_01.charDat";
        string saveSlot_02 = @"\character_02.charDat";
        string saveSlot_03 = @"\character_03.charDat";
        string saveSlot_04 = @"\character_04.charDat";
        string saveSlot_05 = @"\character_05.charDat";


        string nameSaveSlot = @"\chars.txt";

        public string saveGame_01 { set; get; }
        public string saveGame_02 { set; get; }
        public string saveGame_03 { set; get; }
        public string saveGame_04 { set; get; }
        public string saveGame_05 { set; get; }
        public string namesDataBase { set; get; }

        #endregion

        #region SOUND EFFECTS PATHS AND SOUNDPLAYERS

        string ClickSound_FileName = @"\TypeWriter_Click.wav";
        string DiceSound_FileName = @"\DiceRoll.wav";

        string ClickSound_Path;
        string DiceSound_Path;

        SoundPlayer ClickSound;
        SoundPlayer DiceSound;

        #endregion

        #region DATA BASES FILES AND PATHS

        #region ITEM DATA BASES

        // 'DB' = DataBase, e. g. 'IDB' = Item Data Base        

        string IDB_FileName = @"\ItemDataBase.json";
        public string IDB_Path { get; private set; }

        string WDB_FileName = @"\WeaponDataBase.json";
        public string WDB_Path { get; private set; }

        string ADB_FileName = @"\ArmorDataBase.json";
        public string ADB_Path { get; private set; }

        #endregion

        #region SPELL DATA BASES

        string SDB_FileName = @"\SpellDataBase.json";
        
        public string SDB_Path { get; private set; }
        public string SpellDataBase_JSON { get; set; }

        string SCCDB_FileName = @"\SpellCasterClassesDataBase.json";

        public string SCCDB_Path { get; private set; }
        public string SCCDB_JSON { get; set; }


        string SpellListsDB_FileName = @"\SpellListsDataBase.json";

        public string SpellListsDB_Path { set; get; }
        public string SpellListsDB_JSON { set; get; }

       
        string Bard_SpellList_FileName = @"\Bard_SpellList.json";
        public string Bard_SpellList_Path { get; private set; }

        string Cleric_SpellList_FileName = @"\Cleric_SpellList.json";
        public string Cleric_SpellList_Path { get; private set; }

        string Druid_SpellList_FileName = @"\Druid_SpellList.json";
        public string Druid_SpellList_Path { get; private set; }

        string Paladin_SpellList_FileName = @"\Paladin_SpellList.json";
        public string Paladin_SpellList_Path { get; private set; }

        string Ranger_SpellList_FileName = @"\Ranger_SpellList.json";
        public string Ranger_SpellList_Path { get; private set; }

        string Sorcerer_SpellList_FileName = @"\Sorcerer_SpellList.json";
        public string Sorcerer_SpellList_Path { get; private set; }

        string Warlock_SpellList_FileName = @"\Warlock_SpellList.json";
        public string Warlock_SpellList_Path { get; private set; }

        string Wizard_SpellList_FileName = @"\Wizard_SpellList.json";
        public string Wizard_SpellList_Path { get; private set; }
        

        public string BardSpellList_JSON{ get; set; }
        public string ClericSpellList_JSON { get; set; }
        public string DruidSpellList_JSON { get; set; }
        public string PaladinSpellList_JSON { get; set; }
        public string RangerSpellList_JSON { get; set; }
        public string SorcererSpellList_JSON { get; set; }
        public string WarlockSpellList_JSON { get; set; }
        public string WizardSpellList_JSON { get; set; }


        string LDB_FileName = @"\Languages.json";
        public string LDB_Path { get; private set; }

        #endregion

        #endregion

        #endregion

        #region METHODS

        #region FILE SYSTEM INITIALIZATION METHODS
        public void Init_FileSystem()
        {
            Find_ResourceFolder_and_SetFolderPath();
            Set_FilePaths();
            Read_Spells_and_SpellLists();
            Init_SaveGames();
            Init_SoundEffects();
            Init_Images();
        }

        #region FILE OPERATION METHODS
        private void Find_ResourceFolder_and_SetFolderPath()
        {
            rootPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);                        
            ResourceFolderPath = rootPath + ResourceFolderName;            
            DataBasesPath = ResourceFolderPath + DataBasesFolderName;
        }

        private string Check_for_Folder(string folderPath)
        {
            string folder = rootPath + folderPath;

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            return folder;
        }

        public string Read_DataBase(string path)
        {
            string jsonDB = File.ReadAllText(path);
            return jsonDB;
        }

        private void Read_Spells_and_SpellLists()
        {
            SpellDataBase_JSON = File.ReadAllText(SDB_Path);
            SCCDB_JSON = File.ReadAllText(SCCDB_Path);
            SpellListsDB_JSON = File.ReadAllText(SpellListsDB_Path);

            BardSpellList_JSON = File.ReadAllText(Bard_SpellList_Path);
            ClericSpellList_JSON = File.ReadAllText(Druid_SpellList_Path);
            DruidSpellList_JSON = File.ReadAllText(Druid_SpellList_Path);
            PaladinSpellList_JSON = File.ReadAllText(Paladin_SpellList_Path);
            RangerSpellList_JSON = File.ReadAllText(Ranger_SpellList_Path);
            SorcererSpellList_JSON = File.ReadAllText(Sorcerer_SpellList_Path);
            WarlockSpellList_JSON = File.ReadAllText(Warlock_SpellList_Path);
            WizardSpellList_JSON = File.ReadAllText(Wizard_SpellList_Path);
        }

        #endregion

        #region PATH SETTERS
        private void Set_FilePaths()
        {
            Set_ItemDataBasesPaths();
            LDB_Path = DataBasesPath + LDB_FileName;
            Set_Path_Spells_and_SpellLists();
        }

        private void Set_Path_Spells_and_SpellLists()
        {
            SDB_Path = DataBasesPath + SDB_FileName;
            SCCDB_Path = DataBasesPath + SCCDB_FileName;
            SpellListsDB_Path = DataBasesPath + SpellListsDB_FileName;

            Bard_SpellList_Path = DataBasesPath + Bard_SpellList_FileName;
            Cleric_SpellList_Path = DataBasesPath + Cleric_SpellList_FileName;
            Druid_SpellList_Path = DataBasesPath + Druid_SpellList_FileName;
            Paladin_SpellList_Path = DataBasesPath + Paladin_SpellList_FileName;
            Ranger_SpellList_Path = DataBasesPath + Ranger_SpellList_FileName;
            Sorcerer_SpellList_Path = DataBasesPath + Sorcerer_SpellList_FileName;
            Warlock_SpellList_Path = DataBasesPath + Warlock_SpellList_FileName;
            Wizard_SpellList_Path = DataBasesPath + Wizard_SpellList_FileName;
        }        

        private void Init_SaveGames()
        {
            saveGameFolder = Check_for_Folder(saveGameFolderPath);
            Set_SaveGames();
        }

        private void Init_SoundEffects()
        {
            SoundEffectsFolder = ResourceFolderPath + SoundEffectsFolderPath;
            Set_SoundEffects();
        }

        private void Init_Images()
        {
            ImagesFolder = ResourceFolderPath + ImagesFolderPath;
            Load_Images();
        }      

        private void Set_SaveGames()
        {
            saveGame_01 = saveGameFolder + saveSlot_01;
            saveGame_02 = saveGameFolder + saveSlot_02;
            saveGame_03 = saveGameFolder + saveSlot_03;
            saveGame_04 = saveGameFolder + saveSlot_04;
            saveGame_05 = saveGameFolder + saveSlot_05;

            namesDataBase = saveGameFolder + nameSaveSlot;

        }

        private void Set_ItemDataBasesPaths()
        {
            IDB_Path = DataBasesPath + IDB_FileName;
            WDB_Path = DataBasesPath + WDB_FileName;
            ADB_Path = DataBasesPath + ADB_FileName;
        }        

        private void Set_SoundPaths()
        {
            ClickSound_Path = SoundEffectsFolder + ClickSound_FileName;
            DiceSound_Path = SoundEffectsFolder + DiceSound_FileName;
        }

        private void Set_SoundEffects()
        {
            Set_SoundPaths();
            ClickSound = new SoundPlayer(ClickSound_Path);
            DiceSound = new SoundPlayer(DiceSound_Path);
        }

        #endregion

        private void Load_Images()
        {
            try
            {
                ImageFileNames = Directory.GetFiles(ImagesFolder);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

        #region PLAY SOUND METHODS
        public void Play_ClickSound()
        {
            try
            {
                ClickSound.Play();
            }
            catch
            {
                MessageBox.Show("Sound file not found.");
            }

        }

        public void Play_DiceSound()
        {
            try
            {
                DiceSound.Play();
            }
            catch
            {
                MessageBox.Show("Sound file not found.");
            }
        }
        #endregion

        #endregion

    }

}
