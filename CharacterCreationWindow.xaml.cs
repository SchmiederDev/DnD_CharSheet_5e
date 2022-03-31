using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Newtonsoft.Json;

namespace DnD_CharSheet_5e
{
    /// <summary>
    /// Interaktionslogik für CharacterCreationWindow.xaml
    /// </summary>
    public partial class CharacterCreationWindow : Window
    {
        List<Page> CreationPages;
        
        AbilityStatsPage abilityStatsPage;
        RaceSelectionPage raceSelectionPage;
        ClassSelectionPage classSelectionPage;

        RaceOptionsWindow RaceOptionsWdw;

        public CharacterCreationWindow()
        {
            InitializeComponent();

            RaceOptionsWdw = new RaceOptionsWindow();

            Init_CreationPages();
            this.CharCreate_MainFrame.Content = CreationPages[0];            
        }              

        private void Init_CreationPages()
        {
            CreationPages = new List<Page>();

            abilityStatsPage = new AbilityStatsPage();
            abilityStatsPage.Name = "abilityStatsPage";
            CreationPages.Add(abilityStatsPage);
            abilityStatsPage.onStatsConfirmed += Enable_NextBtn;

            raceSelectionPage = new RaceSelectionPage();
            raceSelectionPage.Name = "raceSelectionPage";
            CreationPages.Add(raceSelectionPage);
            raceSelectionPage.onRaceConfirm += Enable_NextBtn;
            raceSelectionPage.onNewRaceSelected += Disable_NextBtn;

            classSelectionPage = new ClassSelectionPage();
            classSelectionPage.Name = "classSelectionPage";
            CreationPages.Add(classSelectionPage);
        }

        private void Previous_Btn_Click(object sender, RoutedEventArgs e)
        {
            Load_PreviousPage();
        }

        private void Next_Btn_Click(object sender, RoutedEventArgs e)
        {
            Load_NextPage();
        }

        private string Get_CurrentPageName()
        {
            Page tempPage = CharCreate_MainFrame.Content as Page;

            return tempPage.Name;
        }

        private int Find_IndexOf_NextPage()
        {           
            
            int indexOfCurrentPage = CreationPages.FindIndex(currentPage => currentPage.Name == Get_CurrentPageName());

            if (CreationPages[indexOfCurrentPage] is RaceSelectionPage)
            {
                RaceOptionsWdw.Set_CharRace();
                RaceOptionsWdw.Show();
            }

            int indexOfNextPage = indexOfCurrentPage + 1;

            return indexOfNextPage;
        }

        private int Find_IndexOf_PreviousPage()
        {
            int indexOfCurrentPage = CreationPages.FindIndex(currentPage => currentPage.Name == Get_CurrentPageName());

            int indexOfPreviousPage = indexOfCurrentPage - 1;

            return indexOfPreviousPage;
        }

        private void Load_NextPage()
        {            
            int indexOfNextPage = Find_IndexOf_NextPage();
            
            if(indexOfNextPage >= 0 && indexOfNextPage < CreationPages.Count)
            {
                CharCreate_MainFrame.Content = CreationPages[indexOfNextPage];                
                
                Disable_NextBtn();
                Enable_PreviousBtn();
            }
        }

        private void Load_PreviousPage()
        {
            int indexOfPreviousPage = Find_IndexOf_PreviousPage();

            if (indexOfPreviousPage >= 0)
            {
                CharCreate_MainFrame.Content = CreationPages[indexOfPreviousPage];

                if (CreationPages[indexOfPreviousPage] is RaceSelectionPage)
                {
                    RaceSelectionPage raceSelectionPage = (RaceSelectionPage)CreationPages[indexOfPreviousPage];
                    raceSelectionPage.ClearPreviewBoxes();
                }
            }
        }

        private void Enable_NextBtn()
        {
            Next_Btn.IsEnabled = true;
        }

        private void Disable_NextBtn()
        {
            Next_Btn.IsEnabled = false;
        }

        private void Enable_PreviousBtn()
        {
            Previous_Btn.IsEnabled = true;
        }

        private void Disable_PreviousBtn()
        {
            Previous_Btn.IsEnabled = false;
        }
    }
}
