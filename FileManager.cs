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

        string saveGameFolder;
        string SoundEffectsFolder;
        string ImagesFolder;
        string[] ImageFileNames;

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

        string RDB_FileName = @"\RaceDataBase.json";
        string RDB_Path;

        string LDB_FileName = @"\Languages.json";
        string LDB_Path;

        string IDB_FileName = @"\ItemDataBase.json";
        string IDB_Path;

        string WDB_FileName = @"\WeaponDataBase.json";
        string WDB_Path;

        string ADB_FileName = @"\ArmorDataBase.json";
        string ADB_Path;

        string DragonbornDB_FileName = @"\DragonbornDB.json";
        string Dragonborn_Path;

        string SDB_FileName = @"\SpellDataBase.json";
        string SDB_Path;
        public string jsonSDB { get; set; }

        string Bard_SpellList_FileName = @"\Bard_SpellList.json";
        string Bard_SpellList_Path;

        string Wizard_SpellList_FileName = @"\Wizard_SpellList.json";
        string Wizard_SpellList_Path;
        
        // SL = 'Spell List', e. g. BSL = 'Bard Spell List' 
        public string jsonBSL { get; set; } 
        public string jsonWSL { get; set; }

        public string Find_RootPath()
        {
            rootPath = Path.GetFullPath(folderPath);

            return rootPath;
        }

        public void Check_for_SaveGameFolder()
        {
            saveGameFolder = rootPath + saveGameFolderPath;

            if (!Directory.Exists(saveGameFolder))
            {
                Directory.CreateDirectory(saveGameFolder);
            }
        }

        public void Check_for_SoundEffects_Folder()
        {
            SoundEffectsFolder = rootPath + SoundEffects_FolderPath;

            if (!Directory.Exists(SoundEffectsFolder))
            {
                Directory.CreateDirectory(SoundEffectsFolder);
            }
        }

        public void Check_for_Images_Folder()
        {
            ImagesFolder = rootPath + Images_FolderPath;

            if (!Directory.Exists(ImagesFolder))
            {
                Directory.CreateDirectory(ImagesFolder);
            }

            else
            {
                ImageFileNames = Directory.GetFiles(ImagesFolder);
            }
        }

        public void Set_SaveGames()
        {
            saveGame_01 = saveGameFolder + saveSlot_01;
            saveGame_02 = saveGameFolder + saveSlot_02;
            saveGame_03 = saveGameFolder + saveSlot_03;
            saveGame_04 = saveGameFolder + saveSlot_04;
            saveGame_05 = saveGameFolder + saveSlot_05;

            namesDataBase = saveGameFolder + nameSaveSlot;

        }

        private void Set_SoundPaths()
        {
            ClickSound_Path = SoundEffectsFolder + ClickSound_FileName;
            DiceSound_Path = SoundEffectsFolder + DiceSound_FileName;
        }

        public void Init_SoundEffects()
        {
            Set_SoundPaths();
            ClickSound = new SoundPlayer(ClickSound_Path);
            DiceSound = new SoundPlayer(DiceSound_Path);
        }

        public string Get_ImagesFolder()
        {
            return ImagesFolder;
        }

        public string[] Get_ImagesFileNames()
        {
            return ImageFileNames;
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

        public void Set_RDBPath()
        {
            RDB_Path = Find_RootPath() + RDB_FileName;
        }

        public void Set_LanguageDBPath()
        {
            LDB_Path = Find_RootPath() + LDB_FileName;
        }

        public void Set_IDBPath()
        {
            IDB_Path = Find_RootPath() + IDB_FileName;
        }

        public void Set_WDBPath()
        {
            WDB_Path = Find_RootPath() + WDB_FileName;
        }

        public void Set_ADBPath()
        {
            ADB_Path = Find_RootPath() + ADB_FileName;
        }

        public void Set_DragonbornDBPath()
        {
            Dragonborn_Path = Find_RootPath() + DragonbornDB_FileName;
        }

        public void Set_Path_Spells_and_SpellLists()
        {
            string roothpath;
            roothpath = Find_RootPath();
            SDB_Path = roothpath + SDB_FileName;
            Bard_SpellList_Path = roothpath + Bard_SpellList_FileName;
            Wizard_SpellList_Path = roothpath + Wizard_SpellList_FileName;
        }

        public string Read_RaceDataBase()
        {
            string jsonRDB = File.ReadAllText(RDB_Path);

            return jsonRDB;
        }

        public string Read_LanguageDataBase()
        {
            string jsonLDB = File.ReadAllText(LDB_Path);

            return jsonLDB;
        }

        public string Read_ItemDataBase()
        {
            string jsonIDB = File.ReadAllText(IDB_Path);

            return jsonIDB;
        }

        public string Read_WeaponDataBase()
        {
            string jsonWDB = File.ReadAllText(WDB_Path);

            return jsonWDB;
        }

        public string Read_ArmorDataBase()
        {
            string jsonADB = File.ReadAllText(ADB_Path);

            return jsonADB;
        }      

        public string Read_DragonbornDB()
        {
            string jsonDragonbornDB = File.ReadAllText(Dragonborn_Path);

            return jsonDragonbornDB;
        }
        
        public void Read_Spells_and_SpellLists()
        {
            jsonSDB = File.ReadAllText(SDB_Path);
            jsonBSL = File.ReadAllText(Bard_SpellList_Path);
            jsonWSL = File.ReadAllText(Wizard_SpellList_Path);
        }

    }
}
