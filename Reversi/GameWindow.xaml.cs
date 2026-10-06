using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Reversi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class GameWindow : Window
    {
        //making ellipses in the grids, disks for game
        private Ellipse[,] disks = new Ellipse[8, 8];
        public GameWindow()
        {
            InitializeComponent();
            CreateBoard();
            CreateDisks();
            SetupStartingBoard();
        }

        private void CreateBoard()
        {
            for (int i = 0; i < 8; i++)
            {
                GameGrid.RowDefinitions.Add(new RowDefinition());
                GameGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    Rectangle rectangle = new Rectangle
                    {
                        Fill = Brushes.Green,
                        Stroke = Brushes.White,
                        StrokeThickness = 2,
                        Margin = new Thickness(2)
                    };
                    rectangle.MouseLeftButtonDown += Cell_MouseLeftButtonDown;
                    Grid.SetRow(rectangle, row);
                    Grid.SetColumn(rectangle, column);
                    GameGrid.Children.Add(rectangle);
                }
            }
        }

        private void Cell_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle rectangle = (Rectangle)sender;
            int row = Grid.GetRow(rectangle);
            int column = Grid.GetColumn(rectangle);

            MessageBox.Show($"Row: {row}, Column: {column}");
        }
        private void CreateDisks()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    Ellipse disk = new Ellipse
                    {
                        Width = 30,
                        Height = 30,
                        Visibility = Visibility.Collapsed,
                        //mouseclicks must go through the disk to the rectangle under
                        IsHitTestVisible = false,
                        Effect = new DropShadowEffect
                        {
                            Color = Colors.Black,
                            BlurRadius = 8,
                            ShadowDepth = 5,
                            Opacity = 0.8
                        }
                    };
                    Grid.SetRow(disk, row);
                    Grid.SetColumn(disk, column);
                    GameGrid.Children.Add(disk);
                    disks[row, column] = disk;
                }
            }
        }
        
        //show black disks, Fill, border & visibility
        private void ShowBlackDisk(int row, int column)
        {
            disks[row, column].Fill = Brushes.Black;
            disks[row, column].Stroke = Brushes.White;
            disks[row, column].StrokeThickness = 2;
            disks[row, column].Visibility = Visibility.Visible;
        }

        
        private void ShowWhiteDisk(int row, int column)
        {
            disks[row, column].Fill = Brushes.White;
            disks[row, column].Stroke = Brushes.Black;
            disks[row, column].StrokeThickness = 2;
            disks[row, column].Visibility = Visibility.Visible;
        }

        private void ClearDisk(int row, int column)
        {
            disks[row, column].Visibility = Visibility.Collapsed;
        }

        private void SetupStartingBoard()
        {
            ShowWhiteDisk(3, 3);
            ShowBlackDisk(3, 4);
            ShowWhiteDisk(4, 4);
            ShowBlackDisk(4, 3);
        }
    }
}

// HEJ HEJ MAHDY!!