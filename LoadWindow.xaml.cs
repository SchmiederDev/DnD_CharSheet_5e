using System.Windows;
using System.Collections.Generic;


namespace DnD_CharSheet_5e
{
    /// <summary>
    /// Interaktionslogik für LoadScreen.xaml
    /// </summary>
    
    // Class for loading saved characters -> will initialize a loaded Character to the instance of Character-class and to UI in MainWindow 
    
    public partial class LoadWindow : Window
    {
        #region PROPERTIES

        CharacterData L_charData = new CharacterData();
        Character L_character = new Character();
        
        List<string> characterNames = new List<string>();

        const string NoCharacterMessage = "Sorry, no character exists under this Slot.";

        #endregion

        #region CONSTRUCTOR AND SLOT BUTTON INITIALIZATION
        public LoadWindow()
        {
            InitializeComponent();
            Load_Names_for_SlotButtons();
        }

        private void Load_Names_for_SlotButtons()
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

        #region LOAD CHARACTER METHOD AND SLOT BUTTON CLICK EVENT HANDLER
        private void LoadCharacter()
        {
            if (L_charData != null)
            {

                L_character.Load_Character(L_charData);

                if (L_character != null)
                {
                    SheetManager.CS_Manager_Inst.character = L_character;
                    MainWindow.mainWindow_Inst.Load_Character();
                }
            }

            else
            {
                MessageBox.Show(NoCharacterMessage);
            }

            MainWindow.mainWindow_Inst.Activate_SideBarMenu_Buttons();
            Close();
        }

        private void LoadCharacter_Slot01Btn_Click(object sender, RoutedEventArgs e)
        {
            L_charData = SaveSystem.LoadCharacter(FileManager.FM_Inst.saveGame_01);
            LoadCharacter();            
        }

        private void LoadCharacter_Slot02Btn_Click(object sender, RoutedEventArgs e)
        {
            L_charData = SaveSystem.LoadCharacter(FileManager.FM_Inst.saveGame_02);
            LoadCharacter();
        }

        private void LoadCharacter_Slot03Btn_Click(object sender, RoutedEventArgs e)
        {
            L_charData = SaveSystem.LoadCharacter(FileManager.FM_Inst.saveGame_03);
            LoadCharacter();
        }

        private void LoadCharacter_Slot04Btn_Click(object sender, RoutedEventArgs e)
        {
            L_charData = SaveSystem.LoadCharacter(FileManager.FM_Inst.saveGame_04);
            LoadCharacter();
        }

        private void LoadCharacter_Slot05Btn_Click(object sender, RoutedEventArgs e)
        {
            L_charData = SaveSystem.LoadCharacter(FileManager.FM_Inst.saveGame_05);
            LoadCharacter();
        }
        #endregion
    }
}
