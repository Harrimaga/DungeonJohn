using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework.Input;

public class Floor
{
    int maxRooms = 7, minRooms = 5, floorWidth = 9, floorHeight = 9, CurrentRooms, b = 0, q;
    public Room currentRoom;
    public int screenwidth, screenheight, used, CurrentLevel = 1, doortimer = 0, displayint = 1;
    public bool FloorGenerated = false;
    public Vector2 startPlayerPosition;
    Random random = new Random();
    public WornItems wornItems;
    string Level;
    int[,] possiblespecial, AdjacentRooms;
    public Room[,] floor;
    bool[,] Checked;

    public Floor()
    {
        floor = new Room[floorWidth, floorHeight];
        Checked = new bool[floorWidth, floorHeight];
        AdjacentRooms = new int[floorWidth, floorHeight];
        possiblespecial = new int[floorWidth * floorHeight / 2, 2];
        screenwidth = GameEnvironment.WindowSize.X;
        screenheight = GameEnvironment.WindowSize.Y;
        wornItems = new WornItems(new Vector2(0, 0));
    }

    void FloorGenerator()
    {
        ClearFloor();
        int RoomAmount = random.Next(maxRooms - minRooms + 1) + minRooms;
        int x = random.Next(floorWidth - 2) + 2;
        int y = random.Next(floorHeight - 2) + 2;
        floor[x, y] = new Room("", 1, x, y);
        currentRoom = floor[x, y];
        FloorGeneratorRecursive(x, y, RoomAmount);
        SpawnItemRoom();
        if (CurrentLevel >= 7)
            SpawnItemRoom();
        SpawnBossRoom(x, y);
        DoorCheck();
        FloorGenerated = false;
    }
    int RandomRoom()
    {
        return random.Next(5) + 5;
    }
    void FloorGeneratorRecursive(int x, int y, int RoomAmount)
    {
        if (CurrentRooms < RoomAmount)
        {
            if (y + 1 < floorHeight)
            {
                if (floor[x, y + 1] == null && random.Next(100) <= CheckAdjacent(x, y + 1))
                {
                    CurrentRooms++;
                    floor[x, y + 1] = new Room("", RandomRoom(), x, y + 1);
                }
                else
                    Checked[x, y + 1] = true;
            }

            if (y - 1 >= 0)
            {
                if (floor[x, y - 1] == null && random.Next(100) <= CheckAdjacent(x, y - 1))
                {
                    CurrentRooms++;
                    floor[x, y - 1] = new Room("", RandomRoom(), x, y - 1);
                }
                else
                    Checked[x, y - 1] = true;
            }

            if (x + 1 < floorWidth)
            {
                if (floor[x + 1, y] == null && random.Next(100) <= CheckAdjacent(x + 1, y))
                {
                    CurrentRooms++;
                    floor[x + 1, y] = new Room("", RandomRoom(), x + 1, y);
                }
                else
                    Checked[x + 1, y] = true;
            }

            if (x - 1 >= 0)
            {
                if (floor[x - 1, y] == null && random.Next(100) <= CheckAdjacent(x - 1, y))
                {
                    CurrentRooms++;
                    floor[x - 1, y] = new Room("", RandomRoom(), x - 1, y);
                }
                else
                    Checked[x - 1, y] = true;
            }
        }

        Checked[x, y] = true;
        int counter = 0;
        if (CurrentRooms < RoomAmount)
        {
            for (int m = floorWidth - 1; m >= 0; m--)
                for (int n = floorHeight - 1; n >= 0; n--)
                    if (floor[m, n] != null && !Checked[m, n])
                    {
                        FloorGeneratorRecursive(m, n, RoomAmount);
                        counter++;
                    }
            if (counter == 0)
            {
                ClearFloor();
                RoomAmount = random.Next(maxRooms - minRooms + 1) + minRooms;
                x = random.Next(floorWidth - 2) + 2;
                y = random.Next(floorHeight - 2) + 2;
                floor[x, y] = new Room("", 1, x, y);
                currentRoom = floor[x, y];
                FloorGeneratorRecursive(x, y, RoomAmount);
            }
        }
    }

