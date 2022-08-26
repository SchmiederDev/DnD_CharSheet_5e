using System.Windows;


namespace DnD_CharSheet_5e
{
    /// <summary>
    /// Interaktionslogik für JustAddWdw.xaml
    /// </summary>
    public partial class BuyOrAddWindow : Window
    {
        Item ItemToAdd;

        public delegate void OnMoneyChanged();
        public OnMoneyChanged onMoneyChanged;

        public BuyOrAddWindow()
        {
            InitializeComponent();
        }

        public void SendItem(Item sentItem)
        {
            ItemToAdd = sentItem;
        }

        private void AddItem()
        {
           if(ItemToAdd is Weapon)
                SheetManager.CS_Manager_Inst.character.cInventory.Add_Weapon(ItemToAdd as Weapon);
           else if(ItemToAdd is Armor)
                SheetManager.CS_Manager_Inst.character.cInventory.Add_Armor(ItemToAdd as Armor);
           else
                SheetManager.CS_Manager_Inst.character.cInventory.Add_Item(ItemToAdd);
        }

        private void RefreshAndCheckOut()
        {
            MainWindow.mainWindow_Inst.InventoryWdw.Refresh_UI();
            Hide();
        }

        private void BuyItemBtn_Click(object sender, RoutedEventArgs e)
        {
            if (SheetManager.CS_Manager_Inst.character.cInventory.Pay_Item(ItemToAdd.Coin) == true)
            {
                AddItem();

                onMoneyChanged.Invoke();

                RefreshAndCheckOut();
            }

            else
            {
                MessageBox.Show("Sorry, you haven't got enough money to buy the item. -\nClick on the 'Just Add'-Button if you just found the item or cancel out.");
            }    
        }

        private void JustAddBtn_Click(object sender, RoutedEventArgs e)
        {
            AddItem();
            RefreshAndCheckOut();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
