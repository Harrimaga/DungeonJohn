using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

public class Room : GameObjectList
{

    public bool updoor = false, downdoor = false, leftdoor = false, rightdoor = false, Visited = false;
    public int RoomListIndex, a, b, CellWidth, CellHeight, roomwidth, roomheight, enemycounter = 0;
    public static GameObjectList enemies, solid, door, consumable, bosses;
    public Vector2 Up, Down, Left, Right, Exit;
    Vector2 TilePosition;
    int roomarraywidth, roomarrayheight;
    Random random = new Random();
    public string[,] roomarray;

    public Room(int roomListIndex, int A, int B, int layer = 0, string id = "") : base(layer)
    {
        consumable = new GameObjectList();
        enemies = new GameObjectList();
        bosses = new GameObjectList();
        solid = new GameObjectList();
        door = new GameObjectList();
        RoomListIndex = roomListIndex;
        a = A;
        b = B;
        position = new Vector2(a, b);
    }

    public void LoadTiles()
    {
        StreamReader fileReader = new StreamReader("Content/Levels/" + RoomListIndex + ".txt");
        string line = fileReader.ReadLine();
        List<string> textLines = new List<string>();
        while (line != null)
        {
            textLines.Add(line);
            line = fileReader.ReadLine();
        }
        line = textLines[0];

        roomarraywidth = line.Length;
        roomarrayheight = textLines.Count;
        CellWidth = GameEnvironment.assetManager.GetSprite("Sprites/Standardtile").Width;
        CellHeight = GameEnvironment.assetManager.GetSprite("Sprites/Standardtile").Height;
        roomwidth = line.Length * CellWidth;
        roomheight = textLines.Count * CellHeight;
        roomarray = new string[roomarraywidth, roomarrayheight];

        for (int x = 0; x < roomarraywidth; ++x)
            for (int y = 0; y < roomarrayheight; ++y)
                AssignType(textLines[y][x], x, y);
    }

