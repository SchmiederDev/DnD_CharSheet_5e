using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DnD_CharSheet_5e
{
    /// <summary>
    /// Interaktionslogik für InventoryWindow.xaml
    /// </summary>
    /// 

    public partial class InventoryWindow : Window
    {
        
        public static InventoryWindow inventoryWindow_Inst;

        public InventoryWindow()
        {
            InitializeComponent();

            if(inventoryWindow_Inst == null)
            {
                inventoryWindow_Inst = this;
            }

            Init_UI();
            Load_IconsForEquipedItems();
        }       
       
        public void Init_UI()
        {
            CharName_Box.Text = SheetManager.CS_Manager_Inst.character.CharacterName;
            PlayerName_Box.Text = SheetManager.CS_Manager_Inst.character.PlayerName;

            Update_Riches();
            Clear_InventoryPanels();
            Generate_ItemBtns();
        }
        public void Load_IconsForEquipedItems()
        {
            if (SheetManager.CS_Manager_Inst.character.CharEquipment.RightHand_Weapon != null)
            {
                RightHand_Img.Source = ImageHandler.ImgHandlerInst.Get_SourceUri(SheetManager.CS_Manager_Inst.character.CharEquipment.RightHand_Weapon.ItemName);
            }

            if (SheetManager.CS_Manager_Inst.character.CharEquipment.LeftHand_Armor != null)
            {
                LeftHand_Img.Source = ImageHandler.ImgHandlerInst.Get_SourceUri(SheetManager.CS_Manager_Inst.character.CharEquipment.LeftHand_Armor.ItemName);
            }

            if (SheetManager.CS_Manager_Inst.character.CharEquipment.LeftHand_Weapon != null)
            {
                LeftHand_Img.Source = ImageHandler.ImgHandlerInst.Get_SourceUri(SheetManager.CS_Manager_Inst.character.CharEquipment.LeftHand_Weapon.ItemName);
            }

            if (SheetManager.CS_Manager_Inst.character.CharEquipment.CharacterArmor != null)
            {
                Armor_Img.Source = ImageHandler.ImgHandlerInst.Get_SourceUri(SheetManager.CS_Manager_Inst.character.CharEquipment.CharacterArmor.ItemName);
            }
        }

        public void Refresh_UI()
        {
            Update_Riches();
            Clear_InventoryPanels();
            Generate_ItemBtns();
        }

        public void Update_Riches()
        {
            Platinum_Box.Text = SheetManager.CS_Manager_Inst.character.cInventory.Platinum.ToString();
            Gold_Box.Text = SheetManager.CS_Manager_Inst.character.cInventory.Gold.ToString();
            Silver_Box.Text = SheetManager.CS_Manager_Inst.character.cInventory.Silver.ToString();
            Copper_Box.Text = SheetManager.CS_Manager_Inst.character.cInventory.Copper.ToString();
        }

        public void Clear_InventoryPanels()
        {
            ItemsPanel.Children.Clear();
            WeaponsPanel.Children.Clear();
            ArmorPanel.Children.Clear();
            
        }


        public void Generate_ItemBtns()
        {
            foreach (Item item in SheetManager.CS_Manager_Inst.character.cInventory.cItems)
            {                
                Create_Item_Button(item);
            }            

            foreach (Weapon weapon in SheetManager.CS_Manager_Inst.character.cInventory.cWeapons)
            {
                Create_Weapon_Button(weapon);
            }

            foreach (Armor armor in SheetManager.CS_Manager_Inst.character.cInventory.cArmor)
            {
                Create_Armor_Button(armor);
            }
        }

        public void Create_Item_Button(Item item)
        {
            Button itemButton = new Button();
            itemButton.Height = 20;
            itemButton.Width = 250;

            Thickness BtnMargin = new Thickness();
            BtnMargin.Top = 10;
            BtnMargin.Bottom = 5;

            itemButton.Margin = BtnMargin;

            itemButton.FontWeight = FontWeights.Bold;

            itemButton.Foreground = Brushes.DarkSlateGray; 

            itemButton.Background = Brushes.WhiteSmoke;

            itemButton.Name = item.Item_ID;

            itemButton.Content = item.ItemName + " | Price: " + item.Coin.Price + " " + item.Coin.CoinKey + " | Weight: " + item.ItemWeight;

            // Later on a functionality will be implemented here for the consumption of 'consumable' items on 'Item-Button-Click'
            //itemButton.Click += new RoutedEventHandler(Item_Button_Click);

            itemButton.MouseEnter += new MouseEventHandler(Item_Hover_Over);
            itemButton.MouseRightButtonDown += new MouseButtonEventHandler(Item_Button_RightClick);
            
            ItemsPanel.Children.Add(itemButton);
        }

        public void Create_Weapon_Button(Weapon weapon)
        {
            Button weaponButton = new Button();
            weaponButton.Height = 20;
            weaponButton.Width = 250;

            Thickness BtnMargin = new Thickness();
            BtnMargin.Top = 10;
            BtnMargin.Bottom = 5;

            weaponButton.Margin = BtnMargin;

            weaponButton.FontWeight = FontWeights.Bold;

            weaponButton.Foreground = Brushes.DarkSlateGray;

            weaponButton.Background = Brushes.WhiteSmoke;

            weaponButton.Name = weapon.Item_ID;

            weaponButton.Content = weapon.ItemName + " | Price: " + weapon.Coin.Price + " " + weapon.Coin.CoinKey + " | Weight: " + weapon.ItemWeight;

            weaponButton.Click += new RoutedEventHandler(Weapon_Button_LeftClick);
            weaponButton.MouseEnter += new MouseEventHandler(Weapon_Hover_Over);
            weaponButton.MouseRightButtonDown += new MouseButtonEventHandler(Weapon_Button_RightClick);

            WeaponsPanel.Children.Add(weaponButton);
        }

        public void Create_Armor_Button(Armor armor)
        {
            Button armorButton = new Button();
            armorButton.Height = 20;
            armorButton.Width = 250;

            Thickness BtnMargin = new Thickness();
            BtnMargin.Top = 10;
            BtnMargin.Bottom = 5;

            armorButton.Margin = BtnMargin;

            armorButton.FontWeight = FontWeights.Bold;

            armorButton.Foreground = Brushes.DarkSlateGray;

            armorButton.Background = Brushes.WhiteSmoke;

            armorButton.Name = armor.Item_ID;

            armorButton.Content = armor.ItemName + " | Price: " + armor.Coin.Price + " " + armor.Coin.CoinKey + " | Weight: " + armor.ItemWeight;

            armorButton.MouseEnter += new MouseEventHandler(Armor_Hover_Over);
            armorButton.Click += new RoutedEventHandler(Armor_Button_LeftClick);
            armorButton.MouseRightButtonDown += new MouseButtonEventHandler(Armor_Button_RightClick);

            ArmorPanel.Children.Add(armorButton);
        }       

        private void Item_Hover_Over(object sender, MouseEventArgs e)
        {
            Button itemButton = (Button)e.Source;
            Item tempItem = SheetManager.CS_Manager_Inst.character.cInventory.Find_Item_byID(itemButton.Name);
            ToolTip tt = new ToolTip();
            tt.Content = tempItem.ItemInfo;
            itemButton.ToolTip = tt;
        }

        private void Item_Button_RightClick(object sender, MouseButtonEventArgs e)
        {
            const string message = "Are you sure you want to drop this item?";
            const string caption = "Drop Item";

            Button itemButton = (Button)e.Source;

            Item tempItem = SheetManager.CS_Manager_Inst.character.cInventory.Find_Item_byID(itemButton.Name);

            var result = MessageBox.Show(message, caption, MessageBoxButton.YesNo);
            
            if(result == MessageBoxResult.Yes)
            {
                if (tempItem != null)
                {
                    SheetManager.CS_Manager_Inst.character.cInventory.Remove_Item(tempItem);
                }

                ItemsPanel.Children.Remove(itemButton);
            }
            
        }

        private void Weapon_Button_LeftClick(object sender, RoutedEventArgs e)
        {
            Button WeaponButton = (Button)e.Source;

            Weapon TempWeapon = SheetManager.CS_Manager_Inst.character.cInventory.Find_Weapon_byID(WeaponButton.Name);

            const string message = "Equip this Weapon?";
            const string caption = "Equip Weapon";

            var equipResult = MessageBox.Show(message, caption, MessageBoxButton.YesNo);

            if(TempWeapon != null && equipResult == MessageBoxResult.Yes)
            {
                if(!TempWeapon.IsTwoHanded)
                {                    
                    SheetManager.CS_Manager_Inst.character.CharEquipment.RightHand_Weapon = TempWeapon;
                    RightHand_Img.Source = ImageHandler.ImgHandlerInst.Get_SourceUri(TempWeapon.ItemName);

                    if(SheetManager.CS_Manager_Inst.character.CharEquipment.LeftHand_Weapon != null)
                    {
                        SheetManager.CS_Manager_Inst.character.CharEquipment.LeftHand_Weapon = null;
                        LeftHand_Img.Source = null;
                    }
                }

                else
                {
                    if(SheetManager.CS_Manager_Inst.character.CharEquipment.LeftHand_Armor == null)
                    {
                        SheetManager.CS_Manager_Inst.character.CharEquipment.RightHand_Weapon = TempWeapon;
                        SheetManager.CS_Manager_Inst.character.CharEquipment.LeftHand_Weapon = TempWeapon;
                        RightHand_Img.Source = ImageHandler.ImgHandlerInst.Get_SourceUri(TempWeapon.ItemName);
                        LeftHand_Img.Source = ImageHandler.ImgHandlerInst.Get_SourceUri(TempWeapon.ItemName);
                    }

                    else
                    {
                        const string shieldCaption = "Shield Warning";
                        const string shieldWarning = "You have already equiped a shield and you need both hands to wield this weapon. Unequip Shield?";

                        var shieldResult = MessageBox.Show(shieldWarning, shieldCaption, MessageBoxButton.YesNo);

                        if(shieldResult == MessageBoxResult.Yes)
                        {
                            SheetManager.CS_Manager_Inst.character.CharEquipment.LeftHand_Armor = null;
                            SheetManager.CS_Manager_Inst.character.CharEquipment.LeftHand_Weapon = TempWeapon;
                            SheetManager.CS_Manager_Inst.character.CharEquipment.RightHand_Weapon = TempWeapon;
                            SheetManager.CS_Manager_Inst.character.Calculate_AC();
                            RightHand_Img.Source = ImageHandler.ImgHandlerInst.Get_SourceUri(TempWeapon.ItemName);
                            LeftHand_Img.Source = ImageHandler.ImgHandlerInst.Get_SourceUri(TempWeapon.ItemName);
                        }
                    }
                }

            }
            
        }

        private void Weapon_Hover_Over(object sender, MouseEventArgs e)
        {
            Button weaponButton = (Button)e.Source;
            Weapon tempWeapon = SheetManager.CS_Manager_Inst.character.cInventory.Find_Weapon_byID(weaponButton.Name);
            ToolTip tt = new ToolTip();
            tt.Content = tempWeapon.ItemInfo;
            weaponButton.ToolTip = tt;
        }

        private void Weapon_Button_RightClick(object sender, MouseButtonEventArgs e)
        {
            const string message = "Are you sure you want to drop this item?";
            const string caption = "Drop Item";

            Button weaponButton = (Button)e.Source;

            Weapon tempWeapon = SheetManager.CS_Manager_Inst.character.cInventory.Find_Weapon_byID(weaponButton.Name);

            var result = MessageBox.Show(message, caption, MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                if (tempWeapon != null)
                {
                    SheetManager.CS_Manager_Inst.character.cInventory.Remove_Weapon(tempWeapon);
                }

                WeaponsPanel.Children.Remove(weaponButton);
            }

        }

        private void Armor_Button_LeftClick(object sender, RoutedEventArgs e)
        {
            Button armorButton = (Button)e.Source;

            Armor tempArmor = SheetManager.CS_Manager_Inst.character.cInventory.Find_Armor_byID(armorButton.Name);

            const string caption = "Don Armor";
            const string message = "Equip this Armor?";

            var result = MessageBox.Show(message, caption, MessageBoxButton.YesNo);

            if (tempArmor != null && result == MessageBoxResult.Yes)
            {
                if(tempArmor.ItemName == "Shield")
                {
                    if(SheetManager.CS_Manager_Inst.character.CharEquipment.LeftHand_Weapon == null)
                    {
                        SheetManager.CS_Manager_Inst.character.CharEquipment.LeftHand_Armor = tempArmor;
                        SheetManager.CS_Manager_Inst.character.Calculate_AC();
                        LeftHand_Img.Source = ImageHandler.ImgHandlerInst.Get_SourceUri(tempArmor.ItemName);                        
                    }

                    else
                    {
                        const string weaponCaption = "Unequip two-handed weapon?";
                        const string weaponMessage = "You are already wielding a weapon that needs both hands. Do you want to unequip that weapon and wear the shield instead?";
                        var weaponResult = MessageBox.Show(weaponMessage, weaponCaption, MessageBoxButton.YesNo);

                        if(weaponResult == MessageBoxResult.Yes)
                        {
                            SheetManager.CS_Manager_Inst.character.CharEquipment.LeftHand_Weapon = null;
                            SheetManager.CS_Manager_Inst.character.CharEquipment.RightHand_Weapon = null;
                            SheetManager.CS_Manager_Inst.character.CharEquipment.LeftHand_Armor = tempArmor;
                            SheetManager.CS_Manager_Inst.character.Calculate_AC();
                            LeftHand_Img.Source = ImageHandler.ImgHandlerInst.Get_SourceUri(tempArmor.ItemName);
                            RightHand_Img.Source = null;
                        }
                    }
                    
                }

                else if(tempArmor.ItemName != "Shield")
                {
                    if (SheetManager.CS_Manager_Inst.character.Check_STR_Requirement(tempArmor))
                    {
                        SheetManager.CS_Manager_Inst.character.CharEquipment.CharacterArmor = tempArmor;
                        SheetManager.CS_Manager_Inst.character.Calculate_AC();
                        Armor_Img.Source = ImageHandler.ImgHandlerInst.Get_SourceUri(tempArmor.ItemName);
                    }

                    else
                    {
                        MessageBox.Show($"Your character hasn't got the necessary strength-requirement to don this armor.");                       
                    }
                }
            }
        }

        private void Armor_Hover_Over(object sender, MouseEventArgs e)
        {
            Button armorButton = (Button)e.Source;
            Armor tempArmor = SheetManager.CS_Manager_Inst.character.cInventory.Find_Armor_byID(armorButton.Name);
            ToolTip tt = new ToolTip();
            tt.Content = tempArmor.ItemInfo;
            armorButton.ToolTip = tt;
        }

        private void Armor_Button_RightClick(object sender, MouseButtonEventArgs e)
        {
            const string message = "Are you sure you want to drop this item?";
            const string caption = "Drop Item";

            Button armorButton = (Button)e.Source;

            Armor tempArmor = SheetManager.CS_Manager_Inst.character.cInventory.Find_Armor_byID(armorButton.Name);

            var result = MessageBox.Show(message, caption, MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                if (tempArmor != null)
                {
                    SheetManager.CS_Manager_Inst.character.cInventory.Remove_Armor(tempArmor);                    
                    
                    if(tempArmor.ItemName == "Shield")
                    {
                        SheetManager.CS_Manager_Inst.character.CharEquipment.LeftHand_Armor = null;
                    }

                    else
                    {
                        SheetManager.CS_Manager_Inst.character.CharEquipment.CharacterArmor = null;
                    }
                    
                    SheetManager.CS_Manager_Inst.character.Calculate_AC();
                }

                ArmorPanel.Children.Remove(armorButton);
            }

        }

        private void Edit_Money_Btn_Click(object sender, RoutedEventArgs e)
        {
            Apply_Money_Btn.IsEnabled = true;
            Apply_Money_Btn.Visibility = Visibility.Visible;

            Edit_Money_Btn.IsEnabled = false;
            Edit_Money_Btn.Visibility = Visibility.Hidden;

            Platinum_Box.IsEnabled = true;
            Gold_Box.IsEnabled = true;
            Silver_Box.IsEnabled = true;
            Copper_Box.IsEnabled = true;
        }

        private void Apply_Money_Bt_Click(object sender, RoutedEventArgs e)
        {
            FileManager.FM_Inst.Play_ClickSound();
            Set_Riches();
            Reset_Edit();
        }

        private void Set_Riches()
        {
            SheetManager.CS_Manager_Inst.character.cInventory.Set_Platinum_byTxt(Platinum_Box.Text);
            SheetManager.CS_Manager_Inst.character.cInventory.Set_Gold_byTxt(Gold_Box.Text);
            SheetManager.CS_Manager_Inst.character.cInventory.Set_Silver_byTxt(Silver_Box.Text);
            SheetManager.CS_Manager_Inst.character.cInventory.Set_Copper_byTxt(Copper_Box.Text);
        }

        private void Reset_Edit()
        {
            Apply_Money_Btn.IsEnabled = false;
            Apply_Money_Btn.Visibility = Visibility.Hidden;

            Edit_Money_Btn.IsEnabled = true;
            Edit_Money_Btn.Visibility = Visibility.Visible;

            Platinum_Box.IsEnabled = false;
            Gold_Box.IsEnabled = false;
            Silver_Box.IsEnabled = false;
            Copper_Box.IsEnabled = false;
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void Button_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
