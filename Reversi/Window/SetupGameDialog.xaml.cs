using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
namespace Reversi
{
    /// <summary>
    /// The main popup dialog used for inputing player names, player types, if they're human or not(bool).
    /// It closes the dialog safely afterwards.
    /// </summary>
    public partial class SetupGameDialog : Window
    {
        public string NameOne { get; private set; }
        public string NameTwo { get; private set; }

        public bool Player1IsHuman { get; private set; }
        public bool Player2IsHuman { get; private set; }
        
        /// <summary>
        /// Verifies that player names, player type selection are valid.
        /// Saves them.
        /// </summary>
        /// <param name="sender">Confirm button clicked.</param>
        /// <param name="e">How to handle.</param>
        public void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (P1Human.IsChecked == P1CPU.IsChecked || P2Human.IsChecked == P2CPU.IsChecked)
            {
                MessageBox.Show("Select one type for each player");
                return;
            }

            if (string.IsNullOrWhiteSpace(Player1Name.Text) || string.IsNullOrWhiteSpace(Player2Name.Text))
                {
                    MessageBox.Show("Enter both player names.");
                    return;
                }
            NameOne = Player1Name.Text.Trim();
            NameTwo = Player2Name.Text.Trim();
            Player1IsHuman = P1Human.IsChecked == true;
            Player2IsHuman = P2Human.IsChecked == true;

            DialogResult = true;
        }

        /// <summary>
        /// Initialize SetupGameDialog
        /// </summary>
        public SetupGameDialog()
        {
            InitializeComponent();
            Closing += SetupGameDialog_Closing;
        }

        /// <summary>
        /// If the X is clicked instead of confirm, it refuses to exit.
        /// Prevents player from reaching the game without inputing names and playertypes.
        /// </summary>
        /// <param name="sender">If forcefully closing</param>
        /// <param name="e">handles it. Doesn't allow.</param>
        private void SetupGameDialog_Closing(object  sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DialogResult != true)
            {
                e.Cancel = true;
            }
        }
    }
}
