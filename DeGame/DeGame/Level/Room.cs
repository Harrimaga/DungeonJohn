using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;

public class Room : GameObjectList
{
    public int RoomListIndex, a, b, CellWidth, CellHeight, roomwidth, roomheight, Lastentrypoint, enemycounter = 0, updoor = 0, downdoor = 0, leftdoor = 0, rightdoor = 0;
    public string Map;
    public GameObjectList enemies, tiles, solid, consumable, bosses, altars, anvils, enemybullets, homingenemybullets;
    public static GameObjectList door;
    public Vector2 Up, Down, Left, Right, Exit, ExitShop, LastEntryPoint;
    public bool Visited = false, CameraMoving = false;
    public IList addedenemies = new List<Enemy>();
    protected int roomarraywidth, roomarrayheight;
    public string Type = "normalroom";
    Random random = new Random();
    public string[,] roomarray;
    public int lavatimer = 0;
    Vector2 TilePosition;


    public Room(string map, int roomListIndex, int A, int B, int layer = 0, string id = "") : base(layer)
    {
        tiles = new GameObjectList();
        consumable = new GameObjectList();
        enemies = new GameObjectList();
        bosses = new GameObjectList();
        solid = new GameObjectList();
        door = new GameObjectList();
        altars = new GameObjectList();
        anvils = new GameObjectList();
        enemybullets = new GameObjectList();
        homingenemybullets = new GameObjectList();
        RoomListIndex = roomListIndex;
        Map = map;
        a = A;
        b = B;
        position = new Vector2(a, b);
    }

    public void LoadTiles()
    {
        StreamReader fileReader = new StreamReader("Content/Levels/" + Map + "/" + RoomListIndex + ".txt");
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
        CellWidth = GameEnvironment.assetManager.GetSprite("Sprites/Tiles/Standardtile").Width;
        CellHeight = GameEnvironment.assetManager.GetSprite("Sprites/Tiles/Standardtile").Height;
        roomwidth = line.Length * CellWidth;
        roomheight = textLines.Count * CellHeight;
        roomarray = new string[roomarraywidth, roomarrayheight];

        for (int x = 0; x < roomarraywidth; ++x)
            for (int y = 0; y < roomarrayheight; ++y)
                AssignType(textLines[y][x], x, y);
    }

