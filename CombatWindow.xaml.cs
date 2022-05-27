using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Shapes;

namespace DnD_CharSheet_5e
{
    /// <summary>
    /// Interaktionslogik für CombatWindow.xaml
    /// </summary>
    public partial class CombatWindow : Window
    {
        #region PROPERTIES

        List<Combatant> combatants = new List<Combatant>();

        Weapon FirstWeapon = new Weapon();
        Weapon SecondWeapon = new Weapon();
        Weapon ThirdWeapon = new Weapon();

        string firstSelectedItem = "Unarmed Strike";
        string secondSelectedItem = "Unarmed Strike";
        string thirdSelectedItem = "Unarmed Strike";

        const int unarmedStrike_Damage = 1;
        const string unarmedStrike_Caption = "You have no weapon selected. But you can still punch and kick your enemies.";

        bool IsFirstInitiative = true;        

        // The respective Ellipses are called 'RadioButtons' here because they serve as such.
        
        List<Ellipse> DeathSaveRadioButtons;

        // 'ds' is short for 'Death Saving Throw' or 'Death Save'       

        bool IsUnconscious = false;

        bool ds_01_success_checked = false;
        bool ds_02_success_checked = false;
        bool ds_03_success_checked = false;

        bool ds_01_failure_checked = false;
        bool ds_02_failure_checked = false;
        bool ds_03_failure_checked = false;

        /* EXPLANATORY NOTE ON THE GAME MECHANICS BEHIND 'DEATH SAVING THROWS':
         * In the game whenever the hitpoints of a character drop to 0 or lower this character is considered to be in the process of dying - but not dead yet.
         * By rolling the 20-sided die several times it is determined whether the character actually dies or stabilizes. 
         * The number on the D20 has to be higher or equal to 10 to be a success. A lower result is considered a failure.
         * A '20' on the die means two successes - a '1' means two failures.
         * Has the character stabilzed he or she are is unconscious until healed properly. 
         * Has the character actually died he or she is dead for good. But characters can still be resurrected in the game by means of magic/ spells.
        */

        const string DeathMessage = "Oh No! How sad! Your character died!\nBut, there is still hope.\nWith the approval of your DM click the 'resurrect'-button below and your character will again walk among the living.";
        const string StabilizedMessage = "Oh, thank goodness! Your character has stabilized.";

        const string InvalidValueErrorMessage = "You have entered an invalid value. Please enter an integer.";

        #endregion

        #region CONSTRUCTOR
        public CombatWindow()
        {
            InitializeComponent();

            this.DataContext = this;

            Init_CombatUI();

            SheetManager.CS_Manager_Inst.character.hpChanged += Update_CharacterState;
            SheetManager.CS_Manager_Inst.character.hpChanged += Update_HP_Txt;
            SheetManager.CS_Manager_Inst.character.tempHPChanged += Update_TempHP_Txt;
            SheetManager.CS_Manager_Inst.character.acChanged += Update_AC_Txt;
        }
        #endregion

        #region METHODS FOR INITIALIZING UI
        private void Init_CombatUI()
        {
            Initialize_InitiativeBoxText();
            Initialize_WeaponDropDowns();
            Initialize_ACHP_Panel();

            Initialize_DeathSave_RadioButtons();

            Initialize_DiceSound_for_DieButtons();
        }

        private void Initialize_InitiativeBoxText()
        {
            InitiativeBonusBox.Text = SheetManager.CS_Manager_Inst.character.Dexterity.Modifier.ToString();
        }

        private void Initialize_WeaponDropDowns()
        {
            FirstWeapon_CB.Items.Add("Unarmed Strike");
            SecondWeapon_CB.Items.Add("Unarmed Strike");
            ThirdWeapon_CB.Items.Add("Unarmed Strike");
            
            foreach (Weapon weapon in SheetManager.CS_Manager_Inst.character.cInventory.cWeapons)
            {
                FirstWeapon_CB.Items.Add(weapon.ItemName);
                SecondWeapon_CB.Items.Add(weapon.ItemName);
                ThirdWeapon_CB.Items.Add(weapon.ItemName);
            }
        }       

