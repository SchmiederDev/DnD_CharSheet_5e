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

        public string saveGame_01 { set;  get; }
        string saveGame_02;
        string saveGame_03;
        string saveGame_04;
        string saveGame_05;

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
        }

    }
}
