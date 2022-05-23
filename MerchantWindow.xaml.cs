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

        public MerchantWindow()
        {            
            InitializeComponent();
            
            Load_Databases();
            Init_UI();
        }

        public void Load_Databases()
        {

            //FileManager.FM_Inst.Set_IDBPath();
            try
            {
                merchant.Load_ItemDataBase(FileManager.FM_Inst.Read_DataBase(FileManager.FM_Inst.IDB_Path));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            //FileManager.FM_Inst.Set_WDBPath();
            try
            {
                merchant.Load_WeaponDataBase(FileManager.FM_Inst.Read_DataBase(FileManager.FM_Inst.WDB_Path));
            }
            catch(Exception ex01)
            {
                MessageBox.Show(ex01.Message.ToString());
            }

            //FileManager.FM_Inst.Set_ADBPath();
            try
            {
                merchant.Load_ArmorDataBase(FileManager.FM_Inst.Read_DataBase(FileManager.FM_Inst.ADB_Path));
            }
            catch(Exception ex02)
            {
                MessageBox.Show(ex02.Message.ToString());
            }
            
        }               
        
        public void Create_Item_Buttons(Item item)
        {
            Button itemButton = new Button();
            itemButton.Height = 20;
            itemButton.Width = 300;

            Thickness BtnMargin = new Thickness();
            BtnMargin.Top = 10;
            BtnMargin.Bottom = 5;

            itemButton.Margin = BtnMargin;

            itemButton.FontWeight = FontWeights.Bold;

            itemButton.Foreground = Brushes.DarkSlateGray;

            itemButton.Background = Brushes.WhiteSmoke;

            itemButton.Name = item.Item_ID;
            
            itemButton.Content = item.ItemName + " | Price: " + item.Coin.Price + " " + item.Coin.CoinKey + " | Weight: " + item.ItemWeight;

            itemButton.Click += new RoutedEventHandler(Item_Button_Click);
            itemButton.MouseEnter += new MouseEventHandler(Item_Hover_Over);

            ItemsPanel.Children.Add(itemButton);
        }

        public void Create_Weapon_Buttons(Weapon weapon)
        {
            Button weaponButton = new Button();
            weaponButton.Height = 20;
            weaponButton.Width = 300;

            Thickness BtnMargin = new Thickness();
            BtnMargin.Top = 10;
            BtnMargin.Bottom = 5;

            weaponButton.Margin = BtnMargin;

            weaponButton.FontWeight = FontWeights.Bold;

            weaponButton.Foreground = Brushes.DarkSlateGray;

            weaponButton.Background = Brushes.WhiteSmoke;

            weaponButton.Name = weapon.Item_ID;

            weaponButton.Content = weapon.ItemName + " | Price: " + weapon.Coin.Price + " " + weapon.Coin.CoinKey + " | Weight: " + weapon.ItemWeight;

            weaponButton.Click += new RoutedEventHandler(Weapon_Button_Click);
            weaponButton.MouseEnter += new MouseEventHandler(Weapon_Hover_Over);

            WeaponsPanel.Children.Add(weaponButton);
        }

        public void Create_Armor_Buttons(Armor armor)
        {
            Button armorButton = new Button();
            armorButton.Height = 20;
            armorButton.Width = 300;

            Thickness BtnMargin = new Thickness();
            BtnMargin.Top = 10;
            BtnMargin.Bottom = 5;

            armorButton.Margin = BtnMargin;

            armorButton.FontWeight = FontWeights.Bold;

            armorButton.Foreground = Brushes.DarkSlateGray;

            armorButton.Background = Brushes.WhiteSmoke;

            armorButton.Name = armor.Item_ID;

            armorButton.Content = armor.ItemName + " | Price: " + armor.Coin.Price + " " + armor.Coin.CoinKey + " | Weight: " + armor.ItemWeight;

            armorButton.Click += new RoutedEventHandler(Armor_Button_Click);
            armorButton.MouseEnter += new MouseEventHandler(Armor_Hover_Over);

            ArmorPanel.Children.Add(armorButton);
        }

        public void Init_UI()
        {
            Update_PlayerFortune();

            foreach (Item item in merchant.merchItems)
            {
                Create_Item_Buttons(item);
            }
            
            foreach(Weapon weapon in merchant.merchWeapons)
            {
                Create_Weapon_Buttons(weapon);
            }

            foreach(Armor armor in merchant.merchArmor)
            {
                Create_Armor_Buttons(armor);
            }
        }

        private void Update_PlayerFortune()
        {
            pPlatinum_Box.Text = SheetManager.CS_Manager_Inst.character.cInventory.Platinum.ToString();
            pGold_Box.Text = SheetManager.CS_Manager_Inst.character.cInventory.Gold.ToString();
            pSilver_Box.Text = SheetManager.CS_Manager_Inst.character.cInventory.Silver.ToString();
            pCopper_Box.Text = SheetManager.CS_Manager_Inst.character.cInventory.Copper.ToString();
        }

        private void Item_Button_Click(object sender, RoutedEventArgs e)
        {
            Button itemButton = (Button)e.Source;
            Item tempItem = merchant.Find_Item_byID(itemButton.Name);
            
            if(tempItem != null)
            {
                if(SheetManager.CS_Manager_Inst.character.cInventory.Pay_Item(tempItem.Coin) == true)
                {
                    SheetManager.CS_Manager_Inst.character.cInventory.Add_Item(tempItem);
                    InventoryWindow.inventoryWindow_Inst.Refresh_UI();
                }
            }

            Update_PlayerFortune();
        }

        private void Weapon_Button_Click(object sender, RoutedEventArgs e)
        {
            Button weaponButton = (Button)e.Source;
            Weapon tempWeapon = merchant.Find_Weapon_byID(weaponButton.Name);

            if(tempWeapon != null)
            {
               if(SheetManager.CS_Manager_Inst.character.cInventory.Pay_Item(tempWeapon.Coin))
                {
                    SheetManager.CS_Manager_Inst.character.cInventory.Add_Weapon(tempWeapon);
                    InventoryWindow.inventoryWindow_Inst.Refresh_UI();
                }
            }

            Update_PlayerFortune();
        }

        private void Armor_Button_Click(object sender, RoutedEventArgs e)
        {
            Button armorButton = (Button)e.Source;
            Armor tempArmor = merchant.Find_Armor_byID(armorButton.Name);

            if(tempArmor != null)
            {
                if(SheetManager.CS_Manager_Inst.character.cInventory.Pay_Item(tempArmor.Coin))
                {
                    SheetManager.CS_Manager_Inst.character.cInventory.Add_Armor(tempArmor);
                    InventoryWindow.inventoryWindow_Inst.Refresh_UI();
                }
            }

            Update_PlayerFortune();
        }

        private void Item_Hover_Over(object sender, MouseEventArgs e)
        {
            Button itemButton = (Button)e.Source;
            Item tempItem = merchant.Find_Item_byID(itemButton.Name);
            ToolTip tt = new ToolTip();
            tt.Content = tempItem.ItemInfo;
            itemButton.ToolTip = tt;
        }

        private void Weapon_Hover_Over(object sender, MouseEventArgs e)
        {
            Button weaponButton = (Button)e.Source;
            Weapon tempWeapon = merchant.Find_Weapon_byID(weaponButton.Name);
            ToolTip tt = new ToolTip();
            tt.Content = tempWeapon.ItemInfo;
            weaponButton.ToolTip = tt;
        }

        private void Armor_Hover_Over(object sender, MouseEventArgs e)
        {
            Button armorButton = (Button)e.Source;
            Armor tempArmor = merchant.Find_Armor_byID(armorButton.Name);
            ToolTip tt = new ToolTip();
            tt.Content = tempArmor.ItemInfo;
            armorButton.ToolTip = tt;
        }
       
    }
}
