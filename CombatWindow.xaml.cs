using System;
using System.Collections.Generic;
using System.Windows;

namespace DnD_CharSheet_5e
{
    /// <summary>
    /// Interaktionslogik für CombatWindow.xaml
    /// </summary>
    public partial class CombatWindow : Window
    {
        SheetManager CW_sheetManager = new SheetManager();

        int diceMachine_result = 0;

        public CombatWindow()
        {
            InitializeComponent();
        }

        public void Set_SheetManager(SheetManager sheetManager)
        {
            CW_sheetManager = sheetManager;
        }

        private void DiceMachine_Roll(int rollResult)
        {
            diceMachine_result += rollResult;
            DM_ResultBox.Text = diceMachine_result.ToString();
        }

        public void Reset_Button_Click(object sender, RoutedEventArgs e)
        {
            diceMachine_result = 0;
            DM_ResultBox.Clear();
        }
        

        public void D4_Bt_Click(object sender, RoutedEventArgs e)
        {
            DiceMachine_Roll(CW_sheetManager.dSys.Roll_D4());
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void D6_Bt_Click(object sender, RoutedEventArgs e)
        {
            DiceMachine_Roll(CW_sheetManager.dSys.Roll_D6());
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void D8_Bt_Click(object sender, RoutedEventArgs e)
        {
            DiceMachine_Roll(CW_sheetManager.dSys.Roll_D8());
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void D10_Bt_Click(object sender, RoutedEventArgs e)
        {
            DiceMachine_Roll(CW_sheetManager.dSys.Roll_D10());
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void D12_Bt_Click(object sender, RoutedEventArgs e)
        {
            DiceMachine_Roll(CW_sheetManager.dSys.Roll_D12());
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void D20_Bt_Click(object sender, RoutedEventArgs e)
        {
            DiceMachine_Roll(CW_sheetManager.dSys.Roll_D20());
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void D100_Bt_Click(object sender, RoutedEventArgs e)
        {
            DiceMachine_Roll(CW_sheetManager.dSys.Roll_D100());
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void Melee_Attack_Click(object sender, RoutedEventArgs e)
        {
            Melee_Result.Text = CW_sheetManager.Melee_Attack().ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void Ranged_Attack_Click(object sender, RoutedEventArgs e)
        {
            Ranged_Result.Text = CW_sheetManager.Ranged_Attack().ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void Damage_Roll_Click(object sender, RoutedEventArgs e)
        {
            Damage_Result.Text = CW_sheetManager.Damage_Roll().ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void INT_Spell_Atk_Click(object sender, RoutedEventArgs e)
        {
            int result;

            result = CW_sheetManager.dSys.Roll_D20() + CW_sheetManager.character.Get_intModifier() + CW_sheetManager.character.Get_ProfBonus();
            Spell_Result.Text = result.ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void WIS_Spell_Atk_Click(object sender, RoutedEventArgs e)
        {
            int result;

            result = CW_sheetManager.dSys.Roll_D20() + CW_sheetManager.character.Get_wisModifier() + CW_sheetManager.character.Get_ProfBonus();
            Spell_Result.Text = result.ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void CHA_Spell_Atk_Click(object sender, RoutedEventArgs e)
        {
            int result;

            result = CW_sheetManager.dSys.Roll_D20() + CW_sheetManager.character.Get_chaModifier() + CW_sheetManager.character.Get_ProfBonus();
            Spell_Result.Text = result.ToString();
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void Add_STRMod_Bt_Click(object sender, RoutedEventArgs e)
        {
            diceMachine_result += CW_sheetManager.character.Get_strModifier();
            DM_ResultBox.Text = diceMachine_result.ToString();
        }

        public void Add_DEXMod_Bt_Click(object sender, RoutedEventArgs e)
        {
            diceMachine_result += CW_sheetManager.character.Get_dexModifier();
            DM_ResultBox.Text = diceMachine_result.ToString();
        }

        public void Add_INTMod_Bt_Click(object sender, RoutedEventArgs e)
        {
            diceMachine_result += CW_sheetManager.character.Get_intModifier();
            DM_ResultBox.Text = diceMachine_result.ToString();
        }

        public void Add_WISMod_Bt_Click(object sender, RoutedEventArgs e)
        {
            diceMachine_result += CW_sheetManager.character.Get_wisModifier();
            DM_ResultBox.Text = diceMachine_result.ToString();
        }

        public void Add_CHAMod_Bt_Click(object sender, RoutedEventArgs e)
        {
            diceMachine_result += CW_sheetManager.character.Get_chaModifier();
            DM_ResultBox.Text = diceMachine_result.ToString();
        }
    }
}
