using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public class Room : GameObjectList
{
    public int RoomListIndex;
    public bool up = false, down = false, left = false, right = false;
    public string[,] roomarray;
    int CellWidth, CellHeight, roomwidth, roomheight, roomarraywidth, roomarrayheight;

    public Room(int roomListIndex, int p, int q, int layer = 0, string id = "") : base(layer)
    {
        RoomListIndex = roomListIndex;
    }

    public void LoadTiles()
    {
        List<string> textLines = new List<string>();
        StreamReader fileReader = new StreamReader("Content/Levels/" + RoomListIndex + ".txt");
        string line = fileReader.ReadLine();

        CellWidth = 5;
        CellHeight = 5;

        while (line != null)
        {
            textLines.Add(line);
            line = fileReader.ReadLine();
        }
        line = textLines[0];
        roomarraywidth = line.Length;
        roomarrayheight = textLines.Count;
        roomwidth = line.Length * CellWidth;
        roomheight = textLines.Count * CellHeight;
        roomarray = new string[line.Length, textLines.Count];

        for (int x = 0; x < line.Length; ++x)        
            for (int y = 0; y < textLines.Count; ++y)
            {
                roomarray[x, y] = AssignType(textLines[y][x]);
                System.Console.WriteLine(roomarray[x, y]);
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
            case 'O':
                return "Pit";
            case 'I':
                return "Item";
            case 'X':
                return "Exit";
            case 'S':
                return "Start";
            default:
                return "N/A";
        }
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch, int a, int b)
    {
        for (int x = 0; x < roomarraywidth; x++)
            for (int y = 0; y < roomarrayheight; y++)            
                switch (roomarray[x, y])
                {
                    case "Background":
                        spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Standardtilemini")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), Color.Green);
                        break;
                    case "Rock":
                        spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Standardtilemini")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), Color.LightGray);
                        break;
                    case "Wall":
                        spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Standardtilemini")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), Color.Brown);
                        break;
                    case "Pit":
                        spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Standardtilemini")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), Color.DarkBlue);
                        break;
                    case "Item":
                        spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Standardtilemini")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), Color.Purple);
                        break;
                    case "Exit":
                        spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Standardtilemini")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), Color.Orange);
                        break;
                    case "Start":
                        spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Standardtilemini")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), Color.Yellow);
                        break;
                    default:
                        spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Standardtilemini")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), Color.Red);
                        break;
                }            
    }
}