namespace GamePicker;

class Program
{
    static void Main(string[] args)
    {

        // Players working
        Player player1 = new Player(playerColor:"red");
        Player player2 = new Player("Pedro 2",playerColor:"blue");
        Player player3 = new Player();
        //Player[] players = new Player[3]{player1,player2,player3};
        Player[] players = new Player[2] { player1, player2 };
        // Help working
        Help gamedictionary = new Help();
        Console.WriteLine(gamedictionary);

        
        foreach (var i in players)
        {
            Console.WriteLine(i.playerName);
        }


        //Board Working but need to create the functions. 

//   Board firstBoard= new Board(width: 5, height: 5);
        Board firstBoard = new Board(width: 6, height: 5);
        firstBoard.printBoard();
        Console.WriteLine("----------------------------------");




        Game SosGame = new SOSGame(arrPlayers:players,gameActivePlayer:1,gameHelp:gamedictionary,gameBoard:firstBoard);

        //Console.WriteLine(SosGame.getPlayer(1));
        //Console.WriteLine(strings.Length);


        //Help gamedictionary = new Help();
        //Console.WriteLine(gamedictionary);
        //Console.OutputEncoding = System.Text.Encoding.UTF8;
        //UserInterface a = new UserInterface();
    }
}
