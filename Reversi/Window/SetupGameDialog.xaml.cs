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
    /// SetupGameDialog assigns an enum for respective Gamemodes that exist,
    ///     (1) Human vs Human
    ///     (2) Human vs CPU
    ///     (3) CPU vs CPU
    /// labels and button content guide the user.
    /// The buttons raise the event MouseClick, The event handlers assign the correct enum
    /// Safely exits with DialogResult = true;
    /// set to public so GameManager can retrieve the value.
    /// 
    /// </summary>
    public enum GameMode
    {
        hvh, hvc, cvc
    }
    public partial class SetupGameDialog : Window
    {
        public void HVHOptionPicked(object sender, RoutedEventArgs e)
        {
            SelectedGameMode = GameMode.hvh;
            DialogResult = true;
        }

        public void HVCOptionPicked(object sender, RoutedEventArgs e)
        {
            SelectedGameMode = GameMode.hvc;
            DialogResult = true;
        }

        public void CVCOptionPicked(object sender, RoutedEventArgs e)
        {
            SelectedGameMode = GameMode.cvc;
            DialogResult = true;
        }
        public GameMode SelectedGameMode
        {
            get; private set;
        }
        public SetupGameDialog()
        {
            InitializeComponent();
        }
    }
}
