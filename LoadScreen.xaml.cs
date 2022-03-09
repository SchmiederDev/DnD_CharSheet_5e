using System.Windows;
using System.Collections.Generic;


namespace DnD_CharSheet_5e
{
    /// <summary>
    /// Interaktionslogik für LoadScreen.xaml
    /// </summary>
    public partial class LoadScreen : Window
    {        
        CharacterData L_charData = new CharacterData();
        Character L_character = new Character();
        List<string> characterNames = new List<string>();

        public LoadScreen()
        {
            InitializeComponent();
            Load_Names_for_SlotButtons();
        }

        public void LoadCharacter_Slot01bt_Click(object sender, RoutedEventArgs e)
        {
            L_charData = SaveSystem.LoadCharacter(FileManager.FM_Inst.saveGame_01);            

            if (L_charData != null)            {
                
                L_character.Load_Character(L_charData);                

                if(L_character != null)
                {                    
                    SheetManager.CS_Manager_Inst.character = L_character;
                    MainWindow.mainWindow_Inst.Load_Character();
                }
            }

            else
            {
                MessageBox.Show($"Sorry, there exists no character under this Slot.");
            }

            MainWindow.mainWindow_Inst.Activate_SideBarMenu_Buttons();
            this.Close();
        }

        public void LoadCharacter_Slot02bt_Click(object sender, RoutedEventArgs e)
        {
            L_charData = SaveSystem.LoadCharacter(FileManager.FM_Inst.saveGame_02);

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
                MessageBox.Show($"Sorry, there exists no character under this Slot.");
            }

            MainWindow.mainWindow_Inst.Activate_SideBarMenu_Buttons();
            this.Close();
        }

        public void LoadCharacter_Slot03bt_Click(object sender, RoutedEventArgs e)
        {
            L_charData = SaveSystem.LoadCharacter(FileManager.FM_Inst.saveGame_03);

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
                MessageBox.Show($"Sorry, there exists no character under this Slot.");
            }

            MainWindow.mainWindow_Inst.Activate_SideBarMenu_Buttons();
            this.Close();
        }

        public void LoadCharacter_Slot04bt_Click(object sender, RoutedEventArgs e)
        {
            L_charData = SaveSystem.LoadCharacter(FileManager.FM_Inst.saveGame_04);

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
                MessageBox.Show($"Sorry, there exists no character under this Slot.");
            }

            MainWindow.mainWindow_Inst.Activate_SideBarMenu_Buttons();
            this.Close();
        }

        public void LoadCharacter_Slot05bt_Click(object sender, RoutedEventArgs e)
        {
            L_charData = SaveSystem.LoadCharacter(FileManager.FM_Inst.saveGame_05);

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
                MessageBox.Show($"Sorry, there exists no character under this Slot.");
            }

            MainWindow.mainWindow_Inst.Activate_SideBarMenu_Buttons();
            this.Close();
        }

        public void Load_Names_for_SlotButtons()
        {
            characterNames = SaveSystem.Load_CharNames(FileManager.FM_Inst.namesDataBase);

            if (characterNames != null)
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
