using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Task2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        char[][] field = new char[][] { new []{ EMPTY_CHAR, EMPTY_CHAR, EMPTY_CHAR }, new [] { EMPTY_CHAR, EMPTY_CHAR, EMPTY_CHAR }, new [] { EMPTY_CHAR, EMPTY_CHAR, EMPTY_CHAR } };

        const char userChar = 'X';
        const char computerChar = 'O';
        const char EMPTY_CHAR = ' ';

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var selectedCell = sender as Button;
            int x = Grid.GetRow(selectedCell);
            int y = Grid.GetColumn(selectedCell);

            field[x][y] = userChar;
            selectedCell.Content = userChar;

            if (CheckWin(userChar, field))
            {
                MessageBox.Show("победа игрока");
                InitField();
            }

            var move = GetComputerMove(field);
            if (move != null)
            {
                field[move.x][move.y] = computerChar;
                (this.FindName("cell" + move.x + move.y) as Button).Content = computerChar;
                if (CheckWin(computerChar, field))
                {
                    MessageBox.Show("победа компьютера");
                    InitField();
                }
            }
            else
            {
                if (CheckDraw(field))
                {
                    MessageBox.Show("ничья");
                    InitField();
                }
            }
        }

        private void InitField()
        {
            field = new char[][] { new[] { EMPTY_CHAR, EMPTY_CHAR, EMPTY_CHAR }, new[] { EMPTY_CHAR, EMPTY_CHAR, EMPTY_CHAR }, new[] { EMPTY_CHAR, EMPTY_CHAR, EMPTY_CHAR } };
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    (this.FindName("cell" + i + j) as Button).Content = string.Empty;
        }

        private bool CheckWin(char player, char[][] board)
        {
            for (int i = 0; i < 3; i ++)
            {
                if (board[i][0]== board[i][1] && board[i][0] == board[i][2] && board[i][0] == player)
                    return true;
            }
            for (int i = 0; i < 3; i++)
            {
                if (board[0][i] == board[1][i] && board[0][i] == board[2][i] && board[0][i] == player)
                    return true;
            }
            if (board[0][0] == board[1][1] && board[0][0] == board[2][2] && board[0][0] == player)
                return true;
            if (board[2][0] == board[1][1] && board[2][0] == board[0][2] && board[2][0] == player)
                return true;
            return false;
        }

        private bool CheckDraw(char[][] board)
        {
            int count = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i][j] == ' ')
                        count++;
                }
            }
            if (count == 0)
                return true;
            return false;
        }

        private int Minimax(char[][] board, int depth, Turn turn)
        {
            if (CheckWin(computerChar, board))
                return 100;
            if (CheckWin(userChar, board))
                return -100;
            if (CheckDraw(board))
                return 0;

            int bestScore;
            if (turn == Turn.Computer)
            {
                bestScore = Int32.MinValue;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (board[i][j] == EMPTY_CHAR)
                        {
                            board[i][j] = computerChar;
                            int score = Minimax(board, depth + 1, Turn.User);
                            board[i][j] = EMPTY_CHAR;
                            bestScore = Math.Max(bestScore, score);
                        }
                    }
                }
            }
            else
            {
                bestScore = Int32.MaxValue;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (board[i][j] == EMPTY_CHAR)
                        {
                            board[i][j] = userChar;
                            int score = Minimax(board, depth + 1, Turn.Computer);
                            board[i][j] = EMPTY_CHAR;
                            bestScore = Math.Min(bestScore, score);
                        }
                    }
                }
            }
            return bestScore;
        }

        private Move GetComputerMove(char[][] field)
        {
            Move move = null;
            int bestScore = Int32.MinValue;
            char[][] board = new char[][] { field[0], field[1], field[2] };
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i][j] == EMPTY_CHAR)
                    {
                        board[i][j] = computerChar;
                        int score = Minimax(board, 0, Turn.User);
                        board[i][j] = EMPTY_CHAR;
                        if (score > bestScore)
                        {
                            bestScore = score;
                            move = new Move(i, j);
                        }
                    }
                }
            }
            return move;
        }
    }

    public class Move
    {
        public Move(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int x { get; set; }
        public int y { get; set; }
    }

    enum Turn
    {
        Computer,
        User,
    }
}
