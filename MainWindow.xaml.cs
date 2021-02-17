using System.Windows;

namespace DnD_CharSheet_5e
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        SheetManager sheetManager = new SheetManager();

        bool hasError = false;

        public MainWindow()
        {
            InitializeComponent();
            sheetManager.dSys.InitializeRandom();
        }
                
        private void createCharacter()
        {
            Reset_Form();            
            FirstLevel();
            Activate_Interaction();
        }

        private void createCharButton_Click(object sender, RoutedEventArgs e)
        {
            createCharacter();
        }

        private void applyCharacter()
        {
            Deactivate_Menus();
            submitCharacter();
            Deactivate_Scores_and_Calculate_Modifiers();
            Activate_IniRolls();
            Activate_AbilityChecks();
            Activate_SavingThrows();
            Activate_SkillChecks();            
        }

        private void applyCharButton_Click(object sender, RoutedEventArgs e)
        {
            if(!hasError)
            {
                applyCharacter();
            }

            else if(hasError)
            {                
                return;
            }
            
        }

        private void Reset_Form()
        {
            Refresh_CharacterMenus();
            Reset_HP_Panel();
            
            Deactivate_IniRolls();
            Reset_IniPanel();            
            
            Deactivate_AbilityChecks();
            Reset_abilityPanel();

            Deactivate_SaveCheck_Buttons();
            Clear_SavingThrows();

            Deactivate_SkillCheck_Buttons();
            Clear_Skills();            
        }

        public void FirstLevel()
        {
            sheetManager.character.Set_charLvl(1);
            LevelText.Text = sheetManager.character.Get_charLvl().ToString();
            Level_Up_bt.IsEnabled = true;
            sheetManager.character.Update_hitDice();
            HDtext.Text = sheetManager.character.Get_hitDice().ToString();
            ProfBonus_Box.Text = sheetManager.character.Get_ProfBonus().ToString();
        }

        private void Activate_Interaction()
        {
            Activate_CharacterMenus();
            Activate_HP_Panel();
            Activate_ACandSpeed_Boxes();
            Activate_Scores();
            Activate_SaveProf_Buttons();
            Activate_SkillProficiency_Buttons();            
        }

        private void Refresh_CharacterMenus()
        {
            CharNameText.Clear();        
            PlayerNameText.Clear();  
            
            AlignmentBox.SelectedIndex = 0;           
            BackgroundBox.SelectedIndex = 0;

            LevelText.Clear();
            
            RaceMenu.SelectedIndex = 0;           
            SubRaceMenu.SelectedIndex = 0;
            
            ClassMenu.SelectedIndex = 0;            
        }

        private void Activate_CharacterMenus()
        {
            CharNameText.IsEnabled = true;
            PlayerNameText.IsEnabled = true;
            AlignmentBox.IsEnabled = true;
            BackgroundBox.IsEnabled = true;
            RaceMenu.IsEnabled = true;
            SubRaceMenu.IsEnabled = true;
            ClassMenu.IsEnabled = true;
        }

        private void Deactivate_Menus()
        {
            CharNameText.IsEnabled = false;
            PlayerNameText.IsEnabled = false;

            AlignmentBox.IsEnabled = false;
            BackgroundBox.IsEnabled = false;

            RaceMenu.IsEnabled = false;
            SubRaceMenu.IsEnabled = false;
            ClassMenu.IsEnabled = false;
        }

        private void submitCharacter()
        {
            sheetManager.character.Set_playerName(PlayerNameText.Text);
            sheetManager.character.Set_charName(CharNameText.Text);
            sheetManager.character.Set_Race(RaceMenu.Text);
            sheetManager.character.Set_Subrace(SubRaceMenu.Text);
            sheetManager.character.Set_charClass(ClassMenu.Text);
            sheetManager.character.Set_Alignment(AlignmentBox.Text);
            sheetManager.character.Set_Background(BackgroundBox.Text);
        }        

        private void Activate_HP_Panel()
        {
            maxHPtext.IsEnabled = true;
            HPtext.IsEnabled = true;
            tempHPtext.IsEnabled = true;
            currHDtext.IsEnabled = true;            
        }

        private void Activate_ACandSpeed_Boxes()
        {
            AC.IsEnabled = true;
            baseSpeed.IsEnabled = true;
        }

        private void Activate_Scores()
        {        
            strScoreText.IsEnabled = true;            
            dexScoreText.IsEnabled = true;            
            conScoreText.IsEnabled = true;            
            intScoreText.IsEnabled = true;            
            wisScoreText.IsEnabled = true;            
            chaScoreText.IsEnabled = true;
        }        

        private void Reset_HP_Panel()
        {
            maxHPtext.Clear();
            HPtext.Clear();
            tempHPtext.Clear();
            HDtext.Clear();
            currHDtext.Clear();
            ProfBonus_Box.Clear();
        }

        private void Reset_IniPanel()
        {
            iniBonus.Clear();
            iniResult.Clear();
            AC.Clear();
            baseSpeed.Clear();
        }

        private void Reset_abilityPanel()
        {
            Clear_abilityScores();
            Clear_abilityModifiers();
            Clear_abilityResults();
        }

        private void Clear_abilityScores()
        {
            strScoreText.Clear();
            dexScoreText.Clear();
            conScoreText.Clear();
            intScoreText.Clear();
            wisScoreText.Clear();
            chaScoreText.Clear();
        }

        private void Clear_abilityModifiers()
        {
            strModifierText.Clear();
            dexModifierText.Clear();
            conModifierText.Clear();
            intModifierText.Clear();
            wisModifierText.Clear();
            chaModifierText.Clear();
        }

        private void Clear_abilityResults()
        {
            strengthResult.Clear();
            dexResult.Clear();
            conResult.Clear();
            intResult.Clear();
            wisResult.Clear();
            chaResult.Clear();
        }

        private void Deactivate_Scores_and_Calculate_Modifiers()
        {
            if (checkValue(maxHPtext.Text))
            {
                maxHPtext.IsEnabled = false;
                hasError = false;
                sheetManager.character.Set_maxHP_byText(maxHPtext.Text);
            }

            else { hasError = true; }

            if (checkValue(strScoreText.Text))
            {
                strScoreText.IsEnabled = false;
                hasError = false;
                sheetManager.character.Set_strValue_byText(strScoreText.Text);
                sheetManager.character.Set_strModifier(sheetManager.character.Get_strValue());
                strModifierText.Text = sheetManager.character.Get_strModifier().ToString();
            }

            else { hasError = true; }

            if (checkValue(dexScoreText.Text))
            {
                dexScoreText.IsEnabled = false;
                hasError = false;
                sheetManager.character.Set_dexValue_byText(dexScoreText.Text);
                sheetManager.character.Set_dexModifier(sheetManager.character.Get_dexValue());
                dexModifierText.Text = sheetManager.character.Get_dexModifier().ToString();
            }

            else { hasError = true; }

            if (checkValue(conScoreText.Text))
            {
                conScoreText.IsEnabled = false;
                hasError = false;
                sheetManager.character.Set_conValue_byText(conScoreText.Text);
                sheetManager.character.Set_conModifier(sheetManager.character.Get_conValue());
                conModifierText.Text = sheetManager.character.Get_conModifier().ToString();
            }

            else { hasError = true; }

            if (checkValue(intScoreText.Text))
            {
                intScoreText.IsEnabled = false;
                hasError = false;
                sheetManager.character.Set_intValue_byText(intScoreText.Text);
                sheetManager.character.Set_intModifier(sheetManager.character.Get_intValue());
                intModifierText.Text = sheetManager.character.Get_intModifier().ToString();
            }


            else { hasError = true; }

            if (checkValue(wisScoreText.Text))
            {
                wisScoreText.IsEnabled = false;
                hasError = false;
                sheetManager.character.Set_wisValue_byText(wisScoreText.Text);
                sheetManager.character.Set_wisModifier(sheetManager.character.Get_wisValue());
                wisModifierText.Text = sheetManager.character.Get_wisModifier().ToString();
            }

            else { hasError = true; }

            if (checkValue(chaScoreText.Text))
            {
                chaScoreText.IsEnabled = false;
                hasError = false;
                sheetManager.character.Set_chaValue_byText(chaScoreText.Text);
                sheetManager.character.Set_chaModifier(sheetManager.character.Get_chaValue());
                chaModifierText.Text = sheetManager.character.Get_chaModifier().ToString();
            }

            else { hasError = true; }
            
        }        

        private bool checkValue(string information)
        {
            int number;

            if(int.TryParse(information, out number))
            {
                return true;
            }

            // -> Solve issue that Message Box either appears for every error or not at all
            else
            {
                MessageBox.Show($"A Value you have entered in one of the number-fields is invalid. Please make sure all values are integers (numbers without decimals).");
                return false;
            }
        }

        private void Activate_IniRolls()
        {
            sheetManager.character.Set_iniBonus();
            iniBonus.Text = sheetManager.character.Get_iniBonus().ToString();
            iniButton.IsEnabled = true;
        }

        private void Deactivate_IniRolls()
        {
            iniButton.IsEnabled = false;
        }

        private void Activate_AbilityChecks()
        {
            strRoll_bt.IsEnabled = true;
            dexRoll_bt.IsEnabled = true;
            conRoll_bt.IsEnabled = true;
            intRoll_bt.IsEnabled = true;
            wisRoll_bt.IsEnabled = true;
            chaRoll_bt.IsEnabled = true;
        }

        private void Deactivate_AbilityChecks()
        {
            strRoll_bt.IsEnabled = false;
            dexRoll_bt.IsEnabled = false;
            conRoll_bt.IsEnabled = false;
            intRoll_bt.IsEnabled = false;
            wisRoll_bt.IsEnabled = false;
            chaRoll_bt.IsEnabled = false;
        }        

        private void Activate_SaveProf_Buttons()
        {
            saveProf_STR.IsEnabled = true;
            saveProf_DEX.IsEnabled = true;
            saveProf_CON.IsEnabled = true;
            saveProf_INT.IsEnabled = true;
            saveProf_WIS.IsEnabled = true;
            saveProf_CHA.IsEnabled = true;
        }

        private void Deactivate_SaveProf_Buttons()
        {
            saveProf_STR.IsEnabled = false;
            saveProf_DEX.IsEnabled = false;
            saveProf_CON.IsEnabled = false;
            saveProf_INT.IsEnabled = false;
            saveProf_WIS.IsEnabled = false;
            saveProf_CHA.IsEnabled = false;
        }

        private void Activate_SaveCheck_Buttons()
        {
            STRsave_roll.IsEnabled = true;
            DEXsave_roll.IsEnabled = true;
            CONsave_roll.IsEnabled = true;
            INTsave_roll.IsEnabled = true;
            WISsave_roll.IsEnabled = true;
            CHAsave_roll.IsEnabled = true;
        }

        private void Deactivate_SaveCheck_Buttons()
        {
            STRsave_roll.IsEnabled = false;
            DEXsave_roll.IsEnabled = false;
            CONsave_roll.IsEnabled = false;
            INTsave_roll.IsEnabled = false;
            WISsave_roll.IsEnabled = false;
            CHAsave_roll.IsEnabled = false;
        }

        private void Clear_SavingThrows()
        {
            saveProf_STR.IsChecked = false;
            STRsave_Val.Clear();
            STRsave_Result.Clear();

            saveProf_DEX.IsChecked = false;
            DEXsave_Val.Clear();
            DEXsave_Result.Clear();

            saveProf_CON.IsChecked = false;
            CONsave_Val.Clear();
            CONsave_Result.Clear();

            saveProf_INT.IsChecked = false;
            INTsave_Val.Clear();
            INTsave_Result.Clear();

            saveProf_WIS.IsChecked = false;
            WISsave_Val.Clear();
            WISsave_Result.Clear();

            saveProf_CHA.IsChecked = false;
            CHAsave_Val.Clear();
            CHAsave_Val.Clear();
        }

        private void Transfer_SavingThrows()
        {
            sheetManager.character.Set_SaveBaseValues();
            sheetManager.character.Set_SaveProficiencies(saveProf_STR.IsChecked.Value, saveProf_DEX.IsChecked.Value, saveProf_CON.IsChecked.Value, saveProf_INT.IsChecked.Value, saveProf_WIS.IsChecked.Value, saveProf_CHA.IsChecked.Value);
        }

        private void Calculate_SaveModifiers()
        {
            sheetManager.character.Calculate_SaveModifiers();
        }

        private void Show_SaveModifiers()
        {
            STRsave_Val.Text = sheetManager.character.Get_STR_Save().ToString();
            DEXsave_Val.Text = sheetManager.character.Get_DEX_Save().ToString();
            CONsave_Val.Text = sheetManager.character.Get_CON_Save().ToString();
            WISsave_Val.Text = sheetManager.character.Get_WIS_Save().ToString();
            INTsave_Val.Text = sheetManager.character.Get_INT_Save().ToString();
            CHAsave_Val.Text = sheetManager.character.Get_CHA_Save().ToString();
        }

        private void Activate_SavingThrows()
        {
            Deactivate_SaveProf_Buttons();
            Transfer_SavingThrows();
            Calculate_SaveModifiers();
            Show_SaveModifiers();
            Activate_SaveCheck_Buttons();
        }

        private void Activate_SkillChecks()
        {
            Deactivate_SkillProficiency_Buttons();
            Transfer_Skill_Values();
            Calculate_SkillModifiers();
            Show_SkillModifiers();
            Activate_SkillCheck_Buttons();
        }

        private void Transfer_Skill_Values()
        {
            sheetManager.character.Set_SkillBaseValues();
            Set_SkillProficiencies();
        }

        private void Set_SkillProficiencies()
        {
            sheetManager.character.Set_Proficiencies_strSkills(AthleticsProf.IsChecked.Value);
            sheetManager.character.Set_Proficiencies_dexSkills(AcrobaticsProf.IsChecked.Value, SleightOfHandProf.IsChecked.Value, StealthProf.IsChecked.Value);
            sheetManager.character.Set_Proficiencies_intSkills(ArcanaProf.IsChecked.Value, HistoryProf.IsChecked.Value, InvestigationProf.IsChecked.Value, NatureProf.IsChecked.Value, ReligionProf.IsChecked.Value);
            sheetManager.character.Set_Proficiencies_wisSkills(AnimalHandlingProf.IsChecked.Value, InsightProf.IsChecked.Value, MedicineProf.IsChecked.Value, PerceptionProf.IsChecked.Value, SurvivalProf.IsChecked.Value);
            sheetManager.character.Set_Proficiencies_chaSkills(DeceptionProf.IsChecked.Value, IntimidationProf.IsChecked.Value, PerfomanceProf.IsChecked.Value, PersuasionProf.IsChecked.Value);
        }

        private void Calculate_SkillModifiers()
        {
            sheetManager.character.Calculate_SkillModifiers();
        }

        private void Activate_SkillProficiency_Buttons()
        {
            AcrobaticsProf.IsEnabled = true;
            AnimalHandlingProf.IsEnabled = true;
            ArcanaProf.IsEnabled = true;
            AthleticsProf.IsEnabled = true;
            DeceptionProf.IsEnabled = true;
            HistoryProf.IsEnabled = true;
            InsightProf.IsEnabled = true;
            IntimidationProf.IsEnabled = true;
            InvestigationProf.IsEnabled = true;
            MedicineProf.IsEnabled = true;
            NatureProf.IsEnabled = true;
            PerceptionProf.IsEnabled = true;
            PerfomanceProf.IsEnabled = true;
            PersuasionProf.IsEnabled = true;
            ReligionProf.IsEnabled = true;
            SleightOfHandProf.IsEnabled = true;
            StealthProf.IsEnabled = true;
            SurvivalProf.IsEnabled = true;
        }

        private void Deactivate_SkillProficiency_Buttons()
        {
            AcrobaticsProf.IsEnabled = false;
            AnimalHandlingProf.IsEnabled = false;
            ArcanaProf.IsEnabled = false;
            AthleticsProf.IsEnabled = false;
            DeceptionProf.IsEnabled = false;
            HistoryProf.IsEnabled = false;
            InsightProf.IsEnabled = false;
            IntimidationProf.IsEnabled = false;
            InvestigationProf.IsEnabled = false;
            MedicineProf.IsEnabled = false;
            NatureProf.IsEnabled = false;
            PerceptionProf.IsEnabled = false;
            PerfomanceProf.IsEnabled = false;
            PersuasionProf.IsEnabled = false;
            ReligionProf.IsEnabled = false;
            SleightOfHandProf.IsEnabled = false;
            StealthProf.IsEnabled = false;
            SurvivalProf.IsEnabled = false;
        }       

        private void Show_SkillModifiers()
        {
            AcrobaticsTxt.Text = sheetManager.character.Get_Acrobatics().ToString();
            AnimalHandlingTxt.Text = sheetManager.character.Get_AnimalHandling().ToString();
            ArcanaTxt.Text = sheetManager.character.Get_Arcana().ToString();
            AthleticsTxt.Text = sheetManager.character.Get_Athletics().ToString();

            DeceptionTxt.Text = sheetManager.character.Get_Deception().ToString();

            HistoryTxt.Text = sheetManager.character.Get_History().ToString();
            InsightTxt.Text = sheetManager.character.Get_Insight().ToString();
            IntimidationTxt.Text = sheetManager.character.Get_Intimidation().ToString();
            InvestigationTxt.Text = sheetManager.character.Get_Investigation().ToString();

            MedicineTxt.Text = sheetManager.character.Get_Medicine().ToString();
            NatureTxt.Text = sheetManager.character.Get_Nature().ToString();

            PerceptionTxt.Text = sheetManager.character.Get_Perception().ToString();
            PerformanceTxt.Text = sheetManager.character.Get_Performance().ToString();
            PersuasionTxt.Text = sheetManager.character.Get_Persuasion().ToString();

            ReligionTxt.Text = sheetManager.character.Get_Religion().ToString();

            SleightOfHandTxt.Text = sheetManager.character.Get_SleightOfHand().ToString();
            StealthTxt.Text = sheetManager.character.Get_Stealth().ToString();

            SurvivalTxt.Text = sheetManager.character.Get_Survival().ToString();
        }

        private void Activate_SkillCheck_Buttons()
        {
            AcrobaticsCheck_bt.IsEnabled = true;
            AnimalHandlingCheck_bt.IsEnabled = true;
            ArcanaCheck_bt.IsEnabled = true;
            AthleticsCheck_bt.IsEnabled = true;

            DeceptionCheck_bt.IsEnabled = true;

            HistoryCheck_bt.IsEnabled = true;

            InsightCheck_bt.IsEnabled = true;
            IntimidationCheck_bt.IsEnabled = true;
            InvestigationCheck_bt.IsEnabled = true;

            MedicineCheck_bt.IsEnabled = true;
            NatureCheck_bt.IsEnabled = true;

            PerceptionCheck_bt.IsEnabled = true;
            PerfomanceCheck_bt.IsEnabled = true;
            PersuasionCheck_bt.IsEnabled = true;

            ReligionCheck_bt.IsEnabled = true;

            SleightOfHandCheck_bt.IsEnabled = true;
            StealthCheck_bt.IsEnabled = true;

            SurvivalCheck_bt.IsEnabled = true;
        }

        private void Deactivate_SkillCheck_Buttons()
        {
            AcrobaticsCheck_bt.IsEnabled = false;
            AnimalHandlingCheck_bt.IsEnabled = false;
            ArcanaCheck_bt.IsEnabled = false;
            AthleticsCheck_bt.IsEnabled = false;

            DeceptionCheck_bt.IsEnabled = false;

            HistoryCheck_bt.IsEnabled = false;

            InsightCheck_bt.IsEnabled = false;
            IntimidationCheck_bt.IsEnabled = false;
            InvestigationCheck_bt.IsEnabled = false;

            MedicineCheck_bt.IsEnabled = false;
            NatureCheck_bt.IsEnabled = false;

            PerceptionCheck_bt.IsEnabled = false;
            PerfomanceCheck_bt.IsEnabled = false;
            PersuasionCheck_bt.IsEnabled = false;

            ReligionCheck_bt.IsEnabled = false;

            SleightOfHandCheck_bt.IsEnabled = false;
            StealthCheck_bt.IsEnabled = false;

            SurvivalCheck_bt.IsEnabled = false;
        }

        private void Clear_Skills()
        {
            AcrobaticsProf.IsChecked = false;
            AcrobaticsTxt.Clear();
            Acrobatics_Result.Clear();

            AnimalHandlingProf.IsChecked = false;
            AnimalHandlingTxt.Clear();
            AnimalHandling_Result.Clear();

            ArcanaProf.IsChecked = false;
            ArcanaTxt.Clear();
            Arcana_Result.Clear();

            AthleticsProf.IsChecked = false;
            AthleticsTxt.Clear();
            Athletics_Result.Clear();

            DeceptionProf.IsChecked = false;
            DeceptionTxt.Clear();
            Deception_Result.Clear();

            HistoryProf.IsChecked = false;
            HistoryTxt.Clear();
            History_Result.Clear();

            InsightProf.IsChecked = false;
            InsightTxt.Clear();
            Insight_Result.Clear();

            IntimidationProf.IsChecked = false;
            IntimidationTxt.Clear();
            Intimidation_Result.Clear();

            InvestigationProf.IsChecked = false;
            InvestigationTxt.Clear();
            Investigation_Result.Clear();

            MedicineProf.IsChecked = false;
            MedicineTxt.Clear();
            Medicine_Result.Clear();

            NatureProf.IsChecked = false;
            NatureTxt.Clear();
            Nature_Result.Clear();

            PerceptionProf.IsChecked = false;
            PerceptionTxt.Clear();
            Perception_Result.Clear();

            PerfomanceProf.IsChecked = false;
            PerformanceTxt.Clear();
            Performance_Result.Clear();

            PersuasionProf.IsChecked = false;
            PersuasionTxt.Clear();
            Persuasion_Result.Clear();

            ReligionProf.IsChecked = false;
            ReligionTxt.Clear();
            Religion_Result.Clear();

            SleightOfHandProf.IsChecked = false;
            SleightOfHandTxt.Clear();
            SleightOfHand_Result.Clear();

            StealthProf.IsChecked = false;
            StealthTxt.Clear();
            Stealth_Result.Clear();

            SurvivalProf.IsChecked = false;
            SurvivalTxt.Clear();
            Survival_Result.Clear();

        }        

        private void LevelUpButton_Click(object sender, RoutedEventArgs e)
        {
            sheetManager.character.Level_Up();
            LevelText.Text = sheetManager.character.Get_charLvl().ToString();            
            HDtext.Text = sheetManager.character.Get_hitDice().ToString();
            ProfBonus_Box.Text = sheetManager.character.Get_ProfBonus().ToString();
        }

        private void BackgroundPageButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(sheetManager.character.Get_charName() + $" is " + sheetManager.character.Get_playName() + $"'s character." +
                sheetManager.character.Get_playName() + $" chose a(n) " + sheetManager.character.Get_Race() + $" for Race, " + sheetManager.character.Get_Subrace()
                + $" as subrace.\n" + sheetManager.character.Get_charName() + $" is of the " + sheetManager.character.Get_charClass() + $"-Class and has the " + sheetManager.character.Get_Background()
                + $"-Background.\n" + sheetManager.character.Get_charName() + $" is of " + sheetManager.character.Get_Alignment() + $" alignment.");
        }

        private void IniButton_Click(object sender, RoutedEventArgs e)
        {
            iniResult.Text = sheetManager.Roll_for_Initiative().ToString();
        }

        public void strButton_Click(object sender, RoutedEventArgs e)
        {
            strengthResult.Text = sheetManager.Ability_Check(sheetManager.character.Get_strModifier()).ToString();
        }

        public void dexButton_Click(object sender, RoutedEventArgs e)
        {
            dexResult.Text = sheetManager.Ability_Check(sheetManager.character.Get_dexModifier()).ToString();
        }

        public void conButton_Click(object sender, RoutedEventArgs e)
        {
            conResult.Text = sheetManager.Ability_Check(sheetManager.character.Get_conModifier()).ToString();
        }

        public void intButton_Click(object sender, RoutedEventArgs e)
        {
            intResult.Text = sheetManager.Ability_Check(sheetManager.character.Get_intModifier()).ToString();
        }

        public void wisButton_Click(object sender, RoutedEventArgs e)
        {
            wisResult.Text = sheetManager.Ability_Check(sheetManager.character.Get_wisModifier()).ToString();
        }

        public void chaButton_Click(object sender, RoutedEventArgs e)
        {
            chaResult.Text = sheetManager.Ability_Check(sheetManager.character.Get_chaModifier()).ToString();
        }

        public void STR_Save_bt_Click(object sender, RoutedEventArgs e)
        {
            STRsave_Result.Text = sheetManager.SavingThrow(sheetManager.character.Get_STR_Save()).ToString();
        }

        public void DEX_Save_bt_Click(object sender, RoutedEventArgs e)
        {
            DEXsave_Result.Text = sheetManager.SavingThrow(sheetManager.character.Get_DEX_Save()).ToString();
        }

        public void CON_Save_bt_Click(object sender, RoutedEventArgs e)
        {
            CONsave_Result.Text = sheetManager.SavingThrow(sheetManager.character.Get_CON_Save()).ToString();
        }

        public void INT_Save_bt_Click(object sender, RoutedEventArgs e)
        {
            INTsave_Result.Text = sheetManager.SavingThrow(sheetManager.character.Get_INT_Save()).ToString();
        }

        public void WIS_Save_bt_Click(object sender, RoutedEventArgs e)
        {
            WISsave_Result.Text = sheetManager.SavingThrow(sheetManager.character.Get_WIS_Save()).ToString();
        }

        public void CHA_Save_bt_Click(object sender, RoutedEventArgs e)
        {
            CHAsave_Result.Text = sheetManager.SavingThrow(sheetManager.character.Get_CHA_Save()).ToString();
        }

        public void acrobaticsBT_Click(object sender, RoutedEventArgs e)
        {
            Acrobatics_Result.Text = sheetManager.Skill_Check(sheetManager.character.Get_Acrobatics()).ToString();
        }

        public void animalHandlingBT_Click(object sender, RoutedEventArgs e)
        {
            AnimalHandling_Result.Text = sheetManager.Skill_Check(sheetManager.character.Get_AnimalHandling()).ToString();
        }

        public void arcanaBT_Click(object sender, RoutedEventArgs e)
        {
            Arcana_Result.Text = sheetManager.Skill_Check(sheetManager.character.Get_Arcana()).ToString();
        }

        public void athleticsBT_Click(object sender, RoutedEventArgs e)
        {
            Athletics_Result.Text = sheetManager.Skill_Check(sheetManager.character.Get_Athletics()).ToString();
        }

        public void deceptionBT_Click(object sender, RoutedEventArgs e)
        {
            Deception_Result.Text = sheetManager.Skill_Check(sheetManager.character.Get_Deception()).ToString();
        }

        public void historyBT_Click(object sender, RoutedEventArgs e)
        {
            History_Result.Text = sheetManager.Skill_Check(sheetManager.character.Get_History()).ToString();
        }

        public void insightBT_Click(object sender, RoutedEventArgs e)
        {
            Insight_Result.Text = sheetManager.Skill_Check(sheetManager.character.Get_Insight()).ToString();
        }

        public void intimidationBT_Click(object sender, RoutedEventArgs e)
        {
            Intimidation_Result.Text = sheetManager.Skill_Check(sheetManager.character.Get_Intimidation()).ToString();
        }

        public void investigationBT_Click(object sender, RoutedEventArgs e)
        {
            Investigation_Result.Text = sheetManager.Skill_Check(sheetManager.character.Get_Investigation()).ToString();
        }

        public void medicineBT_Click(object sender, RoutedEventArgs e)
        {
            Medicine_Result.Text = sheetManager.Skill_Check(sheetManager.character.Get_Medicine()).ToString();
        }

        public void natureBT_Click(object sender, RoutedEventArgs e)
        {
            Nature_Result.Text = sheetManager.Skill_Check(sheetManager.character.Get_Nature()).ToString();
        }

        public void perceptionBT_Click(object sender, RoutedEventArgs e)
        {
            Perception_Result.Text = sheetManager.Skill_Check(sheetManager.character.Get_Perception()).ToString();
        }

        public void performanceBT_Click(object sender, RoutedEventArgs e)
        {
            Performance_Result.Text = sheetManager.Skill_Check(sheetManager.character.Get_Performance()).ToString();
        }

        public void persuasionBT_Click(object sender, RoutedEventArgs e)
        {
            Persuasion_Result.Text = sheetManager.Skill_Check(sheetManager.character.Get_Persuasion()).ToString();
        }

        public void religionBT_Click(object sender, RoutedEventArgs e)
        {
            Religion_Result.Text = sheetManager.Skill_Check(sheetManager.character.Get_Religion()).ToString();
        }

        public void sleightOfHandBT_Click(object sender, RoutedEventArgs e)
        {
            SleightOfHand_Result.Text = sheetManager.Skill_Check(sheetManager.character.Get_SleightOfHand()).ToString();
        }

        public void stealthBT_Click(object sender, RoutedEventArgs e)
        {
            Stealth_Result.Text = sheetManager.Skill_Check(sheetManager.character.Get_Stealth()).ToString();
        }

        public void survivalBT_Click(object sender, RoutedEventArgs e)
        {
            Survival_Result.Text = sheetManager.Skill_Check(sheetManager.character.Get_Survival()).ToString();
        }

        public void CombatWindow_bt_Click(object sender, RoutedEventArgs e)
        {
            CombatWindow combatWindow = new CombatWindow();
            combatWindow.Set_SheetManager(sheetManager);
            combatWindow.Show();
        }

        public void SaveScreen_bt_Click(object sender, RoutedEventArgs e)
        {
            //SaveScreen saveScreenWindow = new SaveScreen();
            //saveScreenWindow.Show_FilePath();
            //saveScreenWindow.Fetch_Character(sheetManager.Get_Character());
            //saveScreenWindow.Show();

            MainPanel.Visibility = Visibility.Collapsed;
            MainFrame.Content = new SavePage();

            
        }
    }
}
