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
        int TempAge;
        float TempHeight;
        float TempWeight;
        
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
            Age_Box.Text = SheetManager.CS_Manager_Inst.character.Age.ToString();
            Height_Box.Text = SheetManager.CS_Manager_Inst.character.Height.ToString();
            Weight_Box.Text = SheetManager.CS_Manager_Inst.character.Weight.ToString();            

            Eyes_Box.Text = SheetManager.CS_Manager_Inst.character.Eyes;
            Skin_Box.Text = SheetManager.CS_Manager_Inst.character.Skin;
            Hair_Box.Text = SheetManager.CS_Manager_Inst.character.Hair;

            Appearance_Box.Text = SheetManager.CS_Manager_Inst.character.CharApperance;
            Backstory_Box.Text = SheetManager.CS_Manager_Inst.character.BackgroundStory;
            Allies_n_Orgas_Box.Text = SheetManager.CS_Manager_Inst.character.AlliesAndOrgas;           
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

        private void Edit_btn_Click(object sender, RoutedEventArgs e)
        {
            Activate_DescriptionElements();
        }

        private void Activate_DescriptionElements()
        {
            Age_Box.IsEnabled = true;
            Height_Box.IsEnabled = true;
            Weight_Box.IsEnabled = true;

            Eyes_Box.IsEnabled = true;
            Skin_Box.IsEnabled = true;
            Hair_Box.IsEnabled = true;

            OK_btn.Visibility = Visibility.Visible;
            OK_btn.IsEnabled = true;
        }

        private void OK_btn_Click(object sender, RoutedEventArgs e)
        {
            if(Check_Description_Values())
            {
                Set_Description_Properties();
                Deactivate_DescriptionElements();
            }

            else
            {
                MessageBox.Show($"You have entered one or several incorrect values - either for 'Age', 'Height' or 'Weight'." +
                    $"Please enter an integer number for 'Age' and integer or decimals for 'Height' and 'Weight'");
            }
        }

        private bool Check_Description_Values()
        {            
            int truthCounter = 0;

            if(int.TryParse(Age_Box.Text.Replace('.', ','), out int tempAge))
            {
                TempAge = tempAge;
                truthCounter += 1;
            }
            

            if(float.TryParse(Height_Box.Text.Replace('.', ','), out float tempHeight))
            {
                TempHeight = tempHeight;
                truthCounter += 1;
            }                        

            if (float.TryParse(Weight_Box.Text.Replace('.', ','), out float tempWeight))
            {
                TempWeight = tempWeight;
                truthCounter += 1;
            }            

            if(truthCounter == 3)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        private void Set_Description_Properties()
        {
            SheetManager.CS_Manager_Inst.character.Age = TempAge;
            SheetManager.CS_Manager_Inst.character.Height = TempHeight;
            SheetManager.CS_Manager_Inst.character.Weight = TempWeight;

            SheetManager.CS_Manager_Inst.character.Eyes = Eyes_Box.Text;
            SheetManager.CS_Manager_Inst.character.Skin = Skin_Box.Text;
            SheetManager.CS_Manager_Inst.character.Hair = Hair_Box.Text;
        }

        private void Deactivate_DescriptionElements()
        {
            Age_Box.IsEnabled = false;
            Height_Box.IsEnabled = false;
            Weight_Box.IsEnabled = false;

            Eyes_Box.IsEnabled = false;
            Skin_Box.IsEnabled = false;
            Hair_Box.IsEnabled = false;

            OK_btn.Visibility = Visibility.Hidden;
            OK_btn.IsEnabled = false;
        }
    }
}
