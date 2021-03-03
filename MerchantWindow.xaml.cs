using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;


namespace DnD_CharSheet_5e
{
    /// <summary>
    /// Interaktionslogik für MerchantWindow.xaml
    /// </summary>
    public partial class MerchantWindow : Window
    {
        Merchant merchant = new Merchant();

        List<Button> IDB_Buttons = new List<Button>();

        public MerchantWindow()
        {
            InitializeComponent();
            Init_Databases();
            Init_UI();
        }

        public void Init_Databases()
        {
            merchant.fManager.Set_IDBPath();
            try
            {
                merchant.Load_ItemDataBase(merchant.fManager.Read_ItemDataBase());
            }
            catch (Exception ex)
            {
                ContentBox.Text = ex.Message.ToString();
            }
            
            ContentBox.Text = "Here is the name of the 2nd Item: " + merchant.merchItems[1].Coin.Price.ToString();
        }
        
        public void Create_Item_Buttons(Item item)
        {
            Button itemButton = new Button();
            itemButton.Height = 30;
            itemButton.Width = 250;

            Thickness thick = itemButton.Margin;
            thick.Bottom = 20;
            itemButton.Margin = thick;

            itemButton.Name = item.Item_ID;
            
            itemButton.Content = item.ItemName + " | Price: " + item.Coin.Price + " " + item.Coin.CoinKey + " | Weight: " + item.ItemWeight;

            itemButton.Click += new RoutedEventHandler(Item_Button_Click);

            IDB_Buttons.Add(itemButton);

            ItemsPanel.Children.Add(itemButton);
        }

        public void Init_UI()
        {
            foreach (Item item in merchant.merchItems)
            {
                Create_Item_Buttons(item);
            }
        }

        private void Item_Button_Click(object sender, RoutedEventArgs e)
        {
            Button itemButton = (Button)e.Source;
            Item tempItem = merchant.Find_Item_byID(itemButton.Name);
            MessageBox.Show(tempItem.ItemInfo);                       
        }

    }
}
