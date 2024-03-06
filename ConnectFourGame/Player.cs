using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace GamePicker;
public class Player
{
    private static List<Player> allPlayers = new List<Player>();
    internal string playerColor;
    internal string playerName;
    /// x:movement,y=0 xmovement y=1 ymovement y=2 piece}
    public string[,] movementHistory;
    internal Board[] boardHistory; 
    
    

    public static List<Player> GetAllPlayers()
    {
        return allPlayers;
    }

    public static int GetTotalPlayerCount()
    {
        return allPlayers.Count + 1;
    }

    public void AddHistoryItemSlot(int rowIncrease=0, int colIncrease = 0)
    {
        int numRows = movementHistory.GetLength(0);
        int numCols = movementHistory.GetLength(1);
        string[,] newHistory = new string[numRows + rowIncrease, numCols + colIncrease];
        for (int row = 0; row < numRows; row++)
        {
            for (int col = 0; col < numCols; col++)
            {
                newHistory[row, col] = movementHistory[row, col];
            }
        }
        movementHistory = newHistory;
    }

    public void addHistoryBoard(Board newboard)
    {
        boardHistory = new Board[3];
        boardHistory[0]= new Board(width: 5, height: 5);
        boardHistory[1] = new Board(width: 6, height: 5);
        boardHistory[2] = newboard;
    }

    public ConsoleColor GetConsoleColor()
    {
        string colorName = playerColor;
        Dictionary<string, ConsoleColor> colorMappings = new Dictionary<string, ConsoleColor>
        {
            {"black", ConsoleColor.Black},
            {"darkblue", ConsoleColor.DarkBlue},
            {"darkgreen", ConsoleColor.DarkGreen},
            {"darkcyan", ConsoleColor.DarkCyan},
            {"darkred", ConsoleColor.DarkRed},
            {"darkmagenta", ConsoleColor.DarkMagenta},
            {"darkyellow", ConsoleColor.DarkYellow},
            {"gray", ConsoleColor.Gray},
            {"darkgray", ConsoleColor.DarkGray},
            {"blue", ConsoleColor.Blue},
            {"green", ConsoleColor.Green},
            {"cyan", ConsoleColor.Cyan},
            {"red", ConsoleColor.Red},
            {"magenta", ConsoleColor.Magenta},
            {"yellow", ConsoleColor.Yellow},
            {"white", ConsoleColor.White}
        };
        if (colorMappings.ContainsKey(colorName))
        {
            return colorMappings[colorName];
        }
        else
        {
            return ConsoleColor.Gray; // Default value for an invalid color name
        }
    }



    public Player(string playeName = "P", Board[] boardHistory = null, string[,] movementHistory=null, string playerColor = "gray"){
        if (playeName == "P"){
            this.playerName=playeName + GetTotalPlayerCount();
        } else {
            this.playerName=playeName;
        }
        this.boardHistory = boardHistory;
        if (this.boardHistory == null)
        {
            this.boardHistory = new Board[0];
        }
        this.movementHistory = movementHistory ?? new string[0,0];
        this.playerColor = playerColor;
        allPlayers.Add(this);
    }
}
