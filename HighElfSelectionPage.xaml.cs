using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DnD_CharSheet_5e
{
    /// <summary>
    /// Interaktionslogik für HighElfSelectionPage.xaml
    /// </summary>
    public partial class HighElfSelectionPage : Page
    {
        List<Spell> WizardCantrips;
        Spell SelectedSpell;

        public delegate void OnSpellSelectionConfirmed();
        public OnSpellSelectionConfirmed onSpellSelectionConfirmed;

        public HighElfSelectionPage()
        {
            InitializeComponent();

            SelectedSpell = new Spell();
            Init_WizardCantrips();
            Generate_CantripButtons();         
        }

        private void Init_WizardCantrips()
        {
            WizardCantrips = new List<Spell>();

            foreach(SpellList_Item spellListItem in SheetManager.CS_Manager_Inst.theWeave.WizardSpellList)
            {
                Spell WizardCantrip = SheetManager.CS_Manager_Inst.theWeave.SpellDataBase.Find(spellELement => spellELement.SpellName == spellListItem.SpellName && spellELement.SpellLevel == 0);
                WizardCantrips.Add(WizardCantrip);
            }
        }

        private void Generate_CantripButtons()
        {
            foreach(Spell wizardCantrip in WizardCantrips)
            {
                Generate_Button(wizardCantrip.SpellName);
            }
        }

        private void Generate_Button(string cantripName)
        {
            Button CantripBtn = new Button();

            CantripBtn.Content = cantripName;
            

            CantripBtn.Height = 25;
            CantripBtn.Width = 150;

            Thickness BtnMargin = new Thickness();

            BtnMargin.Top = 5;
            BtnMargin.Bottom = 5;

            CantripBtn.Margin = BtnMargin;

            CantripBtn.FontWeight = FontWeights.Bold;
            CantripBtn.FontSize = 14;

            CantripBtn.Click += new RoutedEventHandler(CantripBtn_Click);

            WizardCantripsPanel.Children.Add(CantripBtn);

        }

        private void CantripBtn_Click(object sender, RoutedEventArgs e)
        {
            Button tempBtn = (Button)sender;
            ShowSpellProperties(Find_Spell(tempBtn.Content.ToString()));
            SelectedSpell = Find_Spell(tempBtn.Content.ToString());
            SelectedSpellTB.Text = tempBtn.Content.ToString();
        }

        private Spell Find_Spell(string spellName)
        {
            Spell SpellToFind = WizardCantrips.Find(spellElement => spellElement.SpellName == spellName);

            return SpellToFind;
        }

        private void ShowSpellProperties(Spell SpellToShow)
        {
            SpellNameTB.Text = SpellToShow.SpellName;
            SchoolTB.Text = SpellToShow.School;
            CastingTimeTB.Text = SpellToShow.CastingTime;
            RangeTB.Text = SpellToShow.Range;
            ComponentsTB.Text = SpellToShow.Components;
            DurationTB.Text = SpellToShow.Duration;
            EffectTB.Text = SpellToShow.Description;
        }

        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            if(onSpellSelectionConfirmed != null && SelectedSpell.SpellName != null)
            {
                if(onSpellSelectionConfirmed != null)
                {
                    onSpellSelectionConfirmed.Invoke();
                }

            }

            else
            {
                MessageBox.Show("You have no spell selected. Please choose a spell and then click 'confirm and continue' again");
            }

        }
    }

}
