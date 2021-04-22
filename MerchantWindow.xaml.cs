using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;
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
        List<Button> WDB_Buttons = new List<Button>();

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

            merchant.fManager.Set_WDBPath();
            try
            {
                merchant.Load_WeaponDataBase(merchant.fManager.Read_WeaponDataBase());
            }
            catch(Exception ex01)
            {
                ContentBox.Text = ex01.Message.ToString();
            }
            
        }
        
        public void Create_Item_Buttons(Item item)
        {
            Button itemButton = new Button();
            itemButton.Height = 20;
            itemButton.Width = 300;

            Thickness thickB = itemButton.Margin;
            thickB.Bottom = 5;
            itemButton.Margin = thickB;

            Thickness thickU = itemButton.Margin;
            thickU.Top = 10;
            itemButton.Margin = thickU;

            itemButton.FontWeight = FontWeights.Bold;

            itemButton.Foreground = Brushes.SlateGray;

            itemButton.Background = Brushes.WhiteSmoke;

            itemButton.Name = item.Item_ID;
            
            itemButton.Content = item.ItemName + " | Price: " + item.Coin.Price + " " + item.Coin.CoinKey + " | Weight: " + item.ItemWeight;

            itemButton.Click += new RoutedEventHandler(Item_Button_Click);
            itemButton.MouseEnter += new MouseEventHandler(Item_Hover_Over);

            IDB_Buttons.Add(itemButton);

            ItemsPanel.Children.Add(itemButton);
        }

        public void Create_Weapon_Buttons(Weapon weapon)
        {
            Button weaponButton = new Button();
            weaponButton.Height = 20;
            weaponButton.Width = 300;

            Thickness thickB = weaponButton.Margin;
            thickB.Bottom = 5;
            weaponButton.Margin = thickB;

            Thickness thickU = weaponButton.Margin;
            thickU.Top = 10;
            weaponButton.Margin = thickU;

            weaponButton.FontWeight = FontWeights.Bold;

            weaponButton.Foreground = Brushes.SlateGray;

            weaponButton.Background = Brushes.WhiteSmoke;

            weaponButton.Name = weapon.Item_ID;

            weaponButton.Content = weapon.ItemName + " | Price: " + weapon.Coin.Price + " " + weapon.Coin.CoinKey + " | Weight: " + weapon.ItemWeight;

            weaponButton.Click += new RoutedEventHandler(Weapon_Button_Click);
            weaponButton.MouseEnter += new MouseEventHandler(Weapon_Hover_Over);

            WDB_Buttons.Add(weaponButton);

            WeaponsPanel.Children.Add(weaponButton);
        }

        public void Init_UI()
        {
            foreach (Item item in merchant.merchItems)
            {
                Create_Item_Buttons(item);
            }
            
            foreach(Weapon weapon in merchant.merchWeapons)
            {
                Create_Weapon_Buttons(weapon);
            }
        }

        private void Item_Button_Click(object sender, RoutedEventArgs e)
        {
            Button itemButton = (Button)e.Source;
            Item tempItem = merchant.Find_Item_byID(itemButton.Name);
            
            if(tempItem != null)
            {
                SheetManager.CS_Manager_Inst.character.cInventory.Add_Item(tempItem);
            }
        }

        private void Weapon_Button_Click(object sender, RoutedEventArgs e)
        {
            Button weaponButton = (Button)e.Source;
            Weapon tempWeapon = merchant.Find_Weapon_byID(weaponButton.Name);

            if(tempWeapon != null)
            {
                SheetManager.CS_Manager_Inst.character.cInventory.Add_Weapon(tempWeapon);
            }
        }

        private void Item_Hover_Over(object sender, MouseEventArgs e)
        {
            Button itemButton = (Button)e.Source;
            Item tempItem = merchant.Find_Item_byID(itemButton.Name);
            ToolTip tt = new ToolTip();
            tt.Content = tempItem.ItemInfo;
            itemButton.ToolTip = tt;
        }

        private void Weapon_Hover_Over(object sender, RoutedEventArgs e)
        {
            Button weaponButton = (Button)e.Source;
            Weapon tempWeapon = merchant.Find_Weapon_byID(weaponButton.Name);
            ToolTip tt = new ToolTip();
            tt.Content = tempWeapon.ItemInfo;
            weaponButton.ToolTip = tt;
        }
       
    }
}
