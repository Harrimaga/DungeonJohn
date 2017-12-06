using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public class Room : GameObjectList
{

    public string[,] roomarray;
    public void LoadTiles(int RoomListIndex)
    {
        List<string> textLines = new List<string>();
        StreamReader fileReader = new StreamReader("Content/Levels/1.txt"); /*+ RoomListIndex*/
        string line = fileReader.ReadLine();
        roomarray = new string[10, 8];

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
            case '.':
                return "Background";
            //case '!':
            //    return "Rock";
            //case '+':
            //    return "Wall";
            //case 'X':
            //    return "Exit";
            //case 'S':
            //    return "Start";
            //case '?':
            //    return "Unknown";
            default:
                return "N/A";
        }
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch, int a, int b)
    {
        for (int x = 0; x < 10; x++)
            for (int y = 0; y < 8; y++)
            {
                switch (roomarray[x, y])
                {
                    case "Background":
                        spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Standardtile.png")), new Vector2(x * 50 + a * 500, y * 50 + b * 400), Color.Blue);
                        break;
                    default:
                        spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Standardtile.png")), new Vector2(x * 50 + a * 500, y * 50 + b * 400), Color.Black);
                        break;
                }
            }
    }
}