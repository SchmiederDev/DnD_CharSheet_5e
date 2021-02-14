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
        public LoadScreen()
        {
            InitializeComponent();
        }

        public void Check_for_Files()
        {
            fileManager.Find_RootPath();
            fileManager.Set_SaveGames();
        }
    }
}
