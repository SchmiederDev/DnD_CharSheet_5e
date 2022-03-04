using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DnD_CharSheet_5e
{
    /// <summary>
    /// Interaktionslogik für AbilityStatsPage.xaml
    /// </summary>
    public partial class AbilityStatsPage : Page
    {
        List<TextBox> AbilityScoreBoxes;

        List<Ellipse> BulletPoints;

        List<TextBlock> RacesInfo_Txts;
        List<TextBlock> BonusInfo_Txts;

        int[] tempAbilityScores = new int[6];

        int[] standardScores = new int[6] { 15, 14, 13, 12, 10, 8 };

        bool dropIsAllowed = false;

        int selectedScoreValue = 0;
        int targetScoreValue = 0;

        int sourceIndex = 0;
        int targetIndex = 0;

        string sourceName;
        string targetName;

        Race RaceSelected = new Race();
        Subrace SubraceSelected = new Subrace();

        //Point MouseStartPoint = new Point();

        public delegate void OnStatsConfirmed();
        public OnStatsConfirmed onStatsConfirmed;

        public AbilityStatsPage()
        {            
            InitializeComponent();
            Init_BulletPoints();
            Init_RacePreview_DropDown();
            Set_AbiliyScoreBoxes();
            Init_AbilityBonusPanel();
        }

        private void Set_AbiliyScoreBoxes()
        {
            AbilityScoreBoxes = new List<TextBox>();

            AbilityScoreBoxes.Add(STR_ScoreTB);
            AbilityScoreBoxes.Add(DEX_ScoreTB);
            AbilityScoreBoxes.Add(CON_ScoreTB);

            AbilityScoreBoxes.Add(INT_ScoreTB);
            AbilityScoreBoxes.Add(WIS_ScoreTB);
            AbilityScoreBoxes.Add(CHA_ScoreTB);
        }

        private void Init_AbilityBonusPanel()
        {
            RacesInfo_Txts = new List<TextBlock>();
            BonusInfo_Txts = new List<TextBlock>();            

            Generate_RaceInfo_TextBlocks();
            Generate_BonusInfo_TextBlocks();
        }

        private void Init_BulletPoints()
        {
            BulletPoints = new List<Ellipse>();

            foreach (Ellipse bulletPoint in BulletPoints_Panel.Children)
            {
                BulletPoints.Add(bulletPoint);
            }
        }

        private void Init_RacePreview_DropDown()
        {
            foreach(Race race in SheetManager.CS_Manager_Inst.CharacterRaces)
            {
                RaceSelectionCB.Items.Add(race.RaceName);
            }
        }

        private void Create_RaceInfo_TB(string raceName)
        {
            TextBlock RaceInfoTB = new TextBlock();

            Thickness thickTB = RaceInfoTB.Margin;
            thickTB.Top = 5;
            thickTB.Bottom = 5;
            RaceInfoTB.Margin = thickTB;

            RaceInfoTB.FontWeight = FontWeights.Bold;
            RaceInfoTB.FontSize = 16.00;

            RaceInfoTB.Text = raceName;

            RacesInfo_Txts.Add(RaceInfoTB);
            Races_InfoPanel.Children.Add(RaceInfoTB);
        }

        private void Create_SubraceInfo_TB(string subraceName)
        {
            TextBlock SubRaceInfoTB = new TextBlock();

            Thickness thickTB = SubRaceInfoTB.Margin;
            
            thickTB.Bottom = 5;
            thickTB.Top = 5;
            thickTB.Left = 10;
            
            SubRaceInfoTB.Margin = thickTB;

            SubRaceInfoTB.FontSize = 14.00;

            SubRaceInfoTB.Text = subraceName;

            RacesInfo_Txts.Add(SubRaceInfoTB);
            Races_InfoPanel.Children.Add(SubRaceInfoTB);
        }

        private void Create_EmptyLine()
        {
            TextBlock EmptyTB = new TextBlock();

            Thickness thickTB = EmptyTB.Margin;

            thickTB.Top = 2.25f;
            thickTB.Bottom = 2.25f;

            EmptyTB.Margin = thickTB;

            Races_InfoPanel.Children.Add(EmptyTB);

        }

        private void Generate_RaceInfo_TextBlocks()
        {
            foreach(Race race in SheetManager.CS_Manager_Inst.CharacterRaces)
            {
                Create_RaceInfo_TB(race.RaceName);

                if(race.AbilityScoreIncreases.Length > 1)
                {
                    for(int i = 0; i < race.AbilityScoreIncreases.Length; i++)
                    {
                        Create_EmptyLine();
                    }
                }

                if(race.SubracesOfRace != null)
                {
                    foreach(Subrace subrace in race.SubracesOfRace)
                    {
                        if(!subrace.SubraceName.Contains("Variety"))
                        {
                            Create_SubraceInfo_TB(subrace.SubraceName);
                        }
                    }
                }
            }
        }

        private void Create_RaceBonus_TB(uint bonus, string key)
        {
            TextBlock BonusInfoTB = new TextBlock();

            Thickness thickTB = BonusInfoTB.Margin;

            thickTB.Bottom = 5;
            thickTB.Top = 5;

            BonusInfoTB.Margin = thickTB;

            BonusInfoTB.FontWeight = FontWeights.Bold;

            BonusInfoTB.FontSize = 16.00;

            BonusInfoTB.Text = key + " +" + bonus.ToString();

            BonusInfo_Txts.Add(BonusInfoTB);
            AbilityBonus_InfoPanel.Children.Add(BonusInfoTB);

        }

        private void Create_SubRaceBonus_TB(uint bonus, string key)
        {
            TextBlock BonusInfoTB = new TextBlock();

            Thickness thickTB = BonusInfoTB.Margin;

            thickTB.Bottom = 5;
            thickTB.Top = 5;
            thickTB.Left = 10;

            BonusInfoTB.Margin = thickTB;

            BonusInfoTB.FontSize = 14.00;

            BonusInfoTB.Text = key + " +" + bonus.ToString();

            BonusInfo_Txts.Add(BonusInfoTB);
            AbilityBonus_InfoPanel.Children.Add(BonusInfoTB);

        }

        private void Generate_BonusInfo_TextBlocks()
        {
            foreach (Race race in SheetManager.CS_Manager_Inst.CharacterRaces)
            {
                foreach(AbilityScoreIncrease bonus in race.AbilityScoreIncreases)
                {
                    Create_RaceBonus_TB(bonus.AbilityBonus, bonus.AbilityKey);
                }

                if (race.SubracesOfRace != null)
                {
                    foreach (Subrace subrace in race.SubracesOfRace)
                    {
                        if(subrace.SubraceIncrease.AbilityKey != null)
                        {
                            Create_SubRaceBonus_TB(subrace.SubraceIncrease.AbilityBonus, subrace.SubraceIncrease.AbilityKey);
                        }
                    }
                }
            }
        }

        private void AbilityScores_Btn_Click(object sender, RoutedEventArgs e)
        {
            if(!Apply_AbilityScores_Btn.IsEnabled)
            {
                Apply_AbilityScores_Btn.IsEnabled = true;
            }

            if(!RaceSelectionCB.IsEnabled)
            {
                RaceSelectionCB.IsEnabled = true;
            }

            if(!dropIsAllowed)
            {
                dropIsAllowed = true;
            }

            RollScores();
            Show_AbilityScores();
        }

        private void RollScores()
        {
            for (int i = 0; i < tempAbilityScores.Length; i++)
            {
                tempAbilityScores[i] = RollAbilityStat();
            }
        }

        private int RollAbilityStat()
        {
            int abilityScore = 0;
            int[] abilityStatValues = new int[4];

            for(int i = 0; i < abilityStatValues.Length; i++)
            {
                abilityStatValues[i] = SheetManager.CS_Manager_Inst.dSys.Roll_D6();                
            }

            Array.Sort(abilityStatValues);

            abilityScore = abilityStatValues[1] + abilityStatValues[2] + abilityStatValues[3];

            return abilityScore;
        }

        private void Show_AbilityScores()
        {            

            for(int i = 0; i < tempAbilityScores.Length; i++)
            {
                AbilityScoreBoxes[i].Foreground = Brushes.SlateGray;
                AbilityScoreBoxes[i].Text = tempAbilityScores[i].ToString();
            }
        }

        private void Apply_AbilityScores_Btn_Click(object sender, RoutedEventArgs e)
        {
            SheetManager.CS_Manager_Inst.CharGenCharacter.SetAllAbilityScores(tempAbilityScores);

            onStatsConfirmed.Invoke();
        }

        private void Selection_MouseEnter(object sender, MouseEventArgs e)
        {
            Ellipse selectedEllipse = (Ellipse)e.Source;
            selectedEllipse.Fill = Brushes.DeepSkyBlue;
        }

        private void Selection_MouseLeave(object sender, MouseEventArgs e)
        {
            Ellipse selectedEllipse = (Ellipse)e.Source;
            selectedEllipse.Fill = Brushes.White;
        }

        private void BulletPoint_PreviewMouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            //MouseStartPoint = e.GetPosition(null);

            Ellipse selectedEllipse = (Ellipse)e.Source;

            Copy_AbilityScore(selectedEllipse.Name);
        }

        private void BulletPoint_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            //Point mousePos = e.GetPosition(null);
            //Vector diff = MouseStartPoint - mousePos;

            //Potentially add minimal drag distance - issue: either mouse button pressed ignored with 'OR'-Operator (WHY?) or drag distance to far with 'AND'-operator (WHY?)
            //&& Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance || Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Ellipse draggedEllipse = sender as Ellipse;

                DragDrop.DoDragDrop(draggedEllipse, draggedEllipse, DragDropEffects.Move);
            }
        }

        private void BulletPoint_DragEnter(object sender, DragEventArgs e)
        {
            Ellipse draggedEllipse = sender as Ellipse;
            draggedEllipse.Fill = Brushes.LawnGreen;
            Save_LastAbilitScore(draggedEllipse.Name);
        }

        private void BulletPoint_DragLeave(object sender, DragEventArgs e)
        {
            Ellipse draggedEllipse = sender as Ellipse;
            draggedEllipse.Fill = Brushes.White;
        }

        private void Selection_Drop(object sender, DragEventArgs e)
        {            
            if(dropIsAllowed)
            {
                Switch_AbilityScore(sourceIndex, targetIndex);
                Convert_ElementNames_and_Show_ScoreValues(sourceName, targetName);
            }
        }


        private void Copy_AbilityScore(string abilityName)
        {           
            for(int i = 0; i < BulletPoints.Count; i++)
            {
                if(abilityName == BulletPoints[i].Name)
                {
                    if(i >= 0 && i < tempAbilityScores.Length && tempAbilityScores[i] != 0)
                    {
                        selectedScoreValue = tempAbilityScores[i];
                        sourceIndex = i;
                        sourceName = abilityName;
                    }
                }
            }
        }

        private void Save_LastAbilitScore(string abilityName)
        {
            for(int i = 0; i < BulletPoints.Count; i++)
            {
                if(abilityName == BulletPoints[i].Name)
                {
                    if (i >= 0 && i < tempAbilityScores.Length && tempAbilityScores[i] != 0)
                    {
                        targetScoreValue = tempAbilityScores[i];
                        targetIndex = i;
                        targetName = abilityName;
                    }
                }
            }
        }

        private void Switch_AbilityScore(int sIndex, int tIndex)
        {
            tempAbilityScores[sIndex] = targetScoreValue;
            tempAbilityScores[tIndex] = selectedScoreValue;
        }

        private void Convert_ElementNames_and_Show_ScoreValues(string sName, string tName)
        {
            string convertedSourceName = sName.Replace("Selection", "ScoreTB");
            string converetdTargetName = tName.Replace("Selection", "ScoreTB");                       

            int indexOfSource = Find_TextBoxIndex(convertedSourceName);
            int indexOfTarget = Find_TextBoxIndex(converetdTargetName);            

            AbilityScoreBoxes[indexOfSource].Text = tempAbilityScores[indexOfSource].ToString();
            AbilityScoreBoxes[indexOfTarget].Text = tempAbilityScores[indexOfTarget].ToString();
        }

        private int Find_TextBoxIndex(string tbName)
        {
            int indexOfAbilityScore = AbilityScoreBoxes.FindIndex(element => element.Name == tbName);
            return indexOfAbilityScore;
        }

        private void StandardScores_Btn_Click(object sender, RoutedEventArgs e)
        {
            for(int i = 0; i < tempAbilityScores.Length; i++)
            {
                tempAbilityScores[i] = standardScores[i];
            }

            Show_AbilityScores();
        }

        private void ApplyRace_Btn_Click(object sender, RoutedEventArgs e)
        {  
            Race_AbilityScoreIncrease_Preview();
            Subrace_AbilityIncrease_Preview();            
        }

        private void Race_AbilityScoreIncrease_Preview()
        {
            foreach (TextBox abilityScoreBox in AbilityScoreBoxes)
            {
                for (int i = 0; i < RaceSelected.AbilityScoreIncreases.Length; i++)
                {
                    string convertedBoxName = RaceSelected.AbilityScoreIncreases[i].AbilityKey + "_ScoreTB";

                    if (abilityScoreBox.Name == convertedBoxName)
                    {
                        int indexOfBox = Find_TextBoxIndex(abilityScoreBox.Name);
                        int tempScore = tempAbilityScores[indexOfBox] + (int)RaceSelected.AbilityScoreIncreases[i].AbilityBonus;
                        tempAbilityScores[indexOfBox] = tempScore;
                        abilityScoreBox.Foreground = Brushes.Coral;
                        abilityScoreBox.Text = tempScore.ToString();
                    }
                }
            }
        }

        private void Subrace_AbilityIncrease_Preview()
        {
            if (SubraceSelected != null)
            {
                string boxName = SubraceSelected.SubraceIncrease.AbilityKey + "_ScoreTB";
                int indexOfBox = Find_TextBoxIndex(boxName);

                if(indexOfBox >= 0 && indexOfBox < AbilityScoreBoxes.Count)
                {
                    int tempScore = tempAbilityScores[indexOfBox] + (int)SubraceSelected.SubraceIncrease.AbilityBonus;
                    tempAbilityScores[indexOfBox] = tempScore;
                    AbilityScoreBoxes[indexOfBox].Foreground = Brushes.Coral;
                    AbilityScoreBoxes[indexOfBox].Text = tempScore.ToString();
                }
               
            }
        }       

        private void RaceSelectionCB_DropDownClosed(object sender, EventArgs e)
        {
            Show_AbilityScores();

            if(RaceSelectionCB.SelectedItem != null)
            {                
                RaceSelected = SheetManager.CS_Manager_Inst.CharacterRaces.Find(element => element.RaceName == RaceSelectionCB.SelectedItem.ToString());

                if(RaceSelected != null)
                {
                    if(RaceSelected.SubracesOfRace.Length <= 1)
                    {
                        Disable_SubraceSelection();
                    }

                    else
                    {
                        Enable_SubraceSelection();
                    }
                }
            }
           
        }

        private void Init_SubraceCB()
        {
            foreach(Subrace subrace in RaceSelected.SubracesOfRace)
            {
                SubraceSelectionCB.Items.Add(subrace.SubraceName);
            }
        }

        private void Enable_SubraceSelection()
        {
            SubraceSelectionCB.Items.Clear();
            ApplyRace_Btn.IsEnabled = false;
            Init_SubraceCB();
            SubraceSelectionCB.Visibility = Visibility.Visible;
            SubraceSelectionCB.IsEnabled = true;
        }

        private void Disable_SubraceSelection()
        {
            SubraceSelected = null;
            SubraceSelectionCB.Visibility = Visibility.Collapsed;
            SubraceSelectionCB.IsEnabled = false;
            SubraceSelectionCB.Items.Clear();
            ApplyRace_Btn.IsEnabled = true;
        }

        
        private void SubraceSelectionCB_DropDownClosed(object sender, EventArgs e)
        {
            if(!ApplyRace_Btn.IsEnabled)
            {
                ApplyRace_Btn.IsEnabled = true;
                
            }

            Select_Subrace();
        }

        private void Select_Subrace()
        {
            SubraceSelected = RaceSelected.SubracesOfRace[Find_IndexOf_SelectedSubrace()];
        }

        private int Find_IndexOf_SelectedSubrace()
        {
            List<Subrace> tempSubraceList = RaceSelected.SubracesOfRace.ToList<Subrace>();
            int subraceIndex = tempSubraceList.FindIndex(element => element.SubraceName == SubraceSelectionCB.SelectedItem.ToString());
            return subraceIndex;
        }
    }
}
