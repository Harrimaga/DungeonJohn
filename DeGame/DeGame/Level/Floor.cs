using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class Floor
{
    Room[,] floor;
    bool[,] Checked;
    int RoomListIndex = 1;
    int maxRooms = 20;
    int minRooms = 15;
    int floorWidth = 9;
    int floorHeight = 9;
    Random random = new Random();
    int CurrentRooms = 0;
    public Room currentRoom;

    public Floor()
    {
        
        floor = new Room[floorWidth, floorHeight];
        floor[0, 0] = new Room(1);
        floor[1, 0] = new Room(2);
        Checked = new bool[floor.GetLength(0), floor.GetLength(1)];

        foreach (Room room in floor)
        {
            if (room != null)
                room.LoadTiles();
        }

        FloorGenerator();

        currentRoom = floor[0, 0];
        //hele simpele layout voor testen
        //floor[5, 5] = new StartRoom(new Vector2(5,5));
        //floor[6, 5] = new Room();
        //floor[7, 5] = new EndRoom(new Vector2(7,5));
    }

    void FloorGenerator()
    {
        int p = random.Next(9);
        int q = random.Next(9);
        //floor[0, 0] = new Room(1, 0, ""); //StartRoom(1, 0, "");
        int RoomAmount = random.Next(maxRooms - minRooms) + minRooms;
        //FloorGeneratorRecursive(0, 0, RoomAmount);
    }

    void FloorGeneratorRecursive(int x,int y, int RoomAmount)
    {
        Checked[x, y] = true;
        if (CurrentRooms < RoomAmount && x + 1 < floor.GetLength(0) && floor[x + 1, y] == null)
        {
            if (random.Next(100) < 30)
            {
                CurrentRooms++;
                floor[x + 1, y] = new Room(random.Next(2) + 1, 0, "");
            }
            else
                Checked[x + 1, y] = true;
        }
        if (CurrentRooms < RoomAmount && x - 1 >= 0 && floor[x - 1, y] == null)
        {
            if (random.Next(100) < 30)
            {
                CurrentRooms++;
                floor[x - 1, y] = new Room(random.Next(2) + 1, 0, "");
            }
            Checked[x - 1, y] = true;
        }
        if (CurrentRooms < RoomAmount && y + 1 < floor.GetLength(1) && floor[x, y + 1] == null)
        {
            if (random.Next(100) < 30)
            {
                CurrentRooms++;
                floor[x, y + 1] = new Room(random.Next(2) + 1, 0, "");
            }
            else
                Checked[x, y + 1] = true;
        }
        if (CurrentRooms < RoomAmount && y - 1 >= 0 && floor[x, y - 1] == null)
        {
            if (random.Next(100) < 30)
            {
                CurrentRooms++;
                floor[x, y - 1] = new Room(random.Next(2) + 1, 0, "");
            }
            else
                Checked[x, y - 1] = true;
        }
        if (CurrentRooms < RoomAmount)
            for (int m = 0; m < floor.GetLength(0); m++)
                for (int n = 0; n < floor.GetLength(1); n++)
                {
                    if (floor[m, n] != null && Checked[m, n] == false)
                        FloorGeneratorRecursive(m, n, RoomAmount);
                }
    }

    void ClearFloor()
    {
        floor = new Room[floorWidth + 2, floorHeight + 2];
    }
    void NextFloor()
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

