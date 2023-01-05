using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DnD_CharSheet_5e
{
    /// <summary>
    /// Interaktionslogik für GuideWdw.xaml
    /// </summary>
    public partial class GuideWdw : Window
    {

        const string disclaimerTxtTxt = "TestText: Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.";

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
            DisclaimerTxt.Text = disclaimerTxtTxt;
        }

        private void InitCharGenTxt()
        {
            CharGenTxt.Text = FileManager.FM_Inst.CharGenTxt;
        }
    }
}