        private void Initialize_ACHP_Panel()
        {
            AC_TB.Text = SheetManager.CS_Manager_Inst.character.AC.ToString();

            HP_Max_TB.Text = SheetManager.CS_Manager_Inst.character.MaxHP.ToString();
            HP_Curr_TB.Text = SheetManager.CS_Manager_Inst.character.CurrentHP.ToString();
            TempHP_TB.Text = SheetManager.CS_Manager_Inst.character.TempHP.ToString();
        }

        private void Initialize_DeathSave_RadioButtons()
        {
            DeathSaveRadioButtons = new List<Ellipse>();

            foreach(Ellipse PseudoRadioButton in DeathSaveSuccesses_Grid.Children)
            {
                DeathSaveRadioButtons.Add(PseudoRadioButton);
            }

            foreach (Ellipse PseudoRadioButton in DeathSaveFailures_Grid.Children)
            {
                DeathSaveRadioButtons.Add(PseudoRadioButton);
            }
        }

        private void Initialize_DiceSound_for_DieButtons()
        {
            foreach(UIElement element in AttackPanel.Children)
            {
                if(element is Button)
                {
                    // Since the the DiceSound event should fire first - and than the effects should be seen - 'PreviewMouseLeftButtonDown' is used here instead of assigning to the click 
                    Button tempBtn = (Button)element;
                    tempBtn.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(DiceSoundClick);
                }
            }

            // Since not all of the buttons amongst the UI_Elements should play the click sound, the event handler is assigned here manually
            // instead of looping through the elements and finding the respective elements which would make the code longer.

            RollInitiativeButton.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(DiceSoundClick);
            HealDice_Btn.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(DiceSoundClick);
            Dice_Add_TempHP_Btn.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(DiceSoundClick);
            DeathSave_Btn.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(DiceSoundClick);
        }

        private void DiceSoundClick(object sender, MouseButtonEventArgs e)
        {
            FileManager.FM_Inst.Play_DiceSound();
        }

        #endregion

        #region ATTACK PANEL METHODS
        public void Melee_Attack_Click(object sender, RoutedEventArgs e)
        {
            Melee_Result.Text = SheetManager.CS_Manager_Inst.Melee_Attack().ToString();
        }

        public void Ranged_Attack_Click(object sender, RoutedEventArgs e)
        {
            Ranged_Result.Text = SheetManager.CS_Manager_Inst.Ranged_Attack().ToString();
        }

        #region FIND AND SET WEAPONS ON_SELECTIONCHANGED

        private Weapon Find_Weapon(string weaponName)
        {
            Weapon tempWeapon = SheetManager.CS_Manager_Inst.character.cInventory.cWeapons.Find(weaponElement => weaponElement.ItemName == weaponName);

            return tempWeapon;
        }

        private void FirstWeapon_CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            firstSelectedItem = FirstWeapon_CB.SelectedItem.ToString();
            
            if(firstSelectedItem != "Unarmed Strike")
            {
                FirstWeapon = Find_Weapon(firstSelectedItem);
                FirstWeapon_CB.ToolTip = FirstWeapon.ItemInfo;
            }

            else
            {
                FirstWeapon = null;
                FirstWeapon_CB.ToolTip = unarmedStrike_Caption;
            }
        }

        private void SecondWeapon_CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            secondSelectedItem = SecondWeapon_CB.SelectedItem.ToString();
            
            if(secondSelectedItem != "Unarmed Strike")
            {
                SecondWeapon = Find_Weapon(secondSelectedItem);
                SecondWeapon_CB.ToolTip = SecondWeapon.ItemInfo;
            }

