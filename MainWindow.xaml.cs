using System.Windows;

namespace DnD_CharSheet_5e
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static MainWindow mainWindow_Inst;

        SheetManager sheetManager = new SheetManager();

        bool hasError = false;
        bool IsSidebar_Active = false;

        public MainWindow()
        {
            InitializeComponent();

            if(mainWindow_Inst == null)
            {
                mainWindow_Inst = this;
            }
            
            sheetManager.dSys.InitializeRandom();

            Init_FileSystem();
            SheetManager.CS_Manager_Inst.Init_DataBases();
            Init_theWeave();
        }

        private void Init_FileSystem()
        {
            FileManager.FM_Inst.Find_RootPath();
            FileManager.FM_Inst.Check_for_SaveGameFolder();
            FileManager.FM_Inst.Set_SaveGames();
            FileManager.FM_Inst.Check_for_SoundEffects_Folder();
            FileManager.FM_Inst.Init_SoundEffects();
            FileManager.FM_Inst.Check_for_Images_Folder();
        }

        // consider to load ALL relevant databases in SheetManager instead of partially here

        private void Init_theWeave()
        {
            FileManager.FM_Inst.Set_Path_Spells_and_SpellLists();
            FileManager.FM_Inst.Read_Spells_and_SpellLists();
            SheetManager.CS_Manager_Inst.theWeave.Read_SpellDataBase(FileManager.FM_Inst.jsonSDB);
            SheetManager.CS_Manager_Inst.theWeave.Read_BardSpellList(FileManager.FM_Inst.jsonBSL);
            SheetManager.CS_Manager_Inst.theWeave.Read_WizardSpellList(FileManager.FM_Inst.jsonWSL);
        }
                
        private void CreateCharacter()
        {
            //SheetManager.CS_Manager_Inst.character = new Character();                       // Memory Issue here? (Apparently not, Destructor/ Finalizer is called)
            Reset_Form();
            
            if(IsSidebar_Active == true)
            {
                Deactivate_SideBarMenu_Buttons();
            }

            FirstLevel();
            Activate_Interaction();
        }

        private void NewCharButton_Click(object sender, RoutedEventArgs e)
        {
            CreateCharacter();
            if (ApplyButton.IsEnabled == false)
            {
                ApplyButton.IsEnabled = true;
            }
            FileManager.FM_Inst.Play_ClickSound();
        }

        private void CreateCharButton_Click(object sender, RoutedEventArgs e)
        {
            CharacterCreationWindow CharGenWdw = new CharacterCreationWindow();
            CharGenWdw.Show();
            FileManager.FM_Inst.Play_ClickSound();
        }

        private void ApplyCharacter()
        {            
            ApplyButton.IsEnabled = false;
            Activate_SideBarMenu_Buttons();
            Deactivate_Menus();
            SubmitCharacter_byUserInput();
            Init_HPHD_Panel();
            Deactivate_Scores_and_Calculate_AbilityModifiers_byUserInput();
            Activate_IniRolls();            
            Activate_AbilityChecks();
            Activate_SavingThrows();
            Activate_SkillChecks();
            Init_AC_Update();
        }

        private void ApplyCharButton_Click(object sender, RoutedEventArgs e)
        {
            if(!hasError)
            {
                ApplyCharacter();
                FileManager.FM_Inst.Play_ClickSound();
            }

            else if(hasError)
            {                
                return;
            }
            
        }

        public void Load_Character()
        {            
            Reset_Form();
            ApplyButton.IsEnabled = false;

            Deactivate_Menus();
            SubmitCharacter();
            
            Set_Level_and_HP_Panel();
            
            Deactivate_Scores_and_Show_AbilityModifiers();
            Show_AbilityScores();

            Deactivate_SaveProf_Buttons();
            Show_SaveModifiers();
            Set_SaveProficiency_Buttons();

            Deactivate_SkillProficiency_Buttons();
            Show_SkillModifiers();
            Set_SkillProficiency_Buttons();

            Activate_AbilityChecks();
            Activate_IniRolls();
            Activate_SaveCheck_Buttons();
            Activate_SkillCheck_Buttons();
            Init_AC_Update();          
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
            Deactivate_SaveProf_Buttons();

            Deactivate_SkillCheck_Buttons();
            Clear_Skills();            
        }

        public void Activate_SideBarMenu_Buttons()
        {
            saveCharButton.IsEnabled = true;
            SpellWindow_bt.IsEnabled = true;
            CombatWindow_bt.IsEnabled = true;
            DiceMachineWindow_bt.IsEnabled = true;
            BackgroundPage_bt.IsEnabled = true;
            Inventory_bt.IsEnabled = true;
            Merchant_bt.IsEnabled = true;

            IsSidebar_Active = true;
        }

        public void Deactivate_SideBarMenu_Buttons()
        {
            saveCharButton.IsEnabled = false;
            SpellWindow_bt.IsEnabled = false;
            CombatWindow_bt.IsEnabled = false;
            DiceMachineWindow_bt.IsEnabled = false;
            BackgroundPage_bt.IsEnabled = false;
            Inventory_bt.IsEnabled = false;
            Merchant_bt.IsEnabled = false;

            IsSidebar_Active = false;
        }

        private void FirstLevel()
        {
            sheetManager.character.Level = 1;
            LevelText.Text = sheetManager.character.Level.ToString();
            Level_Up_bt.IsEnabled = true;            
            sheetManager.character.Update_HitDice();
            HDtext.Text = sheetManager.character.HitDice.ToString();
            ProfBonus_Box.Text = sheetManager.character.ProficiencyBonus.ToString();
        }

        private void Init_HPHD_Panel()
        {
            if (CheckValue(maxHPtext.Text))
            {
                maxHPtext.IsEnabled = false;
                hasError = false;
                sheetManager.character.Set_MaxHP_byText(maxHPtext.Text);
            }

            else { hasError = true; }

            sheetManager.character.Init_HP_HD();
            SheetManager.CS_Manager_Inst.character.hpChanged += Update_HP;
            SheetManager.CS_Manager_Inst.character.tempHPChanged += Update_TempHP;
            SheetManager.CS_Manager_Inst.Init_TempHPCallback();

            currHPtext.Text = sheetManager.character.CurrentHP.ToString();
            currHDtext.Text = sheetManager.character.CurrentHitDice.ToString();
        }

        private void Set_Level_and_HP_Panel()
        {
            LevelText.Text = sheetManager.character.Level.ToString();
            Level_Up_bt.IsEnabled = true;

            SheetManager.CS_Manager_Inst.character.hpChanged += Update_HP;
            SheetManager.CS_Manager_Inst.character.tempHPChanged += Update_TempHP;
            SheetManager.CS_Manager_Inst.Init_TempHPCallback();

            maxHPtext.Text = sheetManager.character.MaxHP.ToString();
            currHPtext.Text = sheetManager.character.CurrentHP.ToString();
            tempHPtext.Text = sheetManager.character.TempHP.ToString();

            HDtext.Text = sheetManager.character.HitDice.ToString();
            currHDtext.Text = sheetManager.character.CurrentHitDice.ToString();
            
            ProfBonus_Box.Text = sheetManager.character.ProficiencyBonus.ToString();
        }

        private void Activate_Interaction()
        {
            Activate_CharacterMenus();
            Activate_HP_Panel();
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

        private void SubmitCharacter_byUserInput()
        {
            sheetManager.character.PlayerName = PlayerNameText.Text;
            sheetManager.character.CharacterName = CharNameText.Text;
            sheetManager.character.CharacterRace = RaceMenu.Text;
            sheetManager.character.CharacterSubrace = SubRaceMenu.Text;
            sheetManager.character.CharacterClass = ClassMenu.Text;
            sheetManager.character.Alignment = AlignmentBox.Text;
            sheetManager.character.Background = BackgroundBox.Text;
        }
        
        private void SubmitCharacter()
        {
            PlayerNameText.Text = sheetManager.character.PlayerName;
            CharNameText.Text = sheetManager.character.CharacterName;
            RaceMenu.Text = sheetManager.character.CharacterRace;
            SubRaceMenu.Text = sheetManager.character.CharacterSubrace;
            ClassMenu.Text = sheetManager.character.CharacterClass;
            AlignmentBox.Text = sheetManager.character.Alignment;
            BackgroundBox.Text = sheetManager.character.Background;
        }

        private void Activate_HP_Panel()
        {
            maxHPtext.IsEnabled = true;
            currHPtext.IsEnabled = true;
            tempHPtext.IsEnabled = true;
            currHDtext.IsEnabled = true;            
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
            currHPtext.Clear();
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

        private void Show_AbilityScores()
        {
            strScoreText.Text = sheetManager.character.StrScore.ToString();
            dexScoreText.Text = sheetManager.character.DexScore.ToString();
            conScoreText.Text = sheetManager.character.ConScore.ToString();
            intScoreText.Text = sheetManager.character.IntScore.ToString();
            wisScoreText.Text = sheetManager.character.WisScore.ToString();
            chaScoreText.Text = sheetManager.character.ChaScore.ToString();
        }

        private void Deactivate_Scores_and_Calculate_AbilityModifiers_byUserInput()
        {            

            if (CheckValue(strScoreText.Text))
            {
                strScoreText.IsEnabled = false;
                hasError = false;
                sheetManager.character.Set_StrScore_byText(strScoreText.Text);
                sheetManager.character.Calculate_StrModifier();
                strModifierText.Text = sheetManager.character.StrModifier.ToString();
            }

            else { hasError = true; }

            if (CheckValue(dexScoreText.Text))
            {
                dexScoreText.IsEnabled = false;
                hasError = false;
                sheetManager.character.Set_DexScore_byText(dexScoreText.Text);
                sheetManager.character.Calculate_DexModifier();
                dexModifierText.Text = sheetManager.character.DexModifier.ToString();
            }

            else { hasError = true; }

            if (CheckValue(conScoreText.Text))
            {
                conScoreText.IsEnabled = false;
                hasError = false;
                sheetManager.character.Set_ConScore_byText(conScoreText.Text);
                sheetManager.character.Calculate_ConModifier();
                conModifierText.Text = sheetManager.character.ConModifier.ToString();
            }

            else { hasError = true; }

            if (CheckValue(intScoreText.Text))
            {
                intScoreText.IsEnabled = false;
                hasError = false;
                sheetManager.character.Set_IntScore_byText(intScoreText.Text);
                sheetManager.character.Calculate_IntModifier();
                intModifierText.Text = sheetManager.character.IntModifier.ToString();
            }


            else { hasError = true; }

            if (CheckValue(wisScoreText.Text))
            {
                wisScoreText.IsEnabled = false;
                hasError = false;
                sheetManager.character.Set_WisScore_byText(wisScoreText.Text);
                sheetManager.character.Calculate_WisModifier();
                wisModifierText.Text = sheetManager.character.WisModifier.ToString();
            }

            else { hasError = true; }

            if (CheckValue(chaScoreText.Text))
            {
                chaScoreText.IsEnabled = false;
                hasError = false;
                sheetManager.character.Set_ChaScore_byText(chaScoreText.Text);
                sheetManager.character.Calculate_ChaModifier();
                chaModifierText.Text = sheetManager.character.ChaModifier.ToString();
            }

            else { hasError = true; }
            
        }        

        private bool CheckValue(string information)
        {            

            if(int.TryParse(information, out int number))
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

        private void Deactivate_Scores_and_Show_AbilityModifiers()
        {
            maxHPtext.IsEnabled = false;

            strScoreText.IsEnabled = false;            
            strModifierText.Text = sheetManager.character.StrModifier.ToString();
            
            dexScoreText.IsEnabled = false;            
            dexModifierText.Text = sheetManager.character.DexModifier.ToString();

            conScoreText.IsEnabled = false;            
            conModifierText.Text = sheetManager.character.ConModifier.ToString();

            intScoreText.IsEnabled = false;            
            intModifierText.Text = sheetManager.character.IntModifier.ToString();

            wisScoreText.IsEnabled = false;            
            wisModifierText.Text = sheetManager.character.WisModifier.ToString();

            chaScoreText.IsEnabled = false;            
            chaModifierText.Text = sheetManager.character.ChaModifier.ToString();            
        }

        private void Activate_IniRolls()
        {
            sheetManager.character.Set_IniBonus();
            iniBonus.Text = sheetManager.character.InitiativeBonus.ToString();
            iniButton.IsEnabled = true;
        }

        private void Deactivate_IniRolls()
        {
            iniButton.IsEnabled = false;
        }

        private void Init_AC_Update()
        {
            SheetManager.CS_Manager_Inst.character.acChanged += Update_AC;
            sheetManager.character.Calculate_AC();            
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
            sheetManager.character.UI_Set_SaveProficiencies(saveProf_STR.IsChecked.Value, saveProf_DEX.IsChecked.Value, saveProf_CON.IsChecked.Value, saveProf_INT.IsChecked.Value, saveProf_WIS.IsChecked.Value, saveProf_CHA.IsChecked.Value);
        }

        private void Calculate_SaveModifiers()
        {
            sheetManager.character.Calculate_SaveModifiers();
        }

        private void Set_SaveProficiency_Buttons()
        {
            if(sheetManager.character.STR_Save.IsProficient)
            {
                saveProf_STR.IsChecked = true;
            }

            if (sheetManager.character.DEX_Save.IsProficient)
            {
                saveProf_DEX.IsChecked = true;
            }

            if (sheetManager.character.CON_Save.IsProficient)
            {
                saveProf_CON.IsChecked = true;
            }

            if (sheetManager.character.INT_Save.IsProficient)
            {
                saveProf_INT.IsChecked = true;
            }

            if (sheetManager.character.WIS_Save.IsProficient)
            {
                saveProf_WIS.IsChecked = true;
            }

            if (sheetManager.character.CHA_Save.IsProficient)
            {
                saveProf_CHA.IsChecked = true;
            }
        }

        private void Show_SaveModifiers()
        {
            STRsave_Val.Text = sheetManager.character.STR_Save.SaveModifier.ToString();
            DEXsave_Val.Text = sheetManager.character.DEX_Save.SaveModifier.ToString();
            CONsave_Val.Text = sheetManager.character.CON_Save.SaveModifier.ToString();
            WISsave_Val.Text = sheetManager.character.WIS_Save.SaveModifier.ToString();
            INTsave_Val.Text = sheetManager.character.INT_Save.SaveModifier.ToString();
            CHAsave_Val.Text = sheetManager.character.CHA_Save.SaveModifier.ToString();
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
            sheetManager.character.UI_Set_Proficiencies_strSkills(AthleticsProf.IsChecked.Value);
            sheetManager.character.UI_Set_Proficiencies_dexSkills(AcrobaticsProf.IsChecked.Value, SleightOfHandProf.IsChecked.Value, StealthProf.IsChecked.Value);
            sheetManager.character.UI_Set_Proficiencies_intSkills(ArcanaProf.IsChecked.Value, HistoryProf.IsChecked.Value, InvestigationProf.IsChecked.Value, NatureProf.IsChecked.Value, ReligionProf.IsChecked.Value);
            sheetManager.character.UI_Set_Proficiencies_wisSkills(AnimalHandlingProf.IsChecked.Value, InsightProf.IsChecked.Value, MedicineProf.IsChecked.Value, PerceptionProf.IsChecked.Value, SurvivalProf.IsChecked.Value);
            sheetManager.character.UI_Set_Proficiencies_chaSkills(DeceptionProf.IsChecked.Value, IntimidationProf.IsChecked.Value, PerformanceProf.IsChecked.Value, PersuasionProf.IsChecked.Value);
        }

        private void Calculate_SkillModifiers()
        {
            sheetManager.character.Calculate_SkillModifiers();
        }

        private void Set_SkillProficiency_Buttons()
        {
            if(sheetManager.character.Acrobatics.IsProficient)
            {
                AcrobaticsProf.IsChecked = true;
            }

            if(sheetManager.character.AnimalHandling.IsProficient)
            {
                AnimalHandlingProf.IsChecked = true;
            }

            if(sheetManager.character.Arcana.IsProficient)
            {
                ArcanaProf.IsChecked = true;
            }

            if(sheetManager.character.Athletics.IsProficient)
            {
                AthleticsProf.IsChecked = true;
            }

            if(sheetManager.character.Deception.IsProficient)
            {
                DeceptionProf.IsChecked = true;
            }

            if(sheetManager.character.History.IsProficient)
            {
                HistoryProf.IsChecked = true;
            }

            if(sheetManager.character.Insight.IsProficient)
            {
                InsightProf.IsChecked = true;
            }

            if(sheetManager.character.Intimidation.IsProficient)
            {
                IntimidationProf.IsChecked = true;
            }

            if(sheetManager.character.Investigation.IsProficient)
            {
                InvestigationProf.IsChecked = true;
            }

            if(sheetManager.character.Medicine.IsProficient)
            {
                MedicineProf.IsChecked = true;
            }

            if(sheetManager.character.Nature.IsProficient)
            {
                NatureProf.IsChecked = true;
            }

            if(sheetManager.character.Perception.IsProficient)
            {
                PerceptionProf.IsChecked = true;
            }

            if(sheetManager.character.Performance.IsProficient)
            {
                PerformanceProf.IsChecked = true;
            }

            if(sheetManager.character.Religion.IsProficient)
            {
                ReligionProf.IsChecked = true;
            }

            if(sheetManager.character.SleightOfHand.IsProficient)
            {
                SleightOfHandProf.IsChecked = true;
            }

            if(sheetManager.character.Stealth.IsProficient)
            {
                StealthProf.IsChecked = true;
            }

            if(sheetManager.character.Survival.IsProficient)
            {
                SurvivalProf.IsChecked = true;
            }
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
            PerformanceProf.IsEnabled = true;
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
            PerformanceProf.IsEnabled = false;
            PersuasionProf.IsEnabled = false;
            ReligionProf.IsEnabled = false;
            SleightOfHandProf.IsEnabled = false;
            StealthProf.IsEnabled = false;
            SurvivalProf.IsEnabled = false;
        }       

        private void Show_SkillModifiers()
        {
            AcrobaticsTxt.Text = sheetManager.character.Acrobatics.SkillModifier.ToString();
            AnimalHandlingTxt.Text = sheetManager.character.AnimalHandling.SkillModifier.ToString();
            ArcanaTxt.Text = sheetManager.character.Arcana.SkillModifier.ToString();
            AthleticsTxt.Text = sheetManager.character.Athletics.SkillModifier.ToString();

            DeceptionTxt.Text = sheetManager.character.Deception.SkillModifier.ToString();

            HistoryTxt.Text = sheetManager.character.History.SkillModifier.ToString();
            InsightTxt.Text = sheetManager.character.Insight.SkillModifier.ToString();
            IntimidationTxt.Text = sheetManager.character.Intimidation.SkillModifier.ToString();
            InvestigationTxt.Text = sheetManager.character.Investigation.SkillModifier.ToString();

            MedicineTxt.Text = sheetManager.character.Medicine.SkillModifier.ToString();
            NatureTxt.Text = sheetManager.character.Nature.SkillModifier.ToString();

            PerceptionTxt.Text = sheetManager.character.Perception.SkillModifier.ToString();
            PerformanceTxt.Text = sheetManager.character.Performance.SkillModifier.ToString();
            PersuasionTxt.Text = sheetManager.character.Persuasion.SkillModifier.ToString();

            ReligionTxt.Text = sheetManager.character.Religion.SkillModifier.ToString();

            SleightOfHandTxt.Text = sheetManager.character.SleightOfHand.SkillModifier.ToString();
            StealthTxt.Text = sheetManager.character.Stealth.SkillModifier.ToString();

            SurvivalTxt.Text = sheetManager.character.Survival.SkillModifier.ToString();
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

            PerformanceProf.IsChecked = false;
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

        private void Update_AC()
        {
            //SheetManager.CS_Manager_Inst.character.Calculate_AC();
            AC.Text = SheetManager.CS_Manager_Inst.character.AC.ToString();
        }

        private void Update_HP()
        {
            currHPtext.Text = SheetManager.CS_Manager_Inst.character.CurrentHP.ToString();
        }

        private void Update_TempHP()
        {
            if(SheetManager.CS_Manager_Inst.character.TempHP > 0)
            {
                tempHPtext.Text = SheetManager.CS_Manager_Inst.character.TempHP.ToString();
            }

            else
            {
                tempHPtext.Text = null;
            }
        }

        private void LevelUpButton_Click(object sender, RoutedEventArgs e)
        {            
            sheetManager.character.Level_Up();
            LevelText.Text = sheetManager.character.Level.ToString();            
            HDtext.Text = sheetManager.character.HitDice.ToString();
            ProfBonus_Box.Text = sheetManager.character.ProficiencyBonus.ToString();
            FileManager.FM_Inst.Play_ClickSound();
        }        

        private void IniButton_Click(object sender, RoutedEventArgs e)
        {            
            iniResult.Text = sheetManager.Roll_for_Initiative().ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void StrButton_Click(object sender, RoutedEventArgs e)
        {           
            strengthResult.Text = sheetManager.Ability_Check(sheetManager.character.StrModifier).ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void DexButton_Click(object sender, RoutedEventArgs e)
        {
            dexResult.Text = sheetManager.Ability_Check(sheetManager.character.DexModifier).ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void ConButton_Click(object sender, RoutedEventArgs e)
        {
            conResult.Text = sheetManager.Ability_Check(sheetManager.character.ConModifier).ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void IntButton_Click(object sender, RoutedEventArgs e)
        {
            intResult.Text = sheetManager.Ability_Check(sheetManager.character.IntModifier).ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void WisButton_Click(object sender, RoutedEventArgs e)
        {
            wisResult.Text = sheetManager.Ability_Check(sheetManager.character.WisModifier).ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void ChaButton_Click(object sender, RoutedEventArgs e)
        {
            chaResult.Text = sheetManager.Ability_Check(sheetManager.character.ChaModifier).ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void STR_Save_bt_Click(object sender, RoutedEventArgs e)
        {
            STRsave_Result.Text = sheetManager.SavingThrow(sheetManager.character.STR_Save.SaveModifier).ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void DEX_Save_bt_Click(object sender, RoutedEventArgs e)
        {
            DEXsave_Result.Text = sheetManager.SavingThrow(sheetManager.character.DEX_Save.SaveModifier).ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void CON_Save_bt_Click(object sender, RoutedEventArgs e)
        {
            CONsave_Result.Text = sheetManager.SavingThrow(sheetManager.character.CON_Save.SaveModifier).ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void INT_Save_bt_Click(object sender, RoutedEventArgs e)
        {
            INTsave_Result.Text = sheetManager.SavingThrow(sheetManager.character.INT_Save.SaveModifier).ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void WIS_Save_bt_Click(object sender, RoutedEventArgs e)
        {
            WISsave_Result.Text = sheetManager.SavingThrow(sheetManager.character.WIS_Save.SaveModifier).ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void CHA_Save_bt_Click(object sender, RoutedEventArgs e)
        {
            CHAsave_Result.Text = sheetManager.SavingThrow(sheetManager.character.CHA_Save.SaveModifier).ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void AcrobaticsBT_Click(object sender, RoutedEventArgs e)
        {
            Acrobatics_Result.Text = sheetManager.Skill_Check(sheetManager.character.Acrobatics.SkillModifier).ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void AnimalHandlingBT_Click(object sender, RoutedEventArgs e)
        {
            AnimalHandling_Result.Text = sheetManager.Skill_Check(sheetManager.character.AnimalHandling.SkillModifier).ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void ArcanaBT_Click(object sender, RoutedEventArgs e)
        {
            Arcana_Result.Text = sheetManager.Skill_Check(sheetManager.character.Arcana.SkillModifier).ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void AthleticsBT_Click(object sender, RoutedEventArgs e)
        {
            Athletics_Result.Text = sheetManager.Skill_Check(sheetManager.character.Athletics.SkillModifier).ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void DeceptionBT_Click(object sender, RoutedEventArgs e)
        {
            Deception_Result.Text = sheetManager.Skill_Check(sheetManager.character.Deception.SkillModifier).ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void HistoryBT_Click(object sender, RoutedEventArgs e)
        {
            History_Result.Text = sheetManager.Skill_Check(sheetManager.character.History.SkillModifier).ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void InsightBT_Click(object sender, RoutedEventArgs e)
        {
            Insight_Result.Text = sheetManager.Skill_Check(sheetManager.character.Insight.SkillModifier).ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void IntimidationBT_Click(object sender, RoutedEventArgs e)
        {
            Intimidation_Result.Text = sheetManager.Skill_Check(sheetManager.character.Intimidation.SkillModifier).ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void InvestigationBT_Click(object sender, RoutedEventArgs e)
        {
            Investigation_Result.Text = sheetManager.Skill_Check(sheetManager.character.Investigation.SkillModifier).ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void MedicineBT_Click(object sender, RoutedEventArgs e)
        {
            Medicine_Result.Text = sheetManager.Skill_Check(sheetManager.character.Medicine.SkillModifier).ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void NatureBT_Click(object sender, RoutedEventArgs e)
        {
            Nature_Result.Text = sheetManager.Skill_Check(sheetManager.character.Nature.SkillModifier).ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void PerceptionBT_Click(object sender, RoutedEventArgs e)
        {
            Perception_Result.Text = sheetManager.Skill_Check(sheetManager.character.Perception.SkillModifier).ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void PerformanceBT_Click(object sender, RoutedEventArgs e)
        {
            Performance_Result.Text = sheetManager.Skill_Check(sheetManager.character.Performance.SkillModifier).ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void PersuasionBT_Click(object sender, RoutedEventArgs e)
        {
            Persuasion_Result.Text = sheetManager.Skill_Check(sheetManager.character.Persuasion.SkillModifier).ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void ReligionBT_Click(object sender, RoutedEventArgs e)
        {
            Religion_Result.Text = sheetManager.Skill_Check(sheetManager.character.Religion.SkillModifier).ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void SleightOfHandBT_Click(object sender, RoutedEventArgs e)
        {
            SleightOfHand_Result.Text = sheetManager.Skill_Check(sheetManager.character.SleightOfHand.SkillModifier).ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void StealthBT_Click(object sender, RoutedEventArgs e)
        {
            Stealth_Result.Text = sheetManager.Skill_Check(sheetManager.character.Stealth.SkillModifier).ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void SurvivalBT_Click(object sender, RoutedEventArgs e)
        {
            Survival_Result.Text = sheetManager.Skill_Check(sheetManager.character.Survival.SkillModifier).ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }        

        private void SaveScreen_bt_Click(object sender, RoutedEventArgs e)
        {
            FileManager.FM_Inst.Play_ClickSound();
            SaveScreen saveScreenWindow = new SaveScreen();
            saveScreenWindow.Show();                        
        }

        private void LoadPage_bt_Click(object sender, RoutedEventArgs e)
        {            
            LoadScreen loadScreenWindow = new LoadScreen();            
            loadScreenWindow.Show();
            FileManager.FM_Inst.Play_ClickSound();
        }

        private void SpellWindow_bt_Click(object sender, RoutedEventArgs e)
        {            
            SpellsWindow spellsWindow = new SpellsWindow();
            spellsWindow.Show();            
            FileManager.FM_Inst.Play_ClickSound();            
        }

        private void CombatWindow_bt_Click(object sender, RoutedEventArgs e)
        {            
            CombatWindow combatWindow = new CombatWindow();
            combatWindow.Init_CombatUI();
            combatWindow.Show();
            FileManager.FM_Inst.Play_ClickSound();
        }

        private void DiceMachineWindow_bt_Click(object sender, RoutedEventArgs e)
        {
            DiceMachine dmWindow = new DiceMachine();
            dmWindow.Set_SheetManager(sheetManager);
            dmWindow.Show();
            FileManager.FM_Inst.Play_ClickSound();
        }

        private void BackgroundPageButton_Click(object sender, RoutedEventArgs e)
        {            
            BackgroundWindow backgroundWindow = new BackgroundWindow();
            backgroundWindow.Show();
            FileManager.FM_Inst.Play_ClickSound();
        }

        private void InventoryWindow_bt_Click(object sender, RoutedEventArgs e)
        {            
            InventoryWindow inventoryWindow = new InventoryWindow();
            inventoryWindow.Show();
            FileManager.FM_Inst.Play_ClickSound();
        }

        private void MerchantWindow_bt_Click(object sender, RoutedEventArgs e)
        {            
            MerchantWindow merchantWindow = new MerchantWindow();
            merchantWindow.Show();
            FileManager.FM_Inst.Play_ClickSound();
        }
        
    }
}
