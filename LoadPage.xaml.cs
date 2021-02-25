using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DnD_CharSheet_5e
{
    /// <summary>
    /// Interaktionslogik für LoadPage.xaml
    /// </summary>
    public partial class LoadPage : Page
    {
        FileManager fileManager = new FileManager();
        CharacterData L_charData = new CharacterData();
        Character L_character = new Character();

        bool charIsLoaded = false;
        bool wasClosed = false;
        public LoadPage()
        {
            InitializeComponent();
        }

        public void Check_for_Files()
        {
            fileManager.Find_RootPath();
            fileManager.Set_SaveGames();
        }

        public void LoadCharacter_Slot01bt_Click(object sender, RoutedEventArgs e)
        {
            L_charData = SaveSystem.LoadCharacter(fileManager.saveGame_01);
            
            if(L_charData != null)
            {
                L_character.Load_Character(L_charData);
                charIsLoaded = true;
            }            
        }

        public bool Get_CharLoad()
        {
            return charIsLoaded;
        }

        public Character Get_LoadedChar()
        {
            return L_character;
        }        
    }
}