    private void AssignType(char textlines, int x, int y)
    {
        TilePosition = new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight);
        switch (textlines)
        {
            case '.':
                roomarray[x, y] = "Background";
                break;
            case '!':
                roomarray[x, y] = "Rock";
                CreateObject(x, y, "!");
                break;
            case 'A':
                roomarray[x, y] = "IceRock";
                CreateObject(x, y, "A");
                break;
            case '+':
                roomarray[x, y] = "Wall";
                CreateObject(x, y, "+");
                break;
            case 'x':
                roomarray[x, y] = "WoodenWall";
                CreateObject(x, y, "x");
                break;
            case ',':
                roomarray[x, y] = "ShopCounter";
                CreateObject(x, y, ",");
                break;
            case 'H':
                roomarray[x, y] = "Lava";
                CreateObject(x, y, "H");
                break;
            case 'G':
                roomarray[x, y] = "Ice";
                CreateObject(x, y, "G");
                break;
            case 'W':
                roomarray[x, y] = "SpiderWeb";
                CreateObject(x, y, "W");
                break;
            case '-':
                roomarray[x, y] = "UpDoor";
                Up = TilePosition;
                CreateObject(x, y, "-");
                break;
            case '=':
                roomarray[x, y] = "DownDoor";
                Down = TilePosition;
                CreateObject(x, y, "=");
                break;
            case '<':
                roomarray[x, y] = "LeftDoor";
                Left = TilePosition;
                CreateObject(x, y, "<");
                break;
            case '>':
                roomarray[x, y] = "RightDoor";
                Right = TilePosition;
                CreateObject(x, y, ">");
                break;
            case '0':
                roomarray[x, y] = "Shopkeeper";
                CreateObject(x, y, "0");
                break;
            case 'C':
                roomarray[x, y] = "ChasingEnemy";
                CreateObject(x, y, "C");
                break;
            case '2':
                roomarray[x, y] = "TwoPartEnemy";
                CreateObject(x, y, "2");
                break;
            case 'R':
                roomarray[x, y] = "RangedEnemy";
                CreateObject(x, y, "R");
                break;
            case 'Q':
                roomarray[x, y] = "SpamEnemy";
                CreateObject(x, y, "Q");
                break;
            case 'U':
                roomarray[x, y] = "TurretEnemyUp";
                CreateObject(x, y, "U");
                break;
            case 'D':
                roomarray[x, y] = "TurretEnemyDown";
                CreateObject(x, y, "D");
                break;
            case 'F':
                roomarray[x, y] = "TurretEnemyLeft";
                CreateObject(x, y, "F");
                break;
            case 'T':
                roomarray[x, y] = "TurretEnemyRight";
                CreateObject(x, y, "T");
                break;
            case 'X':
                roomarray[x, y] = "SlimeEnemy";
                CreateObject(x, y, "X");
                break;
            case 'Z':
                roomarray[x, y] = "SlimeEnemy2";
                CreateObject(x, y, "Z");
                break;
            case 'P':
                roomarray[x, y] = "SpiderEnemy";
                CreateObject(x, y, "P");
                break;
            case 'K':
                roomarray[x, y] = "IceEnemy";
                CreateObject(x, y, "K");
                break;
            case 'V':
                roomarray[x, y] = "RandomEnemy";
                CreateObject(x, y, "V");
                break;
            case 'B':
                roomarray[x, y] = "HomingBoss";
                CreateObject(x, y, "B");
                break;
            case 'O':
                roomarray[x, y] = "Pit";
                CreateObject(x, y, "O");
                break;
            case 'I':
                roomarray[x, y] = "Item";
                CreateObject(x, y, "I");
                break;
            case 'M':
                roomarray[x, y] = "ShopItem";
                CreateObject(x, y, "M");
                break;
            case 'Y':
                roomarray[x, y] = "Anvil";
                CreateObject(x, y, "Y");
                break;
            case 'E':
                roomarray[x, y] = "Exit";
                Exit = TilePosition;
                break;
            case 'L':
                roomarray[x, y] = "ExitShop";
                ExitShop = TilePosition;
                break;
            case 'S':
                roomarray[x, y] = "Start";
                PlayingState.currentFloor.startPlayerPosition = new Vector2(x * CellWidth + a * roomwidth + CellWidth / 2, y * CellHeight + b * roomheight + CellHeight / 2);
                break;
            default:
                roomarray[x, y] = "N/A";
                break;
        }
    }

    public void DropConsumable(Vector2 position, bool toaster = false)
    {
        if (!toaster)
        {
            int r = random.Next(100);
            if (r < 10)
            {
                Consumables golddrop = new Consumables(position, "gold");
                consumable.Add(golddrop);
            }
            else if (r < 30)
            {
                Consumables healthdrop = new Consumables(position, "heart");
                consumable.Add(healthdrop);
            }
            else if (r < 50)
            {
                Consumables ammodrop = new Consumables(position, "ammo");
                consumable.Add(ammodrop);
            }
        }
        else
        {
            Consumables toasterdrop = new Consumables(position, "toaster");
            consumable.Add(toasterdrop);
        }
    }

    public override void Update(GameTime gameTime)
    {
        int onicecounter = 0;
        int onwebcounter = 0;
        int onSolidcounter = 0;
        if (PlayingState.currentFloor.currentRoom.position == new Vector2(a, b))
        {
            Visited = true;
        }
        enemies.Update(gameTime);
        foreach (Enemy e in addedenemies)
        {
            enemies.Add(e);
        }
        addedenemies.Clear();
        door.Update(gameTime);
        consumable.Update(gameTime);
        bosses.Update(gameTime);
        solid.Update(gameTime);
        tiles.Update(gameTime);
        altars.Update(gameTime);
        anvils.Update(gameTime);
        enemybullets.Update(gameTime);
        homingenemybullets.Update(gameTime);
        CheckExit();
        if (lavatimer > 0)
        {
            lavatimer--;
        }
        if (tiles.Children.Count > 0)
        {
            foreach (Tiles tile in tiles.Children)
            {
                if (tile is Ice)
                {
                    Ice ice = tile as Ice;
                    if (ice.OnThisTile)
                        onicecounter++;
                }
                else if (tile is SpiderWeb)
                {
                    SpiderWeb web = tile as SpiderWeb;
                    if (web.OnThisTile)
                        onwebcounter++;
                }
            }
            foreach (Solid solid in solid.Children)
            {
                if (solid.OnThisTile)
                    onSolidcounter++;
            }
            if (onicecounter == 0)
                PlayingState.player.onIce = false;
            else
                PlayingState.player.onIce = true;
            if (onwebcounter == 0)
                PlayingState.player.onWeb = false;
            else
                PlayingState.player.onWeb = true;
            if (onSolidcounter == 0)
                PlayingState.player.onSolid = false;
            else
                PlayingState.player.onSolid = true;

            switch (Lastentrypoint)
            {
                case 1:
                    LastEntryPoint = new Vector2(10 * CellWidth + a * roomwidth, 2 * CellHeight + b * roomheight);
                    break;
                case 2:
                    LastEntryPoint = new Vector2(10 * CellWidth + a * roomwidth, 14 * CellHeight + b * roomheight - GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerDown").Height);
                    break;
                case 3:
                    LastEntryPoint = new Vector2(2 * CellWidth + a * roomwidth, 7 * CellHeight + b * roomheight);
                    break;
                case 4:
                    LastEntryPoint = new Vector2(20 * CellWidth + a * roomwidth - GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerDown").Width, 7 * CellHeight + b * roomheight);
                    break;
                default:
                    LastEntryPoint = new Vector2(10 * CellWidth + a * roomwidth, 7 * CellHeight + b * roomheight);
                    break;
            }
        }
    }

    public void CreateObject(int x, int y, string Type)
    {
        int enemylevel = PlayingState.currentFloor.displayint;
        TilePosition = new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight);
        switch (Type)
        {
            case ("C"):
                Enemy enemyChase = new ChasingEnemy(TilePosition, new Vector2(a, b), enemylevel, 0, "ChasingEnemy");
                enemies.Add(enemyChase);
                roomarray[x, y] = "Background";
                enemycounter++;
                break;
            case ("R"):
                Enemy enemyRanged = new RangedEnemy(TilePosition, new Vector2(a, b), enemylevel, 0, "RangedEnemy");
                enemies.Add(enemyRanged);
                roomarray[x, y] = "Background";
                enemycounter++;
                break;
            case ("2"):
                Enemy TwoPartEnemy = new TwoPartEnemy(TilePosition, new Vector2(a, b), enemylevel, 0, "TwoPartEnemy");
                enemies.Add(TwoPartEnemy);
                roomarray[x, y] = "Background";
                enemycounter++;
                break;
            case ("V"):
                Enemy enemyRandom = new RandomEnemy(TilePosition, new Vector2(a, b), enemylevel, 0, "RandomEnemy");
                enemies.Add(enemyRandom);
                roomarray[x, y] = "Background";
                enemycounter++;
                break;
            case ("Q"):
                Enemy enemySpam = new SpamEnemy(TilePosition, new Vector2(a, b), enemylevel, 0, "SpamEnemy");
                enemies.Add(enemySpam);
                roomarray[x, y] = "Background";
                enemycounter++;
                break;
            case ("U"):
                Enemy enemyTurretUp = new TurretEnemy(TilePosition, new Vector2(a, b), 1, enemylevel, 0, "TurretEnemyUp");
                enemies.Add(enemyTurretUp);
                roomarray[x, y] = "Background";
                break;
            case ("D"):
                Enemy enemyTurretDown = new TurretEnemy(TilePosition, new Vector2(a, b), 2, enemylevel, 0, "TurretEnemyDown");
                enemies.Add(enemyTurretDown);
                roomarray[x, y] = "Background";
                break;
            case ("F"):
                Enemy enemyTurretLeft = new TurretEnemy(TilePosition, new Vector2(a, b), 3, enemylevel, 0, "TurretEnemyLeft");
                enemies.Add(enemyTurretLeft);
                roomarray[x, y] = "Background";
                break;
            case ("T"):
                Enemy enemyTurretRight = new TurretEnemy(TilePosition, new Vector2(a, b), 4, enemylevel, 0, "TurretEnemyRight");
                enemies.Add(enemyTurretRight);
                roomarray[x, y] = "Background";
                break;
            case ("X"):
                Enemy SlimeEnemy = new SlimeEnemy(TilePosition, new Vector2(a, b), 1, enemylevel, 0, "SlimeEnemy");
                enemies.Add(SlimeEnemy);
                roomarray[x, y] = "Background";
                enemycounter++;
                break;
            case ("Z"):
                Enemy SlimeEnemy2 = new SlimeEnemy(TilePosition, new Vector2(a, b), 2, enemylevel, 0, "'SlimeEnemy2");
                enemies.Add(SlimeEnemy2);
                roomarray[x, y] = "Background";
                enemycounter++;
                break;
            case ("P"):
                Enemy enemySpider = new SpiderEnemy(TilePosition, new Vector2(a, b), enemylevel, 0, "SpiderEnemy");
                enemies.Add(enemySpider);
                roomarray[x, y] = "Background";
                enemycounter++;
                break;
            case ("K"):
                CreateObject(x, y, "G");
                Enemy enemyIce = new IceEnemy(TilePosition, new Vector2(a, b), enemylevel, 0, "IceEnemy");
                enemies.Add(enemyIce);
                break;
            case ("B"):
                HomingBoss boss = new HomingBoss(TilePosition, new Vector2(a, b), 0, 0, "HomingBoss");
                bosses.Add(boss);
                break;
            case ("!"):
                Solid rock = new Rock(TilePosition, 0, "Rock");
                solid.Add(rock);
                roomarray[x, y] = "Background";
                break;
            case ("A"):
                Solid icerock = new Rock(TilePosition, 0, "Rock", true);
                solid.Add(icerock);
                CreateObject(x, y, "G");
                break;
            case ("+"):
                Solid wall = new Wall(TilePosition, 0, "Wall");
                solid.Add(wall);
                break;
            case ("x"):
                Solid WoodenWall = new WoodenWall(TilePosition, 0, "WoodenWall");
                solid.Add(WoodenWall);
                break;
            case (","):
                Solid ShopCounter = new ShopCounter(TilePosition, 0, "ShopCounter");
                solid.Add(ShopCounter);
                break;
            case ("I"):
                int v = random.Next(50);
                ItemSpawn item = new ItemSpawn(TilePosition, false, v, 0, "Item");
                altars.Add(item);
                break;
            case ("M"):
                int w = random.Next(50);
                ItemSpawn Shopitem = new ItemSpawn(TilePosition, true, w, 0, "ShopItem");
                altars.Add(Shopitem);
                break;
            case ("Y"):
                CraftingBench Anvil = new CraftingBench(TilePosition, true, 0, "Anvil");
                anvils.Add(Anvil);
                break;
            case ("H"):
                Lava lava = new Lava(TilePosition, 0, "Lava");
                tiles.Add(lava);
                break;
            case ("G"):
                Ice ice = new Ice(TilePosition, 0, "Ice");
                tiles.Add(ice);
                break;
            case ("W"):
                SpiderWeb web = new SpiderWeb(TilePosition, 0, "Ice");
                tiles.Add(web);
                break;
            case ("O"):
                Pit pit = new Pit(TilePosition, 0, "Pit");
                solid.Add(pit);
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
            case ("0"):
                Shopkeeper Shopkeeper = new Shopkeeper(TilePosition, 0, "Shopkeeper");
                tiles.Add(Shopkeeper);
                break;
        }
    }

    public virtual void CheckExit()
    {
        Vector2 MiddleofPlayer = new Vector2(PlayingState.player.position.X + GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerDown").Width / 2, PlayingState.player.position.Y + GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerDown").Height / 2);
        if (RoomListIndex == 4)
        {
            if (MiddleofPlayer.X >= ExitShop.X && MiddleofPlayer.X <= ExitShop.X + CellWidth)
                if (MiddleofPlayer.Y >= ExitShop.Y && MiddleofPlayer.Y <= ExitShop.Y + CellHeight)
                    PlayingState.currentFloor.NextFloor();
        }
    }

    public override void HandleInput(InputHelper inputHelper, GameTime gameTime)
    {
        anvils.HandleInput(inputHelper, gameTime);
    }

    void WallShader (GameTime gameTime, SpriteBatch spriteBatch, int x, int y)
    {
        //als er...
        if (CheckRoomarray(x - 1, y))
            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Tiles/Wall Sprite Right2")), TilePosition, Color.Gray);
        //links
        else if (CheckRoomarray(x + 1, y))
            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Tiles/Wall Sprite Left2")), TilePosition, Color.Gray);
        //rechts
        else 
        if (CheckRoomarray(x, y - 1))
            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Tiles/Wall Sprite Down2")), TilePosition, Color.Gray);
        //boven
        else if (CheckRoomarray(x, y + 1))
            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Tiles/Wall Sprite Up2")), TilePosition, Color.Gray);
        //onder een steen is of een tile is waar men over kan lopen, teken dan een schaduw op de muur aan die kant.

        //als er..
        else if (CheckRoomarray(x - 1, y - 1))
            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Tiles/Wall Sprite Corner RD")), TilePosition, Color.Gray);
        //linksboven
        else if (CheckRoomarray(x - 1, y + 1))
            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Tiles/Wall Sprite Corner RU")), TilePosition, Color.Gray);
        //linksonder
        else if (CheckRoomarray(x + 1, y - 1))
            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Tiles/Wall Sprite Corner LD")), TilePosition, Color.Gray);
        //rechtsboven
        else if (CheckRoomarray(x + 1, y + 1))
            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Tiles/Wall Sprite Corner LU")), TilePosition, Color.Gray);
        //rechtsonder 
        //...een steen is of een tile is waar men over kan lopen, teken dan een hoekstuk van een muur
    }

    void BackgroundShader(GameTime gameTime, SpriteBatch spriteBatch, int x, int y)
    {
        //als er...
        if (CheckRoomarray(x - 1, y, 2) && CheckRoomarray(x, y - 1, 2))
            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Tiles/Background Sprite LU")), TilePosition, Color.Gray);
        //links en boven
        else if (CheckRoomarray(x - 1, y, 2) && CheckRoomarray(x, y + 1, 2))
            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Tiles/Background Sprite LD")), TilePosition, Color.Gray);
        //links en onder
        else if (CheckRoomarray(x + 1, y, 2) && CheckRoomarray(x, y - 1, 2))
            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Tiles/Background Sprite RU")), TilePosition, Color.Gray);
        //rechts en boven
        else if (CheckRoomarray(x + 1, y, 2) && CheckRoomarray(x, y + 1, 2))
            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Tiles/Background Sprite RD")), TilePosition, Color.Gray);
        //rechts en onder
        //...muren staan, teken dan een backgroundsprite die aan die twee kanten schaduw heeft.

        //als er...
        else if (CheckRoomarray(x - 1, y, 2))
            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Tiles/Background Sprite Left")), TilePosition, Color.Gray);
        //links
        else if (CheckRoomarray(x + 1, y, 2))
            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Tiles/Background Sprite Right")), TilePosition, Color.Gray);
        //rechts
        else if (CheckRoomarray(x, y - 1, 2))
            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Tiles/Background Sprite Up")), TilePosition, Color.Gray);
        //boven
        else if (CheckRoomarray(x, y + 1, 2))
            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Tiles/Background Sprite Down")), TilePosition, Color.Gray);
        //onder
        //...een muur of deur staat, teken dan een backgroundsprite die aan die kant een schaduw heeft.
        else
            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Tiles/Background Sprite")), TilePosition, Color.Gray);
        //als geen van bovenstaande wordt uitgevoerd, teken dan een normale backgroundsprite
    }

    public bool CheckRoomarray(int x, int y, int type = 1)
    {
        if (x >= 0 && x < roomarray.GetLength(0) && y >= 0 && y < roomarray.GetLength(1))
        {
            if (type == 2)
            {
                if (roomarray[x, y] == "Wall" || roomarray[x, y] == "UpDoor" || roomarray[x, y] == "DownDoor" || roomarray[x, y] == "LeftDoor" || roomarray[x, y] == "RightDoor")
                    return true;
            }
            else if (type == 3)
            {
                if (roomarray[x, y] != "Pit")
                    return true;
            }
            else if (roomarray[x, y] == "Background" || roomarray[x, y] == "Lava" || roomarray[x, y] == "Ice" || roomarray[x, y] == "SpiderWeb" || roomarray[x, y] == "IceRock" || roomarray[x,y] == "Pit")
                return true;
        }
        return false;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        for (int x = 0; x < roomarray.GetLength(0); x++)
            for (int y = 0; y < roomarray.GetLength(1); y++)
            {
                TilePosition = new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight);
                if (roomarray != null)
                {
                    switch (roomarray[x, y])
                    {
                        case "ChasingEnemy":
                        case "RangedEnemy":
                        case "Background":
                        case "SpiderWeb":
                        case "ShopItem":
                        case "Anvil":
                        case "Rock":
                        case "Item":
                        case "Boss":
                            BackgroundShader(gameTime, spriteBatch, x, y);
                            break;
                        case "Pit":                            
                            break;
                        case "RightDoor":
                        case "DownDoor":
                        case "LeftDoor":
                        case "UpDoor":
                        case "Wall":
                            WallShader(gameTime, spriteBatch, x, y);
                            break;
                        case "Exit":
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Tiles/EndTileClosed")), TilePosition, Color.White);
                            break;
                        case "ExitShop":
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Tiles/EndTile")), TilePosition, Color.White);
                            break;
                        case "Start":
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Tiles/StartTile")), new Vector2(x * CellWidth + a * roomwidth, y * CellHeight + b * roomheight - 120), Color.Gray);
                            break;
                        default:
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Tiles/Standardtile")), TilePosition, Color.Red);
                            break;
                    }
                }
            }
        foreach (Tiles t in tiles.Children)
        {
            t.Draw(gameTime, spriteBatch);
        }
        foreach (Solid s in solid.Children)
        {
            s.Draw(gameTime, spriteBatch);
        }
        foreach (ItemSpawn a in altars.Children)
        {
            a.Draw(gameTime, spriteBatch);
        }
        foreach (CraftingBench c in anvils.Children)
        {
            c.Draw(gameTime, spriteBatch);
        }
        foreach (Consumables c in consumable.Children)
        {
            c.Draw(gameTime, spriteBatch);
        }
        foreach (Door d in door.Children)
        {
            d.Draw(gameTime, spriteBatch);
        }
        foreach (Boss b in bosses.Children)
        {
            if (!b.alive)
                Remove(b);
            else
                b.Draw(gameTime, spriteBatch);
        }
        foreach (Enemy e in enemies.Children)
        {
            if (!e.alive)
                Remove(e);
            else
                e.Draw(gameTime, spriteBatch);
        }
        enemybullets.Draw(gameTime, spriteBatch);
        homingenemybullets.Draw(gameTime, spriteBatch);
    }
}