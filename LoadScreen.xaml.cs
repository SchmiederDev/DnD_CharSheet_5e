using System.Windows;


namespace DnD_CharSheet_5e
{
    /// <summary>
    /// Interaktionslogik für LoadScreen.xaml
    /// </summary>
    public partial class LoadScreen : Window
    {
        FileManager fileManager = new FileManager();
        CharacterData L_charData = new CharacterData();
        Character L_character = new Character();

        public LoadScreen()
        {
            InitializeComponent();
        }

        public void Check_for_Files()
        {
            fileManager.Find_RootPath();
            fileManager.Check_for_SaveGameFolder();
            fileManager.Set_SaveGames();            
        }

        public void LoadCharacter_Slot01bt_Click(object sender, RoutedEventArgs e)
        {
            L_charData = SaveSystem.LoadCharacter(fileManager.saveGame_01);            

            if (L_charData != null)            {
                
                L_character.Load_Character(L_charData);                

                if(L_character != null)
                {
                    SheetManager.CS_Manager_Inst.Set_Character(L_character);
                    MainWindow.mainWindow_Inst.Load_Character();
                }
            }

            else
            {
                MessageBox.Show($"Sorry, there exists no character under this Slot.");
            }

            this.Close();
        }

        public void LoadCharacter_Slot02bt_Click(object sender, RoutedEventArgs e)
        {
            L_charData = SaveSystem.LoadCharacter(fileManager.saveGame_02);

            if (L_charData != null)
            {

                L_character.Load_Character(L_charData);

                if (L_character != null)
                {
                    SheetManager.CS_Manager_Inst.Set_Character(L_character);
                    MainWindow.mainWindow_Inst.Load_Character();
                }
            }

            else
            {
                MessageBox.Show($"Sorry, there exists no character under this Slot.");
            }

            this.Close();
        }

        public void LoadCharacter_Slot03bt_Click(object sender, RoutedEventArgs e)
        {
            L_charData = SaveSystem.LoadCharacter(fileManager.saveGame_03);

            if (L_charData != null)
            {

                L_character.Load_Character(L_charData);

                if (L_character != null)
                {
                    SheetManager.CS_Manager_Inst.Set_Character(L_character);
                    MainWindow.mainWindow_Inst.Load_Character();
                }
            }

            else
            {
                MessageBox.Show($"Sorry, there exists no character under this Slot.");
            }

            this.Close();
        }

        public void LoadCharacter_Slot04bt_Click(object sender, RoutedEventArgs e)
        {
            L_charData = SaveSystem.LoadCharacter(fileManager.saveGame_04);

            if (L_charData != null)
            {

                L_character.Load_Character(L_charData);

                if (L_character != null)
                {
                    SheetManager.CS_Manager_Inst.Set_Character(L_character);
                    MainWindow.mainWindow_Inst.Load_Character();
                }
            }

            else
            {
                MessageBox.Show($"Sorry, there exists no character under this Slot.");
            }

            this.Close();
        }

        public void LoadCharacter_Slot05bt_Click(object sender, RoutedEventArgs e)
        {
            L_charData = SaveSystem.LoadCharacter(fileManager.saveGame_05);

            if (L_charData != null)
            {

                L_character.Load_Character(L_charData);

                if (L_character != null)
                {
                    SheetManager.CS_Manager_Inst.Set_Character(L_character);
                    MainWindow.mainWindow_Inst.Load_Character();
                }
            }

            else
            {
                MessageBox.Show($"Sorry, there exists no character under this Slot.");
            }

            this.Close();
        }
    }
}
