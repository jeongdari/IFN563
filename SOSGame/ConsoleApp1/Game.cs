using System;
using System.Collections.Generic;
using System.Numerics;

namespace BoardGamesFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Board Games Framework!");

            int gameChoice = UserInterface.GetUserChoice();

            if (gameChoice == 1)
            {
                SOSGame sosGame = new SOSGame(3, "Player 1", "Player 2");
                PlayGame(sosGame);
            }
            else if (gameChoice == 2)
            {
                FourLineGame fourLineGame = new FourLineGame(3, "Player 1", "Player 2", 4);
                PlayGame(fourLineGame);
            }
            else
            {
                Console.WriteLine("Invalid choice. Exiting...");
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static void PlayGame(Game game)
        {
            game.Start();
        }
    }
    public abstract class Game
    {
        protected List<Player> players;
        protected Board board;
        protected int currentPlayerIndex;

        public Game(Board initialBoard, char[] playerSymbols, params string[] playerNames)
        {
            board = initialBoard;
            players = new List<Player>();
            for (int i = 0; i < playerNames.Length; i++)
            {
                players.Add(new Player(playerNames[i], playerSymbols[i]));
            }
        }

        public void Start()
        {
            Console.WriteLine("Welcome to the game!");
            InitializeGame();
            Play();
            DisplayResults();
        }

        public abstract void InitializeGame();
        public abstract void Play();
        protected abstract void DisplayResults();
    }
}
