﻿using System;
using System.Collections.Generic;
using System.Windows;


namespace DnD_CharSheet_5e
{
    /// <summary>
    /// Interaktionslogik für SaveScreen.xaml
    /// </summary>
    public partial class SaveScreen : Window
    {
        FileManager fileManager = new FileManager();
        Character currenCharacter = new Character();
        List<string> characterNames = new List<string>();

        public SaveScreen()
        {
            InitializeComponent();
            fileManager.Find_RootPath();
            fileManager.Check_for_SaveGameFolder();
            fileManager.Set_SaveGames();
            Load_Names_for_SlotButtons();
        }

        public void Show_FilePath()
        {
            pathVisualizer.Text = fileManager.Find_RootPath();
            fileManager.Check_for_SaveGameFolder();
            fileManager.Set_SaveGames();
        }

        public void Fetch_Character(Character character)
        {
            currenCharacter = character;
        }

        public void SaveGame01_BT_Click(object sender, RoutedEventArgs e)
        {
            saveGame_01_bt.Content = currenCharacter.Get_charName();
            SaveSystem.SaveCharacter(currenCharacter, fileManager.saveGame_01);
            SaveSystem.Save_CharName(currenCharacter.Get_charName(), fileManager.namesDataBase, 0);
            this.Close();
        }

        public void SaveGame02_BT_Click(object sender, RoutedEventArgs e)
        {
            saveGame_02_bt.Content = currenCharacter.Get_charName();
            SaveSystem.SaveCharacter(currenCharacter, fileManager.saveGame_02);
            SaveSystem.Save_CharName(currenCharacter.Get_charName(), fileManager.namesDataBase, 1);
            this.Close();
        }

        public void SaveGame03_BT_Click(object sender, RoutedEventArgs e)
        {
            saveGame_03_bt.Content = currenCharacter.Get_charName();
            SaveSystem.SaveCharacter(currenCharacter, fileManager.saveGame_03);
            SaveSystem.Save_CharName(currenCharacter.Get_charName(), fileManager.namesDataBase, 2);
            this.Close();
        }

        public void SaveGame04_BT_Click(object sender, RoutedEventArgs e)
        {
            saveGame_04_bt.Content = currenCharacter.Get_charName();
            SaveSystem.SaveCharacter(currenCharacter, fileManager.saveGame_04);
            SaveSystem.Save_CharName(currenCharacter.Get_charName(), fileManager.namesDataBase, 3);
            this.Close();
        }

        public void SaveGame05_BT_Click(object sender, RoutedEventArgs e)
        {
            saveGame_05_bt.Content = currenCharacter.Get_charName();
            SaveSystem.SaveCharacter(currenCharacter, fileManager.saveGame_05);
            SaveSystem.Save_CharName(currenCharacter.Get_charName(), fileManager.namesDataBase, 4);
            this.Close();
        }

        public void Load_Names_for_SlotButtons()
        {
            characterNames = SaveSystem.Load_CharNames(fileManager.namesDataBase);

            if(characterNames != null)
            {
                saveGame_01_bt.Content = characterNames[0];
                saveGame_02_bt.Content = characterNames[1];
                saveGame_03_bt.Content = characterNames[2];
                saveGame_04_bt.Content = characterNames[3];
                saveGame_05_bt.Content = characterNames[4];
            }
        }
    }
}
