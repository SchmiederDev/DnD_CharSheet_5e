using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;


namespace DnD_CharSheet_5e
{
    /// <summary>
    /// Interaktionslogik für DiceMachine.xaml
    /// </summary>
    public partial class DiceMachine : Window
    {
        D20_System DMachine;

        List<Button> DiceButtons;
        
        int diceMachine_result = 0;
        public DiceMachine()
        {
            InitializeComponent();
            DMachine = new D20_System();
            DMachine.InitializeRandom();

            Initialize_DiceButtons();
        }

        private void Initialize_DiceButtons()
        {
            DiceButtons = new List<Button>();

            Add_DiceButtons();
            Add_DiceSound_OnMouseButtonDown();           
        }


        // Adding element by element here because code for that is shorter than extracting buttons via looping through UIElements in this case
        private void Add_DiceButtons()
        {
            DiceButtons.Add(D4_Button);
            DiceButtons.Add(D6_Button);
            DiceButtons.Add(D8_Button);
            DiceButtons.Add(D10_Button);
            DiceButtons.Add(D12_Button);
            DiceButtons.Add(D20_Button);
            DiceButtons.Add(D100_Button);
        }

        private void Add_DiceSound_OnMouseButtonDown()
        {
            foreach(Button DieButton in DiceButtons)
            {
                DieButton.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(DiceSound_OnDieRoll);
            }
        }

        private void DiceSound_OnDieRoll(object sender, MouseButtonEventArgs e)
        {
            FileManager.FM_Inst.Play_DiceSound();
        }

        private void DiceMachine_Roll(int rollResult)
        {
            if(AddResult_CB.IsChecked == true)
            {
                diceMachine_result += rollResult;
            }

            else
            {
                diceMachine_result = rollResult;
            }

            DM_ResultBox.Text = diceMachine_result.ToString();
        }

        public void Reset_Button_Click(object sender, RoutedEventArgs e)
        {
            diceMachine_result = 0;
            DM_ResultBox.Clear();
        }


        public void D4_Bt_Click(object sender, RoutedEventArgs e)
        {
            DiceMachine_Roll(DMachine.Roll_D4());            
        }

        public void D6_Bt_Click(object sender, RoutedEventArgs e)
        {
            DiceMachine_Roll(DMachine.Roll_D6());
        }

        public void D8_Bt_Click(object sender, RoutedEventArgs e)
        {
            DiceMachine_Roll(DMachine.Roll_D8());
        }

        public void D10_Bt_Click(object sender, RoutedEventArgs e)
        {
            DiceMachine_Roll(DMachine.Roll_D10());
        }

        public void D12_Bt_Click(object sender, RoutedEventArgs e)
        {
            DiceMachine_Roll(DMachine.Roll_D12());
        }

        public void D20_Bt_Click(object sender, RoutedEventArgs e)
        {
            DiceMachine_Roll(DMachine.Roll_D20());
        }

        public void D100_Bt_Click(object sender, RoutedEventArgs e)
        {
            DiceMachine_Roll(DMachine.Roll_D100());
        }
    }
}
