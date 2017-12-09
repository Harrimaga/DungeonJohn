using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class Floor
{
    Room[,] floor;
    bool[,] Checked;
    int maxRooms = 20;
    int minRooms = 15;
    int floorWidth = 9;
    int floorHeight = 9;
    Random random = new Random();
    int CurrentRooms = 0;

    public Floor()
    {
        floor = new Room[floorWidth, floorHeight];
        Checked = new bool[floorWidth, floorHeight];
        floor[0, 0] = new Room(3);
        foreach (Room room in floor)
        {
            if (room != null)
                room.LoadTiles();
        }
        for (int x = 0; x < floorWidth; x++)
            for (int y = 0; y < floorHeight; y++)
                Checked[x, y] = false;

        //int a = 1 + 1;
        //string dit = a.ToString();
        FloorGenerator();
    }

    void FloorGenerator()
    {
        //int p = random.Next(floorWidth);
        //int q = random.Next(floorHeight);
        floor[1, 0] = new Room(1);
        //int RoomAmount = random.Next(maxRooms - minRooms) + minRooms;
        //FloorGeneratorRecursive(0, 0, RoomAmount);
    }

    void FloorGeneratorRecursive(int x,int y, int RoomAmount)
    {
        int RoomSpawnChance = 30;
        Checked[x, y] = true;
        if (CurrentRooms < RoomAmount && x + 1 < floorWidth && floor[x + 1, y] == null)
        {
            if (random.Next(100) < RoomSpawnChance)
            {
                CurrentRooms++;
                floor[x + 1, y] = new Room(random.Next(2) + 1);
            }
            else
                Checked[x + 1, y] = true;
        }
        if (CurrentRooms < RoomAmount && x - 1 >= 0 && floor[x - 1, y] == null)
        {
            if (random.Next(100) < RoomSpawnChance)
            {
                CurrentRooms++;
                floor[x - 1, y] = new Room(random.Next(2) + 1);
            }
            Checked[x - 1, y] = true;
        }
        if (CurrentRooms < RoomAmount && y + 1 < floorHeight && floor[x, y + 1] == null)
        {
            if (random.Next(100) < RoomSpawnChance)
            {
                CurrentRooms++;
                floor[x, y + 1] = new Room(random.Next(2) + 1, 0);
            }
            else
                Checked[x, y + 1] = true;
        }
        if (CurrentRooms < RoomAmount && y - 1 >= 0 && floor[x, y - 1] == null)
        {
            if (random.Next(100) < RoomSpawnChance)
            {
                CurrentRooms++;
                floor[x, y - 1] = new Room(random.Next(2) + 1);
            }
            else
                Checked[x, y - 1] = true;
        }
        if (CurrentRooms < RoomAmount)
            for (int m = 0; m < floorWidth; m++)
                for (int n = 0; n < floorHeight; n++)
                {
                    if (floor[m, n] != null && Checked[m, n] == false)
                        FloorGeneratorRecursive(m, n, RoomAmount);
                }
    }

    void ClearFloor()
    {
        floor = new Room[floor.GetLength(0) + 2, floor.GetLength(1) + 2];
        floorWidth = floor.GetLength(0);
        floorHeight = floor.GetLength(1);
        CurrentRooms = 0;
    }

    void NextFloor(Room[,] floor)
    {
        maxRooms += 5;
        minRooms += 5;
        ClearFloor();
        FloorGenerator();
        //TODO dus new floor maken (FloorGenerator aanroepen) en oude weg halen
    }

    void DoorCheck()
    {
        for (int x = 0; x < 9; x++)
            for (int y = 0; y < 9; y++)
                if (floor[x, y] != null)
                {
                    if (x + 1 < 9 && floor[x + 1, y] != null)                    
                        floor[x, y].right = true;
                    if (x - 1 >= 0 && floor[x - 1, y] != null)                    
                        floor[x, y].left = true;                    
                    if (y + 1 < 9 && floor[x, y + 1] != null)                    
                        floor[x, y].down = true;                    
                    if (y - 1 >= 0 && floor[x, y - 1] != null)                    
                        floor[x, y].up = true;                    
                }
    }

    public virtual void Update(GameTime gameTime)
    {
        foreach (Room room in floor)
        {
            if (room != null)
            {
                room.Update(gameTime);
            }
        }
        //TODO als nextFloor true is voer dan NextFloor() uit
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        for (int a = 0; a < floorWidth; a++)
            for (int b = 0; b < floorHeight; b++)
                if (floor[a, b] != null)                
                    floor[a, b].Draw(gameTime, spriteBatch, a, b);
    }

}

