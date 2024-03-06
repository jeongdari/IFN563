using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace GamePicker;
public class Board
{
    internal int[,] boardCoordinates;
    internal Piece[,] boardElements;
    public int width;
    public int height;
    private int MAXSIZE = 30;
    string[] ABC = new string[26]{
        "A", "B", "C", "D", "E", "F", "G", "H", "I", "J",
        "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T",
        "U", "V", "W", "X", "Y", "Z"};

    public Piece getBoardPiece(int x, int y)
    {
        return boardElements[x,y];
    }

    // check the type is the array ABC or from 0-MAXSIZE
    private bool inputValidation(string x)
    {
        int cx;
        bool parse = int.TryParse(x, out cx);
        if (parse)
        {
            if (cx <= MAXSIZE - 1 & cx >= 0)
            {
                return true;
            }else
            {
                return false;
            }
        } else {
            cx = Array.IndexOf(ABC, x);
            if (cx != -1 & cx<width)
            {
                return true;
            }
            return false; 
        }
    }
    // transform and find text in the array and return int type
    private int[] transformValidInput(string x,string y)
    {
        int cx;
        int cy;
        bool parsex = int.TryParse(x, out cx);
        bool parsey = int.TryParse(y, out cy);
        if (Array.IndexOf(ABC, x) != -1)
        {
            cx = Array.IndexOf(ABC, x);
        }

        if (Array.IndexOf(ABC, y) != -1)
        {
            cy = Array.IndexOf(ABC, y);
        }

        return new int[] { cx, cy };
    }
    // return a string that contains true if the tile is free details if not or explain the error
    public string[] checkTile(string x, string y)
    {
        string[] result = new string[3];
        if (inputValidation(x) & inputValidation(y)) {
            int[] input = transformValidInput(x, y);
            int cx = input[0];
            int cy = input[1];
            if (boardElements[cx, cy] == null)
            {
                result[0]="true";
                result[1] = Convert.ToString(cx);
                result[2] = Convert.ToString(cy);
                return result;
            }
            else
            {
                result[0] = $"tile is occupied by {boardElements[cx, cy].player.playerName} (1) with {boardElements[cx, cy].shape} (2)";
                result[1] = Convert.ToString(boardElements[cx, cy].shape);
                result[2] = boardElements[cx, cy].player.playerName;
                return result;
            }
        }
        else
        {
            result[0] = "invalid Input x (1),y (2)";
            result[1] = x;
            result[2] = y;
            return result;
        }
    }

