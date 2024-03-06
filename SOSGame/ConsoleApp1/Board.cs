
namespace BoardGamesFramework
{
    public class Board
    {
        public static void DisplayBoard(Board board)
        {
            // Display the current state of the game board
            for (int row = 0; row < board.Size; row++)
            {
                for (int col = 0; col < board.Size; col++)
                {
                    Console.Write(board.GetSymbol(row, col) + " ");
                }
                Console.WriteLine();
            }
        }

        private char[,] grid;
        public int Size { get; }

        public Board(int size)
        {
            Size = size;
            grid = new char[size, size];
            Initialize();
        }

        public void Display()
        {
            int size = grid.GetLength(0);

            Console.WriteLine("Current Board:");
            Console.Write("  ");
            for (int col = 0; col < size; col++)
            {
                Console.Write($"{col} ");
            }
            Console.WriteLine();

            for (int row = 0; row < size; row++)
            {
                Console.Write($"{row} ");
                for (int col = 0; col < size; col++)
                {
                    Console.Write($"{grid[row, col]} ");
                }
                Console.WriteLine();
            }
        }
        public void Initialize()
        {
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    grid[row, col] = ' ';
                }
            }
        }
        public void SetPieceAt(int row, int col, char piece)
        {
            grid[row, col] = piece;
        }

        public bool IsValidMove(int row, int col)
        {
            return row >= 0 && row < Size && col >= 0 && col < Size && grid[row, col] == ' ';
        }

        public void MakeMove(int row, int col, char symbol)
        {
            grid[row, col] = symbol;
        }

        public char GetSymbol(int row, int col)
        {
            return grid[row, col];
        }

        public bool IsFull()
        {
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    if (grid[row, col] == ' ')
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public int CheckForSOS(int row, int col, char symbol)
        {
            // Implement the logic to check for SOS sequences starting from the given row and column
            // Return the number of SOS sequences found
            int sosCount = 0;

            // Check horizontally
            if (col >= 2 && grid[row, col - 1] == 'S' && grid[row, col - 2] == 'S')
                sosCount++;
            if (col <= Size - 3 && grid[row, col + 1] == 'S' && grid[row, col + 2] == 'S')
                sosCount++;

            // Check vertically
            if (row >= 2 && grid[row - 1, col] == 'S' && grid[row - 2, col] == 'S')
                sosCount++;
            if (row <= Size - 3 && grid[row + 1, col] == 'S' && grid[row + 2, col] == 'S')
                sosCount++;

            // Check diagonally (top-left to bottom-right)
            if (row >= 2 && col >= 2 && grid[row - 1, col - 1] == 'S' && grid[row - 2, col - 2] == 'S')
                sosCount++;
            if (row <= Size - 3 && col <= Size - 3 && grid[row + 1, col + 1] == 'S' && grid[row + 2, col + 2] == 'S')
                sosCount++;

            // Check diagonally (top-right to bottom-left)
            if (row >= 2 && col <= Size - 3 && grid[row - 1, col + 1] == 'S' && grid[row - 2, col + 2] == 'S')
                sosCount++;
            if (row <= Size - 3 && col >= 2 && grid[row + 1, col - 1] == 'S' && grid[row + 2, col - 2] == 'S')
                sosCount++;

            return sosCount;
        }
        public bool IsGameOver()
        {
            int size = Size;

            // Check for horizontal SOS sequences
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size - 2; col++)
                {
                    if (grid[row, col] == 'S' && grid[row, col + 1] == 'O' && grid[row, col + 2] == 'S')
                    {
                        return true;
                    }
                }
            }

            // Check for vertical SOS sequences
            for (int col = 0; col < size; col++)
            {
                for (int row = 0; row < size - 2; row++)
                {
                    if (grid[row, col] == 'S' && grid[row + 1, col] == 'O' && grid[row + 2, col] == 'S')
                    {
                        return true;
                    }
                }
            }

            // Check for diagonal (top-left to bottom-right) SOS sequences
            for (int row = 0; row < size - 2; row++)
            {
                for (int col = 0; col < size - 2; col++)
                {
                    if (grid[row, col] == 'S' && grid[row + 1, col + 1] == 'O' && grid[row + 2, col + 2] == 'S')
                    {
                        return true;
                    }
                }
            }

            // Check for diagonal (top-right to bottom-left) SOS sequences
            for (int row = 0; row < size - 2; row++)
            {
                for (int col = 2; col < size; col++)
                {
                    if (grid[row, col] == 'S' && grid[row + 1, col - 1] == 'O' && grid[row + 2, col - 2] == 'S')
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        public bool PlaceSymbol(int row, int col, char symbol)
        {
            if (IsValidMove(row, col) && grid[row, col] == '\0')
            {
                grid[row, col] = symbol;
                return true;
            }
            return false;
        }


        public bool CheckHorizontalLine(int row, int targetLineLength)
        {
            int consecutiveCount = 0;
            char currentSymbol = '\0';

            for (int col = 0; col < grid.GetLength(1); col++)
            {
                if (grid[row, col] != '\0')
                {
                    if (grid[row, col] == currentSymbol)
                    {
                        consecutiveCount++;
                        if (consecutiveCount == targetLineLength)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        currentSymbol = grid[row, col];
                        consecutiveCount = 1;
                    }
                }
                else
                {
                    currentSymbol = '\0';
                    consecutiveCount = 0;
                }
            }

            return false;
        }
        public bool CheckVerticalLine(int col, int targetLineLength)
        {
            int consecutiveCount = 0;
            char currentSymbol = '\0';

            for (int row = 0; row < grid.GetLength(0); row++)
            {
                if (grid[row, col] != '\0')
                {
                    if (grid[row, col] == currentSymbol)
                    {
                        consecutiveCount++;
                        if (consecutiveCount == targetLineLength)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        currentSymbol = grid[row, col];
                        consecutiveCount = 1;
                    }
                }
                else
                {
                    currentSymbol = '\0';
                    consecutiveCount = 0;
                }
            }

            return false;
        }
        public bool CheckDiagonalLine(int row, int col, int targetLineLength)
        {
            int consecutiveCount = 0;
            char currentSymbol = '\0';

            int startRow = row - Math.Min(row, col);
            int startCol = col - Math.Min(row, col);

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                int currentRow = startRow + i;
                int currentCol = startCol + i;

                if (currentRow >= grid.GetLength(0) || currentCol >= grid.GetLength(1))
                {
                    break;
                }

                if (grid[currentRow, currentCol] != '\0')
                {
                    if (grid[currentRow, currentCol] == currentSymbol)
                    {
                        consecutiveCount++;
                        if (consecutiveCount == targetLineLength)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        currentSymbol = grid[currentRow, currentCol];
                        consecutiveCount = 1;
                    }
                }
                else
                {
                    currentSymbol = '\0';
                    consecutiveCount = 0;
                }
            }

            return false;
        }
        public bool CheckAntiDiagonalLine(int row, int col, int targetLineLength)
        {
            int consecutiveCount = 0;
            char currentSymbol = '\0';

            int startRow = row - Math.Min(row, grid.GetLength(1) - col - 1);
            int startCol = col + Math.Min(row, grid.GetLength(1) - col - 1);

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                int currentRow = startRow + i;
                int currentCol = startCol - i;

                if (currentRow >= grid.GetLength(0) || currentCol < 0)
                {
                    break;
                }

                if (grid[currentRow, currentCol] != '\0')
                {
                    if (grid[currentRow, currentCol] == currentSymbol)
                    {
                        consecutiveCount++;
                        if (consecutiveCount == targetLineLength)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        currentSymbol = grid[currentRow, currentCol];
                        consecutiveCount = 1;
                    }
                }
                else
                {
                    currentSymbol = '\0';
                    consecutiveCount = 0;
                }
            }

            return false;
        }
    }
}

