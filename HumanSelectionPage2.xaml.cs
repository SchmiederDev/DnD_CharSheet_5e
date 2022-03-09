using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DnD_CharSheet_5e
{
    /// <summary>
    /// Interaktionslogik für HumanSelectionPage2.xaml
    /// </summary>
    public partial class HumanSelectionPage2 : Page
    {
        List<CheckBox> AbilityBoxes;

        Skill SelectedSkill = new Skill();
        Feat CustomFeat = new Feat();

        public delegate void OnSelecionConfirmed();
        public OnSelecionConfirmed onSelecionConfirmed;

        public HumanSelectionPage2()
        {
            InitializeComponent();
            Init_Options();
        }

        private void Init_Options()
        {
            Init_CheckBoxes();
            Init_SkillOptions();
        }

        private void Init_CheckBoxes()
        {
            AbilityBoxes = new List<CheckBox>();

            foreach(CheckBox checkBox in AbilityCheckBoxes.Children)
            {
                AbilityBoxes.Add(checkBox);
            }
        }

        private void Init_SkillOptions()
        {            
            foreach(Skill skill in SheetManager.CS_Manager_Inst.CharGenCharacter.Skills)
            {                
                Generate_SkillTxt(skill.SkillName);
            }
        }

        private void Generate_SkillTxt(string skillName)
        {
            Button SkillButton = new Button();

            SkillButton.Content = skillName;

            SkillButton.Height = 25;
            SkillButton.Width = 125;

            Thickness ButtonMargin = new Thickness();

            ButtonMargin.Top = 5;
            ButtonMargin.Bottom = 5;

            SkillButton.Margin = ButtonMargin;

            SkillButton.FontSize = 14;
            SkillButton.FontWeight = FontWeights.Bold;

            SkillButton.Background = Brushes.Azure;

            SkillButton.Click += new RoutedEventHandler(SkillBtn_Click);
            SkillsPanel.Children.Add(SkillButton);
        }

        private void SkillBtn_Click(object sender, RoutedEventArgs e)
        {
            Button SelectedSkillBtn = (Button)sender;
            SelectedSkill = Find_CorrespondingSkill(SelectedSkillBtn.Content.ToString());
        }

        private Skill Find_CorrespondingSkill(string skillName)
        {
            Skill ChosenSkill = SheetManager.CS_Manager_Inst.CharGenCharacter.Skills.Find(SkillElement => SkillElement.SkillName == skillName);
            return ChosenSkill;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            FeatNameBox.IsEnabled = true;
            FeatDescriptionBox.IsEnabled = true;
        }

        private void OKBtn_Click(object sender, RoutedEventArgs e)
        {
            FeatNameBox.IsEnabled = false;
            FeatDescriptionBox.IsEnabled = false;

            CustomFeat.FeatName = FeatNameBox.Text;
            CustomFeat.FeatDescription = FeatDescriptionBox.Text;
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            foreach(CheckBox checkBox in AbilityBoxes)
            {
                if(checkBox.IsChecked == false)
                {
                    checkBox.IsEnabled = false;
                }
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (CheckBox checkBox in AbilityBoxes)
            {
                if (checkBox.IsChecked == false)
                {
                    checkBox.IsEnabled = true;
                }
            }
        }
    }
}
