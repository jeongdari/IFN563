using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePicker
{
    public class Help
    {
        public Dictionary<string, string> HowToPlay = new Dictionary<string, string>();
        public Dictionary<string, string> HowToWin = new Dictionary<string, string>();
        public Dictionary<string, string> ContactInfo = new Dictionary<string, string>();
        public int length = new int();

        public Help()
        {
            HowToPlay.Add("SOS", "Tow players take turns to add either an S or O on a board.");
            HowToWin.Add("SOS", "Once the grid has been filled up, the winner is the player who made the most SOSs.");
            HowToPlay.Add("Connect4Line", "Two players take turns dropping pieces on a board.");
            HowToWin.Add("Connect4Line", "The player forms an unbroken chain of four pieces wins the game");
            ContactInfo.Add("Producer", "Felipe Game, Inc.");
            ContactInfo.Add("Email", "felipealejandro.manzormanzor@connect.qut.edu.au");
            foreach (KeyValuePair<string, string> kvp in HowToPlay)
            {
                //textBox3.Text += ("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                Console.WriteLine($"Game Name {kvp.Key}: {kvp.Value}");
            }
            foreach (KeyValuePair<string, string> kvp in HowToWin)
            {
                //textBox3.Text += ("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                Console.WriteLine($"Game Name {kvp.Key}: {kvp.Value}");
            }
        }
    }
}
