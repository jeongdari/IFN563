using System.ComponentModel.Design;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Linq.Expressions;
using System.Net;
using System.Numerics;
using System.Reflection;
using System.Runtime.ExceptionServices;

namespace GamePicker;
/// for undo and do in better to use behavioral patterns 
/// 
/// abstract factory for the pieces
/// 
/// for the game we are using template method  is better to use the factory method? 
public abstract class Game
{
    internal Player[] arrPlayer;
    internal Help gameHelp;
    internal Board gameBoard;
    internal int[] gameScore;


    internal int getNumber(string playerName)
    {
        int i=0;
        foreach(Player singleplayer in arrPlayer)
        {
            if (singleplayer.playerName==playerName){
                return i;
            }
            i++;
        }
        return -1;
    }

    // add a player and return the next player
    internal Player changeplayer(Player playerToChange)
    {
        int currentPlayernumber = getNumber(playerToChange.playerName);
        int arrlengt = arrPlayer.Length;
        
        if (currentPlayernumber==arrlengt-1)
        {
            return arrPlayer[0];
        }
        else
        {
            return arrPlayer[currentPlayernumber+1];
        }

    }


    internal int getPlayerScore(int n)
    {
        int playerScore = gameScore[n];
        return playerScore;
    }

    protected abstract void setWinner(Player winnerPlayer);
    protected abstract void redo(Player redoPlayer);
    protected abstract void undo(Player undoPlayer);
    protected abstract void gameStart();
    protected abstract void selectBoardSize();

    public Game(Player[] arrPlayers,Help gameHelp, Board gameBoard, int[] gameScore=null)
    {
        Help standardHelp = new Help();
        this.arrPlayer = arrPlayers;
        this.gameHelp = standardHelp;
        this.gameBoard=gameBoard;
        if (gameScore==null){
            gameScore = new int[arrPlayers.Length];
            for (int i=0; i<arrPlayers.Length;i++){
                gameScore[i] = 0; 
            }
            this.gameScore=gameScore;
        }

    }

}


class SOSGame : Game
{
    private string gameMode;
    internal int gameActivePlayer;
    private string gameWinner;
    private string gameDisplayName;
    private Piece[] gamePieces;
    private int historyMovementId = 0;

    public SOSGame(Player[] arrPlayers, Help gameHelp, Board gameBoard, int gameActivePlayer = 0, int[] gameScore = null, string gameWinner = "") : base(arrPlayers, gameHelp, gameBoard, gameScore)
    {
        this.gameActivePlayer = gameActivePlayer;
        this.gameWinner = gameWinner;
        gameStart();
    }

