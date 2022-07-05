using System.Collections.Generic;
using System.Windows;


namespace DnD_CharSheet_5e
{
    /// <summary>
    /// Interaktionslogik für SaveScreen.xaml
    /// </summary>
    

    // Very simple Window-class to allow players/ users to save their characters
    
    public partial class SaveWindow : Window
    {       
        
        List<string> characterNames = new List<string>();

        #region CONSTRUCTOR AND UI INITILIZATION

        public SaveWindow()
        {
            InitializeComponent();                     
            Load_Names_for_SlotButtons();
        }

        public void Load_Names_for_SlotButtons()
        {
            characterNames = SaveSystem.Load_CharNames(FileManager.FM_Inst.namesDataBase);

            if (characterNames != null)
            {
                saveGame_01_Btn.Content = characterNames[0];
                saveGame_02_Btn.Content = characterNames[1];
                saveGame_03_Btn.Content = characterNames[2];
                saveGame_04_Btn.Content = characterNames[3];
                saveGame_05_Btn.Content = characterNames[4];
            }
        }

        #endregion

        #region SAVE SLOT BUTTON EVENT HANDLER
        private void SaveGame01_Btn_Click(object sender, RoutedEventArgs e)
        {
            saveGame_01_Btn.Content = SheetManager.CS_Manager_Inst.character.CharacterName;
            SaveSystem.SaveCharacter(SheetManager.CS_Manager_Inst.character, FileManager.FM_Inst.saveGame_01);
            SaveSystem.Save_CharName(SheetManager.CS_Manager_Inst.character.CharacterName, FileManager.FM_Inst.namesDataBase, 0);            
            Close();
        }

        private void SaveGame02_Btn_Click(object sender, RoutedEventArgs e)
        {
            saveGame_02_Btn.Content = SheetManager.CS_Manager_Inst.character.CharacterName;
            SaveSystem.SaveCharacter(SheetManager.CS_Manager_Inst.character, FileManager.FM_Inst.saveGame_02);
            SaveSystem.Save_CharName(SheetManager.CS_Manager_Inst.character.CharacterName, FileManager.FM_Inst.namesDataBase, 1);
            Close();
        }

        private void SaveGame03_Btn_Click(object sender, RoutedEventArgs e)
        {
            saveGame_03_Btn.Content = SheetManager.CS_Manager_Inst.character.CharacterName;
            SaveSystem.SaveCharacter(SheetManager.CS_Manager_Inst.character, FileManager.FM_Inst.saveGame_03);
            SaveSystem.Save_CharName(SheetManager.CS_Manager_Inst.character.CharacterName, FileManager.FM_Inst.namesDataBase, 2);
            Close();
        }

        private void SaveGame04_Btn_Click(object sender, RoutedEventArgs e)
        {
            saveGame_04_Btn.Content = SheetManager.CS_Manager_Inst.character.CharacterName;
            SaveSystem.SaveCharacter(SheetManager.CS_Manager_Inst.character, FileManager.FM_Inst.saveGame_04);
            SaveSystem.Save_CharName(SheetManager.CS_Manager_Inst.character.CharacterName, FileManager.FM_Inst.namesDataBase, 3);
            Close();
        }

        private void SaveGame05_Btn_Click(object sender, RoutedEventArgs e)
        {
            saveGame_05_Btn.Content = SheetManager.CS_Manager_Inst.character.CharacterName;
            SaveSystem.SaveCharacter(SheetManager.CS_Manager_Inst.character, FileManager.FM_Inst.saveGame_05);
            SaveSystem.Save_CharName(SheetManager.CS_Manager_Inst.character.CharacterName, FileManager.FM_Inst.namesDataBase, 4);
            Close();
        }
        #endregion
    }
}
