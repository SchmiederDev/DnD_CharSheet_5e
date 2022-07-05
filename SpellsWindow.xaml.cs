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

    /* THE SPELL-WINDOW-CLASS:
     * 
     * This Window serves the purpose of managing the 'Spells' of a player's character, as the the name suggests.
     * It is equivalent to the 3rd page of the officiel D&D paper Character-Sheets.
     * 
     * This Window-class works with items from the 'SpellDataBase' and the 'SpellList'-data bases or instances of the 'Spell'- and 'SpellList_Item'-classes.
     * 
     * Certain 'Character-Classes' (in the game of D&D, not in C#) are able to 'cast' Spells. 
     * But, a specific 'Character-Class' like 'Wizard' or 'Druid' may only choose from a limited number Spells 
     * from all the hundreds of Spells that exist in the game - represented here by the 'SpellDataBase' and the 'Spell'-class data container.
     * 
     * Therefore each of these 'Character-Classes' has a list of Spells to choose from 
     * - represented here by the 'SpellList'-data bases related to a specific type of 'Character-Class' (e. g. 'Wizard')
     * and the 'SpellList_Item'-class data container.
     * 
     * In an instance of the SpellsWindow a user/ player may choose a number of 'Spells' for the 'Character' they are currently playing - depending on the 'Character-Class' of this 'Character -
     * and add them to their own Spell List via interacting with UI. 
     * 
     * Information (= data) about those Spells is also visualized on UI therefore sparing the player the need of browsing the paper version of the Player's Handbook.
     * 
     * Finally, the player/user may 'cast' the available or chosen Spells which means technically, selecting 'Spells' among the UI-Elements and clicking on them.
     * The triggering event will effectively reduce the number of 'Spell Slots' available to the 'Character' of the player/ user - until refilled (= reset to the initial value).
     * 
     * Dependend on the 'Level' of a given 'Character' the player/ user has a number of so called 'Spell Slots' for each 'Spell Level' available.
     * These 'Slots' represent, most basically spoken, the number of times a 'Character' may 'cast' 'Spells' of a given 'Spell Level'.
     */

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

            Thickness thickM = new Thickness();
            thickM.Bottom = 5;
            cantripBox.Margin = thickM;

            Thickness thickP = new Thickness();
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
