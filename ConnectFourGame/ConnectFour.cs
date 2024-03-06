using System;

class ConnectFourGame
{
    private int[,] board;
    private int currentPlayer;
    private bool gameWon;

    public ConnectFourGame()
    {
        board = new int[6, 7];
        currentPlayer = 1;
        gameWon = false;
    }

    public void Play()
    {
        Console.WriteLine("Welcome to Connect Four!");
        PrintBoard();

        while (!gameWon)
        {
            Console.Write($"Player {currentPlayer}, choose a column (0-6): ");
            int column = int.Parse(Console.ReadLine());

            if (column >= 0 && column < 7)
            {
                if (DropToken(column))
                {
                    PrintBoard();
                    CheckWin();
                    if (!gameWon)
                    {
                        currentPlayer = 3 - currentPlayer; // Switch player between 1 and 2
                    }
                }
                else
                {
                    Console.WriteLine("Column is full. Choose another column.");
                }
            }
            else
            {
                Console.WriteLine("Invalid column. Please enter a valid column (0-6).");
            }
        }
    }

    private bool DropToken(int column)
    {
        for (int row = 5; row >= 0; row--)
        {
            if (board[row, column] == 0)
            {
                board[row, column] = currentPlayer;
                return true;
            }
        }
        return false; // Column is full
    }

    private void CheckWin()
    {
        for (int row = 0; row < 6; row++)
        {
            for (int col = 0; col < 7; col++)
            {
                if (board[row, col] != 0)
                {
                    if (CheckLine(row, col, 1, 0) || // Horizontal
                        CheckLine(row, col, 0, 1) || // Vertical
                        CheckLine(row, col, 1, 1) || // Diagonal /
                        CheckLine(row, col, 1, -1))  // Diagonal \
                    {
                        gameWon = true;
                        Console.WriteLine($"Player {currentPlayer} wins!");
                        return;
                    }
                }
            }
        }
        if (IsBoardFull())
        {
            gameWon = true;
            Console.WriteLine("It's a draw!");
        }
    }

    private bool CheckLine(int startRow, int startCol, int rowDirection, int colDirection)
    {
        int count = 0;
        int row = startRow;
        int col = startCol;

        while (row >= 0 && row < 6 && col >= 0 && col < 7 && board[row, col] == currentPlayer)
        {
            count++;
            row += rowDirection;
            col += colDirection;
        }

        return count >= 4;
    }

    private bool IsBoardFull()
    {
        for (int col = 0; col < 7; col++)
        {
            if (board[0, col] == 0)
            {
                return false;
            }
        }
        return true;
    }

    private void PrintBoard()
    {
        Console.Clear();
        for (int row = 0; row < 6; row++)
        {
            for (int col = 0; col < 7; col++)
            {
                Console.Write(board[row, col] == 0 ? " " : board[row, col] == 1 ? "X" : "O");
                Console.Write("|");
            }
            Console.WriteLine();
            Console.WriteLine("-------------");
        }
        Console.WriteLine(" 0 1 2 3 4 5 6 ");
    }

    static void Main(string[] args)
    {
        ConnectFourGame game = new ConnectFourGame();
        game.Play();
    }
}

