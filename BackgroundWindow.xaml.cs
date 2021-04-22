using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace DnD_CharSheet_5e
{
    /// <summary>
    /// Interaktionslogik für BackgroundWindow.xaml
    /// </summary>
    public partial class BackgroundWindow : Window
    {
        
        public BackgroundWindow()
        {
            InitializeComponent();
            Init_UI();
        }

        private void Init_UI()
        {
            CharacterName_Box.Text = SheetManager.CS_Manager_Inst.character.Get_charName();            
            Load_Background();
        }

        private void Load_Background()
        {
            if(SheetManager.CS_Manager_Inst.character.CharApperance != null)
            {
                Appearance_Box.Text = SheetManager.CS_Manager_Inst.character.CharApperance;
            }

            if (SheetManager.CS_Manager_Inst.character.BackgroundStory != null)
            {
                Backstory_Box.Text = SheetManager.CS_Manager_Inst.character.BackgroundStory;
            }

            if (SheetManager.CS_Manager_Inst.character.AlliesAndOrgas != null)
            {
                Allies_n_Orgas_Box.Text = SheetManager.CS_Manager_Inst.character.AlliesAndOrgas;
            }
        }

        private void Window_Closed(object sender, EventArgs e)                                                      // happens after Closing
        {
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)                         // happens before Closing
        {
            SheetManager.CS_Manager_Inst.character.CharApperance = Appearance_Box.Text;                             // <- Insert functionality that checks for text Limit
            SheetManager.CS_Manager_Inst.character.BackgroundStory = Backstory_Box.Text;
            SheetManager.CS_Manager_Inst.character.AlliesAndOrgas = Allies_n_Orgas_Box.Text;           
        }
    }
}
