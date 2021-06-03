using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Windows;

namespace DnD_CharSheet_5e
{
    public class FileManager
    {
        public static FileManager FM_Inst = new FileManager();

        FileManager()
        {          
            if(FM_Inst == null)
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

        public string saveGame_01 { set;  get; }
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

        string IDB_FileName = @"\ItemDataBase.json";
        string IDB_Path;

        string WDB_FileName = @"\WeaponDataBase.json";
        string WDB_Path;

        string ADB_FileName = @"\ArmorDataBase.json";
        string ADB_Path;

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

            if(!Directory.Exists(SoundEffectsFolder))
            {
                Directory.CreateDirectory(SoundEffectsFolder);
            }
        }

        public void Check_for_Images_Folder()
        {
            ImagesFolder = rootPath + Images_FolderPath;

            if(!Directory.Exists(ImagesFolder))
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

    }
}
