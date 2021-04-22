using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DnD_CharSheet_5e
{
    /// <summary>
    /// Interaktionslogik für InventoryWindow.xaml
    /// </summary>
    public partial class InventoryWindow : Window
    {
        List<Button> ItemButtons;
        List<Button> WeaponButtons;
        Inventory characterInventory;

        public InventoryWindow()
        {
            InitializeComponent();

            ItemButtons = new List<Button>();
            WeaponButtons = new List<Button>();
            characterInventory = new Inventory();

            if(SheetManager.CS_Manager_Inst.character.cInventory != null)
            {
                characterInventory = SheetManager.CS_Manager_Inst.character.cInventory;
            }

            Init_UI();
        }

        public void Init_UI()
        {
            CharName_Box.Text = SheetManager.CS_Manager_Inst.character.Get_charName();
            PlayerName_Box.Text = SheetManager.CS_Manager_Inst.character.Get_playerName();

            foreach(Item item in characterInventory.cItems)
            {
                Create_Item_Buttons(item);
            }

            foreach(Weapon weapon in characterInventory.cWeapons)
            {
                Create_Weapon_Buttons(weapon);
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

            itemButton.MouseEnter += new MouseEventHandler(Item_Hover_Over);

            ItemButtons.Add(itemButton);

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

            weaponButton.MouseEnter += new MouseEventHandler(Weapon_Hover_Over);

            WeaponButtons.Add(weaponButton);

            WeaponsPanel.Children.Add(weaponButton);
        }

        private void Item_Hover_Over(object sender, MouseEventArgs e)
        {
            Button itemButton = (Button)e.Source;
            Item tempItem = characterInventory.Find_Item_byID(itemButton.Name);
            ToolTip tt = new ToolTip();
            tt.Content = tempItem.ItemInfo;
            itemButton.ToolTip = tt;
        }

        private void Weapon_Hover_Over(object sender, MouseEventArgs e)
        {
            Button weaponButton = (Button)e.Source;
            Item tempWeapon = characterInventory.Find_Weapon_byID(weaponButton.Name);
            ToolTip tt = new ToolTip();
            tt.Content = tempWeapon.ItemInfo;
            weaponButton.ToolTip = tt;
        }
    }
}
