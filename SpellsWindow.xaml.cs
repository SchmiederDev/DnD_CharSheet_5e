using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DnD_CharSheet_5e
{
    /// <summary>
    /// Interaktionslogik für SpellsWindow.xaml
    /// </summary>
    public partial class SpellsWindow : Window
    {
        public string[] Spellcaster_Classes { get; } = { "Bard", "Cleric", "Druid", "Paladin", "Ranger", "Rogue", "Sorcerer", "Warlock", "Wizard" };
        public List<SpellList_Item> Cantrips;

        int spellFieldHeight = 20;

        public SpellsWindow()
        {
            InitializeComponent();
            InitSpells();
        }

        private void InitSpells()
        {
            Cantrips = new List<SpellList_Item>();
            InitCantrips();
        }

        private void InitCantrips()
        {
            foreach(SpellList_Item sl_item in SheetManager.CS_Manager_Inst.theWeave.BardSpellList)
            {
                if(sl_item.SpellLevel == 0)
                {
                    Cantrips.Add(sl_item);
                }
            }

            //foreach(SpellList_Item sl_item in Cantrips)
            //{
            //    ComboBox cantripBox = new ComboBox();

            //    cantripBox.Height = 20;
            //    cantripBox.Width = 175;

            //    Thickness thickM = cantripBox.Margin;
            //    thickM.Bottom = 5;
            //    cantripBox.Margin = thickM;               

            //    Thickness thickP = cantripBox.Padding;
            //    thickP.Top = 1;
            //    thickP.Bottom = 1;
            //    thickP.Left = 10;
            //    cantripBox.Padding = thickP;

            //    cantripBox.FontWeight = FontWeights.Bold;               

            //    cantripBox.MouseEnter += new MouseEventHandler(MouseHoverOver_Spell);

            //    CantripsPanel.Children.Add(cantripBox);
            //}

            //foreach(ComboBox cantripbox in CantripsPanel.Children)
            //{
            //    foreach(SpellList_Item sl_item in Cantrips)
            //    {
            //        cantripbox.Items.Add(sl_item.SpellName);
            //    }
            //}
        }

        private void MouseHoverOver_Spell(object sender, MouseEventArgs e)
        {
            ComboBox spellBox = (ComboBox)e.Source;

            if(spellBox.SelectedItem != null)
            {
                Spell tempSpell = SheetManager.CS_Manager_Inst.theWeave.Find_Spell_in_Database_byName(spellBox.SelectedItem.ToString());
                Fill_SpellViewer(tempSpell);
            }
        }

        private void Fill_SpellViewer(Spell spell)
        {
            SpellViewer.Text = "Name of Spell: " + spell.SpellName + "\nSchool of Magic: " + spell.School + "\nCasting Time: " + spell.CastingTime
                + "\nRange: " + spell.Range + "\nComponents: " + spell.Components + "\nDuration: " + spell.Duration + "\n\n" + spell.Description;            
        }

        private void Add_Cantrip_Btn_Click(object sender, RoutedEventArgs e)
        {
            ComboBox cantripBox = new ComboBox();

            cantripBox.Height = spellFieldHeight;
            cantripBox.Width = 175;

            Thickness thickM = cantripBox.Margin;
            thickM.Bottom = 5;
            cantripBox.Margin = thickM;

            Thickness thickP = cantripBox.Padding;
            thickP.Top = 1;
            thickP.Bottom = 1;
            thickP.Left = 10;
            cantripBox.Padding = thickP;

            cantripBox.FontWeight = FontWeights.Bold;

            cantripBox.Background = Brushes.Azure;

            cantripBox.MouseEnter += new MouseEventHandler(MouseHoverOver_Spell);

            CantripsPanel.Children.Add(cantripBox);

            foreach (SpellList_Item sl_item in Cantrips)
            {
                cantripBox.Items.Add(sl_item.SpellName);
            }

            Disable_AddCantrip_Btn();
            Enable_CantripsOK_Btn();

            Update_Cantrip_ScrollViewer();

        }

        public void Update_Cantrip_ScrollViewer()
        {
            if(CantripScroller.ExtentHeight > CantripsPanel.MaxHeight - spellFieldHeight)
            {
                CantripScroller.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                CantripScroller.CanContentScroll = true;
            }
        }

        private void Cantrip_OK_Btn_Click(object sender, RoutedEventArgs e)
        {
            Enable_AddCantrip_Btn();
            Disable_CantripsOK_Btn();
        }

        private void Enable_AddCantrip_Btn()
        {
            Add_Cantrip_Btn.IsEnabled = true;
            Add_Cantrip_Btn.Visibility = Visibility.Visible;
        }

        private void Disable_AddCantrip_Btn()
        {
            Add_Cantrip_Btn.IsEnabled = false;
            Add_Cantrip_Btn.Visibility = Visibility.Hidden;
        }

        private void Enable_CantripsOK_Btn()
        {
            Cantrip_OK_Btn.Visibility = Visibility.Visible;
            Cantrip_OK_Btn.IsEnabled = true;
        }

        private void Disable_CantripsOK_Btn()
        {
            Cantrip_OK_Btn.Visibility = Visibility.Hidden;
            Cantrip_OK_Btn.IsEnabled = false;
        }
    }
}
