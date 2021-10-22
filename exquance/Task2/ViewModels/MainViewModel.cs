using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using Task2.Models;
using Task2.ViewModels;

namespace Task2.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public char[][] Field { get; set; }

        public RelayCommand ButtonClickCommand { get; private set; }

        const char userChar = 'X';
        const char computerChar = 'O';
        const char EMPTY_CHAR = ' ';

        public MainViewModel()
        {
            ButtonClickCommand = new RelayCommand(DoButtonClick);
            InitField();
        }

        private void DoButtonClick(object obj)
        {
            int x = Convert.ToInt32(obj) / 10;
            int y = Convert.ToInt32(obj) % 10;

            Field[x][y] = userChar;
            RaisePropertyChanged(nameof(Field));

            if (CheckWin(userChar, Field))
            {
                MessageBox.Show("победа игрока");
                InitField();
            }

            var move = GetComputerMove(Field);

            if (move != null)
            {
                Field[move.x][move.y] = computerChar;
                RaisePropertyChanged(nameof(Field));
                if (CheckWin(computerChar, Field))
                {
                    MessageBox.Show("победа компьютера");
                    InitField();
                }
            }
            else
            {
                if (CheckDraw(Field))
                {
                    MessageBox.Show("ничья");
                    InitField();
                }
            }
        }


        private void InitField()
        {
            Field = new char[][] { new[] { EMPTY_CHAR, EMPTY_CHAR, EMPTY_CHAR }, new[] { EMPTY_CHAR, EMPTY_CHAR, EMPTY_CHAR }, new[] { EMPTY_CHAR, EMPTY_CHAR, EMPTY_CHAR } };
            RaisePropertyChanged(nameof(Field));
        }

        private bool CheckWin(char player, char[][] board)
        {
            for (int i = 0; i < 3; i++)
            {
                if (board[i][0] == board[i][1] && board[i][0] == board[i][2] && board[i][0] == player)
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
}