    private void AssignType(char textlines, int x, int y)
    {
        switch (textlines)
        {
            case '.':
                roomarray[x, y] = "Background";
                break;
            case '!':
                roomarray[x, y] = "Rock";
                CreateObject(x, y, "!");
                break;
            case '+':
                roomarray[x, y] = "Wall";
                CreateObject(x, y, "+");
                break;
            case '-':
                roomarray[x, y] = "UpDoor";
                Up = new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight);
                CreateObject(x, y, "-");
                break;
            case '=':
                roomarray[x, y] = "DownDoor";
                Down = new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight);
                CreateObject(x, y, "=");
                break;
            case '<':
                roomarray[x, y] = "LeftDoor";
                Left = new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight);
                CreateObject(x, y, "<");
                break;
            case '>':
                roomarray[x, y] = "RightDoor";
                Right = new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight);
                CreateObject(x, y, ">");
                break;
            case 'C':
                roomarray[x, y] = "ChasingEnemy";
                CreateObject(x, y, "C");
                break;
            case 'R':
                roomarray[x, y] = "RangedEnemy";
                CreateObject(x, y, "R");
                break;
            case 'B':
                roomarray[x, y] = "Boss";
                CreateObject(x, y, "B");
                break;
            case 'O':
                roomarray[x, y] = "Pit";
                break;
            case 'I':
                roomarray[x, y] = "Item";
                break;
            case 'E':
                roomarray[x, y] = "Exit";
                Exit = new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight);
                break;
            case 'S':
                roomarray[x, y] = "Start";
                break;
            default:
                roomarray[x, y] = "N/A";
                break;
        }
    }

    public void DropConsumable(Vector2 position)
    {
        int r = random.Next(100);
        if (r > 50)
        {
            Consumables golddrop = new Consumables(position, "gold");
            consumable.Add(golddrop);
        }
        else
        {
            Consumables healthdrop = new Consumables(position, "heart");
            consumable.Add(healthdrop);
        }
    }

    public void Update(GameTime gameTime)
    {
        if (PlayingState.currentFloor.currentRoom.position == new Vector2(a, b))
        {
            Visited = true;
            enemies.Update(gameTime);
            door.Update(gameTime);
            consumable.Update(gameTime);
            bosses.Update(gameTime);
            CheckExit();
        }
        solid.Update(gameTime);
    }

    public void CreateObject(int x, int y, string Type)
    {
        //Enemy enemyChase;
        //Enemy enemyRanged;
        //Enemy enemy;
        switch (Type)
        {
            case ("C"):
                Enemy enemyChase = new ChasingEnemy(new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), new Vector2(a,b), 0, "ChasingEnemy");
                enemies.Add(enemyChase);
                roomarray[x, y] = "Background";
                enemycounter++;
                break;
            case ("R"):
                Enemy enemyRanged = new RangedEnemy(new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), new Vector2(a, b), 0, "RangedEnemy");
                enemies.Add(enemyRanged);
                roomarray[x, y] = "Background";
                enemycounter++;
                break;
            case ("B"):
                Boss1 boss = new Boss1(new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), new Vector2(a,b), 0, "Boss");
                bosses.Add(boss);
                enemycounter++;
                break;
            case ("!"):
                Solid rock = new Rock(new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), 0, "Rock");
                solid.Add(rock);
                roomarray[x, y] = "Background";
                break;
            case ("+"):
                Solid wall = new Wall(new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight), 0, "Wall");
                solid.Add(wall);
                break;
            case ("-"):
                    Door up = new Door(updoor, Up, 1);
                    door.Add(up);                
                break;
            case ("="):                
                    Door down = new Door(downdoor, Down, 2);
                    door.Add(down);                
                break;
            case ("<"):
                    Door left = new Door(leftdoor, Left, 3);
                    door.Add(left);                
                break;
            case (">"):
                    Door right = new Door(rightdoor, Right, 4);
                    door.Add(right);                
                break;
        }
    }

    public void CheckExit()
    {
        if (RoomListIndex == 2)
        {
            Vector2 MiddleofPlayer = new Vector2(PlayingState.player.position.X + GameEnvironment.assetManager.GetSprite("Sprites/Random").Width / 2, PlayingState.player.position.Y + GameEnvironment.assetManager.GetSprite("Sprites/Random").Height / 2);
            if (MiddleofPlayer.X >= Exit.X && MiddleofPlayer.X <= Exit.X + CellWidth)
                if (MiddleofPlayer.Y >= Exit.Y && MiddleofPlayer.Y <= Exit.Y + CellHeight)
                    PlayingState.currentFloor.NextFloor();
        }
    }            

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        for (int x = 0; x < roomarray.GetLength(0); x++)
            for (int y = 0; y < roomarray.GetLength(1); y++)
            {
                TilePosition = new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight);
                if (roomarray != null && PlayingState.currentFloor.currentRoom.position == position)
                {
                    if (roomarray[x, y] == "Wall")
                    {
                        if (x > 0 && roomarray[x - 1, y] == "Background")
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Wall Sprite Right2")), TilePosition, Color.Gray);
                        else if (x < roomarray.GetLength(0) - 1 && roomarray[x + 1, y] == "Background")
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Wall Sprite Left2")), TilePosition, Color.Gray);
                        else if (y > 0 && roomarray[x, y - 1] == "Background")
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Wall Sprite Down2")), TilePosition, Color.Gray);
                        else if (y < roomarray.GetLength(1) - 1 && roomarray[x, y + 1] == "Background")
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Wall Sprite Up2")), TilePosition, Color.Gray);
                        else if (y > 0 && x > 0 && roomarray[x - 1, y - 1] == "Background")
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Wall Sprite Corner RD")), TilePosition, Color.Gray);
                        else if (y < roomarray.GetLength(1) - 1 && x > 0 && roomarray[x - 1, y + 1] == "Background")
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Wall Sprite Corner RU")), TilePosition, Color.Gray);
                        else if (y > 0 && x < roomarray.GetLength(0) - 1 && roomarray[x + 1, y - 1] == "Background")
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Wall Sprite Corner LD")), TilePosition, Color.Gray);
                        else if (y < roomarray.GetLength(1) - 1 && x < roomarray.GetLength(0) && roomarray[x + 1, y + 1] == "Background")
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Wall Sprite Corner LU")), TilePosition, Color.Gray);
                    }

                    else if (roomarray[x, y] == "Background")
                    {
                        if (y > 0 && x > 0 && roomarray[x - 1, y] == "Wall" && roomarray[x, y - 1] == "Wall")
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Background Sprite LU")), TilePosition, Color.Gray);
                        else if (y < roomarray.GetLength(1) && x > 0 && roomarray[x - 1, y] == "Wall" && roomarray[x, y + 1] == "Wall")
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Background Sprite LD")), TilePosition, Color.Gray);
                        else if (y > 0 && x < roomarray.GetLength(0) && roomarray[x + 1, y] == "Wall" && roomarray[x, y - 1] == "Wall")
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Background Sprite RU")), TilePosition, Color.Gray);
                        else if (y < roomarray.GetLength(1) && x < roomarray.GetLength(0) && roomarray[x + 1, y] == "Wall" && roomarray[x, y + 1] == "Wall")
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Background Sprite RD")), TilePosition, Color.Gray);
                        else if (x > 0 && roomarray[x - 1, y] == "Wall" || roomarray[x - 1, y] == "LeftDoor")
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Background Sprite Left")), TilePosition, Color.Gray);
                        else if (x < roomarray.GetLength(0) && roomarray[x + 1, y] == "Wall" || roomarray[x + 1, y] == "RightDoor")
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Background Sprite Right")), TilePosition, Color.Gray);
                        else if (y > 0 && roomarray[x, y - 1] == "Wall" || roomarray[x, y - 1] == "UpDoor")
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Background Sprite Up")), TilePosition, Color.Gray);
                        else if (y < roomarray.GetLength(1) && roomarray[x, y + 1] == "Wall" || roomarray[x, y + 1] == "DownDoor")
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Background Sprite Down")), TilePosition, Color.Gray);
                        else if (roomarray[x, y] == "Background")
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Background Sprite")), TilePosition, Color.Gray);
                    }

                    else
                        switch (roomarray[x, y])
                        {
                            case "Background":
                            case "Wall":
                                break;
                            case "Rock":
                                spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Background Sprite")), TilePosition, Color.Gray);
                                break;
                            case "Pit":
                                spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/PitTile")), TilePosition, Color.White);
                                break;
                            case "Item":
                                spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/ItemTile")), TilePosition, Color.Gray);
                                break;
                            case "Exit":
                                spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/EndTile")), TilePosition, Color.White);
                                break;
                            case "Start":
                                spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/StartTile")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight - 120), Color.Gray);
                                //System.Console.WriteLine(TilePosition.ToString());
                                PlayingState.currentFloor.startPlayerPosition = new Vector2(x * CellWidth + a * roomwidth + CellWidth / 2, y * CellHeight + b * roomheight + CellHeight / 2);
                                //Camera.Position = new Vector2(x * CellWidth + a * roomwidth + CellWidth / 2, y * CellHeight + b * roomheight + CellHeight / 2);
                                break;
                            case "Boss":
                                spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Background Sprite")), TilePosition, Color.Gray);
                                break;
                            case "RangedEnemy":
                                spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Background Sprite")), TilePosition, Color.Gray);
                                break;
                            case "ChasingEnemy":
                                spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Background Sprite")), TilePosition, Color.Gray);
                                break;
                            default:
                                spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Standardtile")), TilePosition, Color.Red);
                                break;
                        }
                }
            }
                foreach (Enemy e in enemies.Children)
                    e.Draw(gameTime, spriteBatch);
                foreach (Solid s in solid.Children)
                    s.Draw(gameTime, spriteBatch);
                foreach (Consumables c in consumable.Children)
                    c.Draw(gameTime, spriteBatch);
                foreach (Boss b in bosses.Children)
                    b.Draw(gameTime, spriteBatch);            
        
        foreach (Door d in door.Children)
            d.Draw(gameTime, spriteBatch);
    }
}