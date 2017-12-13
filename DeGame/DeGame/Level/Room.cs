using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public class Room : GameObjectList
{
    int RoomListIndex;
    public bool up = false, down = false, left = false, right = false;
    GameObjectList enemies;
    public Room(int roomListIndex, int layer = 0, string id = "") : base(layer)
    {
        enemies = new GameObjectList();
        RoomListIndex = roomListIndex;
    }

    public string[,] roomarray;
    public int CellWidth, CellHeight, roomwidth, roomheight;

    public void LoadTiles()
    {
        List<string> textLines = new List<string>();
        StreamReader fileReader = new StreamReader("Content/Levels/" + RoomListIndex + ".txt");
        string line = fileReader.ReadLine();
        roomarray = new string[16, 10];

        CellWidth = 50;
        CellHeight = 50;


        while (line != null)
        {
            textLines.Add(line);
            line = fileReader.ReadLine();
        }

        line = textLines[0];

        for (int x = 0; x < line.Length; ++x)
        {
            for (int y = 0; y < textLines.Count; ++y)
            {
                roomarray[x, y] = AssignType(textLines[y][x]);
                System.Console.WriteLine(roomarray[x, y]);
            }
        }
        roomwidth = line.Length * CellWidth;
        roomheight = textLines.Count * CellHeight;
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

            case '-':
                return "UpDoor";
            case '=':
                return "DownDoor";
            case '>':
                return "RightDoor";
            case '<':
                return "LeftDoor";

            case 'C':
                return "ChasingEnemy";
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
    public override void Update(GameTime gameTime)
    {
        enemies.Update(gameTime);
        for (int x = 0; x < 10; x++)
            for (int y = 0; y < 8; y++)
            {
                if (roomarray[x, y] == "UpDoor")
                {
                    //move camera up
                }
                else if (roomarray[x, y] == "DownDoor")
                {
                    //move camera down
                }
                else if (roomarray[x, y] == "RightDoor")
                {
                    //move camera right
                }
                else if (roomarray[x, y] == "LeftDoor")
                {
                    //move camera left
                }

            }
    }
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch, int a, int b)
    {
        enemies.Draw(gameTime, spriteBatch);
        for (int x = 0; x < 10; x++)
            for (int y = 0; y < 8; y++)
            {
                if (roomarray != null)
                {
                    switch (roomarray[x, y])
                    {
                        case "Background":
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Background Sprite")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), Color.Gray);
                            break;
                        case "Rock":
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Rock Sprite")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), Color.Gray);
                            break;
                        case "Wall":
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Wall Sprite")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), Color.Gray);
                            break;


                        case "UpDoor":
                            if (up)
                                spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Standardtile")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), Color.SaddleBrown);
                            else 
                                spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Standardtile")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), Color.RosyBrown);
                            break;
                        case "DownDoor":
                            if (down)
                                spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Standardtile")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), Color.SaddleBrown);
                            else
                                spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Standardtile")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), Color.RosyBrown);
                            break;
                        case "RightDoor":
                            if (right)
                                spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Standardtile")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), Color.SaddleBrown);
                            else
                                spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Standardtile")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), Color.RosyBrown);
                            break;
                        case "LeftDoor":
                            if (up)
                                spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Standardtile")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), Color.SaddleBrown);
                            else
                                spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Standardtile")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), Color.RosyBrown);
                            break;

                        case "ChasingEnemy":
                            Enemy enemy = new ChasingEnemy(new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight),0 ,"ChasingEnemy");
                            //enemies.Add(enemy);
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Standardtile")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), Color.Green);
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
}