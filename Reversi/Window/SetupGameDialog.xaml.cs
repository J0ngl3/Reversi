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
    /// Assigns a value (enum) and defines the available gamemodes
    /// hvh = HUMAN VS HUMAN    --> 0
    /// hvc = HUMAN VS CPU      --> 1
    /// cvc = CPU VS CPU        --> 2
    /// </summary>
    

    /// <summary>
    /// The main popup dialog used for selecting a gamemode and passing the value.
    /// The buttons assign a value defined by enum GameMdoe and passes it to SelectedGameMode.
    /// It closes the dialog safely afterwards.
    /// </summary>
    public partial class SetupGameDialog : Window
    {
        public string NameOne { get; private set; }
        public string NameTwo { get; private set; }

        public bool Player1IsHuman { get; private set; }
        public bool Player2IsHuman { get; private set; }

        
        /// <summary>
        /// Event handler for HUMAN VS HUMAN gamemode picked.
        /// </summary>
        /// <param name="sender">Button that raises the event</param>
        /// <param name="e">event data for Click</param>
        
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

        private void SetupGameDialog_Closing(object  sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DialogResult != true)
            {
                e.Cancel = true;
            }
        }
    }
}
