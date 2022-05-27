using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;


namespace DnD_CharSheet_5e
{
    /// <summary>
    /// Interaktionslogik für BackgroundWindow.xaml
    /// </summary>

    // Besides the technical details (values and actions) of their characters D&D-Players may add notes on the appearance and background story of their characters 
    // to enrichen the game with roleplaying details.
    // Such notes - paralleled by the respective member properties of the 'Character'-class - are handled in this Window (= 2nd page of the physical paper Character-Sheet for D&D).

    public partial class BackgroundWindow : Window
    {
        #region CONSTRUCTOR 
        public BackgroundWindow()
        {
            InitializeComponent();
            Init_UI();
        }
        #endregion

        #region MEMBER PROPERTIES

        const string ErrorMessage = "You have entered one or several incorrect values - either for 'Age', 'Height' or 'Weight'.\nPlease enter an integer number for 'Age' and integer or decimals for 'Height' and 'Weight'.";

        int TempAge;
        float TempHeight;
        float TempWeight;

        List<TextBox> BackgroundBoxes;

        #endregion

        #region MEMBER METHODS

        #region UI-INITIALIZATION
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

        #endregion

        #region LOAD BACKGROUND FROM CHARACTER (FILL TEXTBOXES) ON WINDOW INITIALIZATION
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
        #endregion


        #region ACTIONS ON WINDOW CLOSING - EVENT HANDLER

        // Users may edit the background of their character on 'Edit_Btn_Click' (see below)
        // If they missed to click the 'OK'-Button this event handler checks whether they really don't won't to save their changes. 
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
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
        #endregion


        #region METHOD FOR CHECKING USER INPUT FOR BEING OF TYPE FLOAT
        private bool Check_Description_Values()
        {
            int validValueCounter = 0;

            if (int.TryParse(Age_Box.Text.Replace('.', ','), out int tempAge))
            {
                TempAge = tempAge;
                validValueCounter += 1;
            }


            if (float.TryParse(Height_Box.Text.Replace('.', ','), out float tempHeight))
            {
                TempHeight = tempHeight;
                validValueCounter += 1;
            }

            if (float.TryParse(Weight_Box.Text.Replace('.', ','), out float tempWeight))
            {
                TempWeight = tempWeight;
                validValueCounter += 1;
            }

            if (validValueCounter == 3)
            {
                return true;
            }

            else
            {
                return false;
            }
        }
        #endregion

        #region SETTER FOR BACKGROUND PROPERTIES OF CHARACTER CLASS
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
        #endregion

        #region CLICK EVENT HANDLERS
        private void Edit_Btn_Click(object sender, RoutedEventArgs e)
        {
            Activate_BackgroundElements();
        }

        private void OK_Btn_Click(object sender, RoutedEventArgs e)
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
        #endregion

        #region METHODS FOR ENABLING/ DISABLING TEXTBOXES
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
        #endregion

        #endregion

    }
}
