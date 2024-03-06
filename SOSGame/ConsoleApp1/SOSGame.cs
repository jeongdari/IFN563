using System;
using System.Collections.Generic;

namespace BoardGamesFramework
{
    public class SOSGame : Game
    {
        // private new List<Player> players;
        private readonly Board _board; // Renamed variable to avoid conflict
        private new List<Player> players;

        public SOSGame(int gridSize, params string[] playerNames) : base(new Board(gridSize), "SO".ToCharArray(), playerNames)
        {
            _board = new Board(gridSize); // Updated variable name
            players = new List<Player>();

            char[] playerSymbols = "SO".ToCharArray();
            for (int i = 0; i < playerNames.Length; i++)
            {
                players.Add(new Player(playerNames[i], playerSymbols[i % playerSymbols.Length]));
            }
        }
        public override void InitializeGame()
        {
            Console.WriteLine("Welcome to SOS Game!");
            Console.WriteLine("Rules: ..."); // Explain the rules of the game
            Console.WriteLine("Press any key to start...");
            Console.ReadKey();
        }

        public override void Play()
        {
            int currentPlayerIndex = 0;

            while (!_board.IsFull())
            {
                Console.Clear();
                _board.Display();
                //Board.DisplayBoard(_board); // Use the DisplayBoard method to show the game board
                Player currentPlayer = players[currentPlayerIndex];
                Console.WriteLine($"{currentPlayer.Name}'s turn:");

                // Get player's move (row, column, and symbol)
                int row = GetRowInput();
                int col = GetColInput();
                char symbol = currentPlayer.Symbol; // Use the player's symbol

                if (_board.IsValidMove(row, col))
                {
                    // Place the symbol on the board
                    _board.MakeMove(row, col, symbol);

                    // Check for SOS and update the player's score
                    int sosCount = _board.CheckForSOS(row, col, symbol);
                    currentPlayer.Score += sosCount;

                    // Alternate players
                    currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
                }
                else
                {
                    Console.WriteLine("Invalid move. Please try again.");
                }
            }

            // Determine the winner and display the results
            DetermineWinner();
            Console.Clear();
            Board.DisplayBoard(_board);
            DisplayResults();
        }

        private int GetRowInput()
        {
            // Implement getting row input from the user
            Console.Write("Enter the row number: ");
            int row = int.Parse(Console.ReadLine());
            return row;
        }

        private int GetColInput()
        {
            // Implement getting column input from the user
            Console.Write("Enter the column number: ");
            int col = int.Parse(Console.ReadLine());
            return col;
        }

        private char GetSymbolInput()
        {
            // Implement getting symbol input from the user
            Console.Write("Enter 'S' or 'O': ");
            char symbol = char.Parse(Console.ReadLine().ToUpper());
            return symbol;

        }

        private void DetermineWinner()
        {
            // Implement determining the winner based on scores
            Player winner = players[0]; // Assume the first player is the winner initially
            foreach (Player player in players)
            {
                if (player.Score > winner.Score)
                {
                    winner = player;
                }
            }
            Console.WriteLine($"Winner: {winner.Name}");
        }

        protected override void DisplayResults()
        {
            // Implement displaying the final game results
            Console.WriteLine("Game Over - Final Results:");
            foreach (Player player in players)
            {
                Console.WriteLine($"{player.Name}: {player.Score} SOS");
            }
        }
    }
}


