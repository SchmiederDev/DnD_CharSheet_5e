using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DnD_CharSheet_5e
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>


    #region THE PURPOSE OF MAIN WINDOW
    /* The Main Window of this app contains the actual Character Sheet for the character played in the game. 
     * (or to be more precise the most important page of a D&D-Characrer Sheet - page 1 - for those familiar with the game).
     * 
     * That means it serves in the creation of a character by user input as well as processing this most relevant information (data/ values) about the character.
     * It also serves to emulate some of the most important actions a player/ character can make during the game by processing user input.
     * This concerns mostly clicking buttons for dice rolls. 
     */
    #endregion

    public partial class MainWindow : Window
    {

        #region MEMBER PROPERTIES OF MAIN WINDOW

        // Singleton to access the Main Window from sub windows
        public static MainWindow mainWindow_Inst;

        // Bool for handling invalid types of user input during character creation in all of the important number (int) input fields (when the user fills out the sheet)
        bool hasError = false;

        #region PURPOSE AND EXPLANATION OF MAIN MENU BUTTONS
        /* 
         * The Main Menu Buttons serve to navigate between the Main Window and different sub windows which handle game mechanics not included in the Main Sheet.
         * This serves the purpose of logical structuring of the app as well as making it more usable.
         * 
         * ----------------------------
         * Most of the Buttons of the Main Menu which initialize and load different sub windows are disabled upon start of the app or when the user clicks the 'New Character'-Button
         * because these windows rely on valid data/ member property values of the character-class to work properly. (For the function of the character class, see 'App.xaml.cs' and the class itself)
         * Consequentially, these buttons are only enabled if the user has loaded or created a character with valid member property values.
        */
        #endregion

        List<Button> MainMenuBtns;

        // This boolean serves to check whether main menu buttons are enabled or not and whether they should be enabled/ disabled or not (see explanation above).
        
        bool IsMainMenu_Active = false;

        // A collection of all the TextBoxes on the UI from which numbers (int's) are parsed and processed.
        // The List serves mainly to enable and disable them because the user isn't supposed to be able to change most of these values after character creation is completed.  
        
        List<TextBox> MainSheetNumberBoxes;


        List<TextBox> AbilityScoreBoxes;

        
        List<CheckBox> SavingThrowProficiencyCheckBoxes;
        List<CheckBox> SkillProficiencyCheckBoxes;

        // A collection of all the CheckBoxes on MainWindow - Saving Throw'-Checkboxes and 'Skill Proficiency'-CheckBoxes combined.
        // Again, to enable and disable them for the same reason as with the 'Number Boxes': There Value shoudln't be changed after character creation.

        List<CheckBox> MainSheetCheckBoxes;


        List<Button> DiceRollBtns;

        #endregion

        #region CONSTRUCTOR
        public MainWindow()
        {
            InitializeComponent();

            if(mainWindow_Inst == null)
            {
                mainWindow_Inst = this;
            }

            Init_UIControls();
            
        }
        #endregion


        #region INITIALIZATION OF UI-ELEMENTS - EXECUTED IN THE CONSTTRUCTOR
        private void Init_UIControls()
        {
            Init_MainMenuBtns();
            Init_DiceRollBtns();
            Init_MainSheetNumberBoxes();
            Init_MainSheetCheckBoxes();
            
        }
        #endregion

        #region MEMBER FUNCTIONS FOR INITIALIZATION OF UI-ELEMENTS
        private void Init_MainMenuBtns()
        {
            MainMenuBtns = new List<Button>();

            foreach (Button MenuBtn in MenuBtnsPanel.Children)
            {
                MenuBtn.Click += new RoutedEventHandler(PlayClickSound_Onclick);
                MainMenuBtns.Add(MenuBtn);
            }
        }

        private void PlayClickSound_Onclick(object sender, RoutedEventArgs e)
        {
            FileManager.FM_Inst.Play_ClickSound();
        }

        private void Init_MainSheetNumberBoxes()
        {
            MainSheetNumberBoxes = new List<TextBox>();

            Init_AbilityScoreBoxes();

            Init_AbilityBoxes();
            Init_SavingThrowBoxes();
            Init_SkillBoxes();
        }

        private void Init_AbilityScoreBoxes()
        {
            AbilityScoreBoxes = new List<TextBox>();

            foreach (Grid grid in AbilityScoresPanel.Children)
            {
                foreach (UIElement element in grid.Children)
                {
                    if (element is TextBox)
                    {
                        TextBox textBox = element as TextBox;

                        if (textBox.Name.Contains("ScoreBox"))
                        {
                            AbilityScoreBoxes.Add(textBox);
                        }
                    }
                }
            }
        }

        private void Init_AbilityBoxes()
        {
            foreach (Grid grid in AbilityScoresPanel.Children)
            {
                foreach (UIElement element in grid.Children)
                {
                    if (element is TextBox)
                    {
                        MainSheetNumberBoxes.Add(element as TextBox);
                    }
                }
            }
        }

        private void Init_SavingThrowBoxes()
        {
            foreach (Grid grid in SavesPanel.Children)
            {
                foreach (UIElement element in grid.Children)
                {
                    if (element is TextBox)
                    {
                        MainSheetNumberBoxes.Add(element as TextBox);
                    }
                }
            }
        }

        private void Init_SkillBoxes()
        {
            foreach (StackPanel SkillPnl in SkillsGrid.Children)
            {
                foreach (UIElement element in SkillPnl.Children)
                {
                    if (element is TextBox)
                    {
                        MainSheetNumberBoxes.Add(element as TextBox);
                    }
                }
            }
        }


        private void Init_DiceRollBtns()
        {
            DiceRollBtns = new List<Button>();

            Init_AbilityCheckBtns();
            Init_SavingThrowBtns();
            Init_SkillCheckBtns();

            Set_DieRollBtn_Image();
            Init_DiceSound_OnBtnClick();
        }

        private void Init_AbilityCheckBtns()
        {
            foreach (Grid grid in AbilityScoresPanel.Children)
            {
                foreach (UIElement element in grid.Children)
                {
                    if (element is Button)
                    {
                        DiceRollBtns.Add(element as Button);
                    }
                }
            }
        }

        private void Init_SavingThrowBtns()
        {
            foreach (Grid grid in SavesPanel.Children)
            {
                foreach (UIElement element in grid.Children)
                {
                    if (element is Button)
                    {
                        DiceRollBtns.Add(element as Button);
                    }
                }
            }
        }

        private void Init_SkillCheckBtns()
        {
            foreach (Button SkillCheckBtn in SkillCheckBtns_Panel_AtoI.Children)
            {
                DiceRollBtns.Add(SkillCheckBtn);
            }

            foreach (Button SkillCheckBtn in SkillCheckBtns_Panel_MtoS.Children)
            {
                DiceRollBtns.Add(SkillCheckBtn);
            }
        }

        private void Set_DieRollBtn_Image()
        {
            foreach (Button CheckBtn in DiceRollBtns)
            {
                Image DieImage = new Image();

                Thickness ImagePadding = new Thickness();
                ImagePadding.Top = 2;
                ImagePadding.Bottom = 2;

                DieImage.Margin = ImagePadding;

                DieImage.Source = ImageHandler.ImgHandlerInst.DieImage.Source;
                CheckBtn.Content = DieImage;
            }
        }

        private void Init_MainSheetCheckBoxes()
        {
            MainSheetCheckBoxes = new List<CheckBox>();

            Init_SavingThrow_CheckBoxes();
            MainSheetCheckBoxes.AddRange(SavingThrowProficiencyCheckBoxes);

            Init_SkillCheckBoxes();
            MainSheetCheckBoxes.AddRange(SkillProficiencyCheckBoxes);
        }
        private void Init_SavingThrow_CheckBoxes()
        {
            SavingThrowProficiencyCheckBoxes = new List<CheckBox>();

            foreach (Grid grid in SavesPanel.Children)
            {
                foreach (UIElement element in grid.Children)
                {
                    if (element is CheckBox)
                    {
                        SavingThrowProficiencyCheckBoxes.Add(element as CheckBox);
                    }
                }
            }
        }

        private void Init_SkillCheckBoxes()
        {
            SkillProficiencyCheckBoxes = new List<CheckBox>();

            foreach (StackPanel SkillPnl in SkillsGrid.Children)
            {
                foreach (UIElement element in SkillPnl.Children)
                {
                    if (element is CheckBox)
                    {
                        SkillProficiencyCheckBoxes.Add(element as CheckBox);
                    }
                }
            }
        }

        private void CheckClickSound(object sender, RoutedEventArgs e)
        {
            FileManager.FM_Inst.Play_DiceSound();
        }

        private void Init_DiceSound_OnBtnClick()
        {
            foreach (Button CheckBtn in DiceRollBtns)
            {
                CheckBtn.Click += new RoutedEventHandler(CheckClickSound);
            }
        }

        #endregion


        #region MAIN MENU BUTTON EVENT HANDLER
        private void NewCharButton_Click(object sender, RoutedEventArgs e)
        {
            SheetManager.CS_Manager_Inst.character.Reset_Character();

            CreateCharacter();
            if (ApplyButton.IsEnabled == false)
            {
                ApplyButton.IsEnabled = true;
            }
            
        }

        private void ApplyCharButton_Click(object sender, RoutedEventArgs e)
        {
            ApplyCharacter();

        }

        private void SaveWindow_Btn_Click(object sender, RoutedEventArgs e)
        {
            SaveWindow SaveWdw = new SaveWindow();
            SaveWdw.Show();
        }

        private void LoadWindow_Btn_Click(object sender, RoutedEventArgs e)
        {
            LoadWindow LoadWdw = new LoadWindow();
            LoadWdw.Show();
        }

        private void SpellWindow_Btn_Click(object sender, RoutedEventArgs e)
        {
            SpellsWindow SpellsWdw = new SpellsWindow();
            SpellsWdw.Show();
        }

        private void CombatWindow_Btn_Click(object sender, RoutedEventArgs e)
        {
            CombatWindow CombatWdw = new CombatWindow();
            CombatWdw.Show();
        }

        private void DiceMachineWindow_Btn_Click(object sender, RoutedEventArgs e)
        {
            DiceMachine DiceMachineWdw = new DiceMachine();
            DiceMachineWdw.Show();
        }

        private void BackgroundWindow_Btn_Click(object sender, RoutedEventArgs e)
        {
            BackgroundWindow BackgroundWdw = new BackgroundWindow();
            BackgroundWdw.Show();
        }

        private void InventoryWindow_Btn_Click(object sender, RoutedEventArgs e)
        {
            InventoryWindow InventoryWdw = new InventoryWindow();
            InventoryWdw.Show();
        }

        private void MerchantWindow_Btn_Click(object sender, RoutedEventArgs e)
        {
            MerchantWindow MerchantWdw = new MerchantWindow();
            MerchantWdw.Show();
        }

        #endregion

        #region MAIN FUNCTIONS FOR HANDLING USER INPUT, ENABLING AND DISABLING UI-ELEMENTS
        private void CreateCharacter()
        {
            Reset_Form();

            if (IsMainMenu_Active == true)
            {
                Deactivate_SideBarMenu_Buttons();
            }

            FirstLevel();
            EnableUIForUserInput();
        }

        private void Reset_Form()
        {
            Reset_CharacterInputs();
            Reset_HP_Panel();

            Deactivate_IniRolls();
            Reset_IniPanel();

            Clear_AllValues();
        }

        private void ApplyCharacter()
        {
            Check_Values();
            
            if(!hasError)
            {
                ApplyButton.IsEnabled = false;
                Activate_SideBarMenu_Buttons();
                Deactivate_CharacterInputs();
                SubmitCharacter_byUserInput();
                Init_HPHD_Panel();
                Deactivate_Scores_and_Calculate_AbilityModifiers_byUserInput();
                Activate_IniRolls();

                Activate_SavingThrows();
                Activate_SkillChecks();
                Init_AC_Update();

                Activate_Checks();
            }

            else
            {
                MessageBox.Show($"A Value you have entered in one of the number-fields is invalid.\nPlease make sure all values are integers (numbers without decimals)\nand within the range of 1 and 20.");
            }
        }        

        public void Load_Character()
        {            
            Reset_Form();
            ApplyButton.IsEnabled = false;

            Deactivate_CharacterInputs();

            SubmitCharacter_OnLoad();
            
            Set_Level_and_HP_Panel();
            
            Deactivate_Scores_and_Show_AbilityModifiers();
            Show_AbilityScores();

            Deactivate_SaveProf_CheckBoxes();
            Show_SaveModifiers();
            Set_SaveProficiency_Buttons();

            Deactivate_SkillProficiency_CheckBoxes();
            Show_SkillModifiers();
            Set_SkillProficiency_Buttons();
           
            Activate_IniRolls();
           
            Init_AC_Update();

            Activate_Checks();
        }

        
        #endregion

        public void Activate_SideBarMenu_Buttons()
        {
            foreach(Button MenuBtn in MainMenuBtns)
            {
                MenuBtn.IsEnabled = true;
            }

            IsMainMenu_Active = true;
        }

        public void Deactivate_SideBarMenu_Buttons()
        {
            foreach (Button MenuBtn in MainMenuBtns)
            {
                if(MenuBtn.Name != "LoadCharBtn")
                {
                    MenuBtn.IsEnabled = false;
                }
            }

            IsMainMenu_Active = false;
        }

        private void FirstLevel()
        {
            SheetManager.CS_Manager_Inst.character.Level = 1;
            LevelText.Text = SheetManager.CS_Manager_Inst.character.Level.ToString();
            LevelUp_Btn.IsEnabled = true;            
            SheetManager.CS_Manager_Inst.character.Update_HitDice();
            HDBox.Text = SheetManager.CS_Manager_Inst.character.HitDice.ToString();
            ProficiencyBonusBox.Text = SheetManager.CS_Manager_Inst.character.ProficiencyBonus.ToString();
        }

        private void EnableUIForUserInput()
        {
            Activate_CharacterInputs();
            Activate_HP_Panel();
            Activate_ScoreInput();
            Activate_SaveProf_CheckBoxes();
            Activate_SkillProficiency_CheckBoxes();
        }

        private void Init_HPHD_Panel()
        {
            SheetManager.CS_Manager_Inst.character.Set_MaxHP_byText(MaxHPBox.Text);
            MaxHPBox.IsEnabled = false;            

            SheetManager.CS_Manager_Inst.character.Init_HP_HD();

            SheetManager.CS_Manager_Inst.character.hpChanged += Update_HP;
            SheetManager.CS_Manager_Inst.character.tempHPChanged += Update_TempHP;
            SheetManager.CS_Manager_Inst.Init_TempHPCallback();

            CurrHPBox.Text = SheetManager.CS_Manager_Inst.character.CurrentHP.ToString();
            CurrHDBox.Text = SheetManager.CS_Manager_Inst.character.CurrentHitDice.ToString();
        }

        private void Set_Level_and_HP_Panel()
        {
            LevelText.Text = SheetManager.CS_Manager_Inst.character.Level.ToString();
            LevelUp_Btn.IsEnabled = true;

            SheetManager.CS_Manager_Inst.character.hpChanged += Update_HP;
            SheetManager.CS_Manager_Inst.character.tempHPChanged += Update_TempHP;
            SheetManager.CS_Manager_Inst.Init_TempHPCallback();

            MaxHPBox.Text = SheetManager.CS_Manager_Inst.character.MaxHP.ToString();
            CurrHPBox.Text = SheetManager.CS_Manager_Inst.character.CurrentHP.ToString();
            TempHPBox.Text = SheetManager.CS_Manager_Inst.character.TempHP.ToString();

            HDBox.Text = SheetManager.CS_Manager_Inst.character.HitDice.ToString();
            CurrHDBox.Text = SheetManager.CS_Manager_Inst.character.CurrentHitDice.ToString();

            ProficiencyBonusBox.Text = SheetManager.CS_Manager_Inst.character.ProficiencyBonus.ToString();
        }

        

        // I could have grabbed these elements on initialization of the window - like e. g. the ability score textboxes - to set them all at once with a loop
        // but, since that wouldn't make the code very much shorter - on the contrary - I opted against this method and just set these manually
        private void Activate_CharacterInputs()
        {
            CharNameText.IsEnabled = true;
            PlayerNameText.IsEnabled = true;
            AlignmentBox.IsEnabled = true;
            BackgroundBox.IsEnabled = true;
            RaceBox.IsEnabled = true;
            SubRaceBox.IsEnabled = true;
            ClassBox.IsEnabled = true;
        }

        private void Deactivate_CharacterInputs()
        {
            CharNameText.IsEnabled = false;
            PlayerNameText.IsEnabled = false;

            AlignmentBox.IsEnabled = false;
            BackgroundBox.IsEnabled = false;

            RaceBox.IsEnabled = false;
            SubRaceBox.IsEnabled = false;
            ClassBox.IsEnabled = false;
        }


        private void Reset_CharacterInputs()
        {
            CharNameText.Clear();        
            PlayerNameText.Clear();

            RaceBox.Clear();
            SubRaceBox.Clear();

            ClassBox.Clear();

            AlignmentBox.SelectedIndex = 0;
            BackgroundBox.Clear();

            LevelText.Clear();           
        }        

        private void SubmitCharacter_byUserInput()
        {
            SheetManager.CS_Manager_Inst.character.PlayerName = PlayerNameText.Text;
            SheetManager.CS_Manager_Inst.character.CharacterName = CharNameText.Text;
            SheetManager.CS_Manager_Inst.character.RaceName = RaceBox.Text;
            SheetManager.CS_Manager_Inst.character.SubraceName = SubRaceBox.Text;
            SheetManager.CS_Manager_Inst.character.ClassName = ClassBox.Text;
            SheetManager.CS_Manager_Inst.character.Alignment = AlignmentBox.Text;
            SheetManager.CS_Manager_Inst.character.Background = BackgroundBox.Text;
        }
        
        private void SubmitCharacter_OnLoad()
        {
            PlayerNameText.Text = SheetManager.CS_Manager_Inst.character.PlayerName;
            CharNameText.Text = SheetManager.CS_Manager_Inst.character.CharacterName;
            RaceBox.Text = SheetManager.CS_Manager_Inst.character.RaceName;
            SubRaceBox.Text = SheetManager.CS_Manager_Inst.character.SubraceName;
            ClassBox.Text = SheetManager.CS_Manager_Inst.character.ClassName;
            AlignmentBox.Text = SheetManager.CS_Manager_Inst.character.Alignment;
            BackgroundBox.Text = SheetManager.CS_Manager_Inst.character.Background;
        }

        private void Activate_HP_Panel()
        {
            MaxHPBox.IsEnabled = true;
            CurrHPBox.IsEnabled = true;
            TempHPBox.IsEnabled = true;
            CurrHDBox.IsEnabled = true;            
        }

        private void Activate_ScoreInput()
        {            
            foreach (TextBox ScoreBox in AbilityScoreBoxes)
            {
                ScoreBox.IsEnabled = true;
            }
        }        

        private void Reset_HP_Panel()
        {
            MaxHPBox.Clear();
            CurrHPBox.Clear();
            TempHPBox.Clear();
            HDBox.Clear();
            CurrHDBox.Clear();            
        }

        // Initiative will hereafter sometimes be abbreviated to 'Ini' as is also common for the game
        private void Reset_IniPanel()
        {
            InitiativeBonusBox.Clear();
            InitiativeResultBox.Clear();
            AC_Box.Clear();
            BaseSpeedBox.Clear();
            ProficiencyBonusBox.Clear();
        }

        private void Show_AbilityScores()
        {
            StrScoreBox.Text = SheetManager.CS_Manager_Inst.character.Strength.Score.ToString();
            DexScoreBox.Text = SheetManager.CS_Manager_Inst.character.Dexterity.Score.ToString();
            ConScoreBox.Text = SheetManager.CS_Manager_Inst.character.Constitution.Score.ToString();
            IntScoreBox.Text = SheetManager.CS_Manager_Inst.character.Intelligence.Score.ToString();
            WisScoreBox.Text = SheetManager.CS_Manager_Inst.character.Wisdom.Score.ToString();
            ChaScoreBox.Text = SheetManager.CS_Manager_Inst.character.Charisma.Score.ToString();
        }

        private bool CheckValue(string textBoxTxt)
        {

            if (int.TryParse(textBoxTxt, out int number))
            {
                if(number > 0 && number < 21)
                {
                    return true;
                }

                else
                {
                    return false;
                }
            }

            else
            {
                return false;
            }
        }

        private void Check_Values()
        {            
            if(CheckValue(MaxHPBox.Text))
            {                
                bool AreScoreValuesCorrect = AbilityScoreBoxes.TrueForAll(ScoreBox => CheckValue(ScoreBox.Text));

                if(AreScoreValuesCorrect)
                {
                    hasError = false;
                }

                else
                {
                    hasError = true;
                }
            }

            else
            {
                hasError = true;
            }
        }

        private void Deactivate_Scores_and_Calculate_AbilityModifiers_byUserInput()
        {

            foreach(TextBox ScoreBox in AbilityScoreBoxes)
            {
                ScoreBox.IsEnabled = false;
            }

            SheetManager.CS_Manager_Inst.character.Set_StrScore_byText(StrScoreBox.Text);
            SheetManager.CS_Manager_Inst.character.Strength.Calculate_Modifier();
            StrModifierBox.Text = SheetManager.CS_Manager_Inst.character.Strength.Modifier.ToString();

            SheetManager.CS_Manager_Inst.character.Set_DexScore_byText(DexScoreBox.Text);
            SheetManager.CS_Manager_Inst.character.Dexterity.Calculate_Modifier();
            DexModifierBox.Text = SheetManager.CS_Manager_Inst.character.Dexterity.Modifier.ToString();

            SheetManager.CS_Manager_Inst.character.Set_ConScore_byText(ConScoreBox.Text);
            SheetManager.CS_Manager_Inst.character.Constitution.Calculate_Modifier();
            ConModifierBox.Text = SheetManager.CS_Manager_Inst.character.Constitution.Modifier.ToString();

            SheetManager.CS_Manager_Inst.character.Set_IntScore_byText(IntScoreBox.Text);
            SheetManager.CS_Manager_Inst.character.Intelligence.Calculate_Modifier();
            IntModifierBox.Text = SheetManager.CS_Manager_Inst.character.Intelligence.Modifier.ToString();

            SheetManager.CS_Manager_Inst.character.Set_WisScore_byText(WisScoreBox.Text);
            SheetManager.CS_Manager_Inst.character.Wisdom.Calculate_Modifier();
            WisModifierBox.Text = SheetManager.CS_Manager_Inst.character.Wisdom.Modifier.ToString();

            SheetManager.CS_Manager_Inst.character.Set_ChaScore_byText(ChaScoreBox.Text);
            SheetManager.CS_Manager_Inst.character.Charisma.Calculate_Modifier();
            ChaModifierBox.Text = SheetManager.CS_Manager_Inst.character.Charisma.Modifier.ToString();            
            
        }               

        private void Deactivate_Scores_and_Show_AbilityModifiers()
        {
            MaxHPBox.IsEnabled = false;

            foreach(TextBox ScoreBox in AbilityScoreBoxes)
            {
                ScoreBox.IsEnabled = false;
            }

            StrModifierBox.Text = SheetManager.CS_Manager_Inst.character.Strength.Modifier.ToString();
            DexModifierBox.Text = SheetManager.CS_Manager_Inst.character.Dexterity.Modifier.ToString();
            ConModifierBox.Text = SheetManager.CS_Manager_Inst.character.Constitution.Modifier.ToString();
            IntModifierBox.Text = SheetManager.CS_Manager_Inst.character.Intelligence.Modifier.ToString();
            WisModifierBox.Text = SheetManager.CS_Manager_Inst.character.Wisdom.Modifier.ToString();
            ChaModifierBox.Text = SheetManager.CS_Manager_Inst.character.Charisma.Modifier.ToString();            
        }

        private void Activate_IniRolls()
        {
            SheetManager.CS_Manager_Inst.character.Set_IniBonus();
            InitiativeBonusBox.Text = SheetManager.CS_Manager_Inst.character.InitiativeBonus.ToString();
            InitiativeBtn.IsEnabled = true;
        }

        private void Deactivate_IniRolls()
        {
            InitiativeBtn.IsEnabled = false;
        }

        private void Init_AC_Update()
        {
            SheetManager.CS_Manager_Inst.character.acChanged += Update_AC;
            SheetManager.CS_Manager_Inst.character.Calculate_AC();            
        }    

        private void Activate_SaveProf_CheckBoxes()
        {
            foreach(CheckBox SaveCB in SavingThrowProficiencyCheckBoxes)
            {
                SaveCB.IsEnabled = true;
            }
        }

        private void Deactivate_SaveProf_CheckBoxes()
        {
            foreach (CheckBox SaveCB in SavingThrowProficiencyCheckBoxes)
            {
                SaveCB.IsEnabled = false;
            }
        }       

        private void Activate_Checks()
        {
            foreach(Button CheckBtn in DiceRollBtns)
            {
                CheckBtn.IsEnabled = true;
            }
        }

        private void Deactivate_Checks()
        {
            foreach (Button CheckBtn in DiceRollBtns)
            {
                CheckBtn.IsEnabled = false;
            }
        }

        private void Clear_AllValues()
        {
            Clear_AllNumberBoxes();
            Clear_AllCheckBoxes();
        }

        private void Clear_AllNumberBoxes()
        {
            foreach(TextBox NumberBox in MainSheetNumberBoxes)
            {
                NumberBox.Clear();
            }
        }       
        
        private void Clear_AllCheckBoxes()
        {
            foreach(CheckBox ProficiencyCheckBox in MainSheetCheckBoxes)
            {
                ProficiencyCheckBox.IsChecked = false;
            }
        }

        private void Transfer_SavingThrows()
        {
            SheetManager.CS_Manager_Inst.character.Set_SaveBaseValues();
            SheetManager.CS_Manager_Inst.character.Set_SaveProficiencies(StrSaveProficiency_CB.IsChecked.Value, DexSaveProficiency_CB.IsChecked.Value, ConSaveProficiency_CB.IsChecked.Value, IntSaveProficiency_CB.IsChecked.Value, WisSaveProficiency_CB.IsChecked.Value, ChaSaveProficiency_CB.IsChecked.Value);
        }

        private void Calculate_SaveModifiers()
        {
            SheetManager.CS_Manager_Inst.character.CalculateSavingThrowModifiers();
        }

        private void Set_SaveProficiency_Buttons()
        {
            if(SheetManager.CS_Manager_Inst.character.STR_Save.IsProficient)
            {
                StrSaveProficiency_CB.IsChecked = true;
            }

            if (SheetManager.CS_Manager_Inst.character.DEX_Save.IsProficient)
            {
                DexSaveProficiency_CB.IsChecked = true;
            }

            if (SheetManager.CS_Manager_Inst.character.CON_Save.IsProficient)
            {
                ConSaveProficiency_CB.IsChecked = true;
            }

            if (SheetManager.CS_Manager_Inst.character.INT_Save.IsProficient)
            {
                IntSaveProficiency_CB.IsChecked = true;
            }

            if (SheetManager.CS_Manager_Inst.character.WIS_Save.IsProficient)
            {
                WisSaveProficiency_CB.IsChecked = true;
            }

            if (SheetManager.CS_Manager_Inst.character.CHA_Save.IsProficient)
            {
                ChaSaveProficiency_CB.IsChecked = true;
            }
        }

        private void Show_SaveModifiers()
        {
            StrSaveModifierBox.Text = SheetManager.CS_Manager_Inst.character.STR_Save.SaveModifier.ToString();
            DexSaveModifierBox.Text = SheetManager.CS_Manager_Inst.character.DEX_Save.SaveModifier.ToString();
            ConSaveModifierBox.Text = SheetManager.CS_Manager_Inst.character.CON_Save.SaveModifier.ToString();
            IntSaveModifierBox.Text = SheetManager.CS_Manager_Inst.character.INT_Save.SaveModifier.ToString();
            WisSaveModifierBox.Text = SheetManager.CS_Manager_Inst.character.WIS_Save.SaveModifier.ToString();
            ChaSaveModifierBox.Text = SheetManager.CS_Manager_Inst.character.CHA_Save.SaveModifier.ToString();
        }

        private void Activate_SavingThrows()
        {
            Deactivate_SaveProf_CheckBoxes();
            Transfer_SavingThrows();
            Calculate_SaveModifiers();
            Show_SaveModifiers();
        }

        private void Activate_SkillChecks()
        {
            Deactivate_SkillProficiency_CheckBoxes();
            Transfer_Skill_Values();
            Calculate_SkillModifiers();
            Show_SkillModifiers();
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
            if(SheetManager.CS_Manager_Inst.character.Acrobatics.IsProficient) {
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

        private void Activate_SkillProficiency_CheckBoxes()
        {
            foreach(CheckBox SkillProfBox in SkillProficiencyCheckBoxes)
            {
                SkillProfBox.IsEnabled = true;
            }
        }

        private void Deactivate_SkillProficiency_CheckBoxes()
        {
            foreach(CheckBox SkillProfBox in SkillProficiencyCheckBoxes)
            {
                SkillProfBox.IsEnabled = false;
            }
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

        private void Update_AC()
        {
            AC_Box.Text = SheetManager.CS_Manager_Inst.character.AC.ToString();
        }

        private void Update_HP()
        {
            CurrHPBox.Text = SheetManager.CS_Manager_Inst.character.CurrentHP.ToString();
        }

        private void Update_TempHP()
        {
            if(SheetManager.CS_Manager_Inst.character.TempHP > 0)
            {
                TempHPBox.Text = SheetManager.CS_Manager_Inst.character.TempHP.ToString();
            }

            else
            {
                TempHPBox.Text = null;
            }
        }

        private void LevelUpButton_Click(object sender, RoutedEventArgs e)
        {            
            SheetManager.CS_Manager_Inst.character.Level_Up();
            LevelText.Text = SheetManager.CS_Manager_Inst.character.Level.ToString();            
            HDBox.Text = SheetManager.CS_Manager_Inst.character.HitDice.ToString();
            ProficiencyBonusBox.Text = SheetManager.CS_Manager_Inst.character.ProficiencyBonus.ToString();
            
        }        

        private void IniButton_Click(object sender, RoutedEventArgs e)
        {
            InitiativeResultBox.Text = SheetManager.CS_Manager_Inst.Roll_for_Initiative().ToString();
        }

        public void StrButton_Click(object sender, RoutedEventArgs e)
        {
            StrCheckResultBox.Text = SheetManager.CS_Manager_Inst.character.Strength.Ability_Check().ToString();
        }

        public void DexButton_Click(object sender, RoutedEventArgs e)
        {
            DexCheckResultBox.Text = SheetManager.CS_Manager_Inst.character.Dexterity.Ability_Check().ToString();
        }

        public void ConButton_Click(object sender, RoutedEventArgs e)
        {
            ConCheckResultBox.Text = SheetManager.CS_Manager_Inst.character.Constitution.Ability_Check().ToString();
        }

        public void IntButton_Click(object sender, RoutedEventArgs e)
        {
            IntCheckResultBox.Text = SheetManager.CS_Manager_Inst.character.Intelligence.Ability_Check().ToString();
        }

        public void WisButton_Click(object sender, RoutedEventArgs e)
        {
            WisCheckResultBox.Text = SheetManager.CS_Manager_Inst.character.Wisdom.Ability_Check().ToString();
            
        }

        public void ChaButton_Click(object sender, RoutedEventArgs e)
        {
            ChaCheckResultBox.Text = SheetManager.CS_Manager_Inst.character.Charisma.Ability_Check().ToString();            
        }

        public void STR_Save_bt_Click(object sender, RoutedEventArgs e)
        {
            StrSaveResultBox.Text = SheetManager.CS_Manager_Inst.character.STR_Save.Make_SavingThrow().ToString();            
        }

        public void DEX_Save_bt_Click(object sender, RoutedEventArgs e)
        {
            DexSaveResultBox.Text = SheetManager.CS_Manager_Inst.character.DEX_Save.Make_SavingThrow().ToString();            
        }

        public void CON_Save_bt_Click(object sender, RoutedEventArgs e)
        {
            ConSaveResultBox.Text = SheetManager.CS_Manager_Inst.character.CON_Save.Make_SavingThrow().ToString();            
        }

        public void INT_Save_bt_Click(object sender, RoutedEventArgs e)
        {
            IntSaveResultBox.Text = SheetManager.CS_Manager_Inst.character.INT_Save.Make_SavingThrow().ToString();            
        }

        public void WIS_Save_bt_Click(object sender, RoutedEventArgs e)
        {
            WisSaveResultBox.Text = SheetManager.CS_Manager_Inst.character.WIS_Save.Make_SavingThrow().ToString();           
        }

        public void CHA_Save_bt_Click(object sender, RoutedEventArgs e)
        {
            ChaSaveResultBox.Text = SheetManager.CS_Manager_Inst.character.CHA_Save.Make_SavingThrow().ToString();            
        }

        public void AcrobaticsBT_Click(object sender, RoutedEventArgs e)
        {
            Acrobatics_Result.Text = SheetManager.CS_Manager_Inst.character.Acrobatics.SkillCheck().ToString();
            
        }

        public void AnimalHandlingBT_Click(object sender, RoutedEventArgs e)
        {
            AnimalHandling_Result.Text = SheetManager.CS_Manager_Inst.character.AnimalHandling.SkillCheck().ToString();
            
        }

        public void ArcanaBT_Click(object sender, RoutedEventArgs e)
        {
            Arcana_Result.Text = SheetManager.CS_Manager_Inst.character.Arcana.SkillCheck().ToString();
            
        }

        public void AthleticsBT_Click(object sender, RoutedEventArgs e)
        {
            Athletics_Result.Text = SheetManager.CS_Manager_Inst.character.Athletics.SkillCheck().ToString();
            
        }

        public void DeceptionBT_Click(object sender, RoutedEventArgs e)
        {
            Deception_Result.Text = SheetManager.CS_Manager_Inst.character.Deception.SkillCheck().ToString();
            
        }

        public void HistoryBT_Click(object sender, RoutedEventArgs e)
        {
            History_Result.Text = SheetManager.CS_Manager_Inst.character.History.SkillCheck().ToString();
            
        }

        public void InsightBT_Click(object sender, RoutedEventArgs e)
        {
            Insight_Result.Text = SheetManager.CS_Manager_Inst.character.Insight.SkillCheck().ToString();
            
        }

        public void IntimidationBT_Click(object sender, RoutedEventArgs e)
        {
            Intimidation_Result.Text = SheetManager.CS_Manager_Inst.character.Intimidation.SkillCheck().ToString();
            
        }

        public void InvestigationBT_Click(object sender, RoutedEventArgs e)
        {
            Investigation_Result.Text = SheetManager.CS_Manager_Inst.character.Investigation.SkillCheck().ToString();
            
        }

        public void MedicineBT_Click(object sender, RoutedEventArgs e)
        {
            Medicine_Result.Text = SheetManager.CS_Manager_Inst.character.Medicine.SkillCheck().ToString();
            
        }

        public void NatureBT_Click(object sender, RoutedEventArgs e)
        {
            Nature_Result.Text = SheetManager.CS_Manager_Inst.character.Nature.SkillCheck().ToString();
            
        }

        public void PerceptionBT_Click(object sender, RoutedEventArgs e)
        {
            Perception_Result.Text = SheetManager.CS_Manager_Inst.character.Perception.SkillCheck().ToString();
            
        }

        public void PerformanceBT_Click(object sender, RoutedEventArgs e)
        {
            Performance_Result.Text = SheetManager.CS_Manager_Inst.character.Performance.SkillCheck().ToString();
            
        }

        public void PersuasionBT_Click(object sender, RoutedEventArgs e)
        {
            Persuasion_Result.Text = SheetManager.CS_Manager_Inst.character.Persuasion.SkillCheck().ToString();
            
        }

        public void ReligionBT_Click(object sender, RoutedEventArgs e)
        {
            Religion_Result.Text = SheetManager.CS_Manager_Inst.character.Religion.SkillCheck().ToString();
            
        }

        public void SleightOfHandBT_Click(object sender, RoutedEventArgs e)
        {
            SleightOfHand_Result.Text = SheetManager.CS_Manager_Inst.character.SleightOfHand.SkillCheck().ToString();
            
        }

        public void StealthBT_Click(object sender, RoutedEventArgs e)
        {
            Stealth_Result.Text = SheetManager.CS_Manager_Inst.character.Stealth.SkillCheck().ToString();
            
        }

        public void SurvivalBT_Click(object sender, RoutedEventArgs e)
        {
            Survival_Result.Text = SheetManager.CS_Manager_Inst.character.Survival.SkillCheck().ToString();
            
        }        
        
    }
}
