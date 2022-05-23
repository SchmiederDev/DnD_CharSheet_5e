using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;


namespace DnD_CharSheet_5e
{
    /// <summary>
    /// Interaktionslogik für BackgroundWindow.xaml
    /// </summary>
    public partial class BackgroundWindow : Window
    {
        const string ErrorMessage = "You have entered one or several incorrect values - either for 'Age', 'Height' or 'Weight'.\nPlease enter an integer number for 'Age' and integer or decimals for 'Height' and 'Weight'.";

        int TempAge;
        float TempHeight;
        float TempWeight;

        List<TextBox> BackgroundBoxes;

        //$"You have entered one or several incorrect values - either for 'Age', 'Height' or 'Weight'." +
                   //$"Please enter an integer number for 'Age' and integer or decimals for 'Height' and 'Weight'"
        
        public BackgroundWindow()
        {
            InitializeComponent();
            Init_UI();
        }

        private void Init_UI()
        {
            Init_BoxElements();

            CharacterName_Box.Text = SheetManager.CS_Manager_Inst.character.CharacterName;            
            Load_Background();
        }

        private void Init_BoxElements()
        {
            BackgroundBoxes = new List<TextBox>();

            Init_DesciptionBoxes();
            Init_BackgroundBoxes();
        }

        private void Init_DesciptionBoxes()
        {
            foreach(Grid DescriptionGrid in DescriptionPanel.Children)
            {
                foreach(UIElement element in DescriptionGrid.Children)
                {
                    if(element is TextBox)
                    {
                        BackgroundBoxes.Add(element as TextBox);
                    }
                }
            }
        }

        private void Init_BackgroundBoxes()
        {
            BackgroundBoxes.Add(Appearance_Box);
            BackgroundBoxes.Add(Backstory_Box);
            BackgroundBoxes.Add(Allies_n_Orgas_Box);
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)                         // happens before Closing
        {
            if(Check_Description_Values())
            {
                Set_Description_Properties();
                Set_Background();
            }
            
            else
            {
                MessageBox.Show(ErrorMessage);

                e.Cancel = true;
            }
                       
        }

        private void Edit_btn_Click(object sender, RoutedEventArgs e)
        {
            Activate_BackgroundElements();
        }

        private void OK_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Check_Description_Values())
            {
                Set_Description_Properties();
                Set_Background();
                Deactivate_BackgroundElements();
            }

            else
            {
                MessageBox.Show(ErrorMessage);
            }
        }

        private void Activate_BackgroundElements()
        {
            foreach(TextBox BackgroundBox in BackgroundBoxes)
            {
                BackgroundBox.IsEnabled = true;
            }

            OK_Btn.Visibility = Visibility.Visible;
            OK_Btn.IsEnabled = true;
        }

        private void Deactivate_BackgroundElements()
        {
            foreach (TextBox BackgroundBox in BackgroundBoxes)
            {
                BackgroundBox.IsEnabled = false;
            }

            OK_Btn.Visibility = Visibility.Hidden;
            OK_Btn.IsEnabled = false;
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

        private void Set_Background()
        {
            SheetManager.CS_Manager_Inst.character.CharApperance = Appearance_Box.Text;
            SheetManager.CS_Manager_Inst.character.AlliesAndOrgas = Allies_n_Orgas_Box.Text;
            SheetManager.CS_Manager_Inst.character.BackgroundStory = Backstory_Box.Text;
        }
        
    }
}
