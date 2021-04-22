using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DnD_CharSheet_5e
{
    /// <summary>
    /// Interaktionslogik für SpellsWindow.xaml
    /// </summary>
    public partial class SpellsWindow : Window
    {
        public string[] Spellcaster_Classes { get; } = { "Bard", "Cleric", "Druid", "Paladin", "Ranger", "Rogue", "Sorcerer", "Warlock", "Wizard" };
        public SpellsWindow()
        {
            InitializeComponent();
        }
    }
}
