using System.Net;
using System.Reflection.Metadata;

namespace GamePicker;
public class Piece
{
    public string shape { get; internal set; }
    public string status { get; internal set; }
    public string color { get; internal set; }
    public Player player { get; internal set; }

    public ConsoleColor GetConsoleColor()
    {
        string colorName=color;
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

    public void changeColor(){

    }

    public Piece(string shape=null,Player player=null, string color = "gray", string status="Active"){
        this.shape=shape;
        this.color=color; 
        this.status= status;
        this.player = player;
    }
}
