using System;
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

        public InventoryWindow InventoryWdw;

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

        const string UIElementStandardBorderBrushColor = "#FFABADB3";
        Brush LocalStandardBackgroundColor = Brushes.WhiteSmoke;

        Brush MarkBorderColor = Brushes.PaleVioletRed;
        Brush MarkBackgroundColor = Brushes.MintCream;
        
        #endregion

        #region CONSTRUCTOR
        public MainWindow()
        {
            InitializeComponent();

            if(mainWindow_Inst == null)
            {
                mainWindow_Inst = this;
            }

            Init_CharSheetWindows();
            Init_UIControls();
            
        }
        #endregion

        private void Init_CharSheetWindows()
        {
            InventoryWdw = new InventoryWindow();
        }


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
            InventoryWdw.Refresh_UI();
            InventoryWdw.Show();
        }

        private void MerchantWindow_Btn_Click(object sender, RoutedEventArgs e)
        {
            MerchantWindow MerchantWdw = new MerchantWindow();
            MerchantWdw.Show();
        }

        #endregion


        #region MAIN METHODS FOR HANDLING USER INPUT, ENABLING AND DISABLING UI-ELEMENTS
        private void CreateCharacter()
        {
            Reset_Form();

            if (IsMainMenu_Active == true)
            {
                Deactivate_SideBarMenu_Buttons();
            }

            Set_FirstLevel();
            EnableUIForUserInput();
        }

        private void Reset_Form()
        {
            Reset_CharacterInputs();
            Reset_HP_Panel();

            Deactivate_IniRolls();
            Reset_InitiativePanel();

            Clear_AllValues();
        }

        private void ApplyCharacter()
        {
            if (TryCharacterName())
            {
                if(TryRaceInput())
                {
                    if(TrySubraceInput())
                    {
                        if(TryAlignmentInput())
                        {
                           if(TryClassInput())
                            {
                                Check_Values();

                                if (!hasError)
                                {
                                    if(Try_Skill_and_SavingThrow_Checkboxes())
                                    {
                                        ApplyButton.IsEnabled = false;
                                        Commit_Character_and_Enable_Sheet();
                                    }
                                }

                                else
                                {
                                    MessageBox.Show($"A Value you have entered in one of the number-fields is invalid.\nPlease make sure all values are integers (numbers without decimals)\nand within the range of 1 and 20.");
                                }
                            }
                        }
                    }
                }
            }

            else
                MessageBox.Show("You entered no name for your character. But they should have one! Please enter a name for your character.");
        }        

        private void Commit_Character_and_Enable_Sheet()
        {
            Activate_SideBarMenu_Buttons();
            Deactivate_CharacterInputs();
            UnmarkUIElementsBlockUserInput();
            Commit_Character_byUserInput();
            Init_HPHD_Panel();
            Deactivate_ScoreInput_and_Calculate_AbilityModifiers_byUserInput();
            Activate_IniRolls();

            Activate_SavingThrows();
            Activate_SkillChecks();
            Init_AC_Update();

            Activate_Checks();
        }

        public void Load_Character()
        {            
            Reset_Form();
            ApplyButton.IsEnabled = false;

            Deactivate_CharacterInputs();

            Commit_Character_OnLoad();
            
            Set_Level_and_HP_Panel_OnLoad();
            
            Deactivate_Scores_and_Show_AbilityModifiers();
            Show_AbilityScores();

            Deactivate_SaveProf_CheckBoxes();
            Show_SaveModifiers();
            Set_SaveProficiency_CheckBoxes();

            Deactivate_SkillProficiency_CheckBoxes();
            Show_SkillModifiers();
            Set_SkillProficiency_CheckBoxes();
           
            Activate_IniRolls();
           
            Init_AC_Update();

            Activate_Checks();

            if(InventoryWdw.IsVisible)
                InventoryWdw.Refresh_UI();
        }


        #endregion

        #region METHODS ENABLING AND DISABLING UI-ELEMENTS/ USER INPUT
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

        private void EnableUIForUserInput()
        {
            Activate_CharacterInputs();
            MarkUIElementsForUserInput();
            MarkSavingThrowCheckBoxes();
            Activate_HP_Panel();
            Activate_ScoreInput();
            Activate_SaveProf_CheckBoxes();
            Activate_SkillProficiency_CheckBoxes();
        }

        // One could have grabbed these elements on initialization of the window - like e. g. the ability score textboxes - to set them all at once with a loop
        // but, since that wouldn't make the code shorter - on the contrary - these are just set manually here.
        private void Activate_CharacterInputs()
        {
            CharNameText.IsEnabled = true;

            PlayerNameText.IsEnabled = true;

            AlignmentBox.IsEnabled = true;
            BackgroundBox.IsEnabled = true;

            RaceBox.IsEnabled = true;
            RaceMenu_CoBo.IsEnabled = true;
            SubRaceBox.IsEnabled = true;
            SubRaceMenu_CoBo.IsEnabled = true;

            ClassBox.IsEnabled = true;
            ClassMenu_CoBo.IsEnabled = true;
        }

        private void Deactivate_CharacterInputs()
        {
            CharNameText.IsEnabled = false;
            PlayerNameText.IsEnabled = false;

            AlignmentBox.IsEnabled = false;
            BackgroundBox.IsEnabled = false;

            RaceBox.IsEnabled = false;
            RaceMenu_CoBo.IsEnabled = false;
            SubRaceBox.IsEnabled = false;
            SubRaceMenu_CoBo.IsEnabled = false;

            ClassBox.IsEnabled = false;
            ClassMenu_CoBo.IsEnabled = false;
        }

        private void MarkUIElementsForUserInput()
        {
            MarkCharacterInputs();
            MarkAbilityInputs();
            MarkSavingThrowCheckBoxes();
            MarkSkillCheckBoxes();
        }

        private void UnmarkUIElementsBlockUserInput()
        {
            UnmarkCharacterInputs();
            UnmarkAbilityInputs();
            UnmarkSavingThrowCheckboxes();
            UnmarkSkillCheckboxes();
        }

        private void MarkCharacterInputs()
        {
            CharNameText.BorderBrush = MarkBorderColor;
            CharNameText.Background = MarkBackgroundColor;

            PlayerNameText.BorderBrush = MarkBorderColor;
            PlayerNameText.Background = MarkBackgroundColor;

            RaceCoboBorder.BorderBrush = MarkBorderColor;
            ClassCoBoBorder.BorderBrush = MarkBorderColor;
            AlignmentCoBoBorder.BorderBrush = MarkBorderColor;

            MaxHPBox.BorderBrush = MarkBorderColor;
            MaxHPBox.Background = MarkBackgroundColor;
        }

        private void UnmarkCharacterInputs()
        {
            BrushConverter bc = new BrushConverter();

            CharNameText.BorderBrush = bc.ConvertFromString(UIElementStandardBorderBrushColor) as Brush;
            CharNameText.Background = LocalStandardBackgroundColor;

            PlayerNameText.BorderBrush = bc.ConvertFromString(UIElementStandardBorderBrushColor) as Brush;
            PlayerNameText.Background = LocalStandardBackgroundColor;

            RaceCoboBorder.BorderBrush = Brushes.White;
            ClassCoBoBorder.BorderBrush = Brushes.White;
            AlignmentCoBoBorder.BorderBrush = Brushes.White;

            MaxHPBox.BorderBrush = bc.ConvertFromString(UIElementStandardBorderBrushColor) as Brush;
            MaxHPBox.Background = LocalStandardBackgroundColor;
        }

        private void MarkAbilityInputs()
        {
            foreach(TextBox AbilityBox in AbilityScoreBoxes)
            {
                AbilityBox.BorderBrush = MarkBorderColor;
                AbilityBox.Background = MarkBackgroundColor;
            }
        }

        private void UnmarkAbilityInputs()
        {
            BrushConverter bc = new BrushConverter();
            foreach (TextBox AbilityBox in AbilityScoreBoxes)
            {
                AbilityBox.BorderBrush = bc.ConvertFromString(UIElementStandardBorderBrushColor) as Brush;
                AbilityBox.Background = LocalStandardBackgroundColor;
            }
        }

        private void MarkSavingThrowCheckBoxes()
        {
            foreach(CheckBox SaveCB in SavingThrowProficiencyCheckBoxes)
            {
                SaveCB.BorderBrush = MarkBorderColor;
            }
        }

        private void UnmarkSavingThrowCheckboxes()
        {
            BrushConverter bc = new BrushConverter();
            foreach (CheckBox SaveCB in SavingThrowProficiencyCheckBoxes)
            {
                SaveCB.BorderBrush = bc.ConvertFromString(UIElementStandardBorderBrushColor) as Brush;
            }
        }

        private void MarkSkillCheckBoxes()
        {
            foreach (CheckBox SaveCB in SkillProficiencyCheckBoxes)
            {
                SaveCB.BorderBrush = MarkBorderColor;
            }
        }

        private void UnmarkSkillCheckboxes()
        {
            BrushConverter bc = new BrushConverter();
            foreach (CheckBox SaveCB in SkillProficiencyCheckBoxes)
            {
                SaveCB.BorderBrush = bc.ConvertFromString(UIElementStandardBorderBrushColor) as Brush;
            }
        }

        private void RaceMenu_CoBo_DropDownClosed(object sender, EventArgs e)
        {
            if (RaceMenu_CoBo.Text == "Custom Race")
            {
                CustomRacePanel.Visibility = Visibility.Visible;
                RaceBox.IsEnabled = true;
            }

            else
            {
                CustomRacePanel.Visibility = Visibility.Hidden;
                RaceBox.IsEnabled = false;
                RaceBox.Clear();
            }
        }

        private void SubRaceMenu_CoBo_DropDownClosed(object sender, EventArgs e)
        {
            if (SubRaceMenu_CoBo.Text == "Custom Subrace")
            {
                CustomSubracePanel.Visibility = Visibility.Visible;
                SubRaceBox.IsEnabled = true;
            }

            else
            {
                CustomSubracePanel.Visibility = Visibility.Hidden;
                SubRaceBox.IsEnabled = false;
                SubRaceBox.Clear();
            }
        }

        private void ClassMenu_CoBo_DropDownClosed(object sender, EventArgs e)
        {
            if (ClassMenu_CoBo.Text == "Custom Class")
            {
                CustomClassPanel.Visibility = Visibility.Visible;
                ClassBox.IsEnabled = true;
            }

            else
            {
                CustomClassPanel.Visibility = Visibility.Hidden;
                ClassBox.IsEnabled = false;
                ClassBox.Clear();
            }
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

        private void Activate_SaveProf_CheckBoxes()
        {
            foreach (CheckBox SaveCB in SavingThrowProficiencyCheckBoxes)
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
            foreach (Button CheckBtn in DiceRollBtns)
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

        private void Activate_SkillProficiency_CheckBoxes()
        {
            foreach (CheckBox SkillProfBox in SkillProficiencyCheckBoxes)
            {
                SkillProfBox.IsEnabled = true;
            }
        }

        private void Deactivate_SkillProficiency_CheckBoxes()
        {
            foreach (CheckBox SkillProfBox in SkillProficiencyCheckBoxes)
            {
                SkillProfBox.IsEnabled = false;
            }
        }
        #endregion

        #region METHODS FOR CLEARING/ RESETTING INPUT FIELDS 
        private void Reset_CharacterInputs()
        {
            CharNameText.Clear();
            PlayerNameText.Clear();

            AlignmentBox.SelectedIndex = 0;
            BackgroundBox.Clear();

            RaceBox.Clear();
            RaceMenu_CoBo.SelectedIndex = 0;

            SubRaceBox.Clear();
            SubRaceMenu_CoBo.SelectedIndex = 0;

            ClassBox.Clear();
            ClassMenu_CoBo.SelectedIndex = 0;            

            LevelText.Clear();
        }

        private void Reset_InitiativePanel()
        {
            InitiativeBonusBox.Clear();
            InitiativeResultBox.Clear();
            AC_Box.Clear();
            BaseSpeedBox.Clear();
            ProficiencyBonusBox.Clear();
        }

        private void Reset_HP_Panel()
        {
            MaxHPBox.Clear();
            CurrHPBox.Clear();
            TempHPBox.Clear();
            HDBox.Clear();
            CurrHDBox.Clear();
        }        

        private void Clear_AllValues()
        {
            Clear_AllNumberBoxes();
            Clear_AllCheckBoxes();
        }

        private void Clear_AllNumberBoxes()
        {
            foreach (TextBox NumberBox in MainSheetNumberBoxes)
            {
                NumberBox.Clear();
            }
        }

        private void Clear_AllCheckBoxes()
        {
            foreach (CheckBox ProficiencyCheckBox in MainSheetCheckBoxes)
            {
                ProficiencyCheckBox.IsChecked = false;
            }
        }
        #endregion


        #region METHOD(S) FOR CHECKING USER INPUT

        private bool TryCharacterName()
        {
            if(CharNameText.Text.Length > 0)
            {
                if (CharNameText.Text.Length <= 2)
                {
                    const string nameQuestion = "You entered 2 or less characters for the name of your character.\nMost names have more than two characters.\nWould you like to continue?";
                    const string characterNameIssue = "Character Name";
                    var result = MessageBox.Show(nameQuestion, characterNameIssue, MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        return true;
                    }

                    else
                        return false;
                }

                else
                    return true;
            }

            else
                return false;
        }

        private bool TryRaceInput()
        {
            if (RaceMenu_CoBo.SelectedIndex == 0)
            {
                MessageBox.Show("You neither selected one of the standard races nor entered a custom one. Please select or enter a race for your character.");
                return false;
            }
            
            else
            {
                if (RaceMenu_CoBo.Text == "Custom Race")
                {
                    if (RaceBox.Text.Length == 0)
                    {
                        MessageBox.Show("You did not name your custom race. Please enter a name for your custom race or select one of the standard races.");
                        return false;
                    }

                    else if (RaceBox.Text.Length > 0 && RaceBox.Text.Length <= 2)
                    {
                        string characterQuestion = "You entered less than three characters for your custom Race. Do you wish to proceed?";
                        string caption = "Custom Race";

                        var result = MessageBox.Show(characterQuestion, caption, MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                            return true;
                        else
                            return false;
                    }

                    else
                        return true;
                }

                else
                    return true;
            }            
        }

        private bool TrySubraceInput()
        {
            if (SubRaceMenu_CoBo.Text == "Custom Subrace")
            {
                if (SubRaceBox.Text.Length == 0)
                {
                    MessageBox.Show("You entered no name for your custom Subrace. Please enter one and then click 'apply' again.");
                    return false;
                }

                else if (SubRaceBox.Text.Length > 0 && SubRaceBox.Text.Length <= 2)
                {
                    string characterQuestion = "You entered less than three characters for your custom Subrace. Do you wish to proceed?";
                    string caption = "Custom Subrace";

                    var result = MessageBox.Show(characterQuestion, caption, MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                        return true;
                    else
                        return false;
                }

                else
                    return true;
            }

            else
                return true;
        }

        private bool TryAlignmentInput()
        {
            if (AlignmentBox.SelectedIndex > 0)
                return true;

            else
            {
                MessageBox.Show("You selected no alignment for your character. But rarely anyone is truly unaligned. If so, select 'neutral' and than click apply again.");
                return false;
            }
        }

        private bool TryClassInput()
        {
            if(ClassMenu_CoBo.SelectedIndex == 0)
            {
                MessageBox.Show("You neither selected one of the standard classes nor entered a custom one. Please select or enter a class for your character. ");
                return false;
            }

            else
            {
                if (ClassMenu_CoBo.Text == "Custom Class")
                {
                    if (ClassBox.Text.Length == 0)
                    {
                        MessageBox.Show("You did not name your custom Class. Please enter a name for your custom Class or select one of the standard Class.");
                        return false;
                    }

                    else if (ClassBox.Text.Length > 0 && ClassBox.Text.Length <= 2)
                    {
                        string characterQuestion = "You entered less than three characters for your custom Class. Do you wish to proceed?";
                        string caption = "Custom Class";

                        var result = MessageBox.Show(characterQuestion, caption, MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                            return true;
                        else
                            return false;
                    }

                    else
                        return true;
                }

                else
                    return true;
            }
        }

        private bool CheckValue(string textBoxTxt)
        {

            if (int.TryParse(textBoxTxt, out int number))
            {
                // Ability Score Values in D&D can only be wthinin the range of 1 and 20.
                if (number > 0 && number < 21)
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
            if (CheckValue(MaxHPBox.Text))
            {
                bool AreScoreValuesValid= AbilityScoreBoxes.TrueForAll(ScoreBox => CheckValue(ScoreBox.Text));

                if (AreScoreValuesValid)
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

        private bool Try_Skill_and_SavingThrow_Checkboxes()
        {
            bool allCheckBoxesUnchecked = SavingThrowProficiencyCheckBoxes.TrueForAll(SavingThrowCheckBox => SavingThrowCheckBox.IsChecked == false);

            if(!allCheckBoxesUnchecked)
            {
                int numberOfCheckedCheckboxes = 0;

                foreach(CheckBox SaveCB in SavingThrowProficiencyCheckBoxes)
                {
                    if (SaveCB.IsChecked == true)
                        numberOfCheckedCheckboxes++;
                }

                if (numberOfCheckedCheckboxes >= 2)
                {
                    allCheckBoxesUnchecked = SkillProficiencyCheckBoxes.TrueForAll(SkillProficiencyCheckbox => SkillProficiencyCheckbox.IsChecked == false);

                    if(!allCheckBoxesUnchecked)
                    {
                        numberOfCheckedCheckboxes = 0;

                        foreach (CheckBox SkillCB in SkillProficiencyCheckBoxes)
                            if (SkillCB.IsChecked == true)
                                numberOfCheckedCheckboxes++;

                        if (numberOfCheckedCheckboxes >= 2)
                            return true;

                        else
                        {
                            MessageBox.Show("You have selected less than two Skill Proficiencies.\nEvery D&D-Character(-Class) is proficient in at least two Skills.\n(Usually, four or more: at least two for your Characters Class, potentially more for your Characters Race and7 or Background.)\nPlease select at least two Skill Proficiencies.");
                            return false;
                        }

                    }

                    else
                    {
                        MessageBox.Show("You have selected no Skill Proficiencies at all (not checked any of the little checkboxes next to the Skill names).\nEvery D&D-Character(-Class) is proficient in at least two Skills.\n(Usually, four or more: at least two for your Characters Class, potentially more for your Characters Race and7 or Background.)\nPlease select at least two Skill Proficiencies.");
                        return false;
                    }
                }

                else
                {
                    MessageBox.Show("You have selected less than two Saving Throw Proficiencies\nEvery D&D-Character(-Class) is proficient in at least two types of Saving Throws.\nPlease choose (at least) two Saving Throw proficiencies.");
                    return false;
                }
            }

            else
            {
                MessageBox.Show("You haven't selected any Saving Throw Proficiencies (not checked any of the little checkboxes next to the Saving Throw types).\nEvery D&D-Character(-Class) is proficient in at least two types of Saving Throws.\nPlease choose (at least) two Saving Throw proficiencies.");
                return false;
            }
        }

        #endregion


        #region METHODS FOR PASSING VALUES BETWEEN UI AND THE CHARACTER CLASS

        // For explanations of the calculations done here - the so called 'Modifiers' - see the 'Character'-Class, Ability, Saving Throw and Skill Classes
        // The current character in play is held by the 'SheetManager'-Class which serves as a mediator between the current character and other parts of the app 

        private void Commit_Character_byUserInput()
        {
            SheetManager.CS_Manager_Inst.character.PlayerName = PlayerNameText.Text;
            SheetManager.CS_Manager_Inst.character.CharacterName = CharNameText.Text;
            SelectRaceInput();
            SelectSubRaceInput();
            SelectClassInput();
            SheetManager.CS_Manager_Inst.character.Alignment = AlignmentBox.Text;
            SheetManager.CS_Manager_Inst.character.Background = BackgroundBox.Text;
        }

        private void SelectRaceInput()
        {
            if(RaceMenu_CoBo.SelectedIndex > 0 && RaceMenu_CoBo.Text != "Custom Race")
            {
                SheetManager.CS_Manager_Inst.character.RaceName = RaceMenu_CoBo.Text;
            }

            else if(RaceMenu_CoBo.Text == "Custom Race")
            {
                SheetManager.CS_Manager_Inst.character.RaceName = RaceBox.Text;
            }
        }

        private void SelectSubRaceInput()
        {
            if (SubRaceMenu_CoBo.SelectedIndex > 0 && SubRaceMenu_CoBo.Text != "Custom Subrace")
            {
                SheetManager.CS_Manager_Inst.character.SubraceName = SubRaceMenu_CoBo.Text;
            }

            else if (SubRaceMenu_CoBo.Text == "Custom Subrace")
            {
                SheetManager.CS_Manager_Inst.character.SubraceName = SubRaceBox.Text;
            }            
        }

        private void SelectClassInput()
        {
            if (ClassMenu_CoBo.SelectedIndex > 0 && ClassMenu_CoBo.Text != "Custom Class")
            {
                SheetManager.CS_Manager_Inst.character.ClassName = ClassMenu_CoBo.Text;
            }

            else if (ClassMenu_CoBo.Text == "Custom Class")
            {
                SheetManager.CS_Manager_Inst.character.ClassName = ClassBox.Text;
            }
        }

        private void Commit_Character_OnLoad()
        {
            PlayerNameText.Text = SheetManager.CS_Manager_Inst.character.PlayerName;
            CharNameText.Text = SheetManager.CS_Manager_Inst.character.CharacterName;
            Check_for_and_Select_StandardRace();
            Check_for_and_Select_StandardSubrace();
            Check_for_and_Select_StandardClass();
            AlignmentBox.Text = SheetManager.CS_Manager_Inst.character.Alignment;
            BackgroundBox.Text = SheetManager.CS_Manager_Inst.character.Background;
        }

        private void Check_for_and_Select_StandardRace()
        {
            int RaceIndex = 0;
            bool IsStandardRace = false;

            for(int i = 0; i < SheetManager.CS_Manager_Inst.StandardRaces.Length; i++)
            {
                if (SheetManager.CS_Manager_Inst.character.RaceName == SheetManager.CS_Manager_Inst.StandardRaces[i])
                {
                    RaceIndex = i + 1;
                    IsStandardRace = true;
                }                    
            }

            if (IsStandardRace)
            {
                RaceMenu_CoBo.SelectedIndex = RaceIndex;
                CustomRacePanel.Visibility = Visibility.Hidden;
            }                

            else
            {
                CustomRacePanel.Visibility = Visibility.Visible;
                RaceMenu_CoBo.SelectedIndex = 0;
                RaceBox.Text = SheetManager.CS_Manager_Inst.character.RaceName;
            }
        }

        private void Check_for_and_Select_StandardSubrace()
        {
            int SubraceIndex = 0;
            bool IsStandardSubrace = false;

            for (int i = 0; i < SheetManager.CS_Manager_Inst.StandardSubraces.Length; i++)
            {
                if (SheetManager.CS_Manager_Inst.character.SubraceName == SheetManager.CS_Manager_Inst.StandardSubraces[i])
                {
                    SubraceIndex = i + 1;
                    IsStandardSubrace = true;
                }
            }

            if (IsStandardSubrace)
            {
                SubRaceMenu_CoBo.SelectedIndex = SubraceIndex;
                CustomSubracePanel.Visibility = Visibility.Hidden;
            }
                
            else
            {
                CustomSubracePanel.Visibility = Visibility.Visible;
                SubRaceMenu_CoBo.SelectedIndex = 0;
                SubRaceBox.Text = SheetManager.CS_Manager_Inst.character.SubraceName;
            }
        }

        private void Check_for_and_Select_StandardClass()
        {

            int classIndex = 0;
            bool isStandardClass = false;

            for (int i = 0; i < SheetManager.CS_Manager_Inst.StandardClasses.Length; i++)
            {
                if (SheetManager.CS_Manager_Inst.character.ClassName == SheetManager.CS_Manager_Inst.StandardClasses[i])
                {
                    classIndex = i + 1;
                    isStandardClass = true;
                }
            }

            if (isStandardClass)
            {
                ClassMenu_CoBo.SelectedIndex = classIndex;
                CustomClassPanel.Visibility = Visibility.Hidden;
            }
                
            else
            {
                CustomClassPanel.Visibility = Visibility.Visible;
                ClassMenu_CoBo.SelectedIndex = 0;
                ClassBox.Text = SheetManager.CS_Manager_Inst.character.ClassName;
            }
        }

        private void Set_FirstLevel()
        {
            SheetManager.CS_Manager_Inst.character.Level = 1;
            LevelText.Text = SheetManager.CS_Manager_Inst.character.Level.ToString();
            LevelUp_Btn.IsEnabled = true;
            SheetManager.CS_Manager_Inst.character.Update_HitDice();
            HDBox.Text = SheetManager.CS_Manager_Inst.character.HitDice.ToString();
            ProficiencyBonusBox.Text = SheetManager.CS_Manager_Inst.character.ProficiencyBonus.ToString();
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

        private void Set_Level_and_HP_Panel_OnLoad()
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

        private void Update_HP()
        {
            CurrHPBox.Text = SheetManager.CS_Manager_Inst.character.CurrentHP.ToString();
        }

        private void Update_TempHP()
        {
            if (SheetManager.CS_Manager_Inst.character.TempHP > 0)
            {
                TempHPBox.Text = SheetManager.CS_Manager_Inst.character.TempHP.ToString();
            }

            else
            {
                TempHPBox.Text = null;
            }
        }

        private void Init_AC_Update()
        {
            SheetManager.CS_Manager_Inst.character.acChanged += Update_AC;
            SheetManager.CS_Manager_Inst.character.Calculate_AC();
        }

        private void Update_AC()
        {
            AC_Box.Text = SheetManager.CS_Manager_Inst.character.AC.ToString();
        }        

        private void Deactivate_ScoreInput_and_Calculate_AbilityModifiers_byUserInput()
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

        private void Show_AbilityScores()
        {
            StrScoreBox.Text = SheetManager.CS_Manager_Inst.character.Strength.Score.ToString();
            DexScoreBox.Text = SheetManager.CS_Manager_Inst.character.Dexterity.Score.ToString();
            ConScoreBox.Text = SheetManager.CS_Manager_Inst.character.Constitution.Score.ToString();
            IntScoreBox.Text = SheetManager.CS_Manager_Inst.character.Intelligence.Score.ToString();
            WisScoreBox.Text = SheetManager.CS_Manager_Inst.character.Wisdom.Score.ToString();
            ChaScoreBox.Text = SheetManager.CS_Manager_Inst.character.Charisma.Score.ToString();
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

        private void Activate_SavingThrows()
        {
            Deactivate_SaveProf_CheckBoxes();
            Set_SavingThrows();
            Calculate_SaveModifiers();
            Show_SaveModifiers();
        }

        private void Activate_SkillChecks()
        {
            Deactivate_SkillProficiency_CheckBoxes();
            Set_Skills();
            Calculate_SkillModifiers();
            Show_SkillModifiers();
        }

        private void Set_SavingThrows()
        {
            SheetManager.CS_Manager_Inst.character.Set_SaveAbilityBonuses();
            SheetManager.CS_Manager_Inst.character.Set_SaveProficiencies(StrSaveProficiency_CB.IsChecked.Value, DexSaveProficiency_CB.IsChecked.Value, ConSaveProficiency_CB.IsChecked.Value, IntSaveProficiency_CB.IsChecked.Value, WisSaveProficiency_CB.IsChecked.Value, ChaSaveProficiency_CB.IsChecked.Value);
        }

        private void Calculate_SaveModifiers()
        {
            SheetManager.CS_Manager_Inst.character.CalculateSavingThrowModifiers();
        }

        private void Set_SaveProficiency_CheckBoxes()
        {
            StrSaveProficiency_CB.IsChecked = SheetManager.CS_Manager_Inst.character.STR_Save.IsProficient;
            DexSaveProficiency_CB.IsChecked = SheetManager.CS_Manager_Inst.character.DEX_Save.IsProficient;
            ConSaveProficiency_CB.IsChecked = SheetManager.CS_Manager_Inst.character.CON_Save.IsProficient;
            IntSaveProficiency_CB.IsChecked = SheetManager.CS_Manager_Inst.character.INT_Save.IsProficient;
            WisSaveProficiency_CB.IsChecked = SheetManager.CS_Manager_Inst.character.WIS_Save.IsProficient;
            ChaSaveProficiency_CB.IsChecked = SheetManager.CS_Manager_Inst.character.CHA_Save.IsProficient;
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

        private void Set_Skills()
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

        private void Set_SkillProficiency_CheckBoxes()
        {
            AcrobaticsProf.IsChecked = SheetManager.CS_Manager_Inst.character.Acrobatics.IsProficient;            
            ArcanaProf.IsChecked = SheetManager.CS_Manager_Inst.character.Arcana.IsProficient;
            AnimalHandlingProf.IsChecked = SheetManager.CS_Manager_Inst.character.AnimalHandling.IsProficient;
            AthleticsProf.IsChecked = SheetManager.CS_Manager_Inst.character.Athletics.IsProficient;

            DeceptionProf.IsChecked = SheetManager.CS_Manager_Inst.character.Deception.IsProficient;

            HistoryProf.IsChecked = SheetManager.CS_Manager_Inst.character.History.IsProficient;

            InsightProf.IsChecked = SheetManager.CS_Manager_Inst.character.Insight.IsProficient;
            IntimidationProf.IsChecked = SheetManager.CS_Manager_Inst.character.Intimidation.IsProficient;
            InvestigationProf.IsChecked = SheetManager.CS_Manager_Inst.character.Investigation.IsProficient;

            MedicineProf.IsChecked = SheetManager.CS_Manager_Inst.character.Medicine.IsProficient;

            NatureProf.IsChecked = SheetManager.CS_Manager_Inst.character.Nature.IsProficient;

            PerceptionProf.IsChecked = SheetManager.CS_Manager_Inst.character.Perception.IsProficient;
            PerformanceProf.IsChecked = SheetManager.CS_Manager_Inst.character.Performance.IsProficient;
            PersuasionProf.IsChecked = SheetManager.CS_Manager_Inst.character.Persuasion.IsProficient;

            ReligionProf.IsChecked = SheetManager.CS_Manager_Inst.character.Religion.IsProficient;

            SleightOfHandProf.IsChecked = SheetManager.CS_Manager_Inst.character.SleightOfHand.IsProficient;
            StealthProf.IsChecked = SheetManager.CS_Manager_Inst.character.Stealth.IsProficient;
            SurvivalProf.IsChecked = SheetManager.CS_Manager_Inst.character.Survival.IsProficient;
        }        

        private void Show_SkillModifiers()
        {
            AcrobaticsTxt.Text = SheetManager.CS_Manager_Inst.character.Acrobatics.SkillModifier.ToString();            
            ArcanaTxt.Text = SheetManager.CS_Manager_Inst.character.Arcana.SkillModifier.ToString();
            AnimalHandlingTxt.Text = SheetManager.CS_Manager_Inst.character.AnimalHandling.SkillModifier.ToString();
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

        private void LevelUpButton_Click(object sender, RoutedEventArgs e)
        {            
            SheetManager.CS_Manager_Inst.character.Level_Up();
            LevelText.Text = SheetManager.CS_Manager_Inst.character.Level.ToString();            
            HDBox.Text = SheetManager.CS_Manager_Inst.character.HitDice.ToString();
            ProficiencyBonusBox.Text = SheetManager.CS_Manager_Inst.character.ProficiencyBonus.ToString();
            
        }
        #endregion


        #region DICE ROLL BUTTON EVENT HANDLER
        private void InitiativeButton_Click(object sender, RoutedEventArgs e)
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

        #endregion
    }
}
