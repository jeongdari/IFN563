using System;

namespace BoardGamesFramework
{
    public class UserInterface
    {
        public static void DisplayBoard(Board board)
        {
            int size = board.Size;

            // Display column labels
            Console.Write("  ");
            for (int col = 0; col < size; col++)
            {
                Console.Write($"{col + 1} ");
            }
            Console.WriteLine();

            // Display the current state of the game board
            for (int row = 0; row < size; row++)
            {
                Console.Write($"{row + 1} "); // Display row label
                for (int col = 0; col < size; col++)
                {
                    Console.Write(board.GetSymbol(row, col) + " ");
                }
                Console.WriteLine();
            }
        }

        public static int GetUserChoice()
        {
            Console.WriteLine("Select a game to play:");
            Console.WriteLine("1. SOS Game");
            Console.WriteLine("2. Four Line Game");
            Console.Write("Enter your choice: ");

            int choice = int.Parse(Console.ReadLine());
            return choice;
        }

        public static void DisplayMessage(string message)
        {
            // Display a message to the user
            Console.WriteLine(message);
        }
    }
}