    void SpawnItemRoom()
    {
        if (b == 0)
        {
            for (int x = 0; x < floorWidth; x++)
                for (int y = 0; y < floorHeight; y++)
                    if (CanSpawnSpecialRoom(x, y))
                    {
                        possiblespecial[b, 0] = x;
                        possiblespecial[b, 1] = y;
                        b++;
                    }
            q = random.Next(b);
        }
        else
        {
            q = random.Next(b);
            CheckAdjacent(possiblespecial[q, 0], possiblespecial[q, 1]);
            while (AdjacentRooms[possiblespecial[q, 0], possiblespecial[q, 1]] != 1)
            {
                q = random.Next(b);
                CheckAdjacent(possiblespecial[q, 0], possiblespecial[q, 1]);
            }
        }
        floor[possiblespecial[q, 0], possiblespecial[q, 1]] = new Room("", 3, possiblespecial[q, 0], possiblespecial[q, 1]);
        possiblespecial[q, 0] = possiblespecial[b, 0];
        possiblespecial[q, 1] = possiblespecial[b, 1];
    }

    void SpawnBossRoom(int x, int y)
    {
        int DistancefromStart = 0;
        int bossx = 4, bossy = 4;
        bool changed = false;
        for (int a = 0; a <= b; a++)
            if (CanSpawnSpecialRoom(possiblespecial[a, 0], possiblespecial[a, 1]) && Math.Abs(x - possiblespecial[a, 0]) + Math.Abs(y - possiblespecial[a, 1]) > DistancefromStart)
            {
                bossx = possiblespecial[a, 0];
                bossy = possiblespecial[a, 1];
                DistancefromStart = Math.Abs(x - possiblespecial[a, 0]) + Math.Abs(y - possiblespecial[a, 1]);
                changed = true;
            }
        if (changed)
            floor[bossx, bossy] = new EndRoom("", 2, bossx, bossy);
        else
        {
            ClearFloor();
            FloorGenerator();
        }
    }

    bool CanSpawnSpecialRoom(int x, int y)
    {
        if (floor[x, y] == null)
        {
            CheckAdjacent(x, y);
            if (AdjacentRooms[x, y] == 1)
            {
                if (x + 1 < floorWidth && floor[x + 1, y] != null && floor[x + 1, y].RoomListIndex <= 3)
                    return false;
                if (x - 1 > 0 && floor[x - 1, y] != null && floor[x - 1, y].RoomListIndex <= 3)
                    return false;
                if (y + 1 < floorHeight && floor[x, y + 1] != null && (floor[x, y + 1].RoomListIndex <= 3))
                    return false;
                if (y - 1 > 0 && floor[x, y - 1] != null && floor[x, y - 1].RoomListIndex <= 3)
                    return false;
                return true;
            }
        }
        return false;
    }

    public void DoorCheck()
    {
        for (int x = 0; x < floorWidth; x++)
            for (int y = 0; y < floorHeight; y++)
                if (floor[x, y] != null)
                {
                    if (y - 1 >= 0 && floor[x, y - 1] != null)
                        switch (floor[x, y - 1].RoomListIndex)
                        {
                            case (2):
                                floor[x, y].updoor = 2;
                                break;
                            case (3):
                                floor[x, y].updoor = 3;
                                break;
                            default:
                                floor[x, y].updoor = 1;
                                break;
                        }
                    if (y + 1 < floorWidth && floor[x, y + 1] != null)
                        switch (floor[x, y + 1].RoomListIndex)
                        {
                            case (2):
                                floor[x, y].downdoor = 2;
                                break;
                            case (3):
                                floor[x, y].downdoor = 3;
                                break;
                            default:
                                floor[x, y].downdoor = 1;
                                break;
                        }
                    if (x - 1 >= 0 && floor[x - 1, y] != null)
                        switch (floor[x - 1, y].RoomListIndex)
                        {
                            case (2):
                                floor[x, y].leftdoor = 2;
                                break;
                            case (3):
                                floor[x, y].leftdoor = 3;
                                break;
                            default:
                                floor[x, y].leftdoor = 1;
                                break;
                        }
                    if (x + 1 < floorWidth && floor[x + 1, y] != null)
                        switch (floor[x + 1, y].RoomListIndex)
                        {
                            case (2):
                                floor[x, y].rightdoor = 2;
                                break;
                            case (3):
                                floor[x, y].rightdoor = 3;
                                break;
                            default:
                                floor[x, y].rightdoor = 1;
                                break;
                        }
                }
    }

