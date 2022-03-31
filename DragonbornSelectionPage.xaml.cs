using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;


namespace DnD_CharSheet_5e
{
    /// <summary>
    /// Interaktionslogik für RaceFeatOptionsPage.xaml
    /// </summary>
    public partial class DragonbornSelectionPage : Page
    {
        List<Dragonborn> Dragonborns;
        Dragonborn SelectedDragonborn;
        Race DragonbornRace;

        List<Button> DragonButtons;

        List<TextBox> DamageTBs;
        List<TextBox> ReachTBs;
        List<TextBox> SaveTBs;

        public delegate void OnSelectionConfirmed();
        public OnSelectionConfirmed onSelectionConfirmed;

        public DragonbornSelectionPage()
        {
            InitializeComponent();
            AllocateMemory();
            Init_DragonBtns();
            FillIn_DragonbornData();
        }

        private void AllocateMemory()
        {
            DragonbornRace = new Race();
            Dragonborns = new List<Dragonborn>();
            SelectedDragonborn = new Dragonborn();

            DragonButtons = new List<Button>();

            DamageTBs = new List<TextBox>();
            ReachTBs = new List<TextBox>();
            SaveTBs = new List<TextBox>();
        }

        private void Init_DragonBtns()
        {
            foreach(Button button in DragonBtns.Children)
            {
                DragonButtons.Add(button);
                button.Click += new RoutedEventHandler(DragonButtonClick);
                AddTextboxes(button);
            }
        }

        private void AddTextboxes(Button button)
        {
            TextBox DamageTB = AddTextBox(button.Content.ToString(), DamageTypePanel.Name);
            DamageTBs.Add(DamageTB);
            DamageTypePanel.Children.Add(DamageTB);

            TextBox BreathWeaponTB = AddTextBox(button.Content.ToString(), BreathWeaponPanel.Name);
            ReachTBs.Add(BreathWeaponTB);
            BreathWeaponPanel.Children.Add(BreathWeaponTB);

            TextBox SaveTB = AddTextBox(button.Content.ToString(), SavingThrowPanel.Name);
            SaveTBs.Add(SaveTB);
            SavingThrowPanel.Children.Add(SaveTB);
        }

        private TextBox AddTextBox(string sourceName, string panelName)
        {
            TextBox textBox = new TextBox();

            string tempName = sourceName.Replace(" ", "");
            tempName = String.Concat(tempName, "TB", panelName);
            
            textBox.Name = tempName;

            Thickness tbMargin = new Thickness();
            tbMargin.Top = 15;
            tbMargin.Bottom = 15;

            textBox.Margin = tbMargin;

            textBox.Height = 20;
            textBox.Width = 175;

            textBox.IsEnabled = false;

            return textBox;
        }

        private Dragonborn Find_DragonRace(string name)
        {
            Dragonborn dragonborn = SheetManager.CS_Manager_Inst.Dragonborns.Find(dragonName => dragonName.DragonType == name);
            return dragonborn;
        }

        private void FillIn_DragonbornData()
        {
            for(int i = 0; i < SheetManager.CS_Manager_Inst.Dragonborns.Count; i++)
            {
                DamageTBs[i].Text = SheetManager.CS_Manager_Inst.Dragonborns[i].breathWeapon.DamageType;
                ReachTBs[i].Text = SheetManager.CS_Manager_Inst.Dragonborns[i].breathWeapon.Reach;
                SaveTBs[i].Text = SheetManager.CS_Manager_Inst.Dragonborns[i].BreathWeaponSaveKey + " Saving Throw";
            }
        }

        private void DragonButtonClick(object sender, RoutedEventArgs e)
        {
            Button tempButton = (Button)e.Source;
            SelectedDragonborn = Find_DragonRace(tempButton.Content.ToString());            
        }

        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedDragonborn.DragonType != null)
            {
                SheetManager.CS_Manager_Inst.CharGenCharacter.CharRace = SelectedDragonborn;
                Dragonborn testDragon = (Dragonborn)SheetManager.CS_Manager_Inst.CharGenCharacter.CharRace;
                onSelectionConfirmed.Invoke();
            }

            else
            {
                MessageBox.Show("You have no dragon selected. Please choose one to continue.");
            }
        }
    }
}