    public string getShape(int x, int y)
    {
        if (boardElements[x, y] != null)
        {
            return boardElements[x, y].shape;
        }
        else
        {
            return "";
        }
    }
    public bool addPieceToBoard(Piece piece, string x,string y)
    {
        int[] input;
        if (inputValidation(x) & inputValidation(y))
        {
            input = transformValidInput(x,y);
            int cx = input[0];
            int cy = input[1];
            if (boardElements[cx, cy] == null)
            {
                boardElements[cx, cy] = piece;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }


    }

    public bool overrideToBoard(Piece piece, int x, int y)
    {
        boardElements[x, y] = piece;
        return true;
    }

    public int[,] createSquareBoard()
    {
        int[,] board =new int[width,height]; 
            for (int i=0; i<width;i++){
                for(int k=0; k<height;k++){
                board[i,k]=1;
                }
            }
        return board;
    }

    internal void printBoard()
    {
    string LUC2 = "┌";
    string LLC2 = "└";
    string RLC2 = "┘";
    string SL2 = "─";
    string RUC2 = "┐";
    string SHL2 = "|";
    string colorName;

    int printrow=0;
    int printcol=0;
    int auxprintTop=0;
    int auxprintBot=0;
    string auxprintShape = " ";



    for (int i=0; i < width;i++)
    {
        Console.Write($"   {ABC[i]} ");
    }
    Console.WriteLine();
    for (int row = 0; row < height*3; row++)
    {
        for (int col = 0; col < width; col++)
        {
            if (row % 3 == 0)
            {
                if (auxprintTop==0){
                    Console.Write(" ");
                    auxprintTop=1;
                }
                Console.Write($"{LUC2}{SL2}{SL2}{SL2}{RUC2}");
            }
            else if ((row+1)%3 == 0)
            {
                if (auxprintBot==0){
                    Console.Write(" ");
                    auxprintBot=1;
                }
                Console.Write($"{LLC2}{SL2}{SL2}{SL2}{RLC2}");
            }
            else
            {
                if (printcol==0 ){
                    Console.Write($"{printrow}");
                }
                    Console.WriteLine(boardElements.GetLength(0));
                    Console.WriteLine(boardElements.GetLength(1));
                    if (boardElements.GetLength(0)>=printrow-1 & boardElements.GetLength(1) >= printcol-1)
                    {
                        if (boardElements[printrow, printcol] == null)
                        {
                            auxprintShape = " ";
                        }
                    }
                    else
                    {
                        auxprintShape = boardElements[printrow, printcol].shape;
                        if (boardElements[printrow, printcol].shape == null)
                        {
                            auxprintShape = " ";
                        }
                        if (auxprintShape == "")
                        {
                            auxprintShape = " ";
                        }
                    }
                Console.Write($"{SHL2} ");
                if(boardElements[printrow,printcol]!=null){
                        if (boardElements[printrow, printcol].color != null)
                        {
                            Console.ForegroundColor = boardElements[printrow,printcol].GetConsoleColor();
                        }
                }
                Console.Write($"{auxprintShape}");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write($" {SHL2}");
                printcol=printcol+1;
                if (printcol==width){
                    printrow=printrow+1;
                    printcol=0;
                }
                auxprintTop=0;
                auxprintBot=0;
            }
        }
        Console.WriteLine(); // Add this line to move to the next row
    }
    }


    public Board(int width=3, int height=3, int[,] boardCoordinates=null, Piece[,] boardElements=null)
    {
        this.width = width;
        this.height = height;
        if(boardCoordinates!=null){
            this.width = boardCoordinates.GetLength(0);
            this.height = boardCoordinates.GetLength(1);
        }
        else{
            this.boardCoordinates=createSquareBoard();
            this.boardElements=boardElements;
        }
        if (boardElements != null)
        {
            this.boardElements=boardElements;
        }
        else
        {
            for (int i = 0; i < width - 1; i++)
            {
                for (int j = 0; j < height - 1; j++)
                {
                    boardElements = new Piece[width,height];
                    boardElements[i, j] = null;
                }
            }
            this.boardElements = boardElements;
        }
    }
    //From this line, thi part is for ConnectFour game. 
    public bool dropToken(int column, int playerSymbol)
    {
        for (int row = height - 1; row >= 0; row--)
        {
            if (grid[row, column] == 0)
            {
                grid[row, column] = playerSymbol;
                return true;
            }
        }
        return false; // Column is full
    }

    public bool checkWin(int playerSymbol)
    {
        // Check for a horizontal win
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col <= width - 4; col++)
            {
                if (grid[row, col] == playerSymbol && grid[row, col + 1] == playerSymbol &&
                    grid[row, col + 2] == playerSymbol && grid[row, col + 3] == playerSymbol)
                {
                    return true;
                }
            }
        }

        // Check for a vertical win
        for (int col = 0; col < width; col++)
        {
            for (int row = 0; row <= height - 4; row++)
            {
                if (grid[row, col] == playerSymbol && grid[row + 1, col] == playerSymbol &&
                    grid[row + 2, col] == playerSymbol && grid[row + 3, col] == playerSymbol)
                {
                    return true;
                }
            }
        }

        // Check for a diagonal win (both directions)
        for (int row = 0; row <= height - 4; row++)
        {
            for (int col = 0; col <= width - 4; col++)
            {
                if (grid[row, col] == playerSymbol && grid[row + 1, col + 1] == playerSymbol &&
                    grid[row + 2, col + 2] == playerSymbol && grid[row + 3, col + 3] == playerSymbol)
                {
                    return true;
                }

                if (grid[row, col + 3] == playerSymbol && grid[row + 1, col + 2] == playerSymbol &&
                    grid[row + 2, col + 1] == playerSymbol && grid[row + 3, col] == playerSymbol)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public bool isFull()
    {
        for (int col = 0; col < width; col++)
        {
            if (grid[0, col] == 0)
            {
                return false; // At least one column has an empty slot
            }
        }
        return true; // All columns are full
    }


}
