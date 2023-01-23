using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DnD_CharSheet_5e
{
    /// <summary>
    /// Interaktionslogik für SpellsWindow.xaml
    /// </summary>

    /* THE SPELL-WINDOW-CLASS:
     * 
     * This Window serves the purpose of managing the 'Spells' of a player's character, as the the name suggests.
     * It is equivalent to the 3rd page of the officiel D&D paper Character-Sheets.
     * 
     * This Window-class works with items from the 'SpellDataBase' and the 'SpellList'-data bases or instances of the 'Spell'- and 'SpellList_Item'-classes.
     * 
     * Certain 'Character-Classes' (in the game of D&D, not in C#) are able to 'cast' Spells. 
     * But, a specific 'Character-Class' like 'Wizard' or 'Druid' may only choose from a limited number Spells 
     * from all the hundreds of Spells that exist in the game - represented here by the 'SpellDataBase' and the 'Spell'-class data container.
     * 
     * Therefore each of these 'Character-Classes' has a list of Spells to choose from 
     * - represented here by the 'SpellList'-data bases related to a specific type of 'Character-Class' (e. g. 'Wizard')
     * and the 'SpellList_Item'-class data container.
     * 
     * In an instance of the SpellsWindow a user/ player may choose a number of 'Spells' for the 'Character' they are currently playing - depending on the 'Character-Class' of this 'Character -
     * and add them to their own Spell List via interacting with UI. 
     * 
     * Information (= data) about those Spells is also visualized on UI therefore sparing the player the need of browsing the paper version of the Player's Handbook.
     * 
     * Finally, the player/user may 'cast' the available or chosen Spells which means technically, selecting 'Spells' among the UI-Elements and clicking on them.
     * The triggering event will effectively reduce the number of 'Spell Slots' available to the 'Character' of the player/ user - until refilled (= reset to the initial value).
     * 
     * Dependend on the 'Level' of a given 'Character' the player/ user has a number of so called 'Spell Slots' for each 'Spell Level' available.
     * These 'Slots' represent, most basically spoken, the number of times a 'Character' may 'cast' 'Spells' of a given 'Spell Level'.
     */

    public partial class SpellsWindow : Window
    {

        private SpellCasterClass CharacterCasterClass;

        private List<SlotPanelData> SpellSheetData = new List<SlotPanelData>();

        public List<SpellList_Item> Cantrips { set; get; }
        public List<SpellList_Item> First_LvlSpells { set; get; }
        public List<SpellList_Item> Second_LvlSpells { set; get; }
        public List<SpellList_Item> Third_LvlSpells { set; get; }
        public List<SpellList_Item> Fourth_LvlSpells { set; get; }
        public List<SpellList_Item> Fifth_LvlSpells { set; get; }
        public List<SpellList_Item> Sixth_LvlSpells { set; get; }
        public List<SpellList_Item> Seventh_LvlSpells { set; get; }
        public List<SpellList_Item> Eighth_LvlSpells { set; get; }
        public List<SpellList_Item> Ninth_LvlSpells { set; get; }

        List<TextBox> SpellSlotsTotalBoxes;
        List<TextBox> SpellsExpendedBoxes;
        
        List<Button> CastButtons;
        
        List<StackPanel> LevelPanels;
        List<StackPanel> SlotPanels;
        List<StackPanel> CancelBtnPanels;
        List<StackPanel> PreparedBoxesPanels;

        const string SpellSlotErrorMessage = "One of the values you entered for your spells slots is invalid.\nIt has to be an integer number above zero and below 50.\n" +
                    "Even if your character has special abilities, spellbooks, etc. which allow them to cast more than usual spells\n" +
                    "it likely impossible to have more than 50 spell slots.\n" +
                    "Usually there shouldn't be more than 4 spells slots per spell level:\n" +
                    "This is the number of spells slots a Wizard of level 20 has available for 1st Level Spells - without any special features considered.";

        const string AllSpellSlotsExpendedMessage = "You have no spell slots for this spell level left.\nRegain spell slots to cast again.";
        const string NoSpellSlotsForThisLevelMessage = "You seem to have no spell slots for this spell level.\nIf you want to change this click 'Edit Spell Slots'";


        public SpellsWindow()
        {
            InitializeComponent();
            FindCasterClass();
            SheetManager.CS_Manager_Inst.character.levelChanged += Calculate_CasterClass_Values;
            SheetManager.CS_Manager_Inst.character.levelChanged += Show_BaseValues;
            InitSpells();
            Init_UI();
        }

        private void FindCasterClass()
        {
            CharacterCasterClass = SheetManager.CS_Manager_Inst.theWeave.SpellCasterClasses.Find(casterClass => casterClass.SCC_Name == SheetManager.CS_Manager_Inst.character.ClassName);

            if (CharacterCasterClass != null)
            {
                FindSpellList();
                Calculate_CasterClass_Values();
                Show_BaseValues();
            }

            else
                CasterClassTxt.Text = "Character is not a spell caster";
        }

        private void FindSpellList()
        {
            CharacterCasterClass.CharacterSpellList = SheetManager.CS_Manager_Inst.theWeave.SpellLists.Find(casterClass => casterClass.SpellCasterClassName == SheetManager.CS_Manager_Inst.character.ClassName);
        }

        private void Calculate_CasterClass_Values()
        {
            CharacterCasterClass.Calculate_SpellSaveDC();
            CharacterCasterClass.Calculate_SpellAttackBonus();
        }

        private void Show_BaseValues()
        {
            CasterClassTxt.Text = CharacterCasterClass.SCC_Name;
            SpAbilityTxt.Text = CharacterCasterClass.SpellCastingAbility_Key;
            SaveDCTxt.Text = CharacterCasterClass.SpellSaveDC.ToString();
            SpAtkBonus.Text = CharacterCasterClass.SpellAttackBonus.ToString();
        }

        private void InitSpells()
        {            
            InitCantrips();            
            Init_1stLevelSpells();
            Init_2ndLevelSpells();            
            Init_3rdLevelSpells();            
            Init_4thLevelSpells();            
            Init_5thLevelSpells();            
            Init_6thLevelSpells();            
            Init_7thLevelSpells();            
            Init_8thLevelSpells();            
            Init_9thLevelSpells();

        }

        private void InitCantrips()
        {
            Cantrips = new List<SpellList_Item>();

            foreach (SpellList_Item sl_item in CharacterCasterClass.CharacterSpellList.SpellListItems)
            {
                if(sl_item.SpellLevel == 0)
                {
                    Cantrips.Add(sl_item);
                }
            }
        }

        private void Init_1stLevelSpells()
        {
            First_LvlSpells = new List<SpellList_Item>();

            foreach (SpellList_Item sl_item in CharacterCasterClass.CharacterSpellList.SpellListItems)
            {
                if (sl_item.SpellLevel == 1)
                {
                    First_LvlSpells.Add(sl_item);
                }
            }
        }

        private void Init_2ndLevelSpells()
        {
            Second_LvlSpells = new List<SpellList_Item>();

            foreach (SpellList_Item sl_item in CharacterCasterClass.CharacterSpellList.SpellListItems)
            {
                if (sl_item.SpellLevel == 2)
                {
                    Second_LvlSpells.Add(sl_item);
                }
            }
        }

        private void Init_3rdLevelSpells()
        {
            Third_LvlSpells = new List<SpellList_Item>();

            foreach (SpellList_Item sl_item in CharacterCasterClass.CharacterSpellList.SpellListItems)
            {
                if (sl_item.SpellLevel == 3)
                {
                    Third_LvlSpells.Add(sl_item);
                }
            }
        }

        private void Init_4thLevelSpells()
        {
            Fourth_LvlSpells = new List<SpellList_Item>();

            foreach (SpellList_Item sl_item in CharacterCasterClass.CharacterSpellList.SpellListItems)
            {
                if (sl_item.SpellLevel == 4)
                {
                    Fourth_LvlSpells.Add(sl_item);
                }
            }
        }

        private void Init_5thLevelSpells()
        {
            Fifth_LvlSpells = new List<SpellList_Item>();

            foreach (SpellList_Item sl_item in CharacterCasterClass.CharacterSpellList.SpellListItems)
            {
                if (sl_item.SpellLevel == 5)
                {
                    Fifth_LvlSpells.Add(sl_item);
                }
            }
        }

        private void Init_6thLevelSpells()
        {
            Sixth_LvlSpells = new List<SpellList_Item>();

            foreach (SpellList_Item sl_item in CharacterCasterClass.CharacterSpellList.SpellListItems)
            {
                if (sl_item.SpellLevel == 6)
                {
                    Sixth_LvlSpells.Add(sl_item);
                }
            }
        }

        private void Init_7thLevelSpells()
        {

            Seventh_LvlSpells = new List<SpellList_Item>();

            foreach (SpellList_Item sl_item in CharacterCasterClass.CharacterSpellList.SpellListItems)
            {
                if (sl_item.SpellLevel == 7)
                {
                    Seventh_LvlSpells.Add(sl_item);
                }
            }

        }

        private void Init_8thLevelSpells()
        {
            Eighth_LvlSpells = new List<SpellList_Item>();

            foreach (SpellList_Item sl_item in CharacterCasterClass.CharacterSpellList.SpellListItems)
            {
                if (sl_item.SpellLevel == 8)
                {
                    Eighth_LvlSpells.Add(sl_item);
                }
            }
        }

        private void Init_9thLevelSpells()
        {
            Ninth_LvlSpells = new List<SpellList_Item>();

            foreach (SpellList_Item sl_item in CharacterCasterClass.CharacterSpellList.SpellListItems)
            {
                if (sl_item.SpellLevel == 9)
                {
                    Ninth_LvlSpells.Add(sl_item);
                }
            }
        }

        private void Init_UI()
        {
            Init_SpellComboBoxes();
            Init_SpellSlotsTotal();
            Init_SpellsExpendend();
            Init_CastButtons();
            Init_LevelPanels();
            LoadSpellSheetData();
            EnableCastButtons();
        }

        private void Init_SpellSlotsTotal()
        {
            SpellSlotsTotalBoxes = new List<TextBox>();
            SpellSlotsTotalBoxes.Add(SlotsTotal_1stLvl_TB);
            SpellSlotsTotalBoxes.Add(SlotsTotal_2ndLvl_TB);
            SpellSlotsTotalBoxes.Add(SlotsTotal_3rdLvl_TB);
            SpellSlotsTotalBoxes.Add(SlotsTotal_4thLvl_TB);
            SpellSlotsTotalBoxes.Add(SlotsTotal_5thLvl_TB);
            SpellSlotsTotalBoxes.Add(SlotsTotal_6thLvl_TB);
            SpellSlotsTotalBoxes.Add(SlotsTotal_7thLvl_TB);
            SpellSlotsTotalBoxes.Add(SlotsTotal_8thLvl_TB);
            SpellSlotsTotalBoxes.Add(SlotsTotal_9thLvl_TB);
        }

        private void Init_SpellsExpendend()
        {
            SpellsExpendedBoxes = new List<TextBox>();
            SpellsExpendedBoxes.Add(SlotsExp_1stLvl_TB);
            SpellsExpendedBoxes.Add(SlotsExp_2ndLvl_TB);
            SpellsExpendedBoxes.Add(SlotsExp_3rdLvl_TB);
            SpellsExpendedBoxes.Add(SlotsExp_4thLvl_TB);
            SpellsExpendedBoxes.Add(SlotsExp_5thLvl_TB);
            SpellsExpendedBoxes.Add(SlotsExp_6thLvl_TB);
            SpellsExpendedBoxes.Add(SlotsExp_7thLvl_TB);
            SpellsExpendedBoxes.Add(SlotsExp_8thLvl_TB);
            SpellsExpendedBoxes.Add(SlotsExp_9thLvl_TB);
        }

        private void Init_CastButtons()
        {
            CastButtons = new List<Button>();
            CastButtons.Add(Cast_1stLvl_Btn);
            CastButtons.Add(Cast_2ndLvl_Btn);
            CastButtons.Add(Cast_3rdLvl_Btn);
            CastButtons.Add(Cast_4thLvl_Btn);
            CastButtons.Add(Cast_5thLvl_Btn);
            CastButtons.Add(Cast_6thLvl_Btn);
            CastButtons.Add(Cast_7thLvl_Btn);
            CastButtons.Add(Cast_8thLvl_Btn);
            CastButtons.Add(Cast_9thLvl_Btn);
        }

        private void Init_LevelPanels()
        {
            LevelPanels = new List<StackPanel>();
            Init_SlotPanels();
            Init_CancelBtnPanels();
            Init_PreparedBoxesPanels();
        }

        private void Init_SlotPanels()
        {
            SlotPanels = new List<StackPanel>();
            
            SlotPanels.Add(CantripSlotPanel);            
            SlotPanels.Add(FirstLvl_SlotPanel);
            SlotPanels.Add(SecondLvl_SlotPanel);
            SlotPanels.Add(ThirdLvl_SlotPanel);
            SlotPanels.Add(FourthLvl_SlotPanel);
            SlotPanels.Add(FifthLvl_SlotPanel);
            SlotPanels.Add(SixthLvl_SlotPanel);
            SlotPanels.Add(SeventhLvl_SlotPanel);
            SlotPanels.Add(EighthLvl_SlotPanel);
            SlotPanels.Add(NinthLvl_SlotPanel);

            foreach(StackPanel SlotPanel in SlotPanels)
            {
                SlotPanel.Tag = "SlotPanel";
                LevelPanels.Add(SlotPanel);
            }
            
        }

        private void Init_CancelBtnPanels()
        {
            CancelBtnPanels = new List<StackPanel>();

            CancelBtnPanels.Add(CantripCancelBtnsPanel);
            CancelBtnPanels.Add(FirstLvl_CancelBtnsPanel);
            CancelBtnPanels.Add(SecondLvl_CancelBtnsPanel);
            CancelBtnPanels.Add(ThirdLvl_CancelBtnsPanel);
            CancelBtnPanels.Add(FourthLvl_CancelBtnsPanel);
            CancelBtnPanels.Add(FifthLvl_CancelBtnsPanel);
            CancelBtnPanels.Add(SixthLvl_CancelBtnsPanel);
            CancelBtnPanels.Add(SeventhLvl_CancelBtnsPanel);
            CancelBtnPanels.Add(EighthLvl_CancelBtnsPanel);
            CancelBtnPanels.Add(NinthLvl_CancelBtnsPanel);

            foreach(StackPanel CancelBtnPanel in CancelBtnPanels)
            {
                CancelBtnPanel.Tag = "CancelBtnPanel";
                LevelPanels.Add(CancelBtnPanel);
            }
        }

        private void Init_PreparedBoxesPanels()
        {
            PreparedBoxesPanels = new List<StackPanel>();

            PreparedBoxesPanels.Add(FirstLvl_PreparedBoxesPanel);
            PreparedBoxesPanels.Add(SecondLvl_PreparedBoxesPanel);
            PreparedBoxesPanels.Add(ThirdLvl_PreparedBoxesPanel);
            PreparedBoxesPanels.Add(FourthLvl_PreparedBoxesPanel);
            PreparedBoxesPanels.Add(FifthLvl_PreparedBoxesPanel);
            PreparedBoxesPanels.Add(SixthLvl_PreparedBoxesPanel);
            PreparedBoxesPanels.Add(SeventhLvl_PreparedBoxesPanel);
            PreparedBoxesPanels.Add(EighthLvl_PreparedBoxesPanel);
            PreparedBoxesPanels.Add(NinthLvl_PreparedBoxesPanel);

            foreach(StackPanel PreparedBoxesPanel in PreparedBoxesPanels)
            {
                PreparedBoxesPanel.Tag = "PreparedBoxesPanel";
                LevelPanels.Add(PreparedBoxesPanel);
            }
        }

        private void Init_SpellComboBoxes()
        {
            Init_CantripBox();
            Init_1stLvlBox();
            Init_2ndLvlBox();
            Init_3rdLvlBox();
            Init_4thLvlBox();
            Init_5thLvlBox();
            Init_6thLvlBox();
            Init_7thLvlBox();
            Init_8thLvlBox();
            Init_9thLvlBox();
        }

        private void Init_CantripBox()
        {
            CantripsSelectionCB.Items.Add("");

            foreach(SpellList_Item SL_Item in Cantrips)
            {
                CantripsSelectionCB.Items.Add(SL_Item.SpellName);
            }
        }

        private void Init_1stLvlBox()
        {
            FirstLvl_SelectionCB.Items.Add("");

            foreach (SpellList_Item SL_Item in First_LvlSpells)
            {
                FirstLvl_SelectionCB.Items.Add(SL_Item.SpellName);
            }
        }

        private void Init_2ndLvlBox()
        {
            SecondLvl_SelectionCB.Items.Add("");

            foreach (SpellList_Item SL_Item in Second_LvlSpells)
            {
                SecondLvl_SelectionCB.Items.Add(SL_Item.SpellName);
            }
        }

        private void Init_3rdLvlBox()
        {
            ThirdLvl_SelectionCB.Items.Add("");

            foreach (SpellList_Item SL_Item in Third_LvlSpells)
            {
                ThirdLvl_SelectionCB.Items.Add(SL_Item.SpellName);
            }
        }

        private void Init_4thLvlBox()
        {
            FourthLvl_SelectionCB.Items.Add("");

            foreach (SpellList_Item SL_Item in Fourth_LvlSpells)
            {
                FourthLvl_SelectionCB.Items.Add(SL_Item.SpellName);
            }
        }

        private void Init_5thLvlBox()
        {
            FifthLvl_SelectionCB.Items.Add("");

            foreach (SpellList_Item SL_Item in Fifth_LvlSpells)
            {
                FifthLvl_SelectionCB.Items.Add(SL_Item.SpellName);
            }
        }

        private void Init_6thLvlBox()
        {
            SixthLvl_SelectionCB.Items.Add("");

            foreach (SpellList_Item SL_Item in Sixth_LvlSpells)
            {
                SixthLvl_SelectionCB.Items.Add(SL_Item.SpellName);
            }
        }

        private void Init_7thLvlBox()
        {
            SeventhLvl_SelectionCB.Items.Add("");

            foreach (SpellList_Item SL_Item in Seventh_LvlSpells)
            {
                SeventhLvl_SelectionCB.Items.Add(SL_Item.SpellName);
            }
        }

        private void Init_8thLvlBox()
        {
            EighthLvl_SelectionCB.Items.Add("");

            foreach (SpellList_Item SL_Item in Eighth_LvlSpells)
            {
                EighthLvl_SelectionCB.Items.Add(SL_Item.SpellName);
            }
        }

        private void Init_9thLvlBox()
        {
            NinthLvl_SelectionCB.Items.Add("");

            foreach (SpellList_Item SL_Item in Ninth_LvlSpells)
            {
                NinthLvl_SelectionCB.Items.Add(SL_Item.SpellName);
            }
        }

        private void LoadSpellSheetData()
        {            

            if(SheetManager.CS_Manager_Inst.character.SpellSheetData != null)
            {
                SpellSheetData = SheetManager.CS_Manager_Inst.character.SpellSheetData;

                //MessageBox.Show("Spell Sheet data count: " + SpellSheetData.Count);

                if(SpellSheetData.Count > 0)
                {
                    foreach (SlotPanelData slotPanelData in SpellSheetData)
                    {
                        int currentIndex = SpellSheetData.IndexOf(slotPanelData);

                        LoadSlotItems(slotPanelData.SlotPanelItems, currentIndex);
                        Generate_CancelButtons(CancelBtnPanels[currentIndex], SlotPanels[currentIndex]);


                        if(currentIndex > 0)
                        {
                            int slotBoxIndex = currentIndex - 1;
                            
                            LoadSpellSlotsTotal(slotBoxIndex, slotPanelData.SlotsTotal);
                            LoadSpellSlotsExpended(slotBoxIndex, slotPanelData.SlotsExpended);
                            
                            Generate_PreparedCheckBoxes(PreparedBoxesPanels[slotBoxIndex]);

                            if(slotPanelData.ItemsPrepared != null && slotPanelData.ItemsPrepared.Count > 0)
                            {
                                SetPreparedStates(slotBoxIndex, slotPanelData.ItemsPrepared);
                            }
                        }
                    }
                }
            }
        }

        private void LoadSpellSlotsTotal(int slotIndex, int boxValue)
        {
            if(boxValue > 0)
                SpellSlotsTotalBoxes[slotIndex].Text = boxValue.ToString();
        }

        private void LoadSpellSlotsExpended(int slotIndex, int boxValue)
        {
            if(boxValue > 0)
                SpellsExpendedBoxes[slotIndex].Text = boxValue.ToString();
        }

        private void LoadSlotItems(List<string> slotPanelItemNames, int slotPanelIndex)
        {            

            foreach(string slotPanelItem in slotPanelItemNames)
            {
                Border slotItem = Create_BorderedSpellSlotTextblock(slotPanelItem);
                SlotPanels[slotPanelIndex].Children.Add(slotItem);
            }
            
        }

        private void SetPreparedStates(int index, List<bool> preparedStates)
        {
            foreach(CheckBox PreparedCB in PreparedBoxesPanels[index].Children)
            {
                int preparedIndex = PreparedBoxesPanels[index].Children.IndexOf(PreparedCB);
                PreparedCB.IsChecked = preparedStates[preparedIndex];
            }
        }

        private Button Create_CancelBtn(string slotPanelTag)
        {
            Button CancelButton = new Button();

            CancelButton.BorderBrush = Brushes.SlateGray;
            CancelButton.Background = Brushes.WhiteSmoke;
            CancelButton.BorderThickness = new Thickness(2);

            Thickness CancelBtnMargin = new Thickness();
            CancelBtnMargin.Top = 5;
            CancelBtnMargin.Bottom = 1;
            CancelBtnMargin.Right = 5;
            CancelButton.Margin = CancelBtnMargin;

            CancelButton.Height = 20;

            CancelButton.FontFamily = new FontFamily("Cambria");
            CancelButton.FontSize = 10f;
            CancelButton.FontWeight = FontWeights.Bold;

            CancelButton.Content = "X";
            CancelButton.Tag = slotPanelTag;

            return CancelButton;

        }

        private void Generate_CancelButtons(StackPanel targetPanel, StackPanel linkedPanel)
        {
            targetPanel.Children.Clear();

            foreach (Border SpellTB in linkedPanel.Children)
            {
                Button CancelButton = Create_CancelBtn(targetPanel.Name);
                CancelButton.Click += new RoutedEventHandler(Cancelbutton_Click);
                targetPanel.Children.Add(CancelButton);
            }
        }


        private void Cancelbutton_Click(object sender, RoutedEventArgs e)
        {
            Button SendingButton = (Button)sender;
            List<Button> CancelButtons = new List<Button>();

            StackPanel targetPanel = Find_StackPanel(SendingButton.Tag.ToString());

            int indexOfTargetBtn = -1;

            if (targetPanel != null)
            {
                foreach (UIElement CancelButtonElement in targetPanel.Children)
                {
                    Button CancelButton = (Button)CancelButtonElement;                    
                    CancelButtons.Add(CancelButton);
                }

                foreach (Button CancelButton in CancelButtons)
                {
                    
                    if (CancelButton.GetHashCode() == SendingButton.GetHashCode())
                    {
                        indexOfTargetBtn = CancelButtons.IndexOf(CancelButton);
                        targetPanel.Children.Remove(SendingButton);
                    }
                }

                int indexOfTargetPanel = CancelBtnPanels.IndexOf(targetPanel);

                FindAndRemoveAt_LinkedSlotPanel(indexOfTargetBtn, indexOfTargetPanel);

                if(indexOfTargetPanel > 0)
                    FindAndRemoveAt_LinkedPreparedBoxes(indexOfTargetBtn, indexOfTargetPanel);

            }

            

            else
                MessageBox.Show("Couldn't find target panel");
        }

        private void FindAndRemoveAt_LinkedSlotPanel(int indexOfTargetBtn, int indexOfTargetPanel)
        {
            StackPanel linkedSlotPanel = new StackPanel();

            if (indexOfTargetPanel >= 0 && indexOfTargetPanel < SlotPanels.Count)
                linkedSlotPanel = SlotPanels[indexOfTargetPanel];

            if (linkedSlotPanel != null)
            {
                if (indexOfTargetBtn >= 0 && indexOfTargetBtn < linkedSlotPanel.Children.Count)
                    linkedSlotPanel.Children.RemoveAt(indexOfTargetBtn);
            }
        }

        private void FindAndRemoveAt_LinkedPreparedBoxes(int indexOfTargetBtn, int indexOfCancelBtnPanel)
        {
            StackPanel linkedPrepBoxPanel = new StackPanel();

            int indexOfTargetPanel = indexOfCancelBtnPanel - 1;

            if (indexOfTargetPanel >= 0 && indexOfTargetPanel < PreparedBoxesPanels.Count)
                linkedPrepBoxPanel = PreparedBoxesPanels[indexOfTargetPanel];

            if (linkedPrepBoxPanel != null)
            {
                if (indexOfTargetBtn >= 0 && indexOfTargetBtn < linkedPrepBoxPanel.Children.Count)
                    linkedPrepBoxPanel.Children.RemoveAt(indexOfTargetBtn);
            }
        }

        private CheckBox Create_CheckBox()
        {
            CheckBox PreparedBox = new CheckBox();

            PreparedBox.HorizontalAlignment = HorizontalAlignment.Center;
            PreparedBox.VerticalAlignment = VerticalAlignment.Center;

            Thickness Margin = new Thickness();
            Margin.Top = 7.5;
            Margin.Bottom = 2.5;
            Margin.Left = 5;

            PreparedBox.Margin = Margin;

            return PreparedBox;
        }

        private void Generate_PreparedCheckBoxes(StackPanel targetPanel)
        {
            targetPanel.Children.Clear();
            int indexOfTargetPanel = PreparedBoxesPanels.IndexOf(targetPanel);
            int indexOfCorrespondingSlotPanel = indexOfTargetPanel + 1;

            if(indexOfCorrespondingSlotPanel < SlotPanels.Count)
            {
                foreach(Border SpellTB in SlotPanels[indexOfCorrespondingSlotPanel].Children)
                {
                    CheckBox PreparedBox = Create_CheckBox();
                    PreparedBoxesPanels[indexOfTargetPanel].Children.Add(PreparedBox);
                }
            }
        }


        private void ComboBoxHoverOverSpell(object sender, MouseEventArgs e)
        {
            ComboBox spellBox = (ComboBox)e.Source;

            if(spellBox.SelectedItem != null)
            {
                Spell tempSpell = SheetManager.CS_Manager_Inst.theWeave.Find_Spell_in_Database_byName(spellBox.SelectedItem.ToString());
                
                if(tempSpell != null)
                    Fill_SpellViewer(tempSpell);
            }
        }

        private void MouseHoverOverSpellSlot(object sender, MouseEventArgs e)
        {
            TextBlock SpellSlot = (TextBlock)sender;
            Spell tempSpell = SheetManager.CS_Manager_Inst.theWeave.Find_Spell_in_Database_byName(SpellSlot.Text);
            if (tempSpell != null)
                Fill_SpellViewer(tempSpell);
        }

        private void Fill_SpellViewer(Spell spell)
        {
            SpellViewer.Text = "Name of Spell: " + spell.SpellName + "\nSchool of Magic: " + spell.School + "\nCasting Time: " + spell.CastingTime
                + "\nRange: " + spell.Range + "\nComponents: " + spell.Components + "\nDuration: " + spell.Duration + "\n\n" + spell.Description;            
        }

        private bool CheckIfAlreadyInSpellList(string itemName, StackPanel CheckPanel)
        {
            List<string> SpellListItems = new List<string>();
            
            foreach(UIElement element in CheckPanel.Children)
            {
                Border elementBorder = (Border)element;
                TextBlock itemTB = (TextBlock)elementBorder.Child;
                SpellListItems.Add(itemTB.Text);
            }

            if (SpellListItems.Contains(itemName))
                return true;
            else
                return false;
        }

        private Border Create_BorderedSpellSlotTextblock(string slotText)
        {
            Border SlotItemBorder = new Border();
            SlotItemBorder.Background = Brushes.WhiteSmoke;
            SlotItemBorder.BorderBrush = Brushes.SlateGray;
            SlotItemBorder.BorderThickness = new Thickness(2);

            Thickness ItemMargin = new Thickness();
            ItemMargin.Left = 5;
            ItemMargin.Top = 5;
            ItemMargin.Right = 10;

            SlotItemBorder.Margin = ItemMargin;

            TextBlock SlotListItem = new TextBlock();
            SlotListItem.Text = slotText;

            SlotListItem.FontFamily = new FontFamily("Cambria");
            SlotListItem.FontWeight = FontWeights.Bold;
            SlotListItem.FontSize = 14;

            SlotListItem.HorizontalAlignment = HorizontalAlignment.Center;
            SlotListItem.VerticalAlignment = VerticalAlignment.Center;

            SlotListItem.MouseEnter += MouseHoverOverSpellSlot;
            
            SlotItemBorder.Child = SlotListItem;

            return SlotItemBorder;
        }

        private void Add_Cantrip_Btn_Click(object sender, RoutedEventArgs e)
        {
            
            if (CantripsSelectionCB.SelectedIndex > 0 && !CheckIfAlreadyInSpellList(CantripsSelectionCB.SelectedItem.ToString(), CantripSlotPanel))
            {
                Border slotItem = Create_BorderedSpellSlotTextblock(CantripsSelectionCB.SelectedItem.ToString());
                CantripSlotPanel.Children.Add(slotItem);
                Generate_CancelButtons(CantripCancelBtnsPanel, CantripSlotPanel);
            }
        }

        private void Add_1stLvlSpl_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (FirstLvl_SelectionCB.SelectedIndex > 0 && !CheckIfAlreadyInSpellList(FirstLvl_SelectionCB.SelectedItem.ToString(), FirstLvl_SlotPanel))
            {
                Border slotItem = Create_BorderedSpellSlotTextblock(FirstLvl_SelectionCB.SelectedItem.ToString());
                FirstLvl_SlotPanel.Children.Add(slotItem);
                Generate_CancelButtons(FirstLvl_CancelBtnsPanel, FirstLvl_SlotPanel);
                Generate_PreparedCheckBoxes(FirstLvl_PreparedBoxesPanel);
                Check_SpellListsItems();
            }
        }

        private void Add_2ndLvlSpl_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (SecondLvl_SelectionCB.SelectedIndex > 0 && !CheckIfAlreadyInSpellList(SecondLvl_SelectionCB.SelectedItem.ToString(), SecondLvl_SlotPanel))
            {
                Border slotItem = Create_BorderedSpellSlotTextblock(SecondLvl_SelectionCB.SelectedItem.ToString());
                SecondLvl_SlotPanel.Children.Add(slotItem);
                Generate_CancelButtons(SecondLvl_CancelBtnsPanel, SecondLvl_SlotPanel);
                Generate_PreparedCheckBoxes(SecondLvl_PreparedBoxesPanel);
                Check_SpellListsItems();
            }
        }

        private void Add_3rdLvlSpl_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (ThirdLvl_SelectionCB.SelectedIndex > 0 && !CheckIfAlreadyInSpellList(ThirdLvl_SelectionCB.SelectedItem.ToString(), ThirdLvl_SlotPanel))
            {
                Border slotItem = Create_BorderedSpellSlotTextblock(ThirdLvl_SelectionCB.SelectedItem.ToString());
                ThirdLvl_SlotPanel.Children.Add(slotItem);
                Generate_CancelButtons(ThirdLvl_CancelBtnsPanel, ThirdLvl_SlotPanel);
                Generate_PreparedCheckBoxes(ThirdLvl_PreparedBoxesPanel);
                Check_SpellListsItems();
            }
        }

        private void Add_4thLvlSpl_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (FourthLvl_SelectionCB.SelectedIndex > 0 && !CheckIfAlreadyInSpellList(FourthLvl_SelectionCB.SelectedItem.ToString(), FourthLvl_SlotPanel))
            {
                Border slotItem = Create_BorderedSpellSlotTextblock(FourthLvl_SelectionCB.SelectedItem.ToString());
                FourthLvl_SlotPanel.Children.Add(slotItem);
                Generate_CancelButtons(FourthLvl_CancelBtnsPanel, FourthLvl_SlotPanel);
                Generate_PreparedCheckBoxes(FourthLvl_PreparedBoxesPanel);
                Check_SpellListsItems();
            }
        }

        private void Add_5thLvlSpl_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (FifthLvl_SelectionCB.SelectedIndex > 0 && !CheckIfAlreadyInSpellList(FifthLvl_SelectionCB.SelectedItem.ToString(), FifthLvl_SlotPanel))
            {
                Border slotItem = Create_BorderedSpellSlotTextblock(FifthLvl_SelectionCB.SelectedItem.ToString());
                FifthLvl_SlotPanel.Children.Add(slotItem);
                Generate_CancelButtons(FifthLvl_CancelBtnsPanel, FifthLvl_SlotPanel);
                Generate_PreparedCheckBoxes(FifthLvl_PreparedBoxesPanel);
                Check_SpellListsItems();
            }
        }

        private void Add_6thLvlSpl_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (SixthLvl_SelectionCB.SelectedIndex > 0 && !CheckIfAlreadyInSpellList(SixthLvl_SelectionCB.SelectedItem.ToString(), SixthLvl_SlotPanel))
            {
                Border slotItem = Create_BorderedSpellSlotTextblock(SixthLvl_SelectionCB.SelectedItem.ToString());
                SixthLvl_SlotPanel.Children.Add(slotItem);
                Generate_CancelButtons(SixthLvl_CancelBtnsPanel, SixthLvl_SlotPanel);
                Generate_PreparedCheckBoxes(SixthLvl_PreparedBoxesPanel);
                Check_SpellListsItems();
            }
        }
        private void Add_7thLvlSpl_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (SeventhLvl_SelectionCB.SelectedIndex > 0 && !CheckIfAlreadyInSpellList(SeventhLvl_SelectionCB.SelectedItem.ToString(), SeventhLvl_SlotPanel))
            {
                Border slotItem = Create_BorderedSpellSlotTextblock(SeventhLvl_SelectionCB.SelectedItem.ToString());
                SeventhLvl_SlotPanel.Children.Add(slotItem);
                Generate_CancelButtons(SeventhLvl_CancelBtnsPanel, SeventhLvl_SlotPanel);
                Generate_PreparedCheckBoxes(SeventhLvl_PreparedBoxesPanel);
                Check_SpellListsItems();
            }
        }

        private void Add_8thLvlSpl_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (EighthLvl_SelectionCB.SelectedIndex > 0 && !CheckIfAlreadyInSpellList(EighthLvl_SelectionCB.SelectedItem.ToString(), EighthLvl_SlotPanel))
            {
                Border slotItem = Create_BorderedSpellSlotTextblock(EighthLvl_SelectionCB.SelectedItem.ToString());
                EighthLvl_SlotPanel.Children.Add(slotItem);
                Generate_CancelButtons(EighthLvl_CancelBtnsPanel, EighthLvl_SlotPanel);
                Generate_PreparedCheckBoxes(EighthLvl_PreparedBoxesPanel);
                Check_SpellListsItems();
            }
        }

        private void Add_9thLvlSpl_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (NinthLvl_SelectionCB.SelectedIndex > 0 && !CheckIfAlreadyInSpellList(NinthLvl_SelectionCB.SelectedItem.ToString(), NinthLvl_SlotPanel))
            {
                Border slotItem = Create_BorderedSpellSlotTextblock(NinthLvl_SelectionCB.SelectedItem.ToString());
                NinthLvl_SlotPanel.Children.Add(slotItem);
                Generate_CancelButtons(NinthLvl_CancelBtnsPanel, NinthLvl_SlotPanel);
                Generate_PreparedCheckBoxes(NinthLvl_PreparedBoxesPanel);
                Check_SpellListsItems();
            }
        }

        private void Edit_SpellSlots_Click(object sender, RoutedEventArgs e)
        {
            Apply_SpellSlots.Visibility = Visibility.Visible;
            Enable_SpellSlotsTotal_Edit();
        }       

        private void Apply_SpellSlots_Click(object sender, RoutedEventArgs e)
        {
            Check_SpellsSlotsTotalValues();
        }

        private void Enable_SpellSlotsTotal_Edit()
        {
            SpellSlotsTotal_SetActiveColor();
            SpellSlotsTotalBoxes.ForEach(BoxElement => BoxElement.IsEnabled = true);
        }

        private void Disable_SpellSlotsTotal_Edit()
        {
            SpellSlotsTotalBoxes.ForEach(BoxElement => BoxElement.IsEnabled = false);
        }

        private void HideApplyBtn_and_DisableSpellsSlotsTotal()
        {
            Apply_SpellSlots.Visibility = Visibility.Hidden;
            Disable_SpellSlotsTotal_Edit();
        }

        private void SpellSlotsTotal_SetOriginalColor()
        {
            SpellSlotsTotalBoxes.ForEach(BoxElement => BoxElement.Background = Brushes.WhiteSmoke);
            SpellSlotsTotalBoxes.ForEach(BoxElement => BoxElement.BorderBrush = Brushes.Thistle);
        }

        private void SpellSlotsTotal_SetActiveColor()
        {
            SpellSlotsTotalBoxes.ForEach(BoxElement => BoxElement.Background = Brushes.Thistle);
            SpellSlotsTotalBoxes.ForEach(BoxElement => BoxElement.BorderBrush = Brushes.DarkSlateGray);
        }

        private void Check_SpellsSlotsTotalValues()
        {
            bool AllSpellsSlotsAreValid = SpellSlotsTotalBoxes.TrueForAll(BoxElement => Check_SpellSlotsTotal_Value(BoxElement.Text));

            if (AllSpellsSlotsAreValid)
            {
                HideApplyBtn_and_DisableSpellsSlotsTotal();
                SpellSlotsTotal_SetOriginalColor();
                Set_SpellSlotsExpended_OriginalValue();
                EnableCastButtons();
            }

            else
                MessageBox.Show(SpellSlotErrorMessage, "Invalid Spell Slot Value");
        }

        private bool Check_SpellSlotsTotal_Value(string BoxText)
        {
            if (BoxText.Length > 0)
            {

                if (int.TryParse(BoxText, out int value))
                {

                    if (value > 0 && value < 50)
                        return true;

                    else
                        return false;
                }

                else
                    return false;
            }

            else
                return true;
        }

        private void Set_SpellSlotsExpended_OriginalValue()
        {
            foreach(TextBox SpellSlotsTotalBox in SpellSlotsTotalBoxes)
            {
                int spellLevelIndex = SpellSlotsTotalBoxes.IndexOf(SpellSlotsTotalBox);
                
                if(SpellSlotsTotalBox.Text.Length > 0)
                    SpellsExpendedBoxes[spellLevelIndex].Text = "0";
            }
        }

        private void EnableCastButtons()
        {
            Check_SpellSlotsTotal();
            Check_SpellListsItems();
        }

        private void Check_SpellSlotsTotal()
        {
            foreach (Button CastButton in CastButtons)
            {
                int indexOfCastButton = CastButtons.IndexOf(CastButton);

                foreach (TextBox SlotBox in SpellSlotsTotalBoxes)
                {
                    int indexOfSlotBox = SpellSlotsTotalBoxes.IndexOf(SlotBox);

                    if (indexOfSlotBox == indexOfCastButton)
                    {
                        if (SlotBox.Text.Length > 0)
                            CastButton.IsEnabled = true;
                        else
                            CastButton.IsEnabled = false;
                    }
                }
            }
        }

        private void Check_SpellListsItems()
        {
            // This for-Loop starts at 1 because the Cantrips-Panel - the first element in the LevelPanels-List - should be ignored in this case:
            // because it has no 'SpellSlots' on it (Slots Total and Slots Expended elements).

            for(int i = 0; i < SpellSlotsTotalBoxes.Count; i++)
            {                
                Check_SpellListItem(i, SlotPanels[i+1], SpellSlotsTotalBoxes[i]);
            }
        }

        private void Check_SpellListItem(int index, StackPanel SpellLevelPanel, TextBox SpellSlotsTotalTB)
        {
            if (SpellLevelPanel.Children.Count > 0 && SpellSlotsTotalTB.Text.Length > 0)
                CastButtons[index].IsEnabled = true;
            else
                CastButtons[index].IsEnabled = false;
        }        

        private void Cast_1stLvl_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (SlotsTotal_1stLvl_TB.Text.Length > 0 && int.TryParse(SlotsTotal_1stLvl_TB.Text, out int number))
            {
                int spellsSlotsExpended = int.Parse(SlotsExp_1stLvl_TB.Text);
                spellsSlotsExpended++;

                if (spellsSlotsExpended == int.Parse(SlotsTotal_1stLvl_TB.Text))
                {
                    SlotsExp_1stLvl_TB.Text = SlotsTotal_1stLvl_TB.Text;
                    Cast_1stLvl_Btn.IsEnabled = false;
                    MessageBox.Show(AllSpellSlotsExpendedMessage);
                }

                else
                    SlotsExp_1stLvl_TB.Text = spellsSlotsExpended.ToString();
            }

            else
                MessageBox.Show(NoSpellSlotsForThisLevelMessage);
        }

        private void Cast_2ndLvl_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (SlotsTotal_2ndLvl_TB.Text.Length > 0 && int.TryParse(SlotsTotal_2ndLvl_TB.Text, out int number))
            {
                int spellsSlotsExpended = int.Parse(SlotsExp_2ndLvl_TB.Text);
                spellsSlotsExpended++;

                if (spellsSlotsExpended == int.Parse(SlotsTotal_2ndLvl_TB.Text))
                {
                    SlotsExp_2ndLvl_TB.Text = SlotsTotal_2ndLvl_TB.Text;
                    Cast_2ndLvl_Btn.IsEnabled = false;
                    MessageBox.Show(AllSpellSlotsExpendedMessage);
                }

                else
                    SlotsExp_2ndLvl_TB.Text = spellsSlotsExpended.ToString();
            }

            else
                MessageBox.Show(NoSpellSlotsForThisLevelMessage);
        }

        private void Cast_3rdLvl_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (SlotsTotal_3rdLvl_TB.Text.Length > 0 && int.TryParse(SlotsTotal_3rdLvl_TB.Text, out int number))
            {
                int spellsSlotsExpended = int.Parse(SlotsExp_3rdLvl_TB.Text);
                spellsSlotsExpended++;

                if (spellsSlotsExpended == int.Parse(SlotsTotal_3rdLvl_TB.Text))
                {
                    SlotsExp_3rdLvl_TB.Text = SlotsTotal_3rdLvl_TB.Text;
                    Cast_3rdLvl_Btn.IsEnabled = false;
                    MessageBox.Show(AllSpellSlotsExpendedMessage);
                }

                else
                    SlotsExp_3rdLvl_TB.Text = spellsSlotsExpended.ToString();
            }

            else
                MessageBox.Show(NoSpellSlotsForThisLevelMessage);
        }

        private void Cast_4thLvl_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (SlotsTotal_4thLvl_TB.Text.Length > 0 && int.TryParse(SlotsTotal_4thLvl_TB.Text, out int number))
            {
                int spellsSlotsExpended = int.Parse(SlotsExp_4thLvl_TB.Text);
                spellsSlotsExpended++;

                if (spellsSlotsExpended == int.Parse(SlotsTotal_4thLvl_TB.Text))
                {
                    SlotsExp_4thLvl_TB.Text = SlotsTotal_4thLvl_TB.Text;
                    Cast_4thLvl_Btn.IsEnabled = false;
                    MessageBox.Show(AllSpellSlotsExpendedMessage);
                }

                else
                    SlotsExp_4thLvl_TB.Text = spellsSlotsExpended.ToString();
            }

            else
                MessageBox.Show(NoSpellSlotsForThisLevelMessage);
        }

        private void Cast_5thLvl_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (SlotsTotal_5thLvl_TB.Text.Length > 0 && int.TryParse(SlotsTotal_5thLvl_TB.Text, out int number))
            {
                int spellsSlotsExpended = int.Parse(SlotsExp_5thLvl_TB.Text);
                spellsSlotsExpended++;

                if (spellsSlotsExpended == int.Parse(SlotsTotal_5thLvl_TB.Text))
                {
                    SlotsExp_5thLvl_TB.Text = SlotsTotal_5thLvl_TB.Text;
                    Cast_5thLvl_Btn.IsEnabled = false;
                    MessageBox.Show(AllSpellSlotsExpendedMessage);
                }

                else
                    SlotsExp_5thLvl_TB.Text = spellsSlotsExpended.ToString();
            }

            else
                MessageBox.Show(NoSpellSlotsForThisLevelMessage);
        }

        private void Cast_6thLvl_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (SlotsTotal_6thLvl_TB.Text.Length > 0 && int.TryParse(SlotsTotal_6thLvl_TB.Text, out int number))
            {
                int spellsSlotsExpended = int.Parse(SlotsExp_6thLvl_TB.Text);
                spellsSlotsExpended++;

                if (spellsSlotsExpended == int.Parse(SlotsTotal_6thLvl_TB.Text))
                {
                    SlotsExp_6thLvl_TB.Text = SlotsTotal_6thLvl_TB.Text;
                    Cast_6thLvl_Btn.IsEnabled = false;
                    MessageBox.Show(AllSpellSlotsExpendedMessage);
                }

                else
                    SlotsExp_6thLvl_TB.Text = spellsSlotsExpended.ToString();
            }

            else
                MessageBox.Show(NoSpellSlotsForThisLevelMessage);
        }

        private void Cast_7thLvl_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (SlotsTotal_7thLvl_TB.Text.Length > 0 && int.TryParse(SlotsTotal_7thLvl_TB.Text, out int number))
            {
                int spellsSlotsExpended = int.Parse(SlotsExp_7thLvl_TB.Text);
                spellsSlotsExpended++;

                if (spellsSlotsExpended == int.Parse(SlotsTotal_7thLvl_TB.Text))
                {
                    SlotsExp_7thLvl_TB.Text = SlotsTotal_7thLvl_TB.Text;
                    Cast_7thLvl_Btn.IsEnabled = false;
                    MessageBox.Show(AllSpellSlotsExpendedMessage);
                }

                else
                    SlotsExp_7thLvl_TB.Text = spellsSlotsExpended.ToString();
            }

            else
                MessageBox.Show(NoSpellSlotsForThisLevelMessage);
        }

        private void Cast_8thLvl_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (SlotsTotal_8thLvl_TB.Text.Length == 0 && int.TryParse(SlotsTotal_8thLvl_TB.Text, out int number))
            {
                int spellsSlotsExpended = int.Parse(SlotsExp_8thLvl_TB.Text);
                spellsSlotsExpended++;

                if (spellsSlotsExpended > int.Parse(SlotsTotal_8thLvl_TB.Text))
                {
                    SlotsExp_8thLvl_TB.Text = SlotsTotal_8thLvl_TB.Text;
                    Cast_8thLvl_Btn.IsEnabled = false;
                    MessageBox.Show(AllSpellSlotsExpendedMessage);
                }

                else
                    SlotsExp_8thLvl_TB.Text = spellsSlotsExpended.ToString();
            }

            else
                MessageBox.Show(NoSpellSlotsForThisLevelMessage);
        }

        private void Cast_9thLvl_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (SlotsTotal_9thLvl_TB.Text.Length > 0 && int.TryParse(SlotsTotal_9thLvl_TB.Text, out int number))
            {
                int spellsSlotsExpended = int.Parse(SlotsExp_9thLvl_TB.Text);
                spellsSlotsExpended++;

                if (spellsSlotsExpended == int.Parse(SlotsTotal_9thLvl_TB.Text))
                {
                    SlotsExp_9thLvl_TB.Text = SlotsTotal_9thLvl_TB.Text;
                    Cast_9thLvl_Btn.IsEnabled = false;
                    MessageBox.Show(AllSpellSlotsExpendedMessage);
                }

                else
                    SlotsExp_9thLvl_TB.Text = spellsSlotsExpended.ToString();
            }

            else
                MessageBox.Show(NoSpellSlotsForThisLevelMessage);
        }

        private void SaveSpellSheet()
        {
            SpellSheetData.Clear();

            foreach (StackPanel SlotPanel in SlotPanels)
            {                
                List<string> panelItems = ConvertPanelChildrenToString(SlotPanel);
                List<bool> preparedList = new List<bool>();

                int currentIndex = SlotPanels.IndexOf(SlotPanel);

                int slotsTotal = 0;
                int slotsExpended = 0;

                if(currentIndex > 0)
                {
                    int slotBoxIndex = currentIndex - 1;

                    slotsTotal = TrySlotText(SpellSlotsTotalBoxes[slotBoxIndex].Text);
                    slotsExpended = TrySlotText(SpellsExpendedBoxes[slotBoxIndex].Text);

                    if(PreparedBoxesPanels[slotBoxIndex].Children.Count > 0)
                    {
                        foreach (CheckBox preparedBox in PreparedBoxesPanels[slotBoxIndex].Children)
                        {
                            preparedList.Add((bool)preparedBox.IsChecked);
                        }
                    }
                }


                SlotPanelData slotPanelData = new SlotPanelData(slotsTotal, slotsExpended, panelItems, preparedList);
                SpellSheetData.Add(slotPanelData);
            }

            SheetManager.CS_Manager_Inst.character.SpellSheetData = SpellSheetData;
        }

        private int TrySlotText(string text)
        {
            int slotValue = 0;
            if (text.Length > 0)
            {
                slotValue = int.Parse(text);
                return slotValue;
            }
            else
                return slotValue;
        }

        private List<string> ConvertPanelChildrenToString(StackPanel SpellPanel)
        {
            List<string> slotItems = new List<string>();

            foreach(UIElement SpellElement in SpellPanel.Children)
            {
                Border tempBorder = (Border)SpellElement;
                TextBlock tempBox = (TextBlock)tempBorder.Child;
                string spellName = tempBox.Text;
                slotItems.Add(spellName);
            }

            return slotItems;
        }

        private StackPanel Find_StackPanel(string stackPanelName)
        {
            StackPanel StackPanelToFind = LevelPanels.Find(PanelElement => PanelElement.Name == stackPanelName);
            return StackPanelToFind;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveSpellSheet();
        }
    }
    
}
