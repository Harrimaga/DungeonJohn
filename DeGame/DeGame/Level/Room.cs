using System.Collections.Generic;
using System.IO;

class Room : GameObject
    {
    public void LoadTiles(string path)
    {
        List<string> textLines = new List<string>();
        StreamReader fileReader = new StreamReader(path);
        string line = fileReader.ReadLine();
        string[,] roomarray = new string[10, 8];

        //int CellWidth = 50;
        //int CellHeight = 50;
        //int levelwidth = line.Length * CellWidth;
        //int levelheight = textLines.Count * CellHeight;


        while (line != null)
        {
            textLines.Add(line);
            line = fileReader.ReadLine();
        }

        for (int x = 0; x < line.Length; ++x)
        {
            for (int y = 0; y < textLines.Count; ++y)
            {
                roomarray[x, y] = AssignType(textLines[y][x], x, y);
            }
        }
    }

    private string AssignType(char tileType, int x, int y)
    {
        switch (tileType)
        {
            case '?':
                return "Blank";
            case '.':
                return "Normal";
            case '!':
                return "Rock";
            case '+':
                return "Wall";
            case 'X':
                return "End";
            case 'S':
                return "Start";
            default:
                return "Unkown"; 
        }
    }
}