    int CheckAdjacent(int x, int y)
    {
        int RoomSpawnChance = 80;
        int SpawnChanceReduction = 20;
        int neighbours = 0;
        if (y + 1 < floorHeight && floor[x, y + 1] != null)
        {
            RoomSpawnChance = RoomSpawnChance / SpawnChanceReduction * 10;
            neighbours++;
        }
        if (y - 1 >= 0 && floor[x, y - 1] != null)
        {
            RoomSpawnChance = RoomSpawnChance / SpawnChanceReduction * 10;
            neighbours++;
        }
        if (x + 1 < floorWidth && floor[x + 1, y] != null)
        {
            RoomSpawnChance = RoomSpawnChance / SpawnChanceReduction * 10;
            neighbours++;
        }
        if (x - 1 >= 0 && floor[x - 1, y] != null)
        {
            RoomSpawnChance = RoomSpawnChance / SpawnChanceReduction * 10;
            neighbours++;
        }
        AdjacentRooms[x, y] = neighbours;
        return RoomSpawnChance * SpawnChanceReduction / 10;
    }

    void ClearFloor()
    {
        for (int x = 0; x < floorWidth; x++)
            for (int y = 0; y < floorHeight; y++)
            {
                floor[x, y] = null;
                Checked[x, y] = false;
                AdjacentRooms[x, y] = 0;
            }
        b = 0;
        CurrentRooms = 1;
        floor = new Room[floorWidth, floorHeight];
        Checked = new bool[floorWidth, floorHeight];
        AdjacentRooms = new int[floorWidth, floorHeight];
    }

    public void Update(GameTime gameTime)
    {
        foreach (Room room in floor)
            if (room != null && room.position == currentRoom.position)
            {
                room.Update(gameTime);
            }
        if (doortimer > 0)
        {
            doortimer--;
        }
    }

    public void HandleInput(InputHelper inputHelper)
    {
        if (inputHelper.KeyPressed(Keys.T))
            if (CurrentLevel % 2 == 0)
                NextFloor();
            else
                NextShop();
        if (inputHelper.KeyPressed(Keys.R))
            ResetFloor();
        foreach (Room r in floor)
        {
            if (r != null)
                r.HandleInput(inputHelper);
        }
    }

    public void NextFloor()
    {
        ClearFloor();
        if (displayint <= 5)
        {
            maxRooms += 3;
            minRooms += 3;
        }
        FloorGenerator();
        CurrentLevel++;
        displayint++;
        Level = "Level: " + displayint;
    }

    public void NextShop()
    {
        ClearFloor();
        floor[4, 4] = new Room("", 4, 4, 4);
        currentRoom = floor[4, 4];
        floor[4, 4].LoadTiles();
        CurrentLevel++;
        Level = "Level: Shop after " + displayint;
        FloorGenerated = false;
    }

    public void ResetFloor()
    {
        ClearFloor();
        maxRooms = 7;
        minRooms = 5;
        CurrentLevel = 1;
        displayint = 1;
        Level = "Level: " + displayint;
        FloorGenerator();
        PlayingState.player.Reset();
    }

