﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DnD_CharSheet_5e
{
    /// <summary>
    /// Interaktionslogik für RaceOptions.xaml
    /// </summary>
    public partial class RaceOptionsWindow : Window
    {
        CharacterRace CharRace;

        bool canClose = false;

        DragonbornSelectionPage dragonbornSelectionPage;
        
        DwarfSelectionPage dwarfSelectionPage;
        
        HighElfSelectionPage highElfSelectionPage;
        HighElfSelectionPage2 highElfSelectionPage2;

        HumanSelectionPage humanSelectionPage;

        public bool highElfSelectionPage_IsFirstLoad { get; set; } = true;

        public RaceOptionsWindow()
        {
            InitializeComponent();
            CharRace = new CharacterRace();
            Init_SubPages();
        }

        private void Init_SubPages()
        {
            dragonbornSelectionPage = new DragonbornSelectionPage();
            dragonbornSelectionPage.onSelectionConfirmed += Close_Window;

            dwarfSelectionPage = new DwarfSelectionPage();
            dwarfSelectionPage.onSelectionConfirmed += Close_Window;

            highElfSelectionPage = new HighElfSelectionPage();
            highElfSelectionPage2 = new HighElfSelectionPage2();

            highElfSelectionPage.onSpellSelectionConfirmed += Load_HighElfSelecetionPageTwo;
            highElfSelectionPage2.onLanguageConfirmed += Close_Window;

            humanSelectionPage = new HumanSelectionPage();
            humanSelectionPage.onLanguageConfirmed += Close_Window;
            humanSelectionPage.onVariantSelected += Load_HumanSelectionPageTwo;
        }

        public void Set_CharRace()
        {
            if(SheetManager.CS_Manager_Inst.CharRace != null)
            {
                CharRace = SheetManager.CS_Manager_Inst.CharRace;
                RaceNmeTxt.Text = CharRace.RaceBackground.RaceName;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(!canClose)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            if(CharRace.RaceBackground.RaceName == "Dragonborn")
            {
                RaceOptionsFrame.Content = dragonbornSelectionPage;
            }

            else if(CharRace.RaceBackground.RaceName == "Dwarf")
            {
                RaceOptionsFrame.Content = dwarfSelectionPage;
            }

            else if(CharRace.CharakterSubrace.SubraceName == "High Elf")
            {
                if(highElfSelectionPage_IsFirstLoad)
                {
                    RaceOptionsFrame.Content = highElfSelectionPage;
                    highElfSelectionPage_IsFirstLoad = false;
                }
            }

            else if (CharRace.RaceBackground.RaceName == "Human")
            {
                RaceOptionsFrame.Content = humanSelectionPage;
            }

            else
            {
                Close_Window();
            }
        }

        private void Close_Window()
        {
            highElfSelectionPage_IsFirstLoad = true;
            this.Close();
        }

        private void Load_HighElfSelecetionPageTwo()
        {
            RaceOptionsFrame.Content = highElfSelectionPage2;
        }

        private void Load_HumanSelectionPageTwo()
        {

        }
    }
}