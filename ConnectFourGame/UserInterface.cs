using static System.Net.Mime.MediaTypeNames;

namespace GamePicker;
public class UserInterface
{
    string[] items = { "Play", "Load", "Game Rules", "Exit" };

    int WIDTH =80;
    Double BOTTONPROPORTION = 0.3;
    int SPACEBETWEENBOXES=1;

    int PARAMBOXHEIGH = 1;
    string MOVEUP = "W";
    string MOVEDOWN = "S";
    string SELECT = "Enter";
    string MOVEUP2 = "UpArrow";
    string MOVEDOWN2 = "DownArrow";
    string SELECT2 = "Space";
    string LUC1 = "╔";
    string LUC2 = "┌";
    string LLC1="╚";
    string LLC2="└";
    string RUC1 = "╗";
    string RUC2 = "┐";
    string RLC1 = "╝";
    string RLC2 = "┘";
    string SL1 = "═";
    string SL2 = "─";
    string SHL1 = "║";
    string POINTER = "●";
    string FILLING = " ";

    string JUMP = "\r\n";
    private void menuAction(string action)
    {
        string[] Playitems = { "SOS", "4Line", "Back" };
        string[] LoadSaveitems = Playitems;
        string[] RuleItems = Playitems;
        switch (action)
        {
            case("Play"):
                menuController(elements: Playitems,title: "PLAY GAME");
                break;
            case ("Load"):
                menuController(elements:LoadSaveitems, title: "LOAD GAME");
                break;
            case ("Game Rules"):
                menuController(elements:RuleItems, title: "RULES");
                break;
            case ("Exit"):
                Environment.Exit(0);
                break;
            case ("Back"):
                menuController(items);
                break;
        }
    }
    private void menuController(string[] elements, string selected = "",string key="",string title = "GAME CENTRAL") 
    { 
        int CurrentSelectedPos=0;
        int FuturePos=0;
        int PreviosPos=elements.Length-1;
        ConsoleKeyInfo key2;

        if (selected == "")
        {
            selected = elements[0];
            printMenu(elements,selected,title);
        }
        // Identify Selected Item
            for (int i=0;i<=elements.Length-1;i++)
            {
                if (elements[i]==selected)
                {
                    CurrentSelectedPos=i;
                    if (CurrentSelectedPos==elements.Length-1)
                    {
                        PreviosPos=CurrentSelectedPos-1;
                        FuturePos=0;
                        break;
                    }
                    else if (CurrentSelectedPos==0)
                    {
                        PreviosPos=elements.Length-1;
                        FuturePos=CurrentSelectedPos+1;
                        break;
                    }
                    else
                    {
                        PreviosPos=CurrentSelectedPos-1;
                        FuturePos=CurrentSelectedPos+1;
                        break;
                    }
                }
            }

        if (key == "")
        {
            key2 = Console.ReadKey();
            key = Convert.ToString(key2.Key);
            Console.WriteLine(key);
        }
        if ((MOVEDOWN==key) | (MOVEDOWN2==key))
        {
            System.Console.Clear();
            printMenu(elements, elements[FuturePos],title);
            key2 = Console.ReadKey();
            key = Convert.ToString(key2.Key);
            menuController(elements, elements[FuturePos], key, title);
        }
        else if ((MOVEUP==key) | (MOVEUP2 == key))
        {
            System.Console.Clear();
            printMenu(elements, elements[PreviosPos],title);
            key2 = Console.ReadKey();
            key = Convert.ToString(key2.Key);
            menuController(elements, elements[PreviosPos], key, title);
        }
        else if ((SELECT == key) | (SELECT2 == key))
        {
            System.Console.Clear();
            if ((title != "GAME CENTRAL") & (elements[CurrentSelectedPos]!="Back"))
            {
                menuAction(title + "-" + elements[CurrentSelectedPos]);
            }
            else
            {
                menuAction(elements[CurrentSelectedPos]);
            }
        }
        else 
        {
            printMenu(elements, elements[CurrentSelectedPos]);
            key2 = Console.ReadKey();
            key = Convert.ToString(key2.Key);
            menuController(elements, elements[PreviosPos], key, title);
        }

        Console.Write("RECHING HETER");
        
    }

    private void printMenu(string[] elements,string selected="",string title="GAME CENTRAL")
    {
        int BOXHEIGH=Math.Max(1+PARAMBOXHEIGH*2,3);
        int HIGH = elements.Length*(BOXHEIGH+SPACEBETWEENBOXES)+2*SPACEBETWEENBOXES;
        int boxSizeH = Convert.ToInt16(Math.Floor(WIDTH*BOTTONPROPORTION));
        int freeSpaceH = WIDTH-boxSizeH;
        int[] startElemPos = new int[elements.Length];
        int activeElem = 0;
        int aux=0;
        string InstruccionUP = "Plase Move up whit "+MOVEUP+" or "+MOVEUP2;
        string InstruccionDOWN = "Plase Move Down whit "+MOVEDOWN+" or "+MOVEDOWN2;
        string InstruccionSELECT = "Plase Select whit "+SELECT+" or "+SELECT2;
        string MenuInstructions = InstruccionUP+JUMP+InstruccionDOWN+JUMP+InstruccionSELECT;
        
        for(int i=0;i<startElemPos.Length;i++)
        {
            int startpos = SPACEBETWEENBOXES+2*SPACEBETWEENBOXES*i+BOXHEIGH*i;
            startElemPos[i]=startpos-i;
            //System.Console.Write(startElemPos[i]+",");
        }
        Console.WriteLine();
        for (int r=0; r<Math.Floor(Convert.ToDouble(WIDTH/2)-Convert.ToDouble(title.Length/2)+2); r++){
                Console.Write(FILLING);
            }
        Console.WriteLine(title);
        Console.WriteLine();

        //First line start
        string text = LUC1;
        for (int i=0; i<WIDTH; i++){
            text=text+SL1;
        }
        text=text+RUC1+JUMP;
        //End of first line

        //Content start
        for (int i=0;i<=HIGH-2;i++){

            // IF IS A ITEM MENU PLACE 
            if (i>= startElemPos[activeElem] & i < startElemPos[activeElem]+BOXHEIGH)
            {
                //first part of the element (just filling)
                text=text+SHL1;
                for (int r=0; r<Math.Floor(WIDTH*(1-BOTTONPROPORTION)/2); r++){
                    text=text+FILLING;
                }
                //Midle part of the first line 3 cases
                //If initial peace
                if (i==(startElemPos[activeElem]))
                {
                    text=text+LUC2;
                    for (int r=0; r<boxSizeH; r++){
                        text=text+SL2;
                    }
                text=text+RUC2;
                }
                // if is the last piece
                else if (i==(startElemPos[activeElem]+BOXHEIGH-1))
                {
                    text=text+LLC2;
                    for (int r=0; r<boxSizeH; r++)
                    {
                        text=text+SL2;
                    }
                    text=text+RLC2;
                }
                // if is the piece whit the name on it
                else if (i==(startElemPos[activeElem]+Math.Floor(Convert.ToDouble(BOXHEIGH/2)))) 
                {
                    for (int r=1; r<boxSizeH; r++)
                    {
                        if (r<=Math.Floor(Convert.ToDouble((boxSizeH-elements[activeElem].Length)/2)))
                        {
                            text=text+FILLING;
                        }
                        else if(r>Math.Floor(Convert.ToDouble(boxSizeH-(boxSizeH-elements[activeElem].Length-1)/2)))
                        {
                            text=text+FILLING;
                        }
                        else 
                        {
                            if (aux==0){
                                if (elements[activeElem]==selected)
                                {
                                    text = text + POINTER + FILLING ;
                                }
                                else 
                                {
                                    text =text +FILLING;
                                }
                            }
                            if (aux<=elements[activeElem].Length-1)
                            {
                            text=text+elements[activeElem][aux];
                                if (aux==elements[activeElem].Length-1 & elements[activeElem]==selected)
                                {
                                    text = text + FILLING + POINTER  ;
                                }
                                else if(aux==elements[activeElem].Length-1 & elements[activeElem]!=selected)
                                {
                                    text = text + FILLING+FILLING+FILLING;
                                }
                            }
                            aux = aux+1;
                        }
                    }
                }
                //End Part of the first line 
                for (int r=1; r<Math.Floor(WIDTH*(1-BOTTONPROPORTION)/2)-1; r++){
                    text=text+FILLING;
                }
                // Last Box Element Pass to next active Elemement 
                if ((i == (startElemPos[activeElem]+BOXHEIGH-1)) & (activeElem<startElemPos.Length-1)){
                    activeElem=activeElem+1;
                    aux=0;
                }

                //Universal filling at the end                 
                text=text+SHL1;
            }
            else
            {
                text=text +SHL1;
                for (int k=0; k<WIDTH; k++)
                {
                    text=text+FILLING;
                }
                text=text +SHL1;
            }
            text=text+JUMP;
        }
        //End of content 

        //Last Line 
        text = text + LLC1;
        for (int i=0; i<WIDTH; i++)
        {
            text=text+SL1;
        }
        text=text+RLC1;
        //End Last line
        System.Console.WriteLine(text);
        System.Console.WriteLine();
        System.Console.WriteLine (MenuInstructions);
    }

    public UserInterface(){
        menuController(items);
    }
}
