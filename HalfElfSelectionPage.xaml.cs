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
    /// Interaktionslogik für HalfElfSelectionPage.xaml
    /// </summary>
    public partial class HalfElfSelectionPage : Page
    {
        List<CheckBox> AbilityBoxes;

        Ability FirstSelectedAbility = new Ability();
        Ability SecondSelectedAbility = new Ability();

        Skill FirstSelectedSkill = new Skill();
        Skill SecondSelectedSkill = new Skill();

        List<Button> FirstSkillButtons = new List<Button>();
        List<Button> SecondSkillButtons = new List<Button>();
        

        public delegate void OnSelecionConfirmed();
        public OnSelecionConfirmed onSelecionConfirmed;

        public HalfElfSelectionPage()
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

            foreach (CheckBox checkBox in AbilityCheckBoxes.Children)
            {
                AbilityBoxes.Add(checkBox);
            }
        }

        private void Init_SkillOptions()
        {
            Generate_SkillButtonsDesign();
            Add_EventHandlers_and_AddToPanel();
        }

        private void Generate_SkillButtonsDesign()
        {
            foreach (Skill skill in SheetManager.CS_Manager_Inst.CharGenCharacter.Skills)
            {
                Button SkillButton = Generate_SkillTxt(skill.SkillName);
                FirstSkillButtons.Add(SkillButton);

                Button SecondSkillButton = Generate_SkillTxt(skill.SkillName);              
                SecondSkillButtons.Add(SecondSkillButton);
            }
        }

        private void Add_EventHandlers_and_AddToPanel()
        {
            foreach(Button SkillButton in FirstSkillButtons)
            {
                SkillButton.Click += new RoutedEventHandler(FirstSkillBtn_Click);
                FirstSkillsPanel.Children.Add(SkillButton);
            }

            foreach(Button SecondSkillButton in SecondSkillButtons)
            {
                SecondSkillButton.Click += new RoutedEventHandler(SecondSkillBtn_Click);
                SecondSkillsPanel.Children.Add(SecondSkillButton);
            }
        }

        private Button Generate_SkillTxt(string skillName)
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

            return SkillButton;
        }

        private void FirstSkillBtn_Click(object sender, RoutedEventArgs e)
        {
            Button SelectedSkillBtn = (Button)sender;
            FirstSelectedSkill = Find_CorrespondingSkill(SelectedSkillBtn.Content.ToString());
            FirstSelectedSkillTxt.Text = FirstSelectedSkill.SkillName;
        }

        private void SecondSkillBtn_Click(object sender, RoutedEventArgs e)
        {
            Button SelectedSkillBtn = (Button)sender;
            SecondSelectedSkill = Find_CorrespondingSkill(SelectedSkillBtn.Content.ToString());
            SecondSelectedSkillTxt.Text = SecondSelectedSkill.SkillName;
        }

        private Skill Find_CorrespondingSkill(string skillName)
        {
            Skill ChosenSkill = SheetManager.CS_Manager_Inst.CharGenCharacter.Skills.Find(SkillElement => SkillElement.SkillName == skillName);
            return ChosenSkill;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox SelectedCheckBox = (CheckBox)sender;

            string abKeyToFind = TrimTo_AbilityKey(SelectedCheckBox.Name);

            int numberOfCheckedBoxes = Check_CheckBoxesChecked();

            if(numberOfCheckedBoxes <= 1)
            {               
                FirstSelectedAbility = FindCorrespondingAbility(abKeyToFind);
                FirstSelectedAbilityTxt.Text = FirstSelectedAbility.AbilityName;
            }

            else
            {
                SecondSelectedAbility = FindCorrespondingAbility(abKeyToFind);
                SecondSelectedAbilityTxt.Text = SecondSelectedAbility.AbilityName;
                Disable_CheckBoxes();
            }

        }

        private int Check_CheckBoxesChecked()
        {
            int numberOfCheckedBoxes = 0;

            foreach(CheckBox checkBox in AbilityBoxes)
            {
                if(checkBox.IsChecked == true)
                {
                    numberOfCheckedBoxes++;
                }
            }

            return numberOfCheckedBoxes;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Enable_CheckBoxes();
        }

        private void Enable_CheckBoxes()
        {
            foreach (CheckBox checkBox in AbilityBoxes)
            {
                if (checkBox.IsChecked == false)
                {
                    checkBox.IsEnabled = true;
                }
            }
        }

        private void Disable_CheckBoxes()
        {
            foreach (CheckBox checkBox in AbilityBoxes)
            {
                if (checkBox.IsChecked == false)
                {
                    checkBox.IsEnabled = false;
                }
            }
        }

        private string TrimTo_AbilityKey(string checkBoxName)
        {
            string tempName = checkBoxName;
            string[] tempStrings = tempName.Split('_');
            string abilityToFind = tempStrings[0];
            return abilityToFind;
        }

        private Ability FindCorrespondingAbility(string key)
        {
            Ability AbilityToFind = SheetManager.CS_Manager_Inst.character.Abilities.Find(elementKey => elementKey.ReferenceKey == key);
            return AbilityToFind;
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (Verify_Selection())
            {
                SheetManager.CS_Manager_Inst.CharGenCharacter.CharRace.AbScoreIncrease_One = FirstSelectedAbility;
                SheetManager.CS_Manager_Inst.CharGenCharacter.CharRace.AbScoreIncrease_Two = SecondSelectedAbility;

                SheetManager.CS_Manager_Inst.CharGenCharacter.CharRace.AdditionalSkillProficiency_One = FirstSelectedSkill;
                SheetManager.CS_Manager_Inst.CharGenCharacter.CharRace.AdditionalSkillProficiency_Two = SecondSelectedSkill;

                if(onSelecionConfirmed != null)
                {
                    onSelecionConfirmed.Invoke();
                }
            }

            else
            {
                MessageBox.Show("Your selection is incomplete. Please select/ enter data for all of the options.");
            }
        }

        private bool Verify_Selection()
        {
            bool selectionComplete;

            if (Check_CheckBoxesChecked() == 2 && FirstSelectedSkill.SkillName != null && SecondSelectedSkill.SkillName != null)
            {
                selectionComplete = true;
            }

            else
            {
                selectionComplete = false;
            }

            return selectionComplete;
        }
    }
}
