using System;
using System.Linq;

namespace BoardGamesFramework
{
    public class FourLineGame : Game
    {
        private int targetLineLength;

        public FourLineGame(int boardSize, string player1Name, string player2Name, int targetLineLength)
            : base(new Board(boardSize), new char[] { 'X', 'O' }, player1Name, player2Name)
        {
            this.targetLineLength = targetLineLength;
        }

        public override void InitializeGame()
        {
            Console.WriteLine($"Starting Four Line Game with {board.Size}x{board.Size} board.");
            Console.WriteLine($"Players: {players[0].Name} ({players[0].Symbol}) vs {players[1].Name} ({players[1].Symbol})");
            Console.WriteLine("Press any key to start...");
            Console.ReadKey();
        }

        public override void Play()
        {
            currentPlayerIndex = 0;
            bool gameFinished = false;

            while (!gameFinished)
            {
                Player currentPlayer = players[currentPlayerIndex];
                Console.WriteLine($"{currentPlayer.Name}'s turn ({currentPlayer.Symbol})");

                int row, col;
                do
                {
                    Console.Write("Enter row (0-2): ");
                    row = int.Parse(Console.ReadLine());
                    Console.Write("Enter column (0-2): ");
                    col = int.Parse(Console.ReadLine());
                } while (!board.PlaceSymbol(row, col, currentPlayer.Symbol));

                Board.DisplayBoard(board); // Call the DisplayBoard method on the instance of the Board

                if (CheckWin(row, col))
                {
                    Console.WriteLine($"{currentPlayer.Name} wins!");
                    gameFinished = true;
                }
                else if (board.IsFull())
                {
                    Console.WriteLine("It's a draw!");
                    gameFinished = true;
                }

                currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
            }
        }

        protected bool CheckWin(int row, int col)
        {
            return board.CheckHorizontalLine(row, targetLineLength) ||
                   board.CheckVerticalLine(col, targetLineLength) ||
                   board.CheckDiagonalLine(row, col, targetLineLength) ||
                   board.CheckAntiDiagonalLine(row, col, targetLineLength);
        }

        protected override void DisplayResults()
        {
            Console.WriteLine("Game Over.");
        }
    }
}