            else
            {
                SecondWeapon = null;
                SecondWeapon_CB.ToolTip = unarmedStrike_Caption;
            }
        }

        private void ThirdWeapon_CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            thirdSelectedItem = ThirdWeapon_CB.SelectedItem.ToString();

            if(thirdSelectedItem != "Unarmed Strike")
            {
                ThirdWeapon = Find_Weapon(thirdSelectedItem);
                ThirdWeapon_CB.ToolTip = ThirdWeapon.ItemInfo;
            }

            else
            {
                ThirdWeapon = null;
                ThirdWeapon_CB.ToolTip = unarmedStrike_Caption;
            }
        }

        #endregion


        #region DAMAGE BUTTONS EVENT HANDLER

        private void Damage_Btn_1stWeapon_Click(object sender, RoutedEventArgs e)
        {
            if(FirstWeapon != null)
            {
                Damage_Result_TB_01.Text = SheetManager.CS_Manager_Inst.Roll_for_Damage(FirstWeapon).ToString();
                
            }

            else
            {
                Damage_Result_TB_01.Text = UnarmedStrike(); 
            }
        }

        private void Damage_Btn_2ndWeapon_Click(object sender, RoutedEventArgs e)
        {
            if (SecondWeapon != null)
            {
                Damage_Result_TB_02.Text = SheetManager.CS_Manager_Inst.Roll_for_Damage(SecondWeapon).ToString();

            }

            else
            {
                Damage_Result_TB_02.Text = UnarmedStrike();
            }
        }

        private void Damage_Btn_3rdWeapon_Click(object sender, RoutedEventArgs e)
        {
            if (ThirdWeapon != null)
            {
                Damage_Result_TB_03.Text = SheetManager.CS_Manager_Inst.Roll_for_Damage(ThirdWeapon).ToString();

            }

            else
            {
                Damage_Result_TB_03.Text = UnarmedStrike();
            }
        }

        private string UnarmedStrike()
        {
            return (unarmedStrike_Damage + SheetManager.CS_Manager_Inst.character.Strength.Modifier).ToString();
        }

        #endregion


        #endregion

        #region INITIATIVE PANEL METHODS

        private void InitiativeButton_Click(object sender, RoutedEventArgs e)
        {
            Check_for_FirstInitiativeRoll();
            InitiativeResultBox.Text = SheetManager.CS_Manager_Inst.Roll_for_Initiative().ToString();
            
        }

        #region ACTIVATE/ DEACTIVATE COMBATANTS PANEL
        private void Check_for_FirstInitiativeRoll()
        {
            if(IsFirstInitiative == true)
            {
                IsFirstInitiative = false;
            }
        }

        private void ActivateIniOrder_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (IsFirstInitiative == false)
            {
                if(IniOrderPanel.Visibility == Visibility.Collapsed)
                {
                    Enable_CombatantsPanel();
                }

                else
                {
                    Reset_IniOrder();
                }
            }
        }

        private void Enable_CombatantsPanel()
        {
            IniOrderPanel.Visibility = Visibility.Visible;
        }

        private void Reset_IniOrder()
        {           
            Combatants.Clear();
            InitiativeOrder_LV.Items.Refresh();
        }
        #endregion


        /* EXPLANATORY NOTE ON THE GAME MECHANIC BEHING 'INITIATIVE'
         * In D&D the turn order during combat is determined by the so called 'Initiative'.
         * Every participant of the combat rolls a 20-sided die (d20) and adds their 'Dexterity'-Bonus - the result is the value for the participants Initiative.
         * The turn order is sorted from highest to lowest Initiative-Result.
        */

        #region INITIATIVE ORDER LIST METHODS
        private void NewCombatant_Btn_Click(object sender, RoutedEventArgs e)
        {
            CombatantName_Box.IsEnabled = true;
            IniValue_Box.IsEnabled = true;
        }

        private void AddCombatant_Btn_Click(object sender, RoutedEventArgs e)
        {
            string combatantName = CombatantName_Box.Text;

            if(int.TryParse(IniValue_Box.Text, out int combatantValue))
            {
                Combatant newCombatant = new Combatant(combatantName, combatantValue);
                combatants.Add(newCombatant);
                SortCombatants();
                InitiativeOrder_LV.Items.Refresh();

                Clear_Combatant_InputFields();                
            }

            else
            {
                MessageBox.Show(InvalidValueErrorMessage);
            }

        }        

        private void Clear_Combatant_InputFields()
        {
            CombatantName_Box.IsEnabled = false;
            IniValue_Box.IsEnabled = false;

            CombatantName_Box.Text = null;
            IniValue_Box.Text = null;
        }
        #endregion

        #region ICOMPARABLE FOR SORTING COMBATANTS

        public class Combatant : IComparable<Combatant>
        {
            public string CombatantName { get; }
            public int IniOrderValue { get; }

            public Combatant(string name, int orderValue)
            {
                CombatantName = name;
                IniOrderValue = orderValue;
            }

            public int CompareTo(Combatant ScndCombatant)
            {
                if (IniOrderValue < ScndCombatant.IniOrderValue)
                {
                    return 1;
                }

                else if (IniOrderValue > ScndCombatant.IniOrderValue)
                {
                    return -1;
                }

                else
                    return 0;
            }
        }

        public List<Combatant> Combatants
        {
            get
            {
                return combatants;
            }

        }

        private void SortCombatants()
        {
            combatants.Sort();
        }

        #endregion


        #endregion

        #region HITPOINTS, DAMAGE AND DEATHS SAVING THROW PANEL METHODS

        #region TEMPORARY HITPOINT METHODS
        private void AddTempHP_Btn_Click(object sender, RoutedEventArgs e)
        {
            
            if(int.TryParse(AddTempHP_TB.Text, out int tempHP))
            {
                SheetManager.CS_Manager_Inst.character.Set_tempHP(tempHP);
                AddTempHP_TB.Text = null;
            }

            else
            {
                MessageBox.Show(InvalidValueErrorMessage);
            }
        }

        private void Dice_Add_TempHP_Btn_Click(object sender, RoutedEventArgs e)
        {

            if(int.TryParse(Dice_Add_TempHP_01_TB.Text, out int tempHP_Dice_Numerator) && int.TryParse(Dice_Add_TempHP_02_TP.Text, out int tempHP_Dice_Denominator))
            {
                SheetManager.CS_Manager_Inst.Add_TempHP_withDice(tempHP_Dice_Numerator, tempHP_Dice_Denominator);
            }

            else
            {
                MessageBox.Show(InvalidValueErrorMessage);
            }

        }

        private void Update_TempHP_Txt()
        {
            if (SheetManager.CS_Manager_Inst.character.TempHP > 0)
            {
                TempHP_TB.Text = SheetManager.CS_Manager_Inst.character.TempHP.ToString();
            }

            else
            {
                TempHP_TB.Text = null;
            }
        }

        #endregion

        #region TAKE DAMAGE/ CHECK CHARACTER STATE METHODS
        private void Hit_Btn_Click(object sender, RoutedEventArgs e)
        {            

            if(int.TryParse(Hit_TB.Text, out int damage))
            {
                Check_for_InstantDeath(SheetManager.CS_Manager_Inst.character.CurrentHP, damage);
                SheetManager.CS_Manager_Inst.Get_Hit(damage);
                Check_ConsciousnessStatus();
                Hit_TB.Text = null;
            }

            else
            {
                MessageBox.Show(InvalidValueErrorMessage);
            }
        }

        private void Check_for_InstantDeath(int formerHp, int damage)
        {
            int damageExcess = formerHp - damage;
            int negativeHPMax = -SheetManager.CS_Manager_Inst.character.MaxHP;

            if (damageExcess <= negativeHPMax && damage >= SheetManager.CS_Manager_Inst.character.MaxHP)
            {
                Character_Dies();
            }
        }

        private void Character_Dies()
        {
            SheetManager.CS_Manager_Inst.character.IsAlive = false;
            MessageBox.Show(DeathMessage);
            Uncheck_DeathSaves();
            Disable_DeathSavingThrows();

            TempHP_Panel.IsEnabled = false;
            HealingPanel.IsEnabled = false;

            Resurrect_Btn.IsEnabled = true;

        }

        private void Check_ConsciousnessStatus()
        {
            if (SheetManager.CS_Manager_Inst.character.CurrentHP <= 0)
            {
                IsUnconscious = true;
                Enable_DeathSavingThrows();
                Enable_DS_Panel();
            }

            else
            {
                IsUnconscious = false;
                Check_AliveState();

                if (DS_Panel.IsEnabled == true)
                {
                    Disable_DeathSavingThrows();
                    Disable_DS_Panel();
                }
            }
        }

        private void Update_CharacterState()
        {
            if (SheetManager.CS_Manager_Inst.character.CurrentHP <= 0)
            {
                SheetManager.CS_Manager_Inst.character.IsConscious = false;
            }

            else
            {
                SheetManager.CS_Manager_Inst.character.IsConscious = true;
            }
        }

        private void Check_AliveState()
        {
            if (SheetManager.CS_Manager_Inst.character.IsAlive == false)
            {
                SheetManager.CS_Manager_Inst.character.IsAlive = true;
                TempHP_Panel.IsEnabled = true;
                HealingPanel.IsEnabled = true;
            }
        }
        #endregion

        #region DEATH SAVING THROW METHODS

        #region ENABLE/ DISABLE DEATH SAVING THROW PANEL
        private void Enable_DeathSavingThrows()
        {
            DeathSave_Btn.IsEnabled = true;

            foreach(Ellipse PseudoRadioButton in DeathSaveRadioButtons)
            {
                PseudoRadioButton.IsEnabled = true;
            }
        }

        private void Disable_DeathSavingThrows()
        {
            DeathSave_Btn.IsEnabled = false;

            foreach (Ellipse PseudoRadioButton in DeathSaveRadioButtons)
            {
                PseudoRadioButton.IsEnabled = false;
            }
        } 
        
        private void Enable_DS_Panel()
        {
            DS_Panel_Info_TBl.Visibility = Visibility.Collapsed;
            DS_Panel.Visibility = Visibility.Visible;
        }

        private void Disable_DS_Panel()
        {
            DS_Panel_Info_TBl.Visibility = Visibility.Visible;
            DS_Panel.Visibility = Visibility.Collapsed;
        }
        #endregion

        #region DEATH SAVING THROW SUCCESS AND FAILURE METHODS
        private void Check_DS_Successes(int ds_result)
        {
            if (ds_result == 20)
            {
                Become_Stable();
            }

            else
            {
                if (!ds_01_success_checked)
                {
                    ds_01_success_checked = true;
                    DeathSave_Success_RB_01.Fill = Brushes.LightCoral;
                }

                else
                {
                    if (!ds_02_success_checked)
                    {
                        ds_02_success_checked = true;
                        DeathSave_Success_RB_02.Fill = Brushes.LemonChiffon;
                    }

                    else
                    {
                        ds_03_success_checked = true;
                        DeathSave_Success_RB_03.Fill = Brushes.SpringGreen;
                        Become_Stable();
                    }
                }
            }
        }

        private void Check_DS_Failures(int ds_result)
        {
            if (!ds_01_failure_checked)
            {
                if (ds_result == 1)
                {
                    ds_01_failure_checked = true;
                    DeathSave_Failure_RB_01.Fill = Brushes.LightCoral;

                    ds_02_failure_checked = true;
                    DeathSave_Failure_RB_02.Fill = Brushes.Crimson;
                }

                else
                {
                    ds_01_failure_checked = true;
                    DeathSave_Failure_RB_01.Fill = Brushes.LightCoral;

                }
            }

            else
            {
                if (!ds_02_failure_checked)
                {
                    if (ds_result == 1)
                    {
                        ds_02_failure_checked = true;
                        DeathSave_Failure_RB_02.Fill = Brushes.Crimson;

                        ds_03_failure_checked = true;
                        DeathSave_Failure_RB_03.Fill = Brushes.DarkRed;
                        Character_Dies();
                    }

                    else
                    {
                        ds_02_failure_checked = true;
                        DeathSave_Failure_RB_02.Fill = Brushes.Crimson;
                    }
                }

                else
                {
                    ds_03_failure_checked = true;
                    DeathSave_Failure_RB_03.Fill = Brushes.DarkRed;
                    Character_Dies();
                }
            }
        }

        private void Uncheck_DeathSaves()
        {
            // Setting these bools one after another might not be the most elegant option
            // BUT not initializing them to an array or another list actually saves some code lines

            ds_01_success_checked = false;           
            ds_02_success_checked = false;
            ds_03_success_checked = false;
            ds_01_failure_checked = false;
            ds_02_failure_checked = false;
            ds_03_failure_checked = false;

            foreach(Ellipse PseudoRadioBtn in DeathSaveRadioButtons)
            {
                PseudoRadioBtn.Fill = Brushes.White;
            }
        }

        private void Become_Stable()
        {
            MessageBox.Show(StabilizedMessage);

            if (OneHP_CB.IsChecked == true)
            {
                SheetManager.CS_Manager_Inst.character.Set_CurrHP(1);
                IsUnconscious = false;

            }

            else
            {
                SheetManager.CS_Manager_Inst.character.Set_CurrHP(0);
            }

            Uncheck_DeathSaves();
            Disable_DeathSavingThrows();
            Disable_DS_Panel();
        }
        #endregion

        #region DEATH SAVING THROW UI CLICK EVENT HANDLER
        private void DeathSave_Success_01_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (IsUnconscious == true && ds_02_success_checked == false && ds_03_success_checked == false)
            {
                ds_01_success_checked = !ds_01_success_checked;

                if (ds_01_success_checked)
                {
                    DeathSave_Success_RB_01.Fill = Brushes.LightCoral;
                }

                else
                {
                    DeathSave_Success_RB_01.Fill = Brushes.White;
                }
            }
        }

        private void DeathSave_Success_02_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (IsUnconscious == true && ds_01_success_checked == true && ds_03_success_checked == false)
            {
                ds_02_success_checked = !ds_02_success_checked;

                if (ds_02_success_checked)
                {
                    DeathSave_Success_RB_02.Fill = Brushes.LemonChiffon;
                }

                else
                {
                    DeathSave_Success_RB_02.Fill = Brushes.White;
                }
            }
        }

        private void DeathSave_Success_03_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (IsUnconscious == true && ds_01_success_checked == true && ds_02_success_checked == true)
            {
                ds_03_success_checked = !ds_03_success_checked;

                if (ds_03_success_checked)
                {
                    DeathSave_Success_RB_03.Fill = Brushes.SpringGreen;
                    Become_Stable();
                }

                else
                {
                    DeathSave_Success_RB_03.Fill = Brushes.White;
                }
            }
        }

        private void DeathSave_Failure_01_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (IsUnconscious == true && ds_02_failure_checked == false && ds_03_failure_checked == false)
            {
                ds_01_failure_checked = !ds_01_failure_checked;

                if (ds_01_failure_checked)
                {
                    DeathSave_Failure_RB_01.Fill = Brushes.LightCoral;
                }

                else
                {
                    DeathSave_Failure_RB_01.Fill = Brushes.White;
                }
            }
        }

        private void DeathSave_Failure_02_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (IsUnconscious == true && ds_01_failure_checked == true && ds_03_failure_checked == false)
            {
                ds_02_failure_checked = !ds_02_failure_checked;

                if (ds_02_failure_checked)
                {
                    DeathSave_Failure_RB_02.Fill = Brushes.Crimson;
                }

                else
                {
                    DeathSave_Failure_RB_02.Fill = Brushes.White;
                }
            }
        }

        private void DeathSave_Failure_03_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (IsUnconscious == true && ds_01_failure_checked == true && ds_02_failure_checked == true)
            {
                ds_03_failure_checked = !ds_03_failure_checked;

                if (ds_03_failure_checked)
                {
                    DeathSave_Failure_RB_03.Fill = Brushes.DarkRed;
                    Character_Dies();
                }

                else
                {
                    DeathSave_Failure_RB_03.Fill = Brushes.White;
                }
            }

        }

        private void DeathSave_Btn_Click(object sender, RoutedEventArgs e)
        {
            int DS_Result = SheetManager.CS_Manager_Inst.Roll_DeathSave();
            DeathSave_Result_TB.Text = DS_Result.ToString();

            if(SheetManager.CS_Manager_Inst.DeathSave(DS_Result))
            {
                Check_DS_Successes(DS_Result);
                MessageBox.Show("Death Save was successful.");
            }

            else
            {
                Check_DS_Failures(DS_Result);
                MessageBox.Show("Death Save was unsuccessful.");
            }
        }
        #endregion

        #region RESURRECT BUTTON EVENT HANDLER AND RELATED METHODS
        private void Resurrect_Btn_Click(object sender, RoutedEventArgs e)
        {
            ReturnToLife();
            ComeBackWithOneHP();
            Resurrect_Btn.IsEnabled = false;
            Disable_DS_Panel();
            TempHP_Panel.IsEnabled = true;
            HealingPanel.IsEnabled = true;
        }

        private void ReturnToLife()
        {
            SheetManager.CS_Manager_Inst.character.IsAlive = true;
            IsUnconscious = false;
        }

        private void ComeBackWithOneHP()
        {
            SheetManager.CS_Manager_Inst.character.Set_CurrHP(1);
            HP_Curr_TB.Text = SheetManager.CS_Manager_Inst.character.CurrentHP.ToString();
        }
        #endregion

        #endregion

        #region HEALING METHODS

        private void HealTxt_Btn_Click(object sender, RoutedEventArgs e)
        {            

            if (int.TryParse(Heal_TB.Text, out int hpHealed))
            {
                SheetManager.CS_Manager_Inst.Heal_Amount(hpHealed);
                Check_ConsciousnessStatus();            
                Heal_TB.Text = null;
            }

            else
            {
                MessageBox.Show(InvalidValueErrorMessage);
            }
        }

        private void HealDice_Btn_Click(object sender, RoutedEventArgs e)
        {           

            if (int.TryParse(HP_Dice_01_TB.Text, out int healDiceNumerator) && int.TryParse(HP_Dice_02_TB.Text, out int healDiceDenominator))
            {
                int healDiceResult = SheetManager.CS_Manager_Inst.Heal_withDice(healDiceNumerator, healDiceDenominator);
                Check_ConsciousnessStatus();
                Heal_Dice_Result_TB.Text = healDiceResult.ToString();
            }

            else
            {
                MessageBox.Show(InvalidValueErrorMessage);
            }

        }

        private void Clear_HealingDice()
        {
            HP_Dice_01_TB.Text = null;
            HP_Dice_02_TB.Text = null;
        }

        private void Clear_HealinDice_Btn_Click(object sender, RoutedEventArgs e)
        {
            Clear_HealingDice();
        }
        #endregion

        #region UPDATE HITPOINTS/ ARMOR CLASS
        private void Update_HP_Txt()
        {
            HP_Curr_TB.Text = SheetManager.CS_Manager_Inst.character.CurrentHP.ToString();
        }

        private void Update_AC_Txt()
        {
            AC_TB.Text = SheetManager.CS_Manager_Inst.character.acChanged.ToString();
        }
        #endregion

        #endregion
    }
}
