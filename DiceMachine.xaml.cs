using System.Windows;


namespace DnD_CharSheet_5e
{
    /// <summary>
    /// Interaktionslogik für DiceMachine.xaml
    /// </summary>
    public partial class DiceMachine : Window
    {
        SheetManager DM_SheetManager = new SheetManager();

        int diceMachine_result = 0;
        public DiceMachine()
        {
            InitializeComponent();
        }

        public void Set_SheetManager(SheetManager sheetManager)
        {
            DM_SheetManager = sheetManager;
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
            DiceMachine_Roll(DM_SheetManager.dSys.Roll_D4());
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void D6_Bt_Click(object sender, RoutedEventArgs e)
        {
            DiceMachine_Roll(DM_SheetManager.dSys.Roll_D6());
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void D8_Bt_Click(object sender, RoutedEventArgs e)
        {
            DiceMachine_Roll(DM_SheetManager.dSys.Roll_D8());
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void D10_Bt_Click(object sender, RoutedEventArgs e)
        {
            DiceMachine_Roll(DM_SheetManager.dSys.Roll_D10());
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void D12_Bt_Click(object sender, RoutedEventArgs e)
        {
            DiceMachine_Roll(DM_SheetManager.dSys.Roll_D12());
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void D20_Bt_Click(object sender, RoutedEventArgs e)
        {
            DiceMachine_Roll(DM_SheetManager.dSys.Roll_D20());
            FileManager.FM_Inst.Play_DiceSound();
        }

        public void D100_Bt_Click(object sender, RoutedEventArgs e)
        {
            DiceMachine_Roll(DM_SheetManager.dSys.Roll_D100());
            FileManager.FM_Inst.Play_DiceSound();
        }
    }
}
