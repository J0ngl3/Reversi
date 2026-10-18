using Reversi.Game;
using Reversi.Model;
using Reversi.Players;
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
    /// Final result dialog that pops up.
    /// </summary>
    public partial class ResultDialog : Window
    {
        /// <summary>
        /// placeholder for winner's color
        /// </summary>
        private PlayerColor winner;

        /// <summary>
        /// Initialize window.
        /// Show result.
        /// </summary>
        /// <param name="winner">Winner's color</param>
        public ResultDialog(PlayerColor winner)
        {
            InitializeComponent();
            this.winner = winner;
            Showresult();
        }

        /// <summary>
        /// If winner has no color, draw
        /// Otherwise prints the winner's colour.
        /// </summary>
        private void Showresult()
        {
            if (winner == PlayerColor.None)
            {
                ResultLabel.Content = "DRAW!";
            }
            else
            {
                ResultLabel.Content = $"{winner} WINS!";
            }
        }

        /// <summary>
        /// Closes the Result window and restarts.
        /// Set the boolean restartRequest to true in GameWindow
        /// </summary>
        /// <param name="sender">Click event</param>
        /// <param name="e">Event handler, Closes the result window</param>
        private void RestartGame(object sender, EventArgs e)
        {
            DialogResult = true;
        }

        /// <summary>
        /// Quits the game entirely.
        /// </summary>
        /// <param name="sender">Click event</param>
        /// <param name="e">Handles the click</param>
        private void ExitGame(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}