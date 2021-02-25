using System;
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
            //SaveSystem.SaveName(currenCharacter.Get_charName(), fileManager.namesDataBase, 0, characterNames);
        }

        public void SaveGame02_BT_Click(object sender, RoutedEventArgs e)
        {
            saveGame_02_bt.Content = currenCharacter.Get_charName();
            SaveSystem.SaveCharacter(currenCharacter, fileManager.saveGame_02);
            //SaveSystem.SaveName(currenCharacter.Get_charName(), fileManager.namesDataBase, 1, characterNames);
        }

        public void SaveGame03_BT_Click(object sender, RoutedEventArgs e)
        {
            saveGame_03_bt.Content = currenCharacter.Get_charName();
            SaveSystem.SaveCharacter(currenCharacter, fileManager.saveGame_03);
            //SaveSystem.SaveName(currenCharacter.Get_charName(), fileManager.namesDataBase, 2, characterNames);
        }

        public void SaveGame04_BT_Click(object sender, RoutedEventArgs e)
        {
            saveGame_04_bt.Content = currenCharacter.Get_charName();
            SaveSystem.SaveCharacter(currenCharacter, fileManager.saveGame_04);
            //SaveSystem.SaveName(currenCharacter.Get_charName(), fileManager.namesDataBase, 3, characterNames);
        }

        public void SaveGame05_BT_Click(object sender, RoutedEventArgs e)
        {
            saveGame_05_bt.Content = currenCharacter.Get_charName();
            SaveSystem.SaveCharacter(currenCharacter, fileManager.saveGame_05);
            //SaveSystem.SaveName(currenCharacter.Get_charName(), fileManager.namesDataBase, 4, characterNames);
        }
    }
}
