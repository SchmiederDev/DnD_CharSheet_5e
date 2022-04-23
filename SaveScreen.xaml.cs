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
        
        List<string> characterNames = new List<string>();

        public SaveScreen()
        {
            InitializeComponent();                     
            Load_Names_for_SlotButtons();
        }

        // Is it still called anywhere?
        public void Show_FilePath()
        {            
            FileManager.FM_Inst.Check_for_SaveGameFolder();
            FileManager.FM_Inst.Set_SaveGames();
        }        

        public void SaveGame01_BT_Click(object sender, RoutedEventArgs e)
        {
            saveGame_01_bt.Content = SheetManager.CS_Manager_Inst.character.CharacterName;
            SaveSystem.SaveCharacter(SheetManager.CS_Manager_Inst.character, FileManager.FM_Inst.saveGame_01);
            SaveSystem.Save_CharName(SheetManager.CS_Manager_Inst.character.CharacterName, FileManager.FM_Inst.namesDataBase, 0);            
            this.Close();
        }

        public void SaveGame02_BT_Click(object sender, RoutedEventArgs e)
        {
            saveGame_02_bt.Content = SheetManager.CS_Manager_Inst.character.CharacterName;
            SaveSystem.SaveCharacter(SheetManager.CS_Manager_Inst.character, FileManager.FM_Inst.saveGame_02);
            SaveSystem.Save_CharName(SheetManager.CS_Manager_Inst.character.CharacterName, FileManager.FM_Inst.namesDataBase, 1);
            this.Close();
        }

        public void SaveGame03_BT_Click(object sender, RoutedEventArgs e)
        {
            saveGame_03_bt.Content = SheetManager.CS_Manager_Inst.character.CharacterName;
            SaveSystem.SaveCharacter(SheetManager.CS_Manager_Inst.character, FileManager.FM_Inst.saveGame_03);
            SaveSystem.Save_CharName(SheetManager.CS_Manager_Inst.character.CharacterName, FileManager.FM_Inst.namesDataBase, 2);
            this.Close();
        }

        public void SaveGame04_BT_Click(object sender, RoutedEventArgs e)
        {
            saveGame_04_bt.Content = SheetManager.CS_Manager_Inst.character.CharacterName;
            SaveSystem.SaveCharacter(SheetManager.CS_Manager_Inst.character, FileManager.FM_Inst.saveGame_04);
            SaveSystem.Save_CharName(SheetManager.CS_Manager_Inst.character.CharacterName, FileManager.FM_Inst.namesDataBase, 3);
            this.Close();
        }

        public void SaveGame05_BT_Click(object sender, RoutedEventArgs e)
        {
            saveGame_05_bt.Content = SheetManager.CS_Manager_Inst.character.CharacterName;
            SaveSystem.SaveCharacter(SheetManager.CS_Manager_Inst.character, FileManager.FM_Inst.saveGame_05);
            SaveSystem.Save_CharName(SheetManager.CS_Manager_Inst.character.CharacterName, FileManager.FM_Inst.namesDataBase, 4);
            this.Close();
        }

        public void Load_Names_for_SlotButtons()
        {
            characterNames = SaveSystem.Load_CharNames(FileManager.FM_Inst.namesDataBase);

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
