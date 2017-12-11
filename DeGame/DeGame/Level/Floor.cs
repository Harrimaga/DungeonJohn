using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework.Input;

public class Floor
{
    Room[,] floor;
    bool[,] Checked;
    int[,] AdjacentRooms;
    int maxRooms = 10, minRooms = 7, CurrentRooms = 1, floorWidth = 9, floorHeight = 9;
    Random random = new Random();
    bool FloorGenerated = false;

    public Floor()
    {
        floor = new Room[floorWidth, floorHeight];
        Checked = new bool[floorWidth, floorHeight];
        AdjacentRooms = new int[floorWidth, floorHeight];
        FloorGenerator();
        //int a = 1 + 1;
        //string dit = a.ToString();
    }

    void FloorGenerator()
    {
        for (int e = 0; e < floorWidth; e++)
            for (int f = 0; f < floorHeight; f++)
                Checked[e, f] = false;
        int RoomAmount = random.Next(maxRooms - minRooms + 1) + minRooms;
        int x = random.Next(floorWidth - 2) + 2;
        int y = random.Next(floorHeight - 2) + 2;
        floor[x, y] = new Room(1);
        FloorGeneratorRecursive(x, y, RoomAmount);
        ChooseBossRoom();
        FloorGenerated = true;
    }

    void FloorGeneratorRecursive(int x, int y, int RoomAmount)
    {
        if (y + 1 < floorHeight)
        {
            if (CurrentRooms < RoomAmount && floor[x, y + 1] == null && random.Next(100) <= CheckAdjacent(x, y + 1))
            {
                CurrentRooms++;
                floor[x, y + 1] = new Room(4);
            }
            else
                Checked[x, y + 1] = true;
        }

        if (y - 1 >= 0)
        {
            if (CurrentRooms < RoomAmount && floor[x, y - 1] == null && random.Next(100) <= CheckAdjacent(x, y - 1))
            {
                CurrentRooms++;
                floor[x, y - 1] = new Room(4);
            }
            else
                Checked[x, y - 1] = true;
        }

        if (x + 1 < floorWidth)
        {
            if (CurrentRooms < RoomAmount && floor[x + 1, y] == null && random.Next(100) <= CheckAdjacent(x + 1, y))
            {
                CurrentRooms++;
                floor[x + 1, y] = new Room(4);
            }
            else
                Checked[x + 1, y] = true;
        }

        if (x - 1 >= 0)
        {
            if (CurrentRooms < RoomAmount && floor[x - 1, y] == null && random.Next(100) <= CheckAdjacent(x - 1, y))
            {
                CurrentRooms++;
                floor[x - 1, y] = new Room(4);
            }
            else
                Checked[x - 1, y] = true;
        }

        Checked[x, y] = true;
        int counter = 0;
        if (CurrentRooms < RoomAmount)
        {
            for (int m = 0; m < floorWidth; m++)
                for (int n = 0; n < floorHeight; n++)
                    if (floor[m, n] != null && Checked[m, n] == false)
                    {
                        FloorGeneratorRecursive(m, n, RoomAmount);
                        counter++;
                    }
            if (counter == 0)
            {
                ClearFloor();
                FloorGenerator();
            }
        }
    }

    void ChooseBossRoom()
    {
        int[,] possiblechoice = new int[floorWidth * floorHeight / 2, 2];
        int[,] backupchoice = new int[floorWidth * floorHeight / 2, 2];
        int a = 0, b = 0;
        for (int x = 0; x < floorWidth; x++)
            for (int y = 0; y < floorHeight; y++)
            {
                CheckAdjacent(x, y);
                if (AdjacentRooms[x, y] == 1)
                {
                    if (floor[x, y] != null)
                    {
                        possiblechoice[a, 0] = x;
                        possiblechoice[a, 1] = y;
                        a++;                        
                    }
                    else
                    {
                        backupchoice[b, 0] = x;
                        backupchoice[b, 1] = y;
                        b++;                        
                    }
                }
            }
        int p = random.Next(a - 1);
        int q = random.Next(b - 1);
        if (a > 0)
        {
            floor[possiblechoice[p, 0], possiblechoice[p, 1]] = null;
            floor[possiblechoice[p, 0], possiblechoice[p, 1]] = new Room(2);
        }
        else        
            floor[backupchoice[q, 0], backupchoice[q, 1]] = new Room(2);
        
    }

    int CheckAdjacent(int x, int y)
    {
        int RoomSpawnChance = 50;
        int neighbours = 0;
        if (y + 1 < floorHeight && floor[x, y + 1] != null)
        {
            RoomSpawnChance = RoomSpawnChance / 12 * 10;
            neighbours++;
        }
        if (y - 1 >= 0 && floor[x, y - 1] != null)
        {
            RoomSpawnChance = RoomSpawnChance / 12 * 10;
            neighbours++;
        }
        if (x + 1 < floorWidth && floor[x + 1, y] != null)
        {
            RoomSpawnChance = RoomSpawnChance / 12 * 10;
            neighbours++;
        }
        if (x - 1 >= 0 && floor[x - 1, y] != null)
        {
            RoomSpawnChance = RoomSpawnChance / 12 * 10;
            neighbours++;
        }
        AdjacentRooms[x, y] = neighbours;
        return RoomSpawnChance * 12 / 10;
    }



    void ClearFloor()
    {
        for (int x = 0; x < floorWidth; x++)
            for (int y = 0; y < floorHeight; y++)
                floor[x,y] = null;
        CurrentRooms = 1;
    }

    //void NextFloor(Room[,] floor)
    //{
    //    ClearFloor();
    //    floor = new Room[floor.GetLength(0) + 2, floor.GetLength(1) + 2];
    //    floorWidth = floor.GetLength(0);
    //    floorHeight = floor.GetLength(1);
    //    maxRooms += 5;
    //    minRooms += 5;
    //    FloorGenerator();
    //    //TODO dus new floor maken (FloorGenerator aanroepen) en oude weg halen
    //}

    //void DoorCheck()
    //{
    //    if (FloorGenerated == true)
    //    {
    //        for (int x = 0; x < 9; x++)
    //            for (int y = 0; y < 9; y++)
    //                if (floor[x, y] != null)
    //                {
    //                    if (x + 1 < 9 && floor[x + 1, y] != null)
    //                        floor[x, y].right = true;
    //                    if (x - 1 >= 0 && floor[x - 1, y] != null)
    //                        floor[x, y].left = true;
    //                    if (y + 1 < 9 && floor[x, y + 1] != null)
    //                        floor[x, y].down = true;
    //                    if (y - 1 >= 0 && floor[x, y - 1] != null)
    //                        floor[x, y].up = true;
    //                    FloorGenerated = false;
    //                }
    //    }
    //}

    public virtual void Update(GameTime gameTime)
    {
        //foreach (Room room in floor)
        //{
        //    if (room != null)
        //    {
        //        room.Update(gameTime);
        //    }
        //}
        ////TODO als nextFloor true is voer dan NextFloor() uit
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        for (int a = 0; a < floorWidth; a++)
            for (int b = 0; b < floorHeight; b++)
                if (floor[a, b] != null)
                {
                    if (FloorGenerated == true)
                    {
                        floor[a, b].LoadTiles();
                    }
                    floor[a, b].Draw(gameTime, spriteBatch, a, b);
                }
        FloorGenerated = false;
    }
}

