using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public class Room : GameObjectList
{
    public int RoomListIndex;
    public bool up = false, down = false, left = false, right = false;
    public GameObjectList enemies;
    public GameObjectList rocks;
    public bool start = true;
    public int a, b;
    public string[,] roomarray;
    int CellWidth, CellHeight, roomwidth, roomheight, roomarraywidth, roomarrayheight;

    public Room(int roomListIndex, int A, int B, int layer = 0, string id = "") : base(layer)
    {
        enemies = new GameObjectList();
        rocks = new GameObjectList();
        RoomListIndex = roomListIndex;
        a = A;
        b = B;
    }

    public void LoadTiles()
    {
        List<string> textLines = new List<string>();
        StreamReader fileReader = new StreamReader("Content/Levels/" + RoomListIndex + ".txt");
        string line = fileReader.ReadLine();

        CellWidth = 60;
        CellHeight = 60;

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
        roomarray = new string[roomarraywidth, roomarrayheight];

        for (int x = 0; x < line.Length; ++x)        
            for (int y = 0; y < textLines.Count; ++y)
            {
                roomarray[x, y] = AssignType(textLines[y][x], x , y);
            }        
    }

    private string AssignType(char filetext, int x, int y)
    {
        switch (filetext)
        {
            case '.':
                return "Background";
            case '!':
                CreateGameObject(x, y, "!");
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

            case 'R':
                CreateGameObject(x, y, "R");
                return "RangedEnemy";
            case 'C':
                CreateGameObject(x,y,"C");
                return "ChasingEnemy";
            case 'O':
                return "Pit";
            case 'I':
                return "Item";
            case 'E':
                return "Exit";
            case 'S':
                return "Start";
            default:
                return "N/A";
        }
    }
    void OnLoad()
    {
        //CreateEnemy();
    }
    void CreateGameObject(int x,int y, string Type)
    {
            if (Type == "C")
            {
                Enemy enemy = new ChasingEnemy(new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), 0, "ChasingEnemy");
                enemies.Add(enemy);
            }

            if (Type == "R")
            {
                Enemy enemy = new RangedEnemy(new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), 0, "RangedEnemy");
                enemies.Add(enemy);
            }

            if (Type == "!")
            {
                Rock rock = new Rock(new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), 0, "Rock");
                rocks.Add(rock);
            }
    }
 
    public override void Update(GameTime gameTime)
    {
        //if (start) {OnLoad();}
        if (enemies.Children != null)
        {
            foreach (Enemy enemy in enemies.Children)
            {
                enemies.Update(gameTime);
            }
        }

        if (rocks.Children != null)
        {
            foreach (Rock rock in rocks.Children)
            {
                rock.Update(gameTime);
            }
        }
        /*for (int x = 0; x < roomarraywidth; x++)
            for (int y = 0; y < roomarrayheight; y++)
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
        start = false;*/
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        for (int x = 0; x < roomarray.GetLength(0); x++)
            for (int y = 0; y < roomarray.GetLength(1); y++)
                if (roomarray != null)
                    switch (roomarray[x, y])
                    {
                        case "Background":
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Background Sprite")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), null, Color.Gray, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            break;
                        case "Rock":
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Background Sprite")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), null, Color.Gray, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            break;
                        case "Wall":
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Wall Sprite")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), null, Color.Gray, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            break;
                        case "Pit":
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/PitTile")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            break;
                        case "Item":
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/ItemTile")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), null, Color.Gray, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            break;
                        case "Exit":
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/EndTile")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            break;
                        case "Start":
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/StartTile")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight - 120), null, Color.Gray, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            //System.Console.WriteLine(new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight).ToString());
                            PlayingState.currentFloor.startPlayerPosition = new Vector2(x * CellWidth + a * roomwidth + CellWidth / 2, y * CellHeight + b * roomheight + CellHeight / 2);
                            //Camera.Position = new Vector2(x * CellWidth + a * roomwidth + CellWidth / 2, y * CellHeight + b * roomheight + CellHeight / 2);
                            break;

                        case "UpDoor":
                            if (up)
                                spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Standardtile")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), null, Color.SaddleBrown, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            else
                                spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Standardtile")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), null, Color.RosyBrown, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            break;
                        case "DownDoor":
                            if (down)
                                spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Standardtile")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), null, Color.SaddleBrown, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            else
                                spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Standardtile")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), null, Color.RosyBrown, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            break;
                        case "RightDoor":
                            if (right)
                                spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Standardtile")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), null, Color.SaddleBrown, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            else
                                spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Standardtile")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), null, Color.RosyBrown, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            break;
                        case "LeftDoor":
                            if (up)
                                spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Standardtile")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), null, Color.SaddleBrown, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            else
                                spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Standardtile")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), null, Color.RosyBrown, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            break;

                        case "RangedEnemy":
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Background Sprite")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), Color.Green);
                            break;
                        case "ChasingEnemy":
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Background Sprite")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), Color.Green);
                            break;
                        default:
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Standardtile")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), Color.Red);
                            break;
                    }            
        foreach (Enemy enemy in enemies.Children)
        {
            enemy.Draw(gameTime, spriteBatch);
        }
        foreach (Rock rock in rocks.Children)
        {
            rock.Draw(gameTime, spriteBatch);
        }
    }    
}