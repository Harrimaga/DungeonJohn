﻿using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public class Room : GameObjectList
{
    public int RoomListIndex;
    public bool updoor = false, downdoor = false, leftdoor = false, rightdoor = false, start = true;
    bool onup = false, ondown = false, onleft = false, onright = false;
    public GameObjectList enemies;
    public GameObjectList rocks;
    public List<GameObject> RemovedGameObjects;
    public bool start = true;
    public int a, b;
    public string[,] roomarray;
    int CellWidth, CellHeight, roomwidth, roomheight, roomarraywidth, roomarrayheight, counter;
    Vector2 Up, Down, Left, Right;

    public Room(int roomListIndex, int A, int B, int layer = 0, string id = "") : base(layer)
    {
        enemies = new GameObjectList();
        rocks = new GameObjectList();
        RemovedGameObjects = new List<GameObject>();
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

    void ControlCamera()
    {
        Vector2 Cam = Camera.Position;
        Vector2 MiddelofPlayer = new Vector2(PlayingState.player.position.X + GameEnvironment.assetManager.GetSprite("Sprites/Random").Width / 2, PlayingState.player.position.Y + GameEnvironment.assetManager.GetSprite("Sprites/Random").Height / 2);

        if (updoor && MiddelofPlayer.X >= Up.X && MiddelofPlayer.X <= Up.X + CellWidth)
            if (MiddelofPlayer.Y >= Up.Y && MiddelofPlayer.Y <= Up.Y + CellHeight)
            {
                counter = 0;
                onup = true;
                PlayingState.player.position -= new Vector2(0, 2 * CellHeight);
            }
        }

        if (rocks.Children != null)
        {
            foreach (Rock rock in rocks.Children)
            {
                rock.Update(gameTime);
            }
        }

        foreach (GameObject g in RemovedGameObjects)
        {
            if (g is Enemy)
            {
                enemies.Remove(g);
            }
            else if (g is Rock)
            {
                rocks.Remove(g);
            }
        }


        /*for (int x = 0; x < roomarraywidth; x++)
            for (int y = 0; y < roomarrayheight; y++)
            {
                counter = 0;
                ondown = true;
                PlayingState.player.position += new Vector2(0, 2 * CellHeight);
            }

        if (leftdoor && MiddelofPlayer.X >= Left.X && MiddelofPlayer.X <= Left.X + CellWidth)
            if (MiddelofPlayer.Y >= Left.Y && MiddelofPlayer.Y <= Left.Y + CellHeight)
            {
                counter = 0;
                onleft = true;
                PlayingState.player.position -= new Vector2(2 * CellHeight, 0);
            }

        if (rightdoor && MiddelofPlayer.X >= Right.X && MiddelofPlayer.X <= Right.X + CellWidth)
            if (MiddelofPlayer.Y >= Right.Y && MiddelofPlayer.Y <= Right.Y + CellHeight)
            {
                counter = 0;
                onright = true;
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
                            if (updoor)
                                spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Standardtile")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), null, Color.SaddleBrown, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            else
                                spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Wall Sprite")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), null, Color.Gray, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            break;
                        case "DownDoor":
                            if (downdoor)
                                spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Standardtile")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), null, Color.SaddleBrown, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            else
                                spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Wall Sprite")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), null, Color.Gray, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            break;
                        case "RightDoor":
                            if (rightdoor)
                                spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Standardtile")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), null, Color.SaddleBrown, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            else
                                spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Wall Sprite")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), null, Color.Gray, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            break;
                        case "LeftDoor":
                            if (leftdoor)
                                spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Standardtile")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), null, Color.SaddleBrown, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            else
                                spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Wall Sprite")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), null, Color.Gray, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
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