using System.Windows;

namespace DnD_CharSheet_5e
{
    /// <summary>
    /// Interaktionslogik für GuideWdw.xaml
    /// </summary>
    public partial class GuideWdw : Window
    {

        const string disclaimerTxt = "The scope of the content from the original pen-and-paper game of D&D 5e  (D&D-rules, mechanics and background lore) this app works with extends to the information about the game published under the Open License Agreement.\nIt is meant to enhance the gaming experience of players and make their lives easier, but: for the full extent of game knowledge users will still have to own copies of the original core rule books (or additional source books) which contain also content which is not publicly accessible and falls under copyright owned by the official publisher Wizards of the Coast.";

        public GuideWdw()
        {
            InitializeComponent();
            InitUI();
        }

        private void InitUI()
        {
            InitDisclaimer();
            InitCharGenTxt();
        }

        private void InitDisclaimer()
        {
            DisclaimerTxt.Text = disclaimerTxt;
        }

        private void InitCharGenTxt()
        {
            CharGenTxt.Text = FileManager.FM_Inst.CharGenTxt;
        }
    }
}
