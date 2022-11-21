using System;
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
        List<TextBlock> SpellsExpenedTxts;
        List<Button> CastButtons;

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
            SpellsExpenedTxts = new List<TextBlock>();
            SpellsExpenedTxts.Add(SlotsExp_1stLvlTxt);
            SpellsExpenedTxts.Add(SlotsExp_2ndLvlTxt);
            SpellsExpenedTxts.Add(SlotsExp_3rdLvlTxt);
            SpellsExpenedTxts.Add(SlotsExp_4thLvlTxt);
            SpellsExpenedTxts.Add(SlotsExp_5thLvlTxt);
            SpellsExpenedTxts.Add(SlotsExp_6thLvlTxt);
            SpellsExpenedTxts.Add(SlotsExp_7thLvlTxt);
            SpellsExpenedTxts.Add(SlotsExp_8thLvlTxt);
            SpellsExpenedTxts.Add(SlotsExp_9thLvlTxt);
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



        private Button Create_CancelBtn()
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

            return CancelButton;

        }

        private void Generate_CantripCancelButtons()
        {
            CantripCancelsBtnsPanel.Children.Clear();

            foreach(Border SpellTB in CantripSlotPanel.Children)
            {
                Button CancelButton = Create_CancelBtn();
                CancelButton.Click += new RoutedEventHandler(Cantrips_CancelButtonClick);
                CantripCancelsBtnsPanel.Children.Add(CancelButton);
            }

        }

        private void Generate_1stLvlCancelButtons()
        {
            FirstLvl_CancelBtnsPanel.Children.Clear();

            foreach (Border SpellTB in FirstLvl_SlotPanel.Children)
            {
                Button CancelButton = Create_CancelBtn();
                CancelButton.Click += new RoutedEventHandler(FirstLevel_CancelButtonClick);
                FirstLvl_CancelBtnsPanel.Children.Add(CancelButton);
            }

        }


        private void Generate_2ndLvlCancelButtons()
        {
            SecondLvl_CancelBtnsPanel.Children.Clear();

            foreach (Border SpellTB in SecondLvl_SlotPanel.Children)
            {
                Button CancelButton = Create_CancelBtn();
                CancelButton.Click += new RoutedEventHandler(SecondLevel_CancelButtonClick);
                SecondLvl_CancelBtnsPanel.Children.Add(CancelButton);
            }

        }

        private void Generate_3rdLvlCancelButtons()
        {
            ThirdLvl_CancelBtnsPanel.Children.Clear();

            foreach (Border SpellTB in ThirdLvl_SlotPanel.Children)
            {
                Button CancelButton = Create_CancelBtn();
                CancelButton.Click += new RoutedEventHandler(ThirdLevel_CancelButtonClick);
                ThirdLvl_CancelBtnsPanel.Children.Add(CancelButton);
            }

        }

        private void Generate_4thLvlCancelButtons()
        {
            FourthLvl_CancelBtnsPanel.Children.Clear();

            foreach (Border SpellTB in FourthLvl_SlotPanel.Children)
            {
                Button CancelButton = Create_CancelBtn();
                CancelButton.Click += new RoutedEventHandler(FourthLevel_CancelButtonClick);
                FourthLvl_CancelBtnsPanel.Children.Add(CancelButton);
            }

        }

        private void Generate_5thLvlCancelButtons()
        {
            FifthLvl_CancelBtnsPanel.Children.Clear();

            foreach (Border SpellTB in FifthtLvl_SlotPanel.Children)
            {
                Button CancelButton = Create_CancelBtn();
                CancelButton.Click += new RoutedEventHandler(FifthLevel_CancelButtonClick);
                FifthLvl_CancelBtnsPanel.Children.Add(CancelButton);
            }

        }

        private void Generate_6thLvlCancelButtons()
        {
            SixthLvl_CancelBtnsPanel.Children.Clear();

            foreach (Border SpellTB in SixthLvl_SlotPanel.Children)
            {
                Button CancelButton = Create_CancelBtn();
                CancelButton.Click += new RoutedEventHandler(SixthLevel_CancelButtonClick);
                SixthLvl_CancelBtnsPanel.Children.Add(CancelButton);
            }

        }

        private void Generate_7thLvlCancelButtons()
        {
            SeventhLvl_CancelBtnsPanel.Children.Clear();

            foreach (Border SpellTB in SeventhLvl_SlotPanel.Children)
            {
                Button CancelButton = Create_CancelBtn();
                CancelButton.Click += new RoutedEventHandler(SeventhLevel_CancelButtonClick);
                SeventhLvl_CancelBtnsPanel.Children.Add(CancelButton);
            }

        }

        private void Generate_8thLvlCancelButtons()
        {
            EighthLvl_CancelBtnsPanel.Children.Clear();

            foreach (Border SpellTB in EighthLvl_SlotPanel.Children)
            {
                Button CancelButton = Create_CancelBtn();
                CancelButton.Click += new RoutedEventHandler(EighthLevel_CancelButtonClick);
                EighthLvl_CancelBtnsPanel.Children.Add(CancelButton);
            }

        }


        private void Generate_9thLvlCancelButtons()
        {
            NinthLvl_CancelBtnsPanel.Children.Clear();

            foreach (Border SpellTB in NinthLvl_SlotPanel.Children)
            {
                Button CancelButton = Create_CancelBtn();
                CancelButton.Click += new RoutedEventHandler(NinthLevel_CancelButtonClick);
                NinthLvl_CancelBtnsPanel.Children.Add(CancelButton);
            }

        }

        private void Cantrips_CancelButtonClick(object sender, RoutedEventArgs e)
        {
            List<Button> CancelButtons = new List<Button>();
            Button SendingButton = (Button)sender;

            foreach(UIElement CancelButtonElement in CantripCancelsBtnsPanel.Children)
            {
                Button CancelButton = (Button)CancelButtonElement;
                CancelButtons.Add(CancelButton);                
            }

            foreach(Button CancelButton in CancelButtons)
            {
                if (CancelButton.GetHashCode() == SendingButton.GetHashCode())
                {
                    int i = CancelButtons.IndexOf(CancelButton);

                    if (i < CantripSlotPanel.Children.Count && CantripSlotPanel.Children[i] != null)
                        CantripSlotPanel.Children.RemoveAt(i);

                    CantripCancelsBtnsPanel.Children.Remove(SendingButton);
                }
                    
            }
        }

        private void FirstLevel_CancelButtonClick(object sender, RoutedEventArgs e)
        {
            List<Button> CancelButtons = new List<Button>();
            Button SendingButton = (Button)sender;

            foreach(UIElement CancelButtonElement in FirstLvl_CancelBtnsPanel.Children)
            {
                Button CancelButton = (Button)CancelButtonElement;
                CancelButtons.Add(CancelButton);                
            }

            foreach(Button CancelButton in CancelButtons)
            {
                if (CancelButton.GetHashCode() == SendingButton.GetHashCode())
                {
                    int i = CancelButtons.IndexOf(CancelButton);

                    if (i < FirstLvl_SlotPanel.Children.Count && FirstLvl_SlotPanel.Children[i] != null)
                        FirstLvl_SlotPanel.Children.RemoveAt(i);

                    FirstLvl_CancelBtnsPanel.Children.Remove(SendingButton);
                }
                    
            }
        }

        private void SecondLevel_CancelButtonClick(object sender, RoutedEventArgs e)
        {
            List<Button> CancelButtons = new List<Button>();
            Button SendingButton = (Button)sender;

            foreach (UIElement CancelButtonElement in SecondLvl_CancelBtnsPanel.Children)
            {
                Button CancelButton = (Button)CancelButtonElement;
                CancelButtons.Add(CancelButton);
            }

            foreach (Button CancelButton in CancelButtons)
            {
                if (CancelButton.GetHashCode() == SendingButton.GetHashCode())
                {
                    int i = CancelButtons.IndexOf(CancelButton);

                    if (i < SecondLvl_SlotPanel.Children.Count && SecondLvl_SlotPanel.Children[i] != null)
                        SecondLvl_SlotPanel.Children.RemoveAt(i);

                    SecondLvl_CancelBtnsPanel.Children.Remove(SendingButton);
                }

            }
        }

        private void ThirdLevel_CancelButtonClick(object sender, RoutedEventArgs e)
        {
            List<Button> CancelButtons = new List<Button>();
            Button SendingButton = (Button)sender;

            foreach (UIElement CancelButtonElement in ThirdLvl_CancelBtnsPanel.Children)
            {
                Button CancelButton = (Button)CancelButtonElement;
                CancelButtons.Add(CancelButton);
            }

            foreach (Button CancelButton in CancelButtons)
            {
                if (CancelButton.GetHashCode() == SendingButton.GetHashCode())
                {
                    int i = CancelButtons.IndexOf(CancelButton);

                    if (i < ThirdLvl_SlotPanel.Children.Count && ThirdLvl_SlotPanel.Children[i] != null)
                        ThirdLvl_SlotPanel.Children.RemoveAt(i);

                    ThirdLvl_CancelBtnsPanel.Children.Remove(SendingButton);
                }

            }
        }

        private void FourthLevel_CancelButtonClick(object sender, RoutedEventArgs e)
        {
            List<Button> CancelButtons = new List<Button>();
            Button SendingButton = (Button)sender;

            foreach (UIElement CancelButtonElement in FourthLvl_CancelBtnsPanel.Children)
            {
                Button CancelButton = (Button)CancelButtonElement;
                CancelButtons.Add(CancelButton);
            }

            foreach (Button CancelButton in CancelButtons)
            {
                if (CancelButton.GetHashCode() == SendingButton.GetHashCode())
                {
                    int i = CancelButtons.IndexOf(CancelButton);

                    if (i < FourthLvl_SlotPanel.Children.Count && FourthLvl_SlotPanel.Children[i] != null)
                        FourthLvl_SlotPanel.Children.RemoveAt(i);

                    FourthLvl_CancelBtnsPanel.Children.Remove(SendingButton);
                }

            }
        }

        private void FifthLevel_CancelButtonClick(object sender, RoutedEventArgs e)
        {
            List<Button> CancelButtons = new List<Button>();
            Button SendingButton = (Button)sender;

            foreach (UIElement CancelButtonElement in FifthLvl_CancelBtnsPanel.Children)
            {
                Button CancelButton = (Button)CancelButtonElement;
                CancelButtons.Add(CancelButton);
            }

            foreach (Button CancelButton in CancelButtons)
            {
                if (CancelButton.GetHashCode() == SendingButton.GetHashCode())
                {
                    int i = CancelButtons.IndexOf(CancelButton);

                    if (i < FifthtLvl_SlotPanel.Children.Count && FifthtLvl_SlotPanel.Children[i] != null)
                        FifthtLvl_SlotPanel.Children.RemoveAt(i);

                    FifthLvl_CancelBtnsPanel.Children.Remove(SendingButton);
                }

            }
        }


        private void SixthLevel_CancelButtonClick(object sender, RoutedEventArgs e)
        {
            List<Button> CancelButtons = new List<Button>();
            Button SendingButton = (Button)sender;

            foreach (UIElement CancelButtonElement in SixthLvl_CancelBtnsPanel.Children)
            {
                Button CancelButton = (Button)CancelButtonElement;
                CancelButtons.Add(CancelButton);
            }

            foreach (Button CancelButton in CancelButtons)
            {
                if (CancelButton.GetHashCode() == SendingButton.GetHashCode())
                {
                    int i = CancelButtons.IndexOf(CancelButton);

                    if (i < SixthLvl_SlotPanel.Children.Count && SixthLvl_SlotPanel.Children[i] != null)
                        SixthLvl_SlotPanel.Children.RemoveAt(i);

                    SixthLvl_CancelBtnsPanel.Children.Remove(SendingButton);
                }

            }
        }

        private void SeventhLevel_CancelButtonClick(object sender, RoutedEventArgs e)
        {
            List<Button> CancelButtons = new List<Button>();
            Button SendingButton = (Button)sender;

            foreach (UIElement CancelButtonElement in SeventhLvl_CancelBtnsPanel.Children)
            {
                Button CancelButton = (Button)CancelButtonElement;
                CancelButtons.Add(CancelButton);
            }

            foreach (Button CancelButton in CancelButtons)
            {
                if (CancelButton.GetHashCode() == SendingButton.GetHashCode())
                {
                    int i = CancelButtons.IndexOf(CancelButton);

                    if (i < SeventhLvl_SlotPanel.Children.Count && SeventhLvl_SlotPanel.Children[i] != null)
                        SeventhLvl_SlotPanel.Children.RemoveAt(i);

                    SeventhLvl_CancelBtnsPanel.Children.Remove(SendingButton);
                }

            }
        }

        private void EighthLevel_CancelButtonClick(object sender, RoutedEventArgs e)
        {
            List<Button> CancelButtons = new List<Button>();
            Button SendingButton = (Button)sender;

            foreach (UIElement CancelButtonElement in EighthLvl_CancelBtnsPanel.Children)
            {
                Button CancelButton = (Button)CancelButtonElement;
                CancelButtons.Add(CancelButton);
            }

            foreach (Button CancelButton in CancelButtons)
            {
                if (CancelButton.GetHashCode() == SendingButton.GetHashCode())
                {
                    int i = CancelButtons.IndexOf(CancelButton);

                    if (i < EighthLvl_SlotPanel.Children.Count && EighthLvl_SlotPanel.Children[i] != null)
                        EighthLvl_SlotPanel.Children.RemoveAt(i);

                    EighthLvl_CancelBtnsPanel.Children.Remove(SendingButton);
                }

            }
        }

        private void NinthLevel_CancelButtonClick(object sender, RoutedEventArgs e)
        {
            List<Button> CancelButtons = new List<Button>();
            Button SendingButton = (Button)sender;

            foreach (UIElement CancelButtonElement in NinthLvl_CancelBtnsPanel.Children)
            {
                Button CancelButton = (Button)CancelButtonElement;
                CancelButtons.Add(CancelButton);
            }

            foreach (Button CancelButton in CancelButtons)
            {
                if (CancelButton.GetHashCode() == SendingButton.GetHashCode())
                {
                    int i = CancelButtons.IndexOf(CancelButton);

                    if (i < NinthLvl_SlotPanel.Children.Count && NinthLvl_SlotPanel.Children[i] != null)
                        NinthLvl_SlotPanel.Children.RemoveAt(i);

                    NinthLvl_CancelBtnsPanel.Children.Remove(SendingButton);
                }

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

        private void Generate_PreparedCheckboxes_1stLevel()
        {
            FirstLvl_PreparedBoxesPanel.Children.Clear();

            foreach(Border SpellTB in FirstLvl_SlotPanel.Children)
            {
                CheckBox PreparedBox = Create_CheckBox();                
                FirstLvl_PreparedBoxesPanel.Children.Add(PreparedBox);                
            }
        }

        private void Generate_PreparedCheckboxes_2ndLevel()
        {
            SecondLvl_PreparedBoxesPanel.Children.Clear();

            foreach (Border SpellTB in SecondLvl_SlotPanel.Children)
            {
                CheckBox PreparedBox = Create_CheckBox();
                SecondLvl_PreparedBoxesPanel.Children.Add(PreparedBox);
            }
        }

        private void Generate_PreparedCheckboxes_3rdLevel()
        {
            ThirdLvl_PreparedBoxesPanel.Children.Clear();

            foreach (Border SpellTB in ThirdLvl_SlotPanel.Children)
            {
                CheckBox PreparedBox = Create_CheckBox();
                ThirdLvl_PreparedBoxesPanel.Children.Add(PreparedBox);
            }
        }

        private void Generate_PreparedCheckboxes_4thLevel()
        {
            FourthLvl_PreparedBoxesPanel.Children.Clear();

            foreach (Border SpellTB in FourthLvl_SlotPanel.Children)
            {
                CheckBox PreparedBox = Create_CheckBox();
                FourthLvl_PreparedBoxesPanel.Children.Add(PreparedBox);
            }
        }

        private void Generate_PreparedCheckboxes_5thLevel()
        {
            FifthLvl_PreparedBoxesPanel.Children.Clear();

            foreach (Border SpellTB in FifthtLvl_SlotPanel.Children)
            {
                CheckBox PreparedBox = Create_CheckBox();
                FifthLvl_PreparedBoxesPanel.Children.Add(PreparedBox);
            }
        }

        private void Generate_PreparedCheckboxes_6thLevel()
        {
            SixthLvl_PreparedBoxesPanel.Children.Clear();

            foreach (Border SpellTB in SixthLvl_SlotPanel.Children)
            {
                CheckBox PreparedBox = Create_CheckBox();
                SixthLvl_PreparedBoxesPanel.Children.Add(PreparedBox);
            }
        }

        private void Generate_PreparedCheckboxes_7thLevel()
        {
            SeventhLvl_PreparedBoxesPanel.Children.Clear();

            foreach (Border SpellTB in SeventhLvl_SlotPanel.Children)
            {
                CheckBox PreparedBox = Create_CheckBox();
                SeventhLvl_PreparedBoxesPanel.Children.Add(PreparedBox);
            }
        }

        private void Generate_PreparedCheckboxes_8thLevel()
        {
            EighthLvl_PreparedBoxesPanel.Children.Clear();

            foreach (Border SpellTB in EighthLvl_SlotPanel.Children)
            {
                CheckBox PreparedBox = Create_CheckBox();
                EighthLvl_PreparedBoxesPanel.Children.Add(PreparedBox);
            }
        }

        private void Generate_PreparedCheckboxes_9thLevel()
        {
            NinthLvl_PreparedBoxesPanel.Children.Clear();

            foreach (Border SpellTB in NinthLvl_SlotPanel.Children)
            {
                CheckBox PreparedBox = Create_CheckBox();
                NinthLvl_PreparedBoxesPanel.Children.Add(PreparedBox);
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
                Generate_CantripCancelButtons();
            }
        }

        private void Add_1stLvlSpl_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (FirstLvl_SelectionCB.SelectedIndex > 0 && !CheckIfAlreadyInSpellList(FirstLvl_SelectionCB.SelectedItem.ToString(), FirstLvl_SlotPanel))
            {
                Border slotItem = Create_BorderedSpellSlotTextblock(FirstLvl_SelectionCB.SelectedItem.ToString());
                FirstLvl_SlotPanel.Children.Add(slotItem);
                Generate_1stLvlCancelButtons();
                Generate_PreparedCheckboxes_1stLevel();
            }
        }

        private void Add_2ndLvlSpl_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (SecondLvl_SelectionCB.SelectedIndex > 0 && !CheckIfAlreadyInSpellList(SecondLvl_SelectionCB.SelectedItem.ToString(), SecondLvl_SlotPanel))
            {
                Border slotItem = Create_BorderedSpellSlotTextblock(SecondLvl_SelectionCB.SelectedItem.ToString());
                SecondLvl_SlotPanel.Children.Add(slotItem);
                Generate_2ndLvlCancelButtons();
                Generate_PreparedCheckboxes_2ndLevel();
            }
        }

        private void Add_3rdLvlSpl_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (ThirdLvl_SelectionCB.SelectedIndex > 0 && !CheckIfAlreadyInSpellList(ThirdLvl_SelectionCB.SelectedItem.ToString(), ThirdLvl_SlotPanel))
            {
                Border slotItem = Create_BorderedSpellSlotTextblock(ThirdLvl_SelectionCB.SelectedItem.ToString());
                ThirdLvl_SlotPanel.Children.Add(slotItem);
                Generate_3rdLvlCancelButtons();
                Generate_PreparedCheckboxes_3rdLevel();
            }
        }

        private void Add_4thLvlSpl_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (FourthLvl_SelectionCB.SelectedIndex > 0 && !CheckIfAlreadyInSpellList(FourthLvl_SelectionCB.SelectedItem.ToString(), FourthLvl_SlotPanel))
            {
                Border slotItem = Create_BorderedSpellSlotTextblock(FourthLvl_SelectionCB.SelectedItem.ToString());
                FourthLvl_SlotPanel.Children.Add(slotItem);
                Generate_4thLvlCancelButtons();
                Generate_PreparedCheckboxes_4thLevel();
            }
        }

        private void Add_5thLvlSpl_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (FifthLvl_SelectionCB.SelectedIndex > 0 && !CheckIfAlreadyInSpellList(FifthLvl_SelectionCB.SelectedItem.ToString(), FifthtLvl_SlotPanel))
            {
                Border slotItem = Create_BorderedSpellSlotTextblock(FifthLvl_SelectionCB.SelectedItem.ToString());
                FifthtLvl_SlotPanel.Children.Add(slotItem);
                Generate_5thLvlCancelButtons();
                Generate_PreparedCheckboxes_5thLevel();
            }
        }

        private void Add_6thLvlSpl_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (SixthLvl_SelectionCB.SelectedIndex > 0 && !CheckIfAlreadyInSpellList(SixthLvl_SelectionCB.SelectedItem.ToString(), SixthLvl_SlotPanel))
            {
                Border slotItem = Create_BorderedSpellSlotTextblock(SixthLvl_SelectionCB.SelectedItem.ToString());
                SixthLvl_SlotPanel.Children.Add(slotItem);
                Generate_6thLvlCancelButtons();
                Generate_PreparedCheckboxes_6thLevel();
            }
        }
        private void Add_7thLvlSpl_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (SeventhLvl_SelectionCB.SelectedIndex > 0 && !CheckIfAlreadyInSpellList(SeventhLvl_SelectionCB.SelectedItem.ToString(), SeventhLvl_SlotPanel))
            {
                Border slotItem = Create_BorderedSpellSlotTextblock(SeventhLvl_SelectionCB.SelectedItem.ToString());
                SeventhLvl_SlotPanel.Children.Add(slotItem);
                Generate_7thLvlCancelButtons();
                Generate_PreparedCheckboxes_7thLevel();
            }
        }

        private void Add_8thLvlSpl_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (EighthLvl_SelectionCB.SelectedIndex > 0 && !CheckIfAlreadyInSpellList(EighthLvl_SelectionCB.SelectedItem.ToString(), EighthLvl_SlotPanel))
            {
                Border slotItem = Create_BorderedSpellSlotTextblock(EighthLvl_SelectionCB.SelectedItem.ToString());
                EighthLvl_SlotPanel.Children.Add(slotItem);
                Generate_8thLvlCancelButtons();
                Generate_PreparedCheckboxes_8thLevel();
            }
        }

        private void Add_9thLvlSpl_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (NinthLvl_SelectionCB.SelectedIndex > 0 && !CheckIfAlreadyInSpellList(NinthLvl_SelectionCB.SelectedItem.ToString(), NinthLvl_SlotPanel))
            {
                Border slotItem = Create_BorderedSpellSlotTextblock(NinthLvl_SelectionCB.SelectedItem.ToString());
                NinthLvl_SlotPanel.Children.Add(slotItem);
                Generate_9thLvlCancelButtons();
                Generate_PreparedCheckboxes_9thLevel();
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
            if(BoxText.Length > 0)
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
            {
                return true;
            }
        }

        private void Set_SpellSlotsExpended_OriginalValue()
        {
            foreach(TextBox SpellSlotsTotalBox in SpellSlotsTotalBoxes)
            {
                int spellLevelIndex = SpellSlotsTotalBoxes.IndexOf(SpellSlotsTotalBox);
                
                if(SpellSlotsTotalBox.Text.Length > 0)
                    SpellsExpenedTxts[spellLevelIndex].Text = "0";
            }
        }

        private void EnableCastButtons()
        {
            foreach(Button CastButton in CastButtons)
            {
                int indexOfCastButton = CastButtons.IndexOf(CastButton);

                foreach(TextBox SlotBox in SpellSlotsTotalBoxes)
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

        private void Cast_1stLvl_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (SlotsTotal_1stLvl_TB.Text.Length > 0 && int.TryParse(SlotsTotal_1stLvl_TB.Text, out int number))
            {
                int spellsSlotsExpended = int.Parse(SlotsExp_1stLvlTxt.Text);
                spellsSlotsExpended++;

                if (spellsSlotsExpended > int.Parse(SlotsTotal_1stLvl_TB.Text))
                {
                    SlotsExp_1stLvlTxt.Text = SlotsTotal_1stLvl_TB.Text;
                    MessageBox.Show(AllSpellSlotsExpendedMessage);
                }

                else
                    SlotsExp_1stLvlTxt.Text = spellsSlotsExpended.ToString();
            }

            else
                MessageBox.Show(NoSpellSlotsForThisLevelMessage);
        }

        private void Cast_2ndLvl_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (SlotsTotal_2ndLvl_TB.Text.Length > 0 && int.TryParse(SlotsTotal_2ndLvl_TB.Text, out int number))
            {
                int spellsSlotsExpended = int.Parse(SlotsExp_2ndLvlTxt.Text);
                spellsSlotsExpended++;

                if (spellsSlotsExpended > int.Parse(SlotsTotal_2ndLvl_TB.Text))
                {
                    SlotsExp_2ndLvlTxt.Text = SlotsTotal_2ndLvl_TB.Text;
                    MessageBox.Show(AllSpellSlotsExpendedMessage);
                }

                else
                    SlotsExp_2ndLvlTxt.Text = spellsSlotsExpended.ToString();
            }

            else
                MessageBox.Show(NoSpellSlotsForThisLevelMessage);
        }

        private void Cast_3rdLvl_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (SlotsTotal_3rdLvl_TB.Text.Length > 0 && int.TryParse(SlotsTotal_3rdLvl_TB.Text, out int number))
            {
                int spellsSlotsExpended = int.Parse(SlotsExp_3rdLvlTxt.Text);
                spellsSlotsExpended++;

                if (spellsSlotsExpended > int.Parse(SlotsTotal_3rdLvl_TB.Text))
                {
                    SlotsExp_3rdLvlTxt.Text = SlotsTotal_3rdLvl_TB.Text;
                    MessageBox.Show(AllSpellSlotsExpendedMessage);
                }

                else
                    SlotsExp_3rdLvlTxt.Text = spellsSlotsExpended.ToString();
            }

            else
                MessageBox.Show(NoSpellSlotsForThisLevelMessage);
        }

        private void Cast_4thLvl_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (SlotsTotal_4thLvl_TB.Text.Length > 0 && int.TryParse(SlotsTotal_4thLvl_TB.Text, out int number))
            {
                int spellsSlotsExpended = int.Parse(SlotsExp_4thLvlTxt.Text);
                spellsSlotsExpended++;

                if (spellsSlotsExpended > int.Parse(SlotsTotal_4thLvl_TB.Text))
                {
                    SlotsExp_4thLvlTxt.Text = SlotsTotal_4thLvl_TB.Text;
                    MessageBox.Show(AllSpellSlotsExpendedMessage);
                }

                else
                    SlotsExp_4thLvlTxt.Text = spellsSlotsExpended.ToString();
            }

            else
                MessageBox.Show(NoSpellSlotsForThisLevelMessage);
        }

        private void Cast_5thLvl_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (SlotsTotal_5thLvl_TB.Text.Length > 0 && int.TryParse(SlotsTotal_5thLvl_TB.Text, out int number))
            {
                int spellsSlotsExpended = int.Parse(SlotsExp_5thLvlTxt.Text);
                spellsSlotsExpended++;

                if (spellsSlotsExpended > int.Parse(SlotsTotal_5thLvl_TB.Text))
                {
                    SlotsExp_5thLvlTxt.Text = SlotsTotal_5thLvl_TB.Text;
                    MessageBox.Show(AllSpellSlotsExpendedMessage);
                }

                else
                    SlotsExp_5thLvlTxt.Text = spellsSlotsExpended.ToString();
            }

            else
                MessageBox.Show(NoSpellSlotsForThisLevelMessage);
        }

        private void Cast_6thLvl_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (SlotsTotal_6thLvl_TB.Text.Length > 0 && int.TryParse(SlotsTotal_6thLvl_TB.Text, out int number))
            {
                int spellsSlotsExpended = int.Parse(SlotsExp_6thLvlTxt.Text);
                spellsSlotsExpended++;

                if (spellsSlotsExpended > int.Parse(SlotsTotal_6thLvl_TB.Text))
                {
                    SlotsExp_6thLvlTxt.Text = SlotsTotal_6thLvl_TB.Text;
                    MessageBox.Show(AllSpellSlotsExpendedMessage);
                }

                else
                    SlotsExp_6thLvlTxt.Text = spellsSlotsExpended.ToString();
            }

            else
                MessageBox.Show(NoSpellSlotsForThisLevelMessage);
        }

        private void Cast_7thLvl_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (SlotsTotal_7thLvl_TB.Text.Length > 0 && int.TryParse(SlotsTotal_7thLvl_TB.Text, out int number))
            {
                int spellsSlotsExpended = int.Parse(SlotsExp_7thLvlTxt.Text);
                spellsSlotsExpended++;

                if (spellsSlotsExpended > int.Parse(SlotsTotal_7thLvl_TB.Text))
                {
                    SlotsExp_7thLvlTxt.Text = SlotsTotal_7thLvl_TB.Text;
                    MessageBox.Show(AllSpellSlotsExpendedMessage);
                }

                else
                    SlotsExp_7thLvlTxt.Text = spellsSlotsExpended.ToString();
            }

            else
                MessageBox.Show(NoSpellSlotsForThisLevelMessage);
        }

        private void Cast_8thLvl_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (SlotsTotal_8thLvl_TB.Text.Length > 0 && int.TryParse(SlotsTotal_8thLvl_TB.Text, out int number))
            {
                int spellsSlotsExpended = int.Parse(SlotsExp_8thLvlTxt.Text);
                spellsSlotsExpended++;

                if (spellsSlotsExpended > int.Parse(SlotsTotal_8thLvl_TB.Text))
                {
                    SlotsExp_8thLvlTxt.Text = SlotsTotal_8thLvl_TB.Text;
                    MessageBox.Show(AllSpellSlotsExpendedMessage);
                }

                else
                    SlotsExp_8thLvlTxt.Text = spellsSlotsExpended.ToString();
            }

            else
                MessageBox.Show(NoSpellSlotsForThisLevelMessage);
        }

        private void Cast_9thLvl_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (SlotsTotal_9thLvl_TB.Text.Length > 0 && int.TryParse(SlotsTotal_9thLvl_TB.Text, out int number))
            {
                int spellsSlotsExpended = int.Parse(SlotsExp_9thLvlTxt.Text);
                spellsSlotsExpended++;

                if (spellsSlotsExpended > int.Parse(SlotsTotal_9thLvl_TB.Text))
                {
                    SlotsExp_9thLvlTxt.Text = SlotsTotal_9thLvl_TB.Text;
                    MessageBox.Show(AllSpellSlotsExpendedMessage);
                }

                else
                    SlotsExp_9thLvlTxt.Text = spellsSlotsExpended.ToString();
            }

            else
                MessageBox.Show(NoSpellSlotsForThisLevelMessage);
        }
    }
}