    public void DrawMinimap(SpriteBatch spriteBatch)
    {
        //int roomwidth = PlayingState.currentFloor.currentRoom.roomwidth;
        //int roomheight = PlayingState.currentFloor.currentRoom.roomheight;
        //krijgt soms 0 mee van currentroom
        int FloorCellWidth = 15;
        int FloorCellHeight = 15;
        int currentroomx = (int)PlayingState.player.position.X / 1260;
        int currentroomy = (int)PlayingState.player.position.Y / 900;
        if (floor[currentroomx, currentroomy] != null)
            currentRoom = floor[currentroomx, currentroomy];
        else
            currentRoom.position = new Vector2(currentroomx, currentroomy);

        for (int x = 0; x < floorWidth; x++)
            for (int y = 0; y < floorHeight; y++)
            {
                if (floor[x, y] != null /*&& floor[x,y].Visited*/)
                {
                    switch (floor[x, y].RoomListIndex)
                    {
                        case (1):
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Tiles/MinimapStartTile")), new Vector2(screenwidth - 175 + x * (FloorCellWidth + 2) + (Camera.Position.X - screenwidth / 2), 15 + y * (FloorCellHeight + 2) + (Camera.Position.Y - screenheight / 2)), Color.White);
                            break;
                        case (2):
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Tiles/MinimapBossTile")), new Vector2(screenwidth - 175 + x * (FloorCellWidth + 2) + (Camera.Position.X - screenwidth / 2), 15 + y * (FloorCellHeight + 2) + (Camera.Position.Y - screenheight / 2)), Color.White);
                            break;
                        case (3):
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Tiles/MinimapItemTile")), new Vector2(screenwidth - 175 + x * (FloorCellWidth + 2) + (Camera.Position.X - screenwidth / 2), 15 + y * (FloorCellHeight + 2) + (Camera.Position.Y - screenheight / 2)), Color.White);
                            break;
                        default:
                            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Tiles/MinimapTile")), new Vector2(screenwidth - 175 + x * (FloorCellWidth + 2) + (Camera.Position.X - screenwidth / 2), 15 + y * (FloorCellHeight + 2) + (Camera.Position.Y - screenheight / 2)), Color.White);
                            break;
                    }
                    if (floor[x,y].Type == "bossroom")
                        spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Tiles/MinimapBossTile")), new Vector2(screenwidth - 175 + x * (FloorCellWidth + 2) + (Camera.Position.X - screenwidth / 2), 15 + y * (FloorCellHeight + 2) + (Camera.Position.Y - screenheight / 2)), Color.White);
                }
                if (currentRoom.position == new Vector2(x, y))
                {
                    spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Tiles/CurrentMinimapTile")), new Vector2(screenwidth - 175 + x * (FloorCellWidth + 2) + (Camera.Position.X - screenwidth / 2), 15 + y * (FloorCellHeight + 2) + (Camera.Position.Y - screenheight / 2)), Color.White);
                }
            }
        //TODO alleen kamer tekenen op minimap als de speler er is geweest
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        string enemycount = "Count: " + PlayingState.currentFloor.currentRoom.enemycounter;
        string Gold = "Gold: " + PlayingState.player.gold;
        string tokens; 
        if (PlayingState.player.leveltokens == 1)
            tokens = "You have " + PlayingState.player.leveltokens + " unspent token,";
        else
            tokens = "You have " + PlayingState.player.leveltokens + " unspent tokens,";
        string tokens2 = "press 'O' to level up";

        foreach (Room room in floor)
        {
            if (room != null)
            {
                if (!FloorGenerated)
                    room.LoadTiles();
                if (room != null && (room.position == currentRoom.position) || room.position == currentRoom.position + new Vector2(1, 0) || room.position == currentRoom.position - new Vector2(1, 0)
                || room.position == currentRoom.position + new Vector2(0, 1) || room.position == currentRoom.position - new Vector2(0, 1))
                    room.Draw(gameTime, spriteBatch);


            }
        }

        if (!FloorGenerated)
        {
            PlayingState.player.position = startPlayerPosition - new Vector2(23, 22);
            Camera.Position = startPlayerPosition + new Vector2(170, 0);
            FloorGenerated = true;
        }
        else
        {
            spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/HUD/HUDbackground")), new Vector2(screenwidth - 340 + (Camera.Position.X - screenwidth / 2), (Camera.Position.Y - screenheight / 2)));
            DrawMinimap(spriteBatch);
            spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), Level, new Vector2(screenwidth - 275 + (Camera.Position.X - screenwidth / 2), (Camera.Position.Y - screenheight / 2) + 50), Color.White);
            spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), Gold, new Vector2(screenwidth - 275 + (Camera.Position.X - screenwidth / 2), (Camera.Position.Y - screenheight / 2) + 250), Color.Yellow);
            if (PlayingState.player.leveltokens > 0)
            {
                spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), tokens, new Vector2(screenwidth - 275 + (Camera.Position.X - screenwidth / 2), (Camera.Position.Y - screenheight / 2) + 400), Color.White);
                spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), tokens2, new Vector2(screenwidth - 275 + (Camera.Position.X - screenwidth / 2), (Camera.Position.Y - screenheight / 2) + 425), Color.White);
            }
                spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), enemycount, new Vector2(screenwidth - 275 + (Camera.Position.X - screenwidth / 2), (Camera.Position.Y - screenheight / 2) + 450), Color.White);
        }
        wornItems.Position = new Vector2(screenwidth - 300 + (Camera.Position.X - screenwidth / 2), (Camera.Position.Y - screenheight / 2) + 510);
        wornItems.Draw(gameTime, spriteBatch);
    }
}

