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

        public SaveScreen()
        {
            InitializeComponent();
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
        }       
        
    }
}
