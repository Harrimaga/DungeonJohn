using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework.Input;

public class Floor
{
    Room[,] floor;
    bool[,] Checked;
    int[,] AdjacentRooms;
    int[,] possiblespecial;
    int maxRooms = 6, minRooms = 4, floorWidth = 9, floorHeight = 9, CurrentLevel = 1, CurrentRooms, b = 0, q;
    Random random = new Random();
    public Vector2 startPlayerPosition;
    public Room currentRoom;
    bool FloorGenerated = false;
    public int screenwidth, screenheight;

    public Floor()
    {
        floor = new Room[floorWidth, floorHeight];
        Checked = new bool[floorWidth, floorHeight];
        AdjacentRooms = new int[floorWidth, floorHeight];
        possiblespecial = new int[floorWidth * floorHeight / 2, 2];
        FloorGenerator();
        screenwidth = GameEnvironment.WindowSize.X;
        screenheight = GameEnvironment.WindowSize.Y;
    }

    void FloorGenerator()
    {
        ClearFloor();
        int RoomAmount = random.Next(maxRooms - minRooms + 1) + minRooms;
        int x = random.Next(floorWidth - 2) + 2;
        int y = random.Next(floorHeight - 2) + 2;
        floor[x, y] = new Room(1, x, y);
        currentRoom = floor[x, y];
        //System.Console.WriteLine(currentRoom.position.ToString());
        FloorGeneratorRecursive(x, y, RoomAmount);
        SpawnBossRoom(x, y);
        SpawnItemRoom();
        if (CurrentLevel >= 7)
            SpawnItemRoom();
        DoorCheck();
        //FloorGenerated = true;
    }
    int RandomRoom()
    {
        return random.Next(2) + 4 ;
    }
    void FloorGeneratorRecursive(int x, int y, int RoomAmount)
    {
        if (y + 1 < floorHeight)
        {
            if (CurrentRooms < RoomAmount && floor[x, y + 1] == null && random.Next(100) <= CheckAdjacent(x, y + 1))
            {
                CurrentRooms++;
                floor[x, y + 1] = new Room(RandomRoom(), x, y + 1);
            }
            else
                Checked[x, y + 1] = true;
        }

        if (y - 1 >= 0)
        {
            if (CurrentRooms < RoomAmount && floor[x, y - 1] == null && random.Next(100) <= CheckAdjacent(x, y - 1))
            {
                CurrentRooms++;
                floor[x, y - 1] = new Room(RandomRoom(), x, y - 1);
            }
            else
                Checked[x, y - 1] = true;
        }

        if (x + 1 < floorWidth)
        {
            if (CurrentRooms < RoomAmount && floor[x + 1, y] == null && random.Next(100) <= CheckAdjacent(x + 1, y))
            {
                CurrentRooms++;
                floor[x + 1, y] = new Room(RandomRoom(), x + 1, y);
            }
            else
                Checked[x + 1, y] = true;
        }

        if (x - 1 >= 0)
        {
            if (CurrentRooms < RoomAmount && floor[x - 1, y] == null && random.Next(100) <= CheckAdjacent(x - 1, y))
            {
                CurrentRooms++;
                floor[x - 1, y] = new Room(RandomRoom(), x - 1,y);
            }
            else
                Checked[x - 1, y] = true;
        }

        Checked[x, y] = true;
        int counter = 0;
        if (CurrentRooms < RoomAmount)
        {
            for (int m = floorWidth - 1; m >= 0; m--)
                for (int n = floorHeight - 1; n >= 0; n--)
                    if (floor[m, n] != null && Checked[m, n] == false)
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
                floor[x, y] = new Room(1, x, y);
                FloorGeneratorRecursive(x, y, RoomAmount);
            }                       
        }
    }

    void SpawnItemRoom()
    {
        bool secondtime = false;
        if (b == 0)
        {
            for (int x = 0; x < floorWidth; x++)
                for (int y = 0; y < floorHeight; y++)
                    if (floor[x, y] == null && CanSpawnSpecialRoom(x, y) == true)                        
                        {
                            possiblespecial[b, 0] = x;
                            possiblespecial[b, 1] = y;
                            b++;
                        }                           
        }  
        else
            secondtime = true;
        q = random.Next(b - 1); //error
        if (secondtime == true)
        {
            CheckAdjacent(possiblespecial[q, 0], possiblespecial[q, 1]);
            while (AdjacentRooms[possiblespecial[q, 0], possiblespecial[q, 1]] != 1)
            {
                q = random.Next(b - 1);
                CheckAdjacent(possiblespecial[q, 0], possiblespecial[q, 1]);
            }
        }
            floor[possiblespecial[q, 0], possiblespecial[q, 1]] = new Room(3, possiblespecial[q, 0], possiblespecial[q, 1]);
            if (q != 0)
            {
                possiblespecial[q, 0] = possiblespecial[q - 1, 0];
                possiblespecial[q, 1] = possiblespecial[q - 1, 1];
            }
            else
            {
                possiblespecial[q, 0] = possiblespecial[q + 1, 0];
                possiblespecial[q, 1] = possiblespecial[q + 1, 1];
            }        
    }

    bool CanSpawnSpecialRoom(int x, int y)
    {
        CheckAdjacent(x, y);
        if (AdjacentRooms[x, y] == 1)
        {
            int counter = 0;
            if (x + 1 >= floorWidth)
                counter++;
            else if (floor[x + 1, y] == null || floor[x + 1, y].RoomListIndex >= 4)
                counter++;
            if (x - 1 < 0)
                counter++;
            else if (floor[x - 1, y] == null || floor[x - 1, y].RoomListIndex >= 4)
                counter++;
            if (y + 1 > -floorHeight)
                counter++;
            else if (floor[x, y + 1] == null || floor[x, y + 1].RoomListIndex >= 4)
                counter++;
            if (y - 1 < 0)
                counter++;
            else if (floor[x, y - 1] == null || floor[x, y - 1].RoomListIndex >= 4)
                counter++;
            if (counter == 4)
                return true;
        }
        return false;
    }

    void SpawnBossRoom(int x, int y)
    {
        int DistancefromStart = 0;
        int bossx = 4, bossy = 4;
        for (int a = 0; a < floorWidth; a++)
            for (int b = 0; b < floorHeight; b++)
                if (floor[a, b] == null && CanSpawnSpecialRoom(a, b) == true && Math.Abs(x - a) + Math.Abs(y - b) > DistancefromStart)
                {
                    bossx = a;
                    bossy = b;
                }         
        floor[bossx, bossy] = new Room(2, bossx, bossy);
    }

    int CheckAdjacent(int x, int y)
    {
        int RoomSpawnChance = 70;
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
    }

    public void NextFloor()
    {
        ClearFloor();
        floor = new Room[floorWidth, floorHeight];
        Checked = new bool[floorWidth, floorHeight];
        AdjacentRooms = new int[floorWidth, floorHeight];
        if (CurrentLevel <= 10)
        {
            maxRooms += 4;
            minRooms += 3;
        }
        FloorGenerator();
        CurrentLevel++;
        FloorGenerated = false;
    }

    public void ResetFloor()
    {
        ClearFloor();
        floor = new Room[floorWidth, floorHeight];
        Checked = new bool[floorWidth, floorHeight];
        AdjacentRooms = new int[floorWidth, floorHeight];
        maxRooms = 6;
        minRooms = 4;
        FloorGenerator();
        CurrentLevel = 1;
        FloorGenerated = false;
        PlayingState.player.Reset();
    }

    void DoorCheck()
    {        
            for (int x = 0; x < floorWidth; x++)
                for (int y = 0; y < floorHeight; y++)
                    if (floor[x, y] != null)
                    {
                        if (y - 1 >= 0 && floor[x, y - 1] != null)
                            floor[x, y].updoor = true;
                        if (y + 1 < floorWidth && floor[x, y + 1] != null)
                            floor[x, y].downdoor = true;
                        if (x - 1 >= 0 && floor[x - 1, y] != null)
                            floor[x, y].leftdoor = true;
                        if (x + 1 < floorWidth && floor[x + 1, y] != null)
                            floor[x, y].rightdoor = true;
                    }        
    }

    public void Update(GameTime gameTime)
    {
        foreach (Room room in floor)        
            if (room != null)            
                room.Update(gameTime, currentRoom);
    }

    public void HandleInput(InputHelper inputHelper)
    {
        if (inputHelper.KeyPressed(Keys.T))
            NextFloor();
        if (inputHelper.KeyPressed(Keys.R))
            ResetFloor();
    }
    
    void DrawMinimap(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/HUDbackground")), new Vector2(screenwidth - 340 + (Camera.Position.X - screenwidth / 2), (Camera.Position.Y - screenheight / 2)), Color.White);
        int FloorCellWidth = 15;
        int FloorCellHeight = 15;
        RoomWithPlayer();
        for (int x = 0; x < floorWidth; x++)
            for (int y = 0; y < floorHeight; y++)
                if (floor[x, y] != null) // && floor[x,y].Visited)
                {
                    if (floor[x, y].RoomListIndex == 1)
                    {
                        spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/MinimapStartTile")), new Vector2(screenwidth - 175 + x * (FloorCellWidth + 2) + (Camera.Position.X - screenwidth / 2), 15 + y * (FloorCellHeight + 2) + (Camera.Position.Y - screenheight / 2)), Color.White);
                        //Console.WriteLine(new Vector2(600 + x * (FloorCellWidth + 2) + Camera.Position.X, y * (FloorCellHeight + 2) + Camera.Position.Y).ToString());
                    }
                    else if (floor[x, y].RoomListIndex == 2)                    
                        spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/MinimapBossTile")), new Vector2(screenwidth - 175 + x * (FloorCellWidth + 2) + (Camera.Position.X - screenwidth / 2), 15 + y * (FloorCellHeight + 2) + (Camera.Position.Y - screenheight / 2)), Color.White);
                    else if (floor[x, y].RoomListIndex == 3)                    
                        spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/MinimapItemTile")), new Vector2(screenwidth - 175 + x * (FloorCellWidth + 2) + (Camera.Position.X - screenwidth / 2), 15 + y * (FloorCellHeight + 2) + (Camera.Position.Y - screenheight / 2)), Color.White);                    
                    else                    
                        spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/MinimapTile")), new Vector2(screenwidth - 175 + x * (FloorCellWidth + 2) + (Camera.Position.X - screenwidth / 2), 15 + y * (FloorCellHeight + 2) + (Camera.Position.Y - screenheight / 2)), Color.White);
                    if (new Vector2(x, y) == currentRoom.position)
                    {
                        spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/CurrentMinimapTile")), new Vector2(screenwidth - 175 + x * (FloorCellWidth + 2) + (Camera.Position.X - screenwidth / 2), 15 + y * (FloorCellHeight + 2) + (Camera.Position.Y - screenheight / 2)), Color.White);
                    }
                }
        //TODO alleen kamer tekenen op minimap als de speler er is geweest
    }

    void RoomWithPlayer()
    {
        for (int x = 0; x < floorWidth; x++)
            for (int y = 0; y < floorHeight; y++)
                if (PlayingState.player.position.X >= x * 1260 && PlayingState.player.position.X < (x + 1) * 1260)
                    if (PlayingState.player.position.Y >= y * screenheight && PlayingState.player.position.Y < (y + 1) * screenheight)
                        currentRoom.position = new Vector2(x, y);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        string Level = "Level " + CurrentLevel;
        string Gold = "Gold: " + PlayingState.player.gold;

        for (int a = 0; a < floorWidth; a++)
            for (int b = 0; b < floorHeight; b++)
                if (floor[a, b] != null)
                {
                    if (FloorGenerated == false)
                        floor[a, b].LoadTiles();
                    floor[a, b].Draw(gameTime, spriteBatch);
                }
        if (FloorGenerated == false)
        {
            PlayingState.player.position = startPlayerPosition - new Vector2(23, 22);
            Camera.Position = startPlayerPosition + new Vector2(170, 0);
            FloorGenerated = true;
        }
        DrawMinimap(spriteBatch);
        spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), Level, new Vector2(screenwidth - 275 + (Camera.Position.X - screenwidth / 2),(Camera.Position.Y - screenheight / 2) + 50), Color.White);
        spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), Gold, new Vector2(screenwidth - 275 + (Camera.Position.X - screenwidth / 2), (Camera.Position.Y - screenheight / 2) + 250), Color.White);
    }
}

