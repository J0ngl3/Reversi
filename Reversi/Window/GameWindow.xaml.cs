using Reversi.Game;
using Reversi.Model;
using Reversi.Players;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Main game window.
    /// Responsible for creating the board, the disks and opening the SetupGameDialog for gamemode choice.
    /// handles user interaction with gameboard.
    /// </summary>
    
    public partial class GameWindow : Window
    {
        /// <summary>
        /// Declare gameManger privately so it can be accessed by other methods.
        /// </summary>
        private GameManager gameManager;
        /// <summary>
        /// Holds and stores disk, Legaldisks and playerTrackerLabel positions on the board. Board is 8x8.
        /// </summary>
        /// 
        private Ellipse[,] disks = new Ellipse[8, 8];
        private Ellipse[,] legalDisks = new Ellipse[8, 8];
        private Label playerTrackerLabel;
        private Label DiskCount;
        
        /// <summary>
        /// Initializes, creates the board, creates the disks, launches gamemode selector dialog
        /// Loaded += GameWindow_Loaded is a check so the main window is launched first. This is so dialog.Owner = this; can be used.
        /// Finished += GameWindow_Finished is the final result using a dialog for the result.
        /// If dialog launches before Main Game Window, it can't be owned by the main window.
        /// </summary>
        public GameWindow()
        {
            InitializeComponent();
            CreateBoard();
            CreateDisks();
            SetupStartingBoard();
            Loaded += GameWindow_Loaded;
        }
        /// <summary>
        /// When Game is launched, opens SetupGameDialog, setting ownership to the main window.
        /// Receives player names, types and creates a new gameManager instance.
        /// Sends booleans along to check whether players are human.
        /// Creates a label at the bottom of the Game grid to show which player's turn it is.
        /// Refreshes the board after moves.
        /// Checks whether the game is over or not.
        /// 
        /// Checkss the boolean restartRequest and if true, sets it to false and starts a new game instance.
        /// </summary>
        /// <param name="sender">game window that has loaded.</param>
        /// <param name="e">event info</param>
        private async void GameWindow_Loaded(object sender, RoutedEventArgs e)
        {
            SetupGameDialog dialog = new SetupGameDialog();
            dialog.Owner = this;
            if (dialog.ShowDialog() == true)
            {
                string name1 = dialog.NameOne;
                string name2 = dialog.NameTwo;

                bool human1 = dialog.Player1IsHuman;
                bool human2 = dialog.Player2IsHuman;
                gameManager = new GameManager(name1, name2, human1, human2);
                CreatePlayerTrackerLabel();

                gameManager.BoardRefreshed += Refresh;
                gameManager.GameFinished += ShowGameResult;
                await gameManager.GameStarted();

                do
                {
                    restartRequest = false;
                    await StartNewGame();
                }
                while (restartRequest);
            }
        }

        /// <summary>
        /// Opens SetupGameDialog again. Starts anew GameManager instance
        /// If PlayerTrackerLabel missing, makes a new one.
        /// Refreshes and Show gameresults when game is over.
        /// </summary>
        /// <returns>Task of game operation, async.</returns>
        private async Task StartNewGame()
        {
            SetupGameDialog dialog = new SetupGameDialog();
            dialog.Owner = this;

            if (dialog.ShowDialog() == true)
            {
                gameManager = new GameManager
                (
                    dialog.NameOne, dialog.NameTwo, dialog.Player1IsHuman, dialog.Player2IsHuman
                );

                if (playerTrackerLabel == null)
                {
                    CreatePlayerTrackerLabel();
                }

                gameManager.BoardRefreshed += Refresh;
                gameManager.GameFinished += ShowGameResult;

                await gameManager.GameStarted();
            }
        }
        /// <summary>
        /// creates the 8x8 board. First adds row & column definition.
        /// Adds Mouse Click event handler, Cell_MouseLeftButtonDown
        /// </summary>
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
        /// <summary>
        /// Event handler for when a rectangle is clicked.
        /// Simple check if game is valid.
        /// Passes it to TrySelectMove along with the position (pos)
        /// Clears Legal move disks since they need to be redrawn.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cell_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (gameManager == null || gameManager.GameOver)
            {
                return;
            }
            if (gameManager.CurrentUser is HumanPlayer human)
            {
                Rectangle rectangle = (Rectangle)sender;
                int row = Grid.GetRow(rectangle);
                int column = Grid.GetColumn(rectangle);

                foreach (Position pos in gameManager.ValidMoves.Keys)
                {
                    if (pos.Row == row && pos.Col == column)
                    {
                        if (human.TrySelectMove(pos))
                        {
                            ClearLegalMoves();
                        }
                        
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// creates the disks and legal disks.
        /// uses the defined row & column definitions.
        /// </summary>
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
                    Ellipse legalDisks = new Ellipse
                    {
                        Width = 21,
                        Height = 21,
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
                    Grid.SetRow(legalDisks, row);
                    Grid.SetColumn(legalDisks, column);
                    GameGrid.Children.Add(legalDisks);
                    this.legalDisks[row, column] = legalDisks;
                }
            }
        }

       /// <summary>
       /// Refreshes the table when a move has happened.
       /// Both player disks and legal move indicator disks.
       /// Updates player label tracker when current player changes.
       /// </summary>
        private void Refresh()
        {
            for (int row = 0; row  < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    PlayerColor color = gameManager.gameBoard.Board[row, column];
                    if (color == PlayerColor.Black)
                    {
                        ShowBlackDisk(row, column);
                    }
                    else if (color == PlayerColor.White)
                    {
                        ShowWhiteDisk(row, column);
                    }
                    else
                    {
                        ClearDisk(row, column);
                    }
                }
            }
            ShowLegalMoves();
            UpdatePlayerTracker();
        }

        /// <summary>
        /// Adds label underneath game grid to show which player's turn it is.
        /// Adds label to keep count of Disk amounts for each color.
        /// </summary>
        private void CreatePlayerTrackerLabel()
        {
            playerTrackerLabel = new Label
            {
                Content = $"{gameManager.CurrentUser.Name}'s turn. Their Colour is {gameManager.CurrentUser.playerColor}.",
                HorizontalAlignment = HorizontalAlignment.Left,
                FontWeight = FontWeights.Bold,
                FontSize = 20,
                Foreground = Brushes.White
            };
            InfoGrid.Children.Add(playerTrackerLabel);

            DiskCount = new Label
            {
                Content = $"Number of black disks: {gameManager.gameBoard.DiscCount[PlayerColor.Black]}\nNumber of white disks: {gameManager.gameBoard.DiscCount[PlayerColor.White]}",
                HorizontalAlignment = HorizontalAlignment.Right,
                Margin = new Thickness(0,35,0,0),
                FontWeight = FontWeights.Bold,
                FontSize = 20,
                Foreground = Brushes.White
            };
            InfoGrid.Children.Add(DiskCount);
        }

        /// <summary>
        /// Updates the player tracker label
        /// </summary>
        private void UpdatePlayerTracker()
        {
            playerTrackerLabel.Content = $"{gameManager.CurrentUser.Name}'s turn. Their Colour is {gameManager.CurrentUser.playerColor}.";
            DiskCount.Content = $"Number of black disks: {gameManager.gameBoard.DiscCount[PlayerColor.Black]}\nNumber of white disks: {gameManager.gameBoard.DiscCount[PlayerColor.White]}";
        }

        /// <summary>
        /// Shows the results of the game. Shuts down when clicking ok on messagebox.
        /// </summary>
        /// 

        private bool restartRequest = false;
        private void ShowGameResult()
        {
            if (gameManager.GameOver == true)
            {
                ResultDialog dialog = new ResultDialog(gameManager.Winner);
                dialog.Owner = this;
                restartRequest = dialog.ShowDialog() == true;
            }
            
        }

        /// <summary>
        /// Displays black disk.
        /// Used on a specific cell.
        /// </summary>
        /// <param name="row">board row.</param>
        /// <param name="column">board column.</param>
        private void ShowBlackDisk(int row, int column)
        {
            disks[row, column].Fill = Brushes.Black;
            disks[row, column].Stroke = Brushes.White;
            disks[row, column].StrokeThickness = 2;
            disks[row, column].Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Displays white disk.
        /// Used on specific cell.
        /// </summary>
        /// <param name="row">board row.</param>
        /// <param name="column">board column.</param>
        private void ShowWhiteDisk(int row, int column)
        {
            disks[row, column].Fill = Brushes.White;
            disks[row, column].Stroke = Brushes.Black;
            disks[row, column].StrokeThickness = 2;
            disks[row, column].Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Displays gray disk, used for legal move indicators.
        /// </summary>
        /// <param name="row">board row.</param>
        /// <param name="column">board column.</param>
        private void ShowGrayDisk(int row, int column)
        {
            legalDisks[row, column].Fill = Brushes.Gray;
            legalDisks[row, column].Stroke = Brushes.Black;
            legalDisks[row, column].StrokeThickness = 2;
            legalDisks[row, column].Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Hides specific player disk completely.
        /// </summary>
        /// <param name="row">board row</param>
        /// <param name="column">board column</param>
        private void ClearDisk(int row, int column)
        {
            disks[row, column].Visibility = Visibility.Collapsed;
        }
        
        /// <summary>
        /// Hides Legal indicator disk completely.
        /// </summary>
        /// <param name="row">board row.</param>
        /// <param name="column">board column.</param>
        private void ClearGrayDisk(int row, int column)
        {
            legalDisks[row, column].Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Sets up the starting position for the game.
        /// </summary>
        private void SetupStartingBoard()
        {
            ShowWhiteDisk(3, 3);
            ShowBlackDisk(3, 4);
            ShowWhiteDisk(4, 4);
            ShowBlackDisk(4, 3);
        }
        
        /// <summary>
        /// Clears the old legal move indicators, places new one receiving keys from ValidMovesCalc.
        /// </summary>
        private void ShowLegalMoves()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    ClearGrayDisk(row, column);
                }
            }
            foreach (Position pos in gameManager.ValidMoves.Keys)
            {
                ShowGrayDisk(pos.Row, pos.Col);
            }
        }

        /// <summary>
        /// Hides legal move disks.
        /// </summary>
        private void ClearLegalMoves()
        {
            foreach (Position pos in gameManager.ValidMoves.Keys)
            {
                ClearGrayDisk(pos.Row, pos.Col);
            }
        }
    }
}