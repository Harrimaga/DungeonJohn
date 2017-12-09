using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class Floor
{
    Room[,] floor;
    int RoomListIndex = 1;
    int maxRooms = 20;
    int minRooms = 15;
    int floorWidth = 9;
    int floorHeight = 9;
    Random random = new Random();

    public Floor()
    {
        floor = new Room[floorWidth, floorHeight];
        floor[0, 0] = new Room(1);
        floor[1, 0] = new Room(2);

        foreach (Room room in floor)
        {
            if (room != null)
                room.LoadTiles();
        }
        //hele simpele layout voor testen
        //floor[5, 5] = new StartRoom(new Vector2(5,5));
        //floor[6, 5] = new Room();
        //floor[7, 5] = new EndRoom(new Vector2(7,5));
    }

 
    void FloorGenerator()
    {
        
        int x = random.Next(floorWidth);
        int y = random.Next(floorHeight);
        floor[x, y] = new StartRoom(1, 0, "");
        
        //TODO iets wat elke room aan een plekje in een floorarray linked met coordinaat [a,b]
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
        for (int x = 0; x < floorWidth; x++)
        {
            for (int y = 0; y < floorHeight; y++)
            {
                if (floor[x, y] != null)
                {
                    if (x + 1 < floorWidth && floor[x + 1, y] != null)
                    {
                        floor[x, y].right = true;
                    }
                    if (x - 1 >= 0 && floor[x - 1, y] != null)
                    {
                        floor[x, y].left = true;
                    }
                    if (y + 1 < floorHeight &&  floor[x, y + 1] != null)
                    {
                        floor[x, y].down = true;
                    }
                    if (y - 1 >= 0 && floor[x, y - 1] != null)
                    {
                        floor[x, y].up = true;
                    }
                }
            }
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

