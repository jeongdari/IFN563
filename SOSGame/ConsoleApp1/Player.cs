using System;

namespace BoardGamesFramework
{
    public class Player
    {
        public string Name { get; }
        public char Symbol { get; }
        public int Score { get; set; } // Add the Score property

        public Player(string name, char symbol)
        {
            Name = name;
            Symbol = symbol;
            Score = 0; // Initialize the score to 0
        }

        public virtual int MakeMove(Board board)
        {
            throw new NotImplementedException();
        }
    }

    public class HumanPlayer : Player
    {
        public HumanPlayer(string name, char symbol) : base(name, symbol)
        {
        }

        public override int MakeMove(Board board)
        {
            Console.WriteLine($"{Name}, it's your turn. Enter row and column (e.g., 1 2):");

            while (true)
            {
                string input = Console.ReadLine();
                if (input != null)
                {
                    string[] inputParts = input.Split();
                    if (inputParts.Length == 2 &&
                        int.TryParse(inputParts[0], out int userRow) && int.TryParse(inputParts[1], out int userCol))
                    {
                        // Convert user-friendly row and column numbers to 0-based indices
                        int row = userRow - 1;
                        int col = userCol - 1;

                        if (board.IsValidMove(row, col))
                        {
                            board.MakeMove(row, col, Symbol); // Place the symbol on the board
                            return row * board.Size + col; // Return the move index
                        }
                        else
                        {
                            Console.WriteLine("Invalid move. The selected cell is not empty.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input format. Please enter two space-separated integers.");
                    }
                }
            }
        }
    }
}

