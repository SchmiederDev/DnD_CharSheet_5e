using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DnD_CharSheet_5e
{
    /// <summary>
    /// Interaktionslogik für RaceSelectionPage.xaml
    /// </summary>
    public partial class RaceSelectionPage : Page
    {
        List<Button> RaceButtons;
        List<TextBlock> RaceFeatHeadings;
        List<TextBox> RaceFeatDescriptions;

        Race PreviewRace;
        Subrace SelectedSubrace;

        public delegate void OnRaceConfirm();
        public OnRaceConfirm onRaceConfirm;

        public delegate void OnNewRaceSelected();
        public OnNewRaceSelected onNewRaceSelected;

        public RaceSelectionPage()
        {
            InitializeComponent();

            PreviewRace = new Race();
            SelectedSubrace = new Subrace();
            Init_RaceButtons();
            Init_RaceFeatUI();
        }

        private void Init_RaceButtons()
        {
            RaceButtons = new List<Button>();

            Generate_RaceButtons();

        }

        private void Generate_RaceButtons()
        {
            foreach(Race race in SheetManager.CS_Manager_Inst.CharacterRaces)
            {
                Generate_RaceButton(race.RaceName);
            }
        }

        private void Generate_RaceButton(string raceName)
        {
            Button RaceButton = new Button();

            RaceButton.Width = 150;
            RaceButton.Height = 25;

            Thickness RaceBtnMargin = RaceButton.Margin;

            RaceBtnMargin.Top = 10;
            RaceBtnMargin.Bottom = 10;
            RaceBtnMargin.Left = 95;

            RaceButton.Margin = RaceBtnMargin;

            RaceButton.HorizontalAlignment = HorizontalAlignment.Left;

            RaceButton.FontWeight = FontWeights.Bold;
            RaceButton.FontSize = 16;

            RaceButton.Content = raceName;

            RaceButton.Click += RaceButton_Click;

            RaceButtons.Add(RaceButton);
            RaceSelection_Panel.Children.Add(RaceButton);

        }

        private void Init_RaceFeatUI()
        {
            RaceFeatHeadings = new List<TextBlock>();
            RaceFeatDescriptions = new List<TextBox>();

            foreach(TextBlock textBlock in RaceFeatHeadings_Panel.Children)
            {
                RaceFeatHeadings.Add(textBlock);
            }

            foreach(TextBox textBox in RaceFeatDescriptions_Panel.Children)
            {
                RaceFeatDescriptions.Add(textBox);
            }
        }

        private void RaceButton_Click(object sender, RoutedEventArgs e)
        {
            if(onNewRaceSelected != null)
            {
                onNewRaceSelected.Invoke();
            }

            Button tempButton = (Button)e.Source;
            Get_RaceFromDB(tempButton.Content.ToString());
            
            ClearPreviewBoxes();
            Show_RaceFeats();
            
            CheckSubraceStatus();
        }

        private void Get_RaceFromDB(string raceName)
        {
            PreviewRace = SheetManager.CS_Manager_Inst.CharacterRaces.Find(raceToFind => raceToFind.RaceName == raceName);
            
            if(PreviewRace != null)
            {
                SheetManager.CS_Manager_Inst.CharRace.RaceBackground = PreviewRace;
                SheetManager.CS_Manager_Inst.CharGen_SetLanguages();
            }
        }

        // Remember to integrate -> Dwarves -> Speed: 'Your speed isn't reduced by wearing heavy armor.'
        // - Since Dwarves are the only Race with a remark to their base walking speed it worthwhile to integrate this in the database
        private void Show_RaceFeats()
        {            
            Set_RaceInfo();
            Generate_SubraceSelection();
        }

        private void Set_RaceInfo()
        {
            RaceNameTB.Text = PreviewRace.RaceName;

            FillIn_AbScoreIncreases();

            AgeTB.Text = PreviewRace.AgeBackground;
            AlignmentTB.Text = PreviewRace.AlignmentBackground;
            SizeTB.Text = PreviewRace.SizeBackground + " Your size is " + PreviewRace.Size + ".";
            SpeedTB.Text = "Your base walking speed is " + PreviewRace.Speed + " ft.";

            FillIn_RaceFeats();
            FillIn_Languages();
        }

        private void FillIn_AbScoreIncreases()
        {
            foreach (AbilityScoreIncrease abilityScoreIncrease in PreviewRace.AbilityScoreIncreases)
            {
                AbilityScoreIncreaseTB.Text += abilityScoreIncrease.AbilityKey + " +" + abilityScoreIncrease.AbilityBonus.ToString() + " ";
            }
        }

        // Consider making the racefeats list look more niceley, e. g. using fontweight bold only for featName

        private void FillIn_RaceFeats()
        {
            foreach (RaceFeat raceFeat in PreviewRace.RaceFeats)
            {
                RaceFeatsTB.Text += raceFeat.FeatName + ": " + raceFeat.FeatDescription + "\n";
                RaceFeatsTB.Text += "\n";                
            }
        }       
        
        private void FillIn_Languages()
        {
            foreach(string language in PreviewRace.Languages)
            {
                LanguagesTB.Text += language + "\n";
            }
        }

        public void ClearPreviewBoxes()
        {
            RaceNameTB.Clear();
            AbilityScoreIncreaseTB.Clear();
            AgeTB.Clear();
            AlignmentTB.Clear();
            SizeTB.Clear();
            SpeedTB.Clear();
            RaceFeatsTB.Clear();
            LanguagesTB.Clear();

            Clear_SubraceTextBoxes();
        }

        private void CheckSubraceStatus()
        {
            if(PreviewRace.SubracesOfRace.Length <= 1)
            {
                SheetManager.CS_Manager_Inst.CharRace.CharakterSubrace = PreviewRace.SubracesOfRace[0];                
                onRaceConfirm.Invoke();
            }

        }

        private void Generate_SubraceSelection()
        {
            Clear_Previous_SubraceSElectionButtons();
            Create_SubraceSelectionButtons();            
        }

        private void Create_SubraceSelectionButtons()
        {
            int i = 0;

            foreach (Subrace subrace in PreviewRace.SubracesOfRace)
            {
                if(!subrace.SubraceName.Contains("Variety"))
                {
                    Button SubraceButton = new Button();

                    SubraceButton.Content = subrace.SubraceName;

                    SubraceButton.Height = 30;
                    SubraceButton.Width = 110;

                    Thickness SubraceBtnMargin = new Thickness();
                    SubraceBtnMargin.Left = 10;
                    SubraceBtnMargin.Right = 10;

                    SubraceButton.Margin = SubraceBtnMargin;

                    SubraceButton.FontWeight = FontWeights.Bold;

                    ColumnDefinition columnDefinition = new ColumnDefinition();
                    columnDefinition.Width = GridLength.Auto;
                    SubRace_SelectionGrid.ColumnDefinitions.Add(columnDefinition);

                    Grid.SetColumn(SubraceButton, i);

                    SubraceButton.Click += new RoutedEventHandler(SubraceSelection_BtnClick);

                    SubRace_SelectionGrid.Children.Add(SubraceButton);

                    i++;
                }
            }
        }


        private void Clear_Previous_SubraceSElectionButtons()
        {
            if(SubRace_SelectionGrid.Children != null)
            {
                SubRace_SelectionGrid.Children.Clear();
            }
        }

        private void SubraceSelection_BtnClick(object sender, RoutedEventArgs e)
        {
            Button SelectedSubraceBtn = (Button)e.Source;

            Clear_SubraceTextBoxes();

            SelectedSubrace = Find_CorrespondingSubrace(SelectedSubraceBtn.Content.ToString());

            if(SelectedSubrace != null && !SelectedSubrace.SubraceName.Contains("Variety"))
            {
                Fill_SubracePreview(SelectedSubrace);
                SheetManager.CS_Manager_Inst.CharRace.CharakterSubrace = SelectedSubrace;
            }

            onRaceConfirm.Invoke();
        }

        private Subrace Find_CorrespondingSubrace(string subraceName)
        {          
            List<Subrace> Subraces = PreviewRace.SubracesOfRace.ToList<Subrace>();

            Subrace selectedSubrace = Subraces.Find(subraceElement => subraceElement.SubraceName == subraceName);

            return selectedSubrace;
        }

        private void Fill_SubracePreview(Subrace selectedSubrace)
        {
            SubRaceAbScoreIncreaseTB.Text = selectedSubrace.SubraceIncrease.AbilityKey + " + " + selectedSubrace.SubraceIncrease.AbilityBonus.ToString();
            
            foreach(RaceFeat raceFeat in selectedSubrace.SubraceFeats)
            {
                SubRaceFeatsTB.Text += raceFeat.FeatName + ": " + raceFeat.FeatDescription + "\n";
            }

            foreach(Proficiency proficiency in selectedSubrace.SubraceProficiencies)
            {
                SubRaceProficienciesTB.Text += proficiency.ProficiencyName + "\n";
            }

            foreach(Race_SpellAbility spellAbility in selectedSubrace.Subrace_SpellAbilities)
            {
                if(spellAbility.ClassLevel == 0)
                {
                    SubRaceSpellsTB.Text += "Cantrip: " + spellAbility.SpellName + "\n";
                }

                else
                {
                    SubRaceSpellsTB.Text += spellAbility.ClassLevel + ". Level Spell: " + spellAbility.SpellName;
                }
            }
        }

        private void Clear_SubraceTextBoxes()
        {
            SubRaceAbScoreIncreaseTB.Clear();
            SubRaceFeatsTB.Clear();
            SubRaceProficienciesTB.Clear();
            SubRaceSpellsTB.Clear();
        }       

    }

}
