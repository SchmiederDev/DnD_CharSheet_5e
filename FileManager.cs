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

        public string Read_ItemDataBase()
        {
            string jsonIDB = File.ReadAllText(IDB_Path);            

            return jsonIDB;
        }

    }
}