    private void printTitle()
    {
        int counter = 0;
        string title = @"
░██████╗░█████╗░░██████╗
██╔════╝██╔══██╗██╔════╝
╚█████╗░██║░░██║╚█████╗░
░╚═══██╗██║░░██║░╚═══██╗
██████╔╝╚█████╔╝██████╔╝
╚═════╝░░╚════╝░╚═════╝░";

        Console.WriteLine(title);
        foreach (Player player in arrPlayer)
        {
            Console.ForegroundColor = player.GetConsoleColor();
            Console.Write($"{player.playerName}: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($": {gameScore[counter]}");
            counter++;
        }
        counter = 0;
    }



    protected override void setWinner(Player winnerPlayer)
    {

    }
    protected override void redo(Player redoPlayer)
    {
        int currentposition = 0;
        for (int i = 0; i < redoPlayer.boardHistory.Length; i++)
        {
            if (redoPlayer.boardHistory[i] == gameBoard)
            {
                currentposition = i;
            }
        }
        gameBoard = redoPlayer.boardHistory[currentposition+1];
    }
    protected override void undo(Player undoPlayer)
    {
        int currentposition = 0;
        for (int i = 0; i < undoPlayer.boardHistory.Length; i++)
        {
            undoPlayer.boardHistory[i].printBoard();
            if (undoPlayer.boardHistory[i] == gameBoard)
            {
                currentposition=i;
            }
        }
        gameBoard = undoPlayer.boardHistory[currentposition-1];
        gameBoard.printBoard();
    }
    protected override void gameStart()
    {
        string rowInput = "";
        string columnInput = "";
        int rowint;
        int colint;
        string pieceInput = "";
        string[] valMove;
        Piece activePiece = null;
        bool controller = true;
        int[,] currentPlayPoints;
        
        foreach (Player player in arrPlayer)
        {
            Console.WriteLine("****************");
            player.addHistoryBoard(gameBoard);
            player.boardHistory[0].printBoard();
        }
        ///
        while (gameWinner == "")
        {
            foreach(Board histboard in arrPlayer[gameActivePlayer].boardHistory)
            {
                histboard.printBoard();
            }
            arrPlayer[gameActivePlayer].addHistoryBoard(gameBoard);
            Console.WriteLine(arrPlayer[0].boardHistory.Length);
            //Console.Clear();
            printTitle();
            gameBoard.printBoard();
            Console.Write($"Press 8 to undo or 9 to redo");
            Console.WriteLine();
            Console.Write($"{arrPlayer[gameActivePlayer].playerName} Please Pick the You Want to place: S or O : ");
            while (controller)
            {
                pieceInput = Convert.ToString(Console.ReadKey().Key);
                if (pieceInput == "S" | pieceInput == "O")
                {
                    controller = false;
                }
                else if (pieceInput == "D8")
                {
                    undo(arrPlayer[gameActivePlayer]);
                }
                else if (pieceInput == "D9") 
                { 
                    redo(arrPlayer[gameActivePlayer]);
                }
                else
                {
                    Console.Clear();
                    printTitle();
                    gameBoard.printBoard();
                    Console.Write($"Wrong! please insert S or O : ");
                }
            }
            controller = true;

            while (controller)
            {
                arrPlayer[gameActivePlayer].addHistoryBoard(gameBoard);
                Console.WriteLine();
                Console.Write($"Plase enter a column you want to move: ");
                columnInput = Convert.ToString(Console.ReadLine()).ToUpper();
                Console.WriteLine();
                Console.Write($"Plase enter a row you want to move: ");
                rowInput = Convert.ToString(Console.ReadLine()).ToUpper();
                Console.WriteLine();
                activePiece = new Piece(Convert.ToString(pieceInput), player: arrPlayer[gameActivePlayer]);
                valMove = gameBoard.checkTile(rowInput, columnInput);
                if (valMove[0] == "true")
                {
                    controller = false;
                    gameBoard.addPieceToBoard(activePiece, rowInput, columnInput);
                    arrPlayer[gameActivePlayer].AddHistoryItemSlot(rowIncrease:1) ;
                    if (arrPlayer[gameActivePlayer].movementHistory.GetLength(1) < 3)
                    {
                        arrPlayer[gameActivePlayer].AddHistoryItemSlot(colIncrease: 4);
                    }
                    arrPlayer[gameActivePlayer].movementHistory[arrPlayer[gameActivePlayer].movementHistory.GetLength(0) - 1, 0]= columnInput;
                    arrPlayer[gameActivePlayer].movementHistory[arrPlayer[gameActivePlayer].movementHistory.GetLength(0) - 1, 1] = rowInput;
                    arrPlayer[gameActivePlayer].movementHistory[arrPlayer[gameActivePlayer].movementHistory.GetLength(0) - 1, 2] = pieceInput;
                    arrPlayer[gameActivePlayer].movementHistory[arrPlayer[gameActivePlayer].movementHistory.GetLength(0) - 1, 3] = "Active";
                    int.TryParse(valMove[1], out rowint);
                    int.TryParse(valMove[2], out colint);
                    currentPlayPoints = isPoint(rowint, colint, pieceInput);
                    gameScore[getNumber(arrPlayer[gameActivePlayer].playerName)]=gameScore[getNumber(arrPlayer[gameActivePlayer].playerName)] + currentPlayPoints[0,0];
                    for(int i=1;i<=currentPlayPoints[0,0];i++) 
                    {
                        gameBoard.getBoardPiece(currentPlayPoints[i,0],currentPlayPoints[i,1]).color= arrPlayer[gameActivePlayer].playerColor;
                        gameBoard.getBoardPiece(currentPlayPoints[i,2],currentPlayPoints[i,3]).color= arrPlayer[gameActivePlayer].playerColor;
                        gameBoard.getBoardPiece(rowint,colint).color = arrPlayer[gameActivePlayer].playerColor;
                    }
                    if (currentPlayPoints[0, 0] != 0)
                    {
// gameActivePlayer = changeplayer(gameActivePlayer);
                    }
                }
                else
                {
                    Console.Clear();
                    printTitle();
                    gameBoard.printBoard();
                    Console.WriteLine(valMove[0]);
                }
            }
            controller = true;
        }
    }

    protected internal int[,] isPoint(int x, int y,string shape)
    {
        // we Keep 0,0 for storing the total points of the play
        // points store picece 1 x, 2 store picece 1 y and 3 and 4 the same for piece 2 
        int[,] points = new int[9,4];
        int playPoints = 0;
        int maxXS;
        int minXS;
        int maxYS;
        int minYS;
        int maxXO;
        int minXO;
        int maxYO;
        int minYO;
        int maxRangeToCheckX;
        int minRangeToCheckX;
        int maxRangeToCheckY;
        int minRangeToCheckY;
        if (shape=="O")
        {
            maxRangeToCheckX = Math.Min(x + 1, gameBoard.width-1);
            minRangeToCheckX = Math.Max(x - 1,0);
            maxRangeToCheckY = Math.Min(y + 1,gameBoard.height-1);
            minRangeToCheckY = Math.Max(y - 1, 0);
            if(gameBoard.getShape(minRangeToCheckX, y) == gameBoard.getShape(maxRangeToCheckX, y) & (gameBoard.getShape(maxRangeToCheckX, y) == "S"))
            {
                // 0 store if its S /1 or not/ 1 store x, 2 store and 3 store the playID
                playPoints=playPoints+1;
                points[playPoints,0] = minRangeToCheckX;
                points[playPoints,1] = y;
                points[playPoints,2] = maxRangeToCheckX;
                points[playPoints,3] = y;
            }
            if(gameBoard.getShape(x, minRangeToCheckY) == gameBoard.getShape(x, maxRangeToCheckY) & (gameBoard.getShape(x, maxRangeToCheckY) == "S")){
                playPoints=playPoints+1;
                points[playPoints,0] = x;
                points[playPoints,1] = minRangeToCheckY;
                points[playPoints,2] = x;
                points[playPoints,3] = maxRangeToCheckY;
            }
            if(gameBoard.getShape(minRangeToCheckX, maxRangeToCheckY) == gameBoard.getShape(maxRangeToCheckX, minRangeToCheckY) & gameBoard.getShape(maxRangeToCheckX, minRangeToCheckY)=="S")
            {
                playPoints=playPoints+1;
                points[playPoints,0] = minRangeToCheckX;
                points[playPoints,1] = maxRangeToCheckY;
                points[playPoints,2] = maxRangeToCheckX;
                points[playPoints,3] = maxRangeToCheckY;
            };
            if(gameBoard.getShape(minRangeToCheckX, minRangeToCheckY) == gameBoard.getShape(maxRangeToCheckX, maxRangeToCheckY) & gameBoard.getShape(maxRangeToCheckX, maxRangeToCheckY) == "S")
            {
                playPoints=playPoints+1;
                points[playPoints,0] = minRangeToCheckX;
                points[playPoints,1] = minRangeToCheckY;
                points[playPoints,2] = maxRangeToCheckX;
                points[playPoints,3] = maxRangeToCheckY;
            };
        }
        else
        {
            maxXS = Math.Min(x + 2, gameBoard.width - 1);
            minXS = Math.Max(x - 2, 0);
            maxYS = Math.Min(y + 2, gameBoard.height - 1);
            minYS = Math.Max(y - 2, 0);
            maxXO = Math.Min(x + 1, gameBoard.width - 1);
            minXO = Math.Max(x - 1, 0);
            maxYO = Math.Min(y + 1, gameBoard.height - 1);
            minYO = Math.Max(y - 1, 0);

            if((gameBoard.getShape(x,maxYO)=="O")&(gameBoard.getShape(x,maxYS)=="S"))
            {
                playPoints=playPoints+1;
                points[playPoints,0] = x;
                points[playPoints,1] = maxYO;
                points[playPoints,2] = x;
                points[playPoints,3] = maxYS;
            }; // OK
            if((gameBoard.getShape(minXO,maxYO)=="O")&(gameBoard.getShape(minXS,maxYS)=="S"))
            {
                playPoints=playPoints+1;
                points[playPoints,0] = minXO;
                points[playPoints,1] = maxYO;
                points[playPoints,2] = minXS;
                points[playPoints,3] = maxYS;                
            }; // OK
            if((gameBoard.getShape(minXO,y)=="O")&(gameBoard.getShape(minXS,y)=="S"))
            {
                playPoints=playPoints+1;
                points[playPoints,0] = minXO;
                points[playPoints,1] = y;
                points[playPoints,2] = minXS;
                points[playPoints,3] = y;                 
            }
            if((gameBoard.getShape(maxXO,y)=="O")&(gameBoard.getShape(maxXS,y)=="S"))
            {
                playPoints=playPoints+1;
                points[playPoints,0] = maxXO;
                points[playPoints,1] = y;
                points[playPoints,2] = maxXS;
                points[playPoints,3] = y; 
            }
            if((gameBoard.getShape(minXO,minYO)=="O")&(gameBoard.getShape(minXS,minYS)=="S") & (x!= minXO) & (x!=minXS))
            {
                playPoints=playPoints+1;
                points[playPoints,0] = minXO;
                points[playPoints,1] = minYO;
                points[playPoints,2] = minXS;
                points[playPoints,3] = minYS; 
            }
            if ((gameBoard.getShape(x, minYO) == "O") & (gameBoard.getShape(x, minYS) == "S") & (y != minYO) & (y != minYS))
            {
                playPoints=playPoints+1;
                points[playPoints,0] = x;
                points[playPoints,1] = minYO;
                points[playPoints,2] = x;
                points[playPoints,3] = minYS; 
            }
        }
        points[0,0]=playPoints;
        return points;
    }
    protected override void selectBoardSize() {

    }
    protected internal void checkPoint(int x, int y)
        {

        }



}
