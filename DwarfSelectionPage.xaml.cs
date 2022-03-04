using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

namespace DnD_CharSheet_5e
{
    /// <summary>
    /// Interaktionslogik für DwarfSelectionPage.xaml
    /// </summary>
    public partial class DwarfSelectionPage : Page
    {
        Proficiency SelectedProficiency;
        Proficiency BrewersSupplies;
        Proficiency MasonsTools;
        Proficiency SmithsTools;

        List<Rectangle> CheckBoxes;

        bool brewerCBchecked = false;
        bool masonsCBchecked = false;
        bool smithsCBckecked = false;

        public delegate void OnSelectionConfirmed();
        public OnSelectionConfirmed onSelectionConfirmed;

        public DwarfSelectionPage()
        {
            InitializeComponent();

            SelectedProficiency = new Proficiency();
            BrewersSupplies = new Proficiency();
            MasonsTools = new Proficiency();
            SmithsTools = new Proficiency();

            Init_ArtisansTools();

            Init_CheckBoxes();
        }

        private void Init_ArtisansTools()
        {
            BrewersSupplies.ProficiencyType = "Tool";
            BrewersSupplies.ProficiencyName = "Brewer's Supplies";

            MasonsTools.ProficiencyType = "Tool";
            MasonsTools.ProficiencyName = "Mason's Tools";

            SmithsTools.ProficiencyType = "Tool";
            SmithsTools.ProficiencyName = "Smith's Tools";
        }

        private void Init_CheckBoxes()
        {
            CheckBoxes = new List<Rectangle>();

            foreach(Rectangle checkbox in CheckBoxesPanel.Children)
            {
                CheckBoxes.Add(checkbox);
            }
        }

        private void CheckBox_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Rectangle SelectedCheckBox = (Rectangle)sender;
            
            if(SelectedCheckBox.Fill != Brushes.PowderBlue)
            {
                SelectedCheckBox.Fill = Brushes.Thistle;
            }
        }

        private void CheckBox_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Rectangle SelectedCheckBox = (Rectangle)sender;
            
            if(SelectedCheckBox.Fill != Brushes.PowderBlue)
            {
                SelectedCheckBox.Fill = Brushes.White;
            }
        }

        private void BrewersSuppliesCheckBox_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            brewerCBchecked = true;

            if(masonsCBchecked == true || smithsCBckecked == true)
            {
                masonsCBchecked = false;
                MasonsToolsCheckBox.Fill = Brushes.White;

                smithsCBckecked = false;
                SmithsToolCheckBox.Fill = Brushes.White;
            }

            BrewersSuppliesCheckBox.Fill = Brushes.PowderBlue;

            SelectedProficiency = BrewersSupplies;
        }

        private void MasonsToolsCheckBox_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            masonsCBchecked = true;
            
            if (brewerCBchecked == true || smithsCBckecked == true)
            {
                brewerCBchecked = false;
                BrewersSuppliesCheckBox.Fill = Brushes.White;

                smithsCBckecked = false;
                SmithsToolCheckBox.Fill = Brushes.White;
            }

            MasonsToolsCheckBox.Fill = Brushes.PowderBlue;

            SelectedProficiency = MasonsTools;
        }

        private void SmithsToolCheckBox_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            smithsCBckecked = true;

            if (masonsCBchecked == true || brewerCBchecked == true)
            {
                brewerCBchecked = false;
                BrewersSuppliesCheckBox.Fill = Brushes.White;

                masonsCBchecked = false;
                MasonsToolsCheckBox.Fill = Brushes.White;
            }

            SmithsToolCheckBox.Fill = Brushes.PowderBlue;

            SelectedProficiency = SmithsTools;
        }

        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedProficiency.ProficiencyName != null)
            {
                SheetManager.CS_Manager_Inst.CharRace.RaceProficiencies.Add(SelectedProficiency);
                onSelectionConfirmed.Invoke();
            }

            else
            {
                MessageBox.Show("You haven't selected a tool proficiency yet. Please choose on to continue.");
            }
        }
    }
}
