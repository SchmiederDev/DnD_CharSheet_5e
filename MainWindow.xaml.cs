using System.Windows;

namespace DnD_CharSheet_5e
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static MainWindow mainWindow_Inst;

        bool hasError = false;
        bool IsSidebar_Active = false;

        SheetManager sheetManager = new SheetManager();

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

            SheetManager.CS_Manager_Inst.character.Init_Basics();
            SheetManager.CS_Manager_Inst.CharGenCharacter.Init_Basics();
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
                ApplyButton.IsEnabled = false;
                ApplyCharacter();
                FileManager.FM_Inst.Play_ClickSound();
            }

            else if(hasError)
            {
                ApplyButton.IsEnabled = true;
                return;
            }
            
        }

        public void Load_Character()
        {            
            Reset_Form();
            ApplyButton.IsEnabled = false;

            Deactivate_Menus();

            SubmitCharacter_OnLoad();
            
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
            Reset_CharacterMenus();
            Reset_HP_Panel();
            
            Deactivate_IniRolls();
            Reset_IniPanel();            
            
            Deactivate_AbilityChecks();
            Reset_AbilityPanel();

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
            SheetManager.CS_Manager_Inst.character.Level = 1;
            LevelText.Text = SheetManager.CS_Manager_Inst.character.Level.ToString();
            Level_Up_bt.IsEnabled = true;            
            SheetManager.CS_Manager_Inst.character.Update_HitDice();
            HDtext.Text = SheetManager.CS_Manager_Inst.character.HitDice.ToString();
            ProfBonus_Box.Text = SheetManager.CS_Manager_Inst.character.ProficiencyBonus.ToString();
        }

        private void Init_HPHD_Panel()
        {
            if (CheckValue(maxHPtext.Text))
            {
                maxHPtext.IsEnabled = false;
                hasError = false;
                SheetManager.CS_Manager_Inst.character.Set_MaxHP_byText(maxHPtext.Text);
            }

            else { hasError = true; }

            SheetManager.CS_Manager_Inst.character.Init_HP_HD();
            SheetManager.CS_Manager_Inst.character.hpChanged += Update_HP;
            SheetManager.CS_Manager_Inst.character.tempHPChanged += Update_TempHP;
            SheetManager.CS_Manager_Inst.Init_TempHPCallback();

            currHPtext.Text = SheetManager.CS_Manager_Inst.character.CurrentHP.ToString();
            currHDtext.Text = SheetManager.CS_Manager_Inst.character.CurrentHitDice.ToString();
        }

        private void Set_Level_and_HP_Panel()
        {
            LevelText.Text = SheetManager.CS_Manager_Inst.character.Level.ToString();
            Level_Up_bt.IsEnabled = true;

            SheetManager.CS_Manager_Inst.character.hpChanged += Update_HP;
            SheetManager.CS_Manager_Inst.character.tempHPChanged += Update_TempHP;
            SheetManager.CS_Manager_Inst.Init_TempHPCallback();

            maxHPtext.Text = SheetManager.CS_Manager_Inst.character.MaxHP.ToString();
            currHPtext.Text = SheetManager.CS_Manager_Inst.character.CurrentHP.ToString();
            tempHPtext.Text = SheetManager.CS_Manager_Inst.character.TempHP.ToString();

            HDtext.Text = SheetManager.CS_Manager_Inst.character.HitDice.ToString();
            currHDtext.Text = SheetManager.CS_Manager_Inst.character.CurrentHitDice.ToString();
            
            ProfBonus_Box.Text = SheetManager.CS_Manager_Inst.character.ProficiencyBonus.ToString();
        }

        private void Activate_Interaction()
        {
            Activate_CharacterMenus();
            Activate_HP_Panel();
            Activate_Scores();
            Activate_SaveProf_Buttons();
            Activate_SkillProficiency_Buttons();            
        }

        private void Reset_CharacterMenus()
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
            SheetManager.CS_Manager_Inst.character.PlayerName = PlayerNameText.Text;
            SheetManager.CS_Manager_Inst.character.CharacterName = CharNameText.Text;
            SheetManager.CS_Manager_Inst.character.CharacterRace = RaceMenu.Text;
            SheetManager.CS_Manager_Inst.character.CharacterSubrace = SubRaceMenu.Text;
            SheetManager.CS_Manager_Inst.character.CharacterClass = ClassMenu.Text;
            SheetManager.CS_Manager_Inst.character.Alignment = AlignmentBox.Text;
            SheetManager.CS_Manager_Inst.character.Background = BackgroundBox.Text;
        }
        
        private void SubmitCharacter_OnLoad()
        {
            PlayerNameText.Text = SheetManager.CS_Manager_Inst.character.PlayerName;
            CharNameText.Text = SheetManager.CS_Manager_Inst.character.CharacterName;
            RaceMenu.Text = SheetManager.CS_Manager_Inst.character.CharacterRace;
            SubRaceMenu.Text = SheetManager.CS_Manager_Inst.character.CharacterSubrace;
            ClassMenu.Text = SheetManager.CS_Manager_Inst.character.CharacterClass;
            AlignmentBox.Text = SheetManager.CS_Manager_Inst.character.Alignment;
            BackgroundBox.Text = SheetManager.CS_Manager_Inst.character.Background;
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

        private void Reset_AbilityPanel()
        {
            Clear_AbilityScoreBoxes();
            Clear_AbilityModifierBoxes();
            Clear_AbilityResultBoxes();
        }

        private void Clear_AbilityScoreBoxes()
        {
            strScoreText.Clear();
            dexScoreText.Clear();
            conScoreText.Clear();
            intScoreText.Clear();
            wisScoreText.Clear();
            chaScoreText.Clear();
        }

        private void Clear_AbilityModifierBoxes()
        {
            strModifierText.Clear();
            dexModifierText.Clear();
            conModifierText.Clear();
            intModifierText.Clear();
            wisModifierText.Clear();
            chaModifierText.Clear();
        }

        private void Clear_AbilityResultBoxes()
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
            strScoreText.Text = SheetManager.CS_Manager_Inst.character.Strength.Score.ToString();
            dexScoreText.Text = SheetManager.CS_Manager_Inst.character.Dexterity.Score.ToString();
            conScoreText.Text = SheetManager.CS_Manager_Inst.character.Constitution.Score.ToString();
            intScoreText.Text = SheetManager.CS_Manager_Inst.character.Intelligence.Score.ToString();
            wisScoreText.Text = SheetManager.CS_Manager_Inst.character.Wisdom.Score.ToString();
            chaScoreText.Text = SheetManager.CS_Manager_Inst.character.Charisma.Score.ToString();
        }

        private void Deactivate_Scores_and_Calculate_AbilityModifiers_byUserInput()
        {            

            if (CheckValue(strScoreText.Text))
            {
                strScoreText.IsEnabled = false;
                hasError = false;
                SheetManager.CS_Manager_Inst.character.Set_StrScore_byText(strScoreText.Text);
                SheetManager.CS_Manager_Inst.character.Strength.Calculate_Modifier();
                strModifierText.Text = SheetManager.CS_Manager_Inst.character.Strength.Modifier.ToString();
            }

            else { hasError = true; }

            if (CheckValue(dexScoreText.Text))
            {
                dexScoreText.IsEnabled = false;
                hasError = false;
                SheetManager.CS_Manager_Inst.character.Set_DexScore_byText(dexScoreText.Text);
                SheetManager.CS_Manager_Inst.character.Dexterity.Calculate_Modifier();
                dexModifierText.Text = SheetManager.CS_Manager_Inst.character.Dexterity.Modifier.ToString();
            }

            else { hasError = true; }

            if (CheckValue(conScoreText.Text))
            {
                conScoreText.IsEnabled = false;
                hasError = false;
                SheetManager.CS_Manager_Inst.character.Set_ConScore_byText(conScoreText.Text);
                SheetManager.CS_Manager_Inst.character.Constitution.Calculate_Modifier();
                conModifierText.Text = SheetManager.CS_Manager_Inst.character.Constitution.Modifier.ToString();
            }

            else { hasError = true; }

            if (CheckValue(intScoreText.Text))
            {
                intScoreText.IsEnabled = false;
                hasError = false;
                SheetManager.CS_Manager_Inst.character.Set_IntScore_byText(intScoreText.Text);
                SheetManager.CS_Manager_Inst.character.Intelligence.Calculate_Modifier();
                intModifierText.Text = SheetManager.CS_Manager_Inst.character.Intelligence.Modifier.ToString();
            }


            else { hasError = true; }

            if (CheckValue(wisScoreText.Text))
            {
                wisScoreText.IsEnabled = false;
                hasError = false;
                SheetManager.CS_Manager_Inst.character.Set_WisScore_byText(wisScoreText.Text);
                SheetManager.CS_Manager_Inst.character.Wisdom.Calculate_Modifier();
                wisModifierText.Text = SheetManager.CS_Manager_Inst.character.Wisdom.Modifier.ToString();
            }

            else { hasError = true; }

            if (CheckValue(chaScoreText.Text))
            {
                chaScoreText.IsEnabled = false;
                hasError = false;
                SheetManager.CS_Manager_Inst.character.Set_ChaScore_byText(chaScoreText.Text);
                SheetManager.CS_Manager_Inst.character.Charisma.Calculate_Modifier();
                chaModifierText.Text = SheetManager.CS_Manager_Inst.character.Charisma.Modifier.ToString();
            }

            else { hasError = true; }
            
        }        

        private bool CheckValue(string textBoxTxt)
        {            

            if(int.TryParse(textBoxTxt, out int number))
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
            strModifierText.Text = SheetManager.CS_Manager_Inst.character.Strength.Modifier.ToString();
            
            dexScoreText.IsEnabled = false;            
            dexModifierText.Text = SheetManager.CS_Manager_Inst.character.Dexterity.Modifier.ToString();

            conScoreText.IsEnabled = false;            
            conModifierText.Text = SheetManager.CS_Manager_Inst.character.Constitution.Modifier.ToString();

            intScoreText.IsEnabled = false;            
            intModifierText.Text = SheetManager.CS_Manager_Inst.character.Intelligence.Modifier.ToString();

            wisScoreText.IsEnabled = false;            
            wisModifierText.Text = SheetManager.CS_Manager_Inst.character.Wisdom.Modifier.ToString();

            chaScoreText.IsEnabled = false;            
            chaModifierText.Text = SheetManager.CS_Manager_Inst.character.Charisma.Modifier.ToString();            
        }

        private void Activate_IniRolls()
        {
            SheetManager.CS_Manager_Inst.character.Set_IniBonus();
            iniBonus.Text = SheetManager.CS_Manager_Inst.character.InitiativeBonus.ToString();
            iniButton.IsEnabled = true;
        }

        private void Deactivate_IniRolls()
        {
            iniButton.IsEnabled = false;
        }

        private void Init_AC_Update()
        {
            SheetManager.CS_Manager_Inst.character.acChanged += Update_AC;
            SheetManager.CS_Manager_Inst.character.Calculate_AC();            
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
            CHAsave_Result.Clear();
        }

        private void Transfer_SavingThrows()
        {
            SheetManager.CS_Manager_Inst.character.Set_SaveBaseValues();
            SheetManager.CS_Manager_Inst.character.Set_SaveProficiencies(saveProf_STR.IsChecked.Value, saveProf_DEX.IsChecked.Value, saveProf_CON.IsChecked.Value, saveProf_INT.IsChecked.Value, saveProf_WIS.IsChecked.Value, saveProf_CHA.IsChecked.Value);
        }

        private void Calculate_SaveModifiers()
        {
            SheetManager.CS_Manager_Inst.character.CalculateSavingThrowModifiers();
        }

        private void Set_SaveProficiency_Buttons()
        {
            if(SheetManager.CS_Manager_Inst.character.STR_Save.IsProficient)
            {
                saveProf_STR.IsChecked = true;
            }

            if (SheetManager.CS_Manager_Inst.character.DEX_Save.IsProficient)
            {
                saveProf_DEX.IsChecked = true;
            }

            if (SheetManager.CS_Manager_Inst.character.CON_Save.IsProficient)
            {
                saveProf_CON.IsChecked = true;
            }

            if (SheetManager.CS_Manager_Inst.character.INT_Save.IsProficient)
            {
                saveProf_INT.IsChecked = true;
            }

            if (SheetManager.CS_Manager_Inst.character.WIS_Save.IsProficient)
            {
                saveProf_WIS.IsChecked = true;
            }

            if (SheetManager.CS_Manager_Inst.character.CHA_Save.IsProficient)
            {
                saveProf_CHA.IsChecked = true;
            }
        }

        private void Show_SaveModifiers()
        {
            STRsave_Val.Text = SheetManager.CS_Manager_Inst.character.STR_Save.SaveModifier.ToString();
            DEXsave_Val.Text = SheetManager.CS_Manager_Inst.character.DEX_Save.SaveModifier.ToString();
            CONsave_Val.Text = SheetManager.CS_Manager_Inst.character.CON_Save.SaveModifier.ToString();
            WISsave_Val.Text = SheetManager.CS_Manager_Inst.character.WIS_Save.SaveModifier.ToString();
            INTsave_Val.Text = SheetManager.CS_Manager_Inst.character.INT_Save.SaveModifier.ToString();
            CHAsave_Val.Text = SheetManager.CS_Manager_Inst.character.CHA_Save.SaveModifier.ToString();
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
            SheetManager.CS_Manager_Inst.character.Set_SkillBaseValues();
            Set_SkillProficiencies();
        }

        private void Set_SkillProficiencies()
        {
            SheetManager.CS_Manager_Inst.character.Set_Proficiencies_strSkills(AthleticsProf.IsChecked.Value);
            SheetManager.CS_Manager_Inst.character.Set_Proficiencies_dexSkills(AcrobaticsProf.IsChecked.Value, SleightOfHandProf.IsChecked.Value, StealthProf.IsChecked.Value);
            SheetManager.CS_Manager_Inst.character.Set_Proficiencies_intSkills(ArcanaProf.IsChecked.Value, HistoryProf.IsChecked.Value, InvestigationProf.IsChecked.Value, NatureProf.IsChecked.Value, ReligionProf.IsChecked.Value);
            SheetManager.CS_Manager_Inst.character.Set_Proficiencies_wisSkills(AnimalHandlingProf.IsChecked.Value, InsightProf.IsChecked.Value, MedicineProf.IsChecked.Value, PerceptionProf.IsChecked.Value, SurvivalProf.IsChecked.Value);
            SheetManager.CS_Manager_Inst.character.Set_Proficiencies_chaSkills(DeceptionProf.IsChecked.Value, IntimidationProf.IsChecked.Value, PerformanceProf.IsChecked.Value, PersuasionProf.IsChecked.Value);
        }

        private void Calculate_SkillModifiers()
        {
            SheetManager.CS_Manager_Inst.character.CalculateSkillModifiers();
        }

        private void Set_SkillProficiency_Buttons()
        {
            if(SheetManager.CS_Manager_Inst.character.Acrobatics.IsProficient)
            {
                AcrobaticsProf.IsChecked = true;
            }

            if(SheetManager.CS_Manager_Inst.character.AnimalHandling.IsProficient)
            {
                AnimalHandlingProf.IsChecked = true;
            }

            if(SheetManager.CS_Manager_Inst.character.Arcana.IsProficient)
            {
                ArcanaProf.IsChecked = true;
            }

            if(SheetManager.CS_Manager_Inst.character.Athletics.IsProficient)
            {
                AthleticsProf.IsChecked = true;
            }

            if(SheetManager.CS_Manager_Inst.character.Deception.IsProficient)
            {
                DeceptionProf.IsChecked = true;
            }

            if(SheetManager.CS_Manager_Inst.character.History.IsProficient)
            {
                HistoryProf.IsChecked = true;
            }

            if(SheetManager.CS_Manager_Inst.character.Insight.IsProficient)
            {
                InsightProf.IsChecked = true;
            }

            if(SheetManager.CS_Manager_Inst.character.Intimidation.IsProficient)
            {
                IntimidationProf.IsChecked = true;
            }

            if(SheetManager.CS_Manager_Inst.character.Investigation.IsProficient)
            {
                InvestigationProf.IsChecked = true;
            }

            if(SheetManager.CS_Manager_Inst.character.Medicine.IsProficient)
            {
                MedicineProf.IsChecked = true;
            }

            if(SheetManager.CS_Manager_Inst.character.Nature.IsProficient)
            {
                NatureProf.IsChecked = true;
            }

            if(SheetManager.CS_Manager_Inst.character.Perception.IsProficient)
            {
                PerceptionProf.IsChecked = true;
            }

            if(SheetManager.CS_Manager_Inst.character.Performance.IsProficient)
            {
                PerformanceProf.IsChecked = true;
            }

            if(SheetManager.CS_Manager_Inst.character.Religion.IsProficient)
            {
                ReligionProf.IsChecked = true;
            }

            if(SheetManager.CS_Manager_Inst.character.SleightOfHand.IsProficient)
            {
                SleightOfHandProf.IsChecked = true;
            }

            if(SheetManager.CS_Manager_Inst.character.Stealth.IsProficient)
            {
                StealthProf.IsChecked = true;
            }

            if(SheetManager.CS_Manager_Inst.character.Survival.IsProficient)
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
            AcrobaticsTxt.Text = SheetManager.CS_Manager_Inst.character.Acrobatics.SkillModifier.ToString();
            AnimalHandlingTxt.Text = SheetManager.CS_Manager_Inst.character.AnimalHandling.SkillModifier.ToString();
            ArcanaTxt.Text = SheetManager.CS_Manager_Inst.character.Arcana.SkillModifier.ToString();
            AthleticsTxt.Text = SheetManager.CS_Manager_Inst.character.Athletics.SkillModifier.ToString();

            DeceptionTxt.Text = SheetManager.CS_Manager_Inst.character.Deception.SkillModifier.ToString();

            HistoryTxt.Text = SheetManager.CS_Manager_Inst.character.History.SkillModifier.ToString();
            InsightTxt.Text = SheetManager.CS_Manager_Inst.character.Insight.SkillModifier.ToString();
            IntimidationTxt.Text = SheetManager.CS_Manager_Inst.character.Intimidation.SkillModifier.ToString();
            InvestigationTxt.Text = SheetManager.CS_Manager_Inst.character.Investigation.SkillModifier.ToString();

            MedicineTxt.Text = SheetManager.CS_Manager_Inst.character.Medicine.SkillModifier.ToString();
            NatureTxt.Text = SheetManager.CS_Manager_Inst.character.Nature.SkillModifier.ToString();

            PerceptionTxt.Text = SheetManager.CS_Manager_Inst.character.Perception.SkillModifier.ToString();
            PerformanceTxt.Text = SheetManager.CS_Manager_Inst.character.Performance.SkillModifier.ToString();
            PersuasionTxt.Text = SheetManager.CS_Manager_Inst.character.Persuasion.SkillModifier.ToString();

            ReligionTxt.Text = SheetManager.CS_Manager_Inst.character.Religion.SkillModifier.ToString();

            SleightOfHandTxt.Text = SheetManager.CS_Manager_Inst.character.SleightOfHand.SkillModifier.ToString();
            StealthTxt.Text = SheetManager.CS_Manager_Inst.character.Stealth.SkillModifier.ToString();

            SurvivalTxt.Text = SheetManager.CS_Manager_Inst.character.Survival.SkillModifier.ToString();
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
            SheetManager.CS_Manager_Inst.character.Level_Up();
            LevelText.Text = SheetManager.CS_Manager_Inst.character.Level.ToString();            
            HDtext.Text = SheetManager.CS_Manager_Inst.character.HitDice.ToString();
            ProfBonus_Box.Text = SheetManager.CS_Manager_Inst.character.ProficiencyBonus.ToString();
            FileManager.FM_Inst.Play_ClickSound();
        }        

        private void IniButton_Click(object sender, RoutedEventArgs e)
        {            
            iniResult.Text = SheetManager.CS_Manager_Inst.Roll_for_Initiative().ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void StrButton_Click(object sender, RoutedEventArgs e)
        {           
            strengthResult.Text = SheetManager.CS_Manager_Inst.character.Strength.Ability_Check().ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void DexButton_Click(object sender, RoutedEventArgs e)
        {
            dexResult.Text = SheetManager.CS_Manager_Inst.character.Dexterity.Ability_Check().ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void ConButton_Click(object sender, RoutedEventArgs e)
        {
            conResult.Text = SheetManager.CS_Manager_Inst.character.Constitution.Ability_Check().ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void IntButton_Click(object sender, RoutedEventArgs e)
        {
            intResult.Text = SheetManager.CS_Manager_Inst.character.Intelligence.Ability_Check().ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void WisButton_Click(object sender, RoutedEventArgs e)
        {
            wisResult.Text = SheetManager.CS_Manager_Inst.character.Wisdom.Ability_Check().ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void ChaButton_Click(object sender, RoutedEventArgs e)
        {
            chaResult.Text = SheetManager.CS_Manager_Inst.character.Charisma.Ability_Check().ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void STR_Save_bt_Click(object sender, RoutedEventArgs e)
        {
            STRsave_Result.Text = SheetManager.CS_Manager_Inst.character.STR_Save.Make_SavingThrow().ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void DEX_Save_bt_Click(object sender, RoutedEventArgs e)
        {
            DEXsave_Result.Text = SheetManager.CS_Manager_Inst.character.DEX_Save.Make_SavingThrow().ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void CON_Save_bt_Click(object sender, RoutedEventArgs e)
        {
            CONsave_Result.Text = SheetManager.CS_Manager_Inst.character.CON_Save.Make_SavingThrow().ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void INT_Save_bt_Click(object sender, RoutedEventArgs e)
        {
            INTsave_Result.Text = SheetManager.CS_Manager_Inst.character.INT_Save.Make_SavingThrow().ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void WIS_Save_bt_Click(object sender, RoutedEventArgs e)
        {
            WISsave_Result.Text = SheetManager.CS_Manager_Inst.character.WIS_Save.Make_SavingThrow().ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void CHA_Save_bt_Click(object sender, RoutedEventArgs e)
        {
            CHAsave_Result.Text = SheetManager.CS_Manager_Inst.character.CHA_Save.Make_SavingThrow().ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void AcrobaticsBT_Click(object sender, RoutedEventArgs e)
        {
            Acrobatics_Result.Text = SheetManager.CS_Manager_Inst.character.Acrobatics.SkillCheck().ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void AnimalHandlingBT_Click(object sender, RoutedEventArgs e)
        {
            AnimalHandling_Result.Text = SheetManager.CS_Manager_Inst.character.AnimalHandling.SkillCheck().ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void ArcanaBT_Click(object sender, RoutedEventArgs e)
        {
            Arcana_Result.Text = SheetManager.CS_Manager_Inst.character.Arcana.SkillCheck().ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void AthleticsBT_Click(object sender, RoutedEventArgs e)
        {
            Athletics_Result.Text = SheetManager.CS_Manager_Inst.character.Athletics.SkillCheck().ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void DeceptionBT_Click(object sender, RoutedEventArgs e)
        {
            Deception_Result.Text = SheetManager.CS_Manager_Inst.character.Deception.SkillCheck().ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void HistoryBT_Click(object sender, RoutedEventArgs e)
        {
            History_Result.Text = SheetManager.CS_Manager_Inst.character.History.SkillCheck().ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void InsightBT_Click(object sender, RoutedEventArgs e)
        {
            Insight_Result.Text = SheetManager.CS_Manager_Inst.character.Insight.SkillCheck().ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void IntimidationBT_Click(object sender, RoutedEventArgs e)
        {
            Intimidation_Result.Text = SheetManager.CS_Manager_Inst.character.Intimidation.SkillCheck().ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void InvestigationBT_Click(object sender, RoutedEventArgs e)
        {
            Investigation_Result.Text = SheetManager.CS_Manager_Inst.character.Investigation.SkillCheck().ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void MedicineBT_Click(object sender, RoutedEventArgs e)
        {
            Medicine_Result.Text = SheetManager.CS_Manager_Inst.character.Medicine.SkillCheck().ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void NatureBT_Click(object sender, RoutedEventArgs e)
        {
            Nature_Result.Text = SheetManager.CS_Manager_Inst.character.Nature.SkillCheck().ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void PerceptionBT_Click(object sender, RoutedEventArgs e)
        {
            Perception_Result.Text = SheetManager.CS_Manager_Inst.character.Perception.SkillCheck().ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void PerformanceBT_Click(object sender, RoutedEventArgs e)
        {
            Performance_Result.Text = SheetManager.CS_Manager_Inst.character.Performance.SkillCheck().ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void PersuasionBT_Click(object sender, RoutedEventArgs e)
        {
            Persuasion_Result.Text = SheetManager.CS_Manager_Inst.character.Persuasion.SkillCheck().ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void ReligionBT_Click(object sender, RoutedEventArgs e)
        {
            Religion_Result.Text = SheetManager.CS_Manager_Inst.character.Religion.SkillCheck().ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void SleightOfHandBT_Click(object sender, RoutedEventArgs e)
        {
            SleightOfHand_Result.Text = SheetManager.CS_Manager_Inst.character.SleightOfHand.SkillCheck().ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void StealthBT_Click(object sender, RoutedEventArgs e)
        {
            Stealth_Result.Text = SheetManager.CS_Manager_Inst.character.Stealth.SkillCheck().ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void SurvivalBT_Click(object sender, RoutedEventArgs e)
        {
            Survival_Result.Text = SheetManager.CS_Manager_Inst.character.Survival.SkillCheck().ToString();
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
            dmWindow.Set_SheetManager(SheetManager.CS_Manager_Inst);
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
