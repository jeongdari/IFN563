using System;
using System.IO;
using System.Text;

namespace BoardGamesFramework
{
    public class SaveFile
    {
        public void Save(Board board, string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                int size = board.Size;
                for (int row = 0; row < size; row++)
                {
                    for (int col = 0; col < size; col++)
                    {
                        char symbol = board.GetSymbol(row, col);
                        writer.Write(symbol);
                        if (col < size - 1)
                        {
                            writer.Write(",");
                        }
                    }
                    writer.WriteLine();
                }
            }
        }

        public static Board LoadGame(string filename)
        {
            using (FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read))
            using (StreamReader reader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string dimensionsLine = reader.ReadLine();
                string[] dimensions = dimensionsLine.Split(',');
                int rows = int.Parse(dimensions[0]);
                int columns = int.Parse(dimensions[1]);

                Board board = new Board(rows); // Initialize the board with the specified rows
                for (int row = 0; row < rows; row++)
                {
                    string line = reader.ReadLine();
                    for (int col = 0; col < columns; col++)
                    {
                        board.SetPieceAt(row, col, line[col]);
                    }
                }

                return board;
            }
        }
    }
}
