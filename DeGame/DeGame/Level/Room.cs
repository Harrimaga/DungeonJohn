using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public class Room : GameObjectList
{
    public Room(int layer = 0, string id = "") : base(layer, id)
    {

    }

    public string[,] roomarray;
    int CellWidth, CellHeight, roomwidth, roomheight;

    public void LoadTiles(int RoomListIndex)
    {
        List<string> textLines = new List<string>();
        StreamReader fileReader = new StreamReader("Content/Levels/" + RoomListIndex + ".txt");
        string line = fileReader.ReadLine();
        roomarray = new string[10, 8];

        CellWidth = 50;
        CellHeight = 50;
        roomwidth = line.Length * CellWidth;
        roomheight = textLines.Count * CellHeight;

        while (line != null)
        {
            textLines.Add(line);
            line = fileReader.ReadLine();
        }

        line = textLines[0];

        for (int x = 0; x < line.Length; ++x)
        {
            for (int y = 0; y < textLines.Count - 1; ++y)
            {
                roomarray[x, y] = AssignType(textLines[y][x]);
                System.Console.WriteLine(roomarray[x, y]);
            }
        }
    }

    private string AssignType(char filetext)
    {
        switch (filetext)
        {
            case '.':
                return "Background";
            case '!':
                return "Rock";
            case '+':
                return "Wall";
            case 'X':
                return "Exit";
            case 'S':
                return "Start";
            case '?':
                return "Unknown";
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
                        spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Standardtile")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), Color.Green);
                        break;
                    case "Rock":
                        spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Standardtile")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), Color.LightGray);
                        break;
                    case "Wall":
                        spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Standardtile")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), Color.Brown);
                        break;
                    case "Exit":
                        spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Standardtile")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), Color.DarkCyan);
                        break;
                    case "Start":
                        spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Standardtile")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), Color.Yellow);
                        break;
                    case "Unkown":
                        spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Standardtile")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), Color.DarkBlue);
                        break;
                    default:
                        spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Standardtile")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), Color.Red);
                        break;
                }
            }
    }
}