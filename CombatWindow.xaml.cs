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
        List<Combatant> combatants = new List<Combatant>();

        Weapon FirstWeapon = new Weapon();
        Weapon SecondWeapon = new Weapon();
        Weapon ThirdWeapon = new Weapon();

        string firstSelectedItem = "Unarmed Strike";
        string secondSelectedItem = "Unarmed Strike";
        string thirdSelectedItem = "Unarmed Strike";

        // Later monk abilities will be taken into consideration here
        const int unarmedStrike_Damage = 1;
        const string unarmedStrike_Caption = "You have no weapon selected. But you can still punch and kick your enemies.";

        bool IsFirstInitiative = true;

        const string DeathMessage = "Oh No! How sad! Your character died!\nBut, there is still hope.\nWith the approval of your DM click the 'resurrect'-button below and your character will again walk among the living.";
        const string StabilizedMessage = "Oh, thank goodness! Your character has stabilized.";

        bool IsUnconscious = false;

        // The respective Ellipses are called 'RadioButtons' here because they serve as such.
        List<Ellipse> DeathSaveRadioButtons;

        // 'ds' is short for 'Death Saving Throw' or 'Death Save'
        
        // Explanation of the mechanics:
        // In the game whenever the hitpoints of a character drop to 0 or lower this character is considered to be in the process of dying - but not dead yet.
        // By rolling the 20-sided die several times it is determined whether the character actually dies or stabilizes. 
        // The number on the D20 has to be higher or equal to 10 to be a success. A lower result is considered a failure.
        // A '20' on the die means two successes - a '1' means two failures.
        // Has the character stabilzed he or she are is unconscious until healed properly. 
        // Has the character actually died he or she is dead for good. But characters can still be resurrected in the game by means of magic/ spells.

        bool ds_01_success_checked = false;
        bool ds_02_success_checked = false;
        bool ds_03_success_checked = false;

        bool ds_01_failure_checked = false;
        bool ds_02_failure_checked = false;
        bool ds_03_failure_checked = false;

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

        private void Init_CombatUI()
        {
            Initialize_IniPanel();
            Initialize_WeaponDropDowns();
            Initialize_ACHP_Panel();

            Initialize_DeathSave_RadioButtons();

            Initialize_DiceSound_for_DieButtons();
        }

        private void Initialize_IniPanel()
        {
            IniBonus.Text = SheetManager.CS_Manager_Inst.character.Dexterity.Modifier.ToString();
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
                    // Since I want to fire the DiceSound first - and than see the effects - I use 'PreviewMouseLeftButtonDown' here instead of assigning to the click 
                    Button tempBtn = (Button)element;
                    tempBtn.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(DiceSoundClick);
                }
            }

            // Since not all of the buttons amongst the UI_Elements should play the click sound I considered easier here to assign the Handler for playing the click sound directly
            // instead of looping through the elements to find the respective elements.

            IniButton.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(DiceSoundClick);
            HealDice_Btn.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(DiceSoundClick);
            Dice_Add_TempHP_Btn.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(DiceSoundClick);
            DeathSave_Btn.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(DiceSoundClick);
        }

        private void DiceSoundClick(object sender, MouseButtonEventArgs e)
        {
            FileManager.FM_Inst.Play_DiceSound();
        }        

        public void Melee_Attack_Click(object sender, RoutedEventArgs e)
        {
            Melee_Result.Text = SheetManager.CS_Manager_Inst.Melee_Attack().ToString();
        }

        public void Ranged_Attack_Click(object sender, RoutedEventArgs e)
        {
            Ranged_Result.Text = SheetManager.CS_Manager_Inst.Ranged_Attack().ToString();
        }

        private Weapon Find_Weapon(string weaponName)
        {
            Weapon tempWeapon = new Weapon();

            foreach (Weapon weapon in SheetManager.CS_Manager_Inst.character.cInventory.cWeapons)
            {
                if (weapon.ItemName == weaponName)
                {
                    tempWeapon = weapon;
                }
            }

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

        private void Damage_Btn_1stWeapon_Click(object sender, RoutedEventArgs e)
        {
            if(FirstWeapon != null)
            {
                Damage_Result_TB_01.Text = Roll_for_Damage(FirstWeapon).ToString();
                
            }

            else
            {
                Damage_Result_TB_01.Text = (unarmedStrike_Damage + SheetManager.CS_Manager_Inst.character.Strength.Modifier).ToString(); 
            }
        }

        private void Damage_Btn_2ndWeapon_Click(object sender, RoutedEventArgs e)
        {
            if (SecondWeapon != null)
            {
                Damage_Result_TB_02.Text = Roll_for_Damage(SecondWeapon).ToString();
                
            }

            else
            {
                Damage_Result_TB_02.Text = (unarmedStrike_Damage + SheetManager.CS_Manager_Inst.character.Strength.Modifier).ToString();
            }
        }

        private void Damage_Btn_3rdWeapon_Click(object sender, RoutedEventArgs e)
        {
            if (ThirdWeapon != null)
            {
                Damage_Result_TB_03.Text = Roll_for_Damage(ThirdWeapon).ToString();
                
            }

            else
            {
                Damage_Result_TB_03.Text = (unarmedStrike_Damage + SheetManager.CS_Manager_Inst.character.Strength.Modifier).ToString();
            }
        }

        // eventually shorten here and insert functions to SheetManager or leave out there
        private int Roll_for_Damage(Weapon attackingWeapon)
        {
            int result = 0;

            if(attackingWeapon.IsRanged == false)
            {
                if(attackingWeapon.IsFinesse == false)
                {
                    result = SheetManager.CS_Manager_Inst.dSys.Roll_Custom(attackingWeapon.DamageNumerator, attackingWeapon.DamageDenominator) + SheetManager.CS_Manager_Inst.character.Strength.Modifier;
                }

                else
                {
                    if(SheetManager.CS_Manager_Inst.character.Dexterity.Modifier >= SheetManager.CS_Manager_Inst.character.Strength.Modifier)
                    {
                        result = SheetManager.CS_Manager_Inst.dSys.Roll_Custom(attackingWeapon.DamageNumerator, attackingWeapon.DamageDenominator) + SheetManager.CS_Manager_Inst.character.Dexterity.Modifier;
                    }

                    else
                    {
                        result = SheetManager.CS_Manager_Inst.dSys.Roll_Custom(attackingWeapon.DamageNumerator, attackingWeapon.DamageDenominator) + SheetManager.CS_Manager_Inst.character.Strength.Modifier;
                    }
                }
            }

            else
            {
                result = SheetManager.CS_Manager_Inst.dSys.Roll_Custom(attackingWeapon.DamageNumerator, attackingWeapon.DamageDenominator) + SheetManager.CS_Manager_Inst.character.Dexterity.Modifier;
            }

            return result;
        }        

        private void IniButton_Click(object sender, RoutedEventArgs e)
        {
            Check_for_FirstInitiativeRoll();
            IniResult.Text = SheetManager.CS_Manager_Inst.Roll_for_Initiative().ToString();
            
        }        

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
                MessageBox.Show("You entered an invalid value. Please enter an integer number.");
            }

        }           

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
                if (this.IniOrderValue < ScndCombatant.IniOrderValue)
                {
                    return 1;
                }

                else if (this.IniOrderValue > ScndCombatant.IniOrderValue)
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

        private void Clear_Combatant_InputFields()
        {
            CombatantName_Box.IsEnabled = false;
            IniValue_Box.IsEnabled = false;

            CombatantName_Box.Text = null;
            IniValue_Box.Text = null;
        }


        private void AddTempHP_Btn_Click(object sender, RoutedEventArgs e)
        {
            
            if(int.TryParse(AddTempHP_TB.Text, out int tempHP))
            {
                SheetManager.CS_Manager_Inst.character.Set_tempHP(tempHP);
                AddTempHP_TB.Text = null;
            }

            else
            {
                MessageBox.Show("You have entered an invalid value. Please enter an integer.");
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
                MessageBox.Show("You have entered one or more invalid values. Please enter integers.");
            }

        }

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
                MessageBox.Show("You have entered an invalid value. Please enter an integer.");
            }
        }

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

        private void Check_ConsciousnessStatus()
        {
            if(SheetManager.CS_Manager_Inst.character.CurrentHP <= 0)
            {
                IsUnconscious = true;
                Enable_DeathSavingThrows();
                Enable_DS_Panel();
            }

            else
            {
                IsUnconscious = false;
                Check_AliveState();

                if(DS_Panel.IsEnabled == true)
                {
                    Disable_DeathSavingThrows();
                    Disable_DS_Panel();
                }
            }
        }

        private void Check_AliveState()
        {
            if(SheetManager.CS_Manager_Inst.character.IsAlive == false)
            {
                SheetManager.CS_Manager_Inst.character.IsAlive = true;
                TempHP_Panel.IsEnabled = true;
                HealingPanel.IsEnabled = true;
            }
        }

        private void Check_for_InstantDeath(int formerHp, int damage)
        {
            int damageExcess = formerHp - damage;
            int negativeHPMax = -SheetManager.CS_Manager_Inst.character.MaxHP;

            if(damageExcess <= negativeHPMax && damage >= SheetManager.CS_Manager_Inst.character.MaxHP)
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

        // maybe do async await to make it look cooler -> the 'radio buttons' will change color only after a few (mili)seconds

        private void Uncheck_DeathSaves()
        {
            // Setting these bools one after another might not be the most elegant option, but I it seemed to me
            // not initializing them to an array or another list would actually save me some code lines

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

        // Temporary solution: There are several spells that bring a character back to life. This solution so far only imitates the effect of the basic 'Revivify'.
        // In the future I will use a small window, page or further UI-elements instead.

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
                MessageBox.Show("You have entered an invalid value. Please enter an integer.");
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
                MessageBox.Show("You have entered one or more invalid values. Please enter integers.");
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

        private void Resurrect_Btn_Click(object sender, RoutedEventArgs e)
        {
            ReturnToLife();
            ComeBackWithOneHP();
            Resurrect_Btn.IsEnabled = false;
            Disable_DS_Panel();
            TempHP_Panel.IsEnabled = true;
            HealingPanel.IsEnabled = true;
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

        private void Update_HP_Txt()
        {
            HP_Curr_TB.Text = SheetManager.CS_Manager_Inst.character.CurrentHP.ToString();
        }

        private void Update_AC_Txt()
        {
            AC_TB.Text = SheetManager.CS_Manager_Inst.character.acChanged.ToString();
        }

        private void Update_TempHP_Txt()
        {
            if(SheetManager.CS_Manager_Inst.character.TempHP > 0)
            {
                TempHP_TB.Text = SheetManager.CS_Manager_Inst.character.TempHP.ToString();
            }

            else
            {
                TempHP_TB.Text = null;
            }
        }

    }
}
