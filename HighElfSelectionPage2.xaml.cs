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
using System.Windows.Shapes;

namespace DnD_CharSheet_5e
{
    /// <summary>
    /// Interaktionslogik für HighElfSelectionPage2.xaml
    /// </summary>
    public partial class HighElfSelectionPage2 : Page
    {
        List<DnDLanguage> Languages;
        DnDLanguage ExtraLanguage;

        public delegate void OnLanguageConfirmed();
        public OnLanguageConfirmed onLanguageConfirmed;

        public HighElfSelectionPage2()
        {
            InitializeComponent();

            ExtraLanguage = new DnDLanguage();
            Init_Languages();
            Generate_LanguagesUI();
        }

        private void Init_Languages()
        {
            Languages = SheetManager.CS_Manager_Inst.Languages;
        }

        private void Generate_LanguagesUI()
        {
            foreach (DnDLanguage language in Languages)
            {
                Generate_LanguageButton(language.Language);
                Generate_SpeakersBlock(language.TypicalSpeakers);
                Generate_ScriptTBl(language.Script);
            }
        }

        private void Generate_LanguageButton(string languageName)
        {
            Button LanguageButton = new Button();

            LanguageButton.Content = languageName;

            LanguageButton.Height = 25;
            LanguageButton.Width = 125;

            Thickness ButtonMargin = new Thickness();

            ButtonMargin.Top = 5;
            ButtonMargin.Bottom = 5;

            LanguageButton.Margin = ButtonMargin;

            LanguageButton.FontSize = 14;
            LanguageButton.FontWeight = FontWeights.Bold;

            LanguageButton.Background = Brushes.Azure;

            LanguageButton.Click += new RoutedEventHandler(LanguageButton_Click);

            LanguagesPanel.Children.Add(LanguageButton);
        }

        private void LanguageButton_Click(object sender, RoutedEventArgs e)
        {
            Button ClickedLanguageButton = (Button)sender;
            ExtraLanguage = Find_Language(ClickedLanguageButton.Content.ToString());
            SelectedLanguageTxt.Text = ExtraLanguage.Language;
        }

        private DnDLanguage Find_Language(string languageName)
        {
            DnDLanguage LanguageToFind = Languages.Find(languageElement => languageElement.Language == languageName);
            return LanguageToFind;
        }

        private void Generate_SpeakersBlock(string speakersTxt)
        {
            TextBlock SpeakerTBl = new TextBlock();

            SpeakerTBl.Text = speakersTxt;

            SpeakerTBl.Height = 25;
            SpeakerTBl.Width = 175;

            Thickness BlockMargin = new Thickness();

            BlockMargin.Top = 5;
            BlockMargin.Bottom = 5;

            SpeakerTBl.Margin = BlockMargin;

            Thickness BlockPadding = new Thickness();

            BlockPadding.Left = 25;

            SpeakerTBl.Padding = BlockPadding;

            SpeakerTBl.FontSize = 14;
            SpeakerTBl.FontWeight = FontWeights.Bold;

            SpeakerTBl.Background = Brushes.Azure;

            TypicalSpeakersPanel.Children.Add(SpeakerTBl);
        }

        private void Generate_ScriptTBl(string scriptTxt)
        {
            TextBlock ScriptTBl = new TextBlock();

            ScriptTBl.Text = scriptTxt;

            ScriptTBl.Height = 25;
            ScriptTBl.Width = 175;

            Thickness BlockMargin = new Thickness();

            BlockMargin.Top = 5;
            BlockMargin.Bottom = 5;

            ScriptTBl.Margin = BlockMargin;

            Thickness BlockPadding = new Thickness();

            BlockPadding.Left = 25;

            ScriptTBl.Padding = BlockPadding;

            ScriptTBl.FontSize = 14;
            ScriptTBl.FontWeight = FontWeights.Bold;

            ScriptTBl.Background = Brushes.Azure;

            ScriptPanel.Children.Add(ScriptTBl);
        }

        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            if(ExtraLanguage.Language != null)
            {

                if(!SheetManager.CS_Manager_Inst.CharGenCharacter.CharLanguages.Contains(ExtraLanguage.Language))
                {
                    SheetManager.CS_Manager_Inst.CharGenCharacter.CharLanguages.Add(ExtraLanguage.Language);

                    if (onLanguageConfirmed != null)
                    {
                        onLanguageConfirmed.Invoke();
                    }
                }

                else
                {
                    MessageBox.Show("You already know this language via your chosen race-background. Please select another one.");
                }
            }
        }
    }
}
