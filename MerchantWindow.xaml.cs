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
        public static MerchantWindow merchantWindow_Inst;

        Merchant merchant = new Merchant();

        List<Button> IDB_Buttons = new List<Button>();
        List<Button> WDB_Buttons = new List<Button>();
        List<Button> ADB_Buttons = new List<Button>();

        public MerchantWindow()
        {            
            InitializeComponent();

            if (merchantWindow_Inst == null)
            {
                merchantWindow_Inst = this;
            }

            Init_Databases();
            Init_UI();
        }

        public void Init_Databases()
        {

            FileManager.FM_Inst.Set_IDBPath();
            try
            {
                merchant.Load_ItemDataBase(FileManager.FM_Inst.Read_ItemDataBase());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            FileManager.FM_Inst.Set_WDBPath();
            try
            {
                merchant.Load_WeaponDataBase(FileManager.FM_Inst.Read_WeaponDataBase());
            }
            catch(Exception ex01)
            {
                MessageBox.Show(ex01.Message.ToString());
            }

            FileManager.FM_Inst.Set_ADBPath();
            try
            {
                merchant.Load_ArmorDataBase(FileManager.FM_Inst.Read_ArmorDataBase());
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

        public void Create_Armor_Buttons(Armor armor)
        {
            Button armorButton = new Button();
            armorButton.Height = 20;
            armorButton.Width = 300;

            Thickness thickB = armorButton.Margin;
            thickB.Bottom = 5;
            armorButton.Margin = thickB;

            Thickness thickU = armorButton.Margin;
            thickU.Top = 10;
            armorButton.Margin = thickU;

            armorButton.FontWeight = FontWeights.Bold;

            armorButton.Foreground = Brushes.SlateGray;

            armorButton.Background = Brushes.WhiteSmoke;

            armorButton.Name = armor.Item_ID;

            armorButton.Content = armor.ItemName + " | Price: " + armor.Coin.Price + " " + armor.Coin.CoinKey + " | Weight: " + armor.ItemWeight;

            armorButton.Click += new RoutedEventHandler(Armor_Button_Click);
            armorButton.MouseEnter += new MouseEventHandler(Armor_Hover_Over);

            ADB_Buttons.Add(armorButton);

            ArmorPanel.Children.Add(armorButton);
        }

        public void Init_UI()
        {
            pGold_Box.Text = SheetManager.CS_Manager_Inst.character.cInventory.Get_Gold().ToString();
            pSilver_Box.Text = SheetManager.CS_Manager_Inst.character.cInventory.Get_Silver().ToString();
            pCopper_Box.Text = SheetManager.CS_Manager_Inst.character.cInventory.Get_Copper().ToString();
            pPlatinum_Box.Text = SheetManager.CS_Manager_Inst.character.cInventory.Get_Platinum().ToString();

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
            pGold_Box.Text = SheetManager.CS_Manager_Inst.character.cInventory.Get_Gold().ToString();
            pSilver_Box.Text = SheetManager.CS_Manager_Inst.character.cInventory.Get_Silver().ToString();
            pCopper_Box.Text = SheetManager.CS_Manager_Inst.character.cInventory.Get_Copper().ToString();
            pPlatinum_Box.Text = SheetManager.CS_Manager_Inst.character.cInventory.Get_Platinum().ToString();
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

        private void Weapon_Hover_Over(object sender, RoutedEventArgs e)
        {
            Button weaponButton = (Button)e.Source;
            Weapon tempWeapon = merchant.Find_Weapon_byID(weaponButton.Name);
            ToolTip tt = new ToolTip();
            tt.Content = tempWeapon.ItemInfo;
            weaponButton.ToolTip = tt;
        }

        private void Armor_Hover_Over(object sender, RoutedEventArgs e)
        {
            Button armorButton = (Button)e.Source;
            Armor tempArmor = merchant.Find_Armor_byID(armorButton.Name);
            ToolTip tt = new ToolTip();
            tt.Content = tempArmor.ItemInfo;
            armorButton.ToolTip = tt;
        }
       
    }
}
