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

    /* PURPOSE OF MERCHANT WINDOW 
     * 
     * The Merchant Window serves the purpose of 'selling' items to the Character of the Player/ user:
     * These will be added to the Inventory of the Character-class instance after a 'price' was 'paid' that is recorded in the Item Data Bases (JSON Files).
     * 
     * So, MerchantWindow is the Player's access point to all kinds of items which appear in the game of D&D.
     * Whereas there will always be a limited amount of items in the Characters inventory (see 'Character' and 'Inventory'-Classes)
     * 'MerchantWindow' and its helper class 'Merchant' give access to the full range of game items exstracted from the respective item data bases.
     * 
     * Prices and other information like Weight etc. about Items are drawn from the 'Player's Handbook' of the D&D Core Source Books. 
    */

    public partial class MerchantWindow : Window
    {
        // Helper class for MerchantWindow -> see 'Merchant'-class

        Merchant merchant = new Merchant();

        BuyOrAddWindow buyOrAddWdw;

        public MerchantWindow()
        {            
            InitializeComponent();
            buyOrAddWdw = new BuyOrAddWindow();
            buyOrAddWdw.onMoneyChanged += Update_PlayerFortune;
            Load_Databases();
            Init_UI();
        }


        #region UI INITILIZATION METHODS

        private void Init_UI()
        {
            Update_PlayerFortune();
            Create_Merchant_Inventory();
        }

        private void Load_Databases()
        {
            try
            {
                merchant.Load_ItemDataBase(FileManager.FM_Inst.Read_DataBase(FileManager.FM_Inst.IDB_Path));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            try
            {
                merchant.Load_WeaponDataBase(FileManager.FM_Inst.Read_DataBase(FileManager.FM_Inst.WDB_Path));
            }
            catch(Exception ex01)
            {
                MessageBox.Show(ex01.Message.ToString());
            }

            try
            {
                merchant.Load_ArmorDataBase(FileManager.FM_Inst.Read_DataBase(FileManager.FM_Inst.ADB_Path));
            }
            catch(Exception ex02)
            {
                MessageBox.Show(ex02.Message.ToString());
            }            
        }        

        private void Create_Merchant_Inventory()
        {
            foreach (Item item in merchant.merchItems)
            {
                Create_Item_Buttons(item);
            }

            foreach (Weapon weapon in merchant.merchWeapons)
            {
                Create_Weapon_Buttons(weapon);
            }

            foreach (Armor armor in merchant.merchArmor)
            {
                Create_Armor_Buttons(armor);
            }
        }

        #endregion

        #region BUTTON GENERATION METHODS (+ Assignment of Event Handler)

        private Button Create_Item_Button(Item item)
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

            return itemButton;
        }

        public void Create_Item_Buttons(Item item)
        {
            Button itemButton = Create_Item_Button(item);

            itemButton.Click += new RoutedEventHandler(Item_Button_Click);
            itemButton.MouseEnter += new MouseEventHandler(Item_Hover_Over);

            ItemsPanel.Children.Add(itemButton);
        }

        public void Create_Weapon_Buttons(Weapon weapon)
        {
            Button weaponButton = Create_Item_Button(weapon);

            weaponButton.Click += new RoutedEventHandler(Weapon_Button_Click);
            weaponButton.MouseEnter += new MouseEventHandler(Weapon_Hover_Over);

            WeaponsPanel.Children.Add(weaponButton);
        }

        public void Create_Armor_Buttons(Armor armor)
        {
            Button armorButton = Create_Item_Button(armor);

            armorButton.Click += new RoutedEventHandler(Armor_Button_Click);
            armorButton.MouseEnter += new MouseEventHandler(Armor_Hover_Over);

            ArmorPanel.Children.Add(armorButton);
        }

        #endregion

        #region BUTTON EVENT HANDLER

        #region CLICK EVENTS

        private void Item_Button_Click(object sender, RoutedEventArgs e)
        {
            Button itemButton = (Button)e.Source;
            Item tempItem = merchant.Find_Item_byID(itemButton.Name);
            
            if(tempItem != null)
            {
                buyOrAddWdw.SendItem(tempItem);
                buyOrAddWdw.Show();
                
            }
            
        }

        private void Weapon_Button_Click(object sender, RoutedEventArgs e)
        {
            Button weaponButton = (Button)e.Source;
            Weapon tempWeapon = merchant.Find_Weapon_byID(weaponButton.Name);

            if(tempWeapon != null)
            {
                buyOrAddWdw.SendItem(tempWeapon);
                buyOrAddWdw.Show();
            }
        }

        private void Armor_Button_Click(object sender, RoutedEventArgs e)
        {
            Button armorButton = (Button)e.Source;
            Armor tempArmor = merchant.Find_Armor_byID(armorButton.Name);

            if(tempArmor != null)
            {
                buyOrAddWdw.SendItem(tempArmor);
                buyOrAddWdw.Show();
            }
        }

        #endregion

        #region HOVER OVER EVENTS

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

        #endregion

        #endregion

        #region UPDATE PLAYER FORTUNE METHOD 

        private void Update_PlayerFortune()
        {
            pPlatinum_Box.Text = SheetManager.CS_Manager_Inst.character.cInventory.Platinum.ToString();
            pGold_Box.Text = SheetManager.CS_Manager_Inst.character.cInventory.Gold.ToString();
            pSilver_Box.Text = SheetManager.CS_Manager_Inst.character.cInventory.Silver.ToString();
            pCopper_Box.Text = SheetManager.CS_Manager_Inst.character.cInventory.Copper.ToString();
        }

        #endregion

    }
}
