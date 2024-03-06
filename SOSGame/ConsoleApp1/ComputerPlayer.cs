using System;

namespace BoardGamesFramework
{
    public class ComputerPlayer : Player
    {
        public ComputerPlayer(string name, char symbol) : base(name, symbol)
        {
        }

        public override int MakeMove(Board board)
        {
            // Check for any opportunities to complete SOS sequences
            for (int row = 0; row < board.Size; row++)
            {
                for (int col = 0; col < board.Size; col++)
                {
                    if (board.IsValidMove(row, col))
                    {
                        // Try placing 'S'
                        board.MakeMove(row, col, 'S');
                        if (board.IsGameOver())
                        {
                            return row * board.Size + col;
                        }

                        // Try placing 'O'
                        board.MakeMove(row, col, 'O');
                        if (board.IsGameOver())
                        {
                            return row * board.Size + col;
                        }

                        // If no SOS sequence found, undo the move
                        board.MakeMove(row, col, ' ');
                    }
                }
            }

            // If no immediate opportunities, make a random move
            Random random = new Random();
            while (true)
            {
                int randomRow = random.Next(board.Size);
                int randomCol = random.Next(board.Size);
                if (board.IsValidMove(randomRow, randomCol))
                {
                    board.MakeMove(randomRow, randomCol, Symbol);
                    return randomRow * board.Size + randomCol;
                }
            }
        }
    }
}


