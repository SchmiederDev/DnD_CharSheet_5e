using System;
using System.Collections.Generic;
using System.IO;

namespace DnD_CharSheet_5e
{
    public class FileManager
    {
        string rootPath;

        string folderPath = @"DnD_CharSheet_5e";
        string folderName = @"\SaveGames";

        string saveGameFolder;

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

        string IDB_File = @"\ItemDataBase.json";
        string IDB_Path;

        string WDB_File = @"\WeaponDataBase.json";
        string WDB_Path;

        string ADB_File = @"\ArmorDataBase.json";
        string ADB_Path;

        public string Find_RootPath()
        {
            rootPath = Path.GetFullPath(folderPath);                       

            return rootPath;
        }

        public void Check_for_SaveGameFolder()
        {
            saveGameFolder = rootPath + folderName;

            if (!Directory.Exists(saveGameFolder))
            {
                Directory.CreateDirectory(saveGameFolder);
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

        public void Set_IDBPath()
        {
            IDB_Path = Find_RootPath() + IDB_File;
        }

        public void Set_WDBPath()
        {
            WDB_Path = Find_RootPath() + WDB_File;
        }

        public void Set_ADBPath()
        {
            ADB_Path = Find_RootPath() + ADB_File;
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
