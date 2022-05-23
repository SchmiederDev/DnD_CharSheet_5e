using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Windows;

namespace DnD_CharSheet_5e
{
    public class FileManager
    {
        public static FileManager FM_Inst = new FileManager();

        FileManager()
        {
            if (FM_Inst == null)
            {
                FM_Inst = this;
            }
        }

        string rootPath;

        string folderPath = @"DnD_CharSheet_5e";
        string saveGameFolderPath = @"\SaveGames";
        string SoundEffects_FolderPath = @"\SoundEffects";
        string Images_FolderPath = @"\Images";

        public string saveGameFolder { get; private set; }
        public string SoundEffectsFolder { get; private set; }
        public string ImagesFolder { get; private set; }
        public string[] ImageFileNames { get; private set; }

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

        string ClickSound_FileName = @"\TypeWriter_Click.wav";
        string DiceSound_FileName = @"\DiceRoll.wav";

        string ClickSound_Path;
        string DiceSound_Path;

        SoundPlayer ClickSound;
        SoundPlayer DiceSound;

        // 'DB' = DataBase, e. g. 'IDB' = Item Data Base        

        string LDB_FileName = @"\Languages.json";
        public string  LDB_Path { get; private set; }

        string IDB_FileName = @"\ItemDataBase.json";
        public string IDB_Path { get; private set; }

        string WDB_FileName = @"\WeaponDataBase.json";
        public string WDB_Path { get; private set; }

        string ADB_FileName = @"\ArmorDataBase.json";
        public string ADB_Path { get; private set; }

        string SDB_FileName = @"\SpellDataBase.json";
        public string SDB_Path { get; private set; }
        public string jsonSDB { get; set; }

        string Bard_SpellList_FileName = @"\Bard_SpellList.json";
        public string Bard_SpellList_Path { get; private set; }

        string Wizard_SpellList_FileName = @"\Wizard_SpellList.json";
        public string Wizard_SpellList_Path { get; private set; }
        
        // SL = 'Spell List', e. g. BSL = 'Bard Spell List' 
        public string jsonBSL { get; set; } 
        public string jsonWSL { get; set; }

        public void Init_FileSystem()
        {
            Find_RootPath();
            Set_FilePaths();
            Read_Spells_and_SpellLists();
            Init_SaveGames();
            Init_SoundEffects();
            Init_Images();
        }

        private void Find_RootPath()
        {
            rootPath = Path.GetFullPath(folderPath);
        }

        private void Set_FilePaths()
        {
            Set_ItemDataBasesPaths();
            LDB_Path = rootPath + LDB_FileName;
            Set_Path_Spells_and_SpellLists();
        }

        private void Set_Path_Spells_and_SpellLists()
        {
            SDB_Path = rootPath + SDB_FileName;
            Bard_SpellList_Path = rootPath + Bard_SpellList_FileName;
            Wizard_SpellList_Path = rootPath + Wizard_SpellList_FileName;
        }

        private void Read_Spells_and_SpellLists()
        {
            jsonSDB = File.ReadAllText(SDB_Path);
            jsonBSL = File.ReadAllText(Bard_SpellList_Path);
            jsonWSL = File.ReadAllText(Wizard_SpellList_Path);
        }

        private void Init_SaveGames()
        {
            saveGameFolder = Check_for_Folder(saveGameFolderPath);
            Set_SaveGames();
        }

        private void Init_SoundEffects()
        {
            SoundEffectsFolder = Check_for_Folder(SoundEffects_FolderPath);
            Set_SoundEffects();
        }

        private void Init_Images()
        {
            ImagesFolder = Check_for_Folder(Images_FolderPath);
            Load_Images();
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
            IDB_Path = rootPath + IDB_FileName;
            WDB_Path = rootPath + WDB_FileName;
            ADB_Path = rootPath + ADB_FileName;
        }
        public string Read_DataBase(string path)
        {
            string jsonDB = File.ReadAllText(path);
            return jsonDB;
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

    }

}
