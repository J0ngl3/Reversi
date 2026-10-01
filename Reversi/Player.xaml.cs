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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Player : Window
    {
        public bool IsHuman { get; private set }
        public bool IsCPU { get; private set; }
        public Player()
        {
            InitializeComponent();
        }
        public void OnClickHuman(object sender, RoutedEventArgs e)
        {
            IsHuman = true;
            DialogResult = true;
            Close();
        }
        public void OnClickCPU(object sender, RoutedEventArgs e)
        {
            IsCPU = true;
            DialogResult = true;
            Close();
        }
    }
}
