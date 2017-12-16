using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public class Room : GameObjectList
{
    public int RoomListIndex;
    public bool up = false, down = false, left = false, right = false, onup = false, ondown = false, onleft = false, onright = false;
    public GameObjectList enemies;
    public bool start = true;
    public int a, b;
    public string[,] roomarray;
    int CellWidth, CellHeight, roomwidth, roomheight, roomarraywidth, roomarrayheight, counter;
    Vector2 Up, Down, Left, Right;

    public Room(int roomListIndex, int A, int B, int layer = 0, string id = "") : base(layer)
    {
        enemies = new GameObjectList();
        RoomListIndex = roomListIndex;
        a = A;
        b = B;
    }

    public void LoadTiles()
    {
        StreamReader fileReader = new StreamReader("Content/Levels/" + RoomListIndex + ".txt");
        string line = fileReader.ReadLine();
        List<string> textLines = new List<string>();

        CellWidth = GameEnvironment.assetManager.GetSprite("Sprites/Standardtile").Width;
        CellHeight = GameEnvironment.assetManager.GetSprite("Sprites/Standardtile").Height;

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
        for (int x = 0; x < roomarraywidth; ++x)
            for (int y = 0; y < roomarrayheight; ++y)
                AssignType(textLines[y][x], x, y);
        //System.Console.WriteLine(roomarray[x, y]);
    }       
    

    private void AssignType(char textlines,int x, int y)
    {
                switch (textlines)
                {
                    case '.':
                        roomarray[x,y] = "Background";
                        break;
                    case '!':
                        roomarray[x, y] = "Rock";
                        break;
                    case '+':
                        roomarray[x, y] = "Wall";
                        break;

                    case '-':
                        roomarray[x, y] = "UpDoor";
                        Up = new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight);
                        break;
                    case '=':
                        roomarray[x, y] = "DownDoor";
                        Down = new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight);
                        break;
                    case '<':
                        roomarray[x, y] = "LeftDoor";
                        Left = new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight);
                        break;
                    case '>':
                        roomarray[x, y] = "RightDoor";
                        Right = new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight);
                        break;

                    case 'C':
                        roomarray[x, y] = "ChasingEnemy";
                        break;
                    case 'O':
                        roomarray[x, y] = "Pit";
                        break;
                    case 'I':
                        roomarray[x, y] = "Item";
                        break;
                    case 'E':
                        roomarray[x, y] = "Exit";
                        break;
                    case 'S':
                        roomarray[x, y] = "Start";
                        break;
                    default:
                        roomarray[x, y] = "N/A";
                        break;
                }
    }
    void OnLoad()
    {
        //CreateEnemy();
    }
    void CreateEnemy(int x, int y, string TypeEnemy)
    {
            if (TypeEnemy == "C")
            {
                Enemy enemy = new ChasingEnemy(new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), 0, "ChasingEnemy");
                enemies.Add(enemy);
            }

            if (TypeEnemy == "R")
            {
                Enemy enemy = new RangedEnemy(new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), 0, "RangedEnemy");
                enemies.Add(enemy);
            }
    }

    void ControlCamera()
    {
        Vector2 Cam = Camera.Position;

        if (PlayingState.player.position.X >= Up.X && PlayingState.player.position.X <= Up.X + CellWidth)
            if (PlayingState.player.position.Y >= Up.Y && PlayingState.player.position.Y <= Up.Y + CellHeight)
            {
                counter = 0;
                onup = true;
                PlayingState.player.position -= new Vector2(0, 2 * CellHeight);
            }

        if (PlayingState.player.position.X >= Down.X && PlayingState.player.position.X <= Down.X + CellWidth)
            if (PlayingState.player.position.Y >= Down.Y && PlayingState.player.position.Y <= Down.Y + CellHeight)
            {
                counter = 0;
                ondown = true;
                PlayingState.player.position += new Vector2(0, 2 * CellHeight);
            }

        if (PlayingState.player.position.X >= Left.X && PlayingState.player.position.X <= Left.X + CellWidth)
            if (PlayingState.player.position.Y >= Left.Y && PlayingState.player.position.Y <= Left.Y + CellHeight)
            {
                counter = 0;        //ligt aan deze
                onleft = true;      //ligt aan deze
                PlayingState.player.position -= new Vector2(2 * CellHeight, 0);
            }

        if (PlayingState.player.position.X >= Right.X && PlayingState.player.position.X <= Right.X + CellWidth)
            if (PlayingState.player.position.Y >= Right.Y && PlayingState.player.position.Y <= Right.Y + CellHeight)
            {
                counter = 0;        //ligt aan deze
                onright = true;     //ligt aan deze
                PlayingState.player.position += new Vector2(2 * CellHeight, 0);
            }
        start = false;

        if (Camera.Position.Y > Cam.Y - roomheight && onup == true && counter < 30)
        {
            Camera.Position -= new Vector2(0, roomheight / 30);
            counter++;
        }
        if (Camera.Position.Y < Cam.Y + roomheight && ondown == true && counter < 30)
        {
            Camera.Position += new Vector2(0, roomheight / 30);
            counter++;
        }
        if (Camera.Position.X > Cam.X - roomwidth && onleft == true && counter < 30)
        {
            Camera.Position -= new Vector2(roomwidth / 30, 0);
            counter++;
        }
        if (Camera.Position.Y < Cam.X + roomwidth && onright == true && counter < 30)
        {
            Camera.Position += new Vector2(roomwidth / 30, 0);
            counter++;
        }

        if (counter >= 30)
        {
            onup = false;
            ondown = false;
            onleft = false;
            onright = false;
        }
    }

    public void Update(GameTime gameTime)
    {
        if (enemies.Children != null)
            foreach (Enemy enemy in enemies.Children)
                enemies.Update(gameTime);

        if (start) { OnLoad(); }
        enemies.Update(gameTime);
        ControlCamera();
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
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Rock Sprite")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), null, Color.Gray, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
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
    }    
}