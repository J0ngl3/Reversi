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
    public enum GameMode
    {
        hvh, hvc, cvc
    }

    /// <summary>
    /// The main popup dialog used for selecting a gamemode and passing the value.
    /// The buttons assign a value defined by enum GameMdoe and passes it to SelectedGameMode.
    /// It closes the dialog safely afterwards.
    /// </summary>
    public partial class SetupGameDialog : Window
    {
        /// <summary>
        /// Event handler for HUMAN VS HUMAN gamemode picked.
        /// </summary>
        /// <param name="sender">Button that raises the event</param>
        /// <param name="e">event data for Click</param>
        public void HVHOptionPicked(object sender, RoutedEventArgs e)
        {
            SelectedGameMode = GameMode.hvh;
            DialogResult = true;
        }
        /// <summary>
        /// Event handler for HUMAN VS CPU gamemode picked.
        /// </summary>
        /// <param name="sender">Button that raises the event</param>
        /// <param name="e">Event data for Click</param>
        public void HVCOptionPicked(object sender, RoutedEventArgs e)
        {
            SelectedGameMode = GameMode.hvc;
            DialogResult = true;
        }
        /// <summary>
        /// Event handler for CPU VS CPU gamemode picked.
        /// </summary>
        /// <param name="sender">Button that raises the event</param>
        /// <param name="e">Event data for Click</param>
        public void CVCOptionPicked(object sender, RoutedEventArgs e)
        {
            SelectedGameMode = GameMode.cvc;
            DialogResult = true;
        }
        /// <summary>
        /// Used to get the gamemode selected by user
        /// </summary>
        public GameMode SelectedGameMode
        {
            get; private set;
        }
        /// <summary>
        /// Initialize SetupGameDialog
        /// </summary>
        public SetupGameDialog()
        {
            InitializeComponent();
        }
    }
}
