using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class Floor
{
    Room[,] floor;
    int RoomListIndex = 1;
    Random random = new Random();

    public Floor()
    {
        floor = new Room[9, 9];
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
        int maxRooms = 20;
        int minRooms = 15;
        int x = 5;//random.Next(9);
        int y = 5;//random.Next(9);
        //floor[x,y]
        //TODO iets wat elke room aan een plekje in een floorarray linked met coordinaat [a,b]
    }

    void NextFloor()
    {
        //TODO dus new floor maken (FloorGenerator aanroepen) en oude weg halen
    }
    void DoorCheck()
    {
        for (int x = 0; x < 9; x++)
        {
            for (int y = 0; y < 9; y++)
            {
                if (floor[x, y] != null)
                {
                    if (x + 1 < 9 && floor[x + 1, y] != null)
                    {
                        // door right
                    }

                    if (x - 1 > 0 && floor[x - 1, y] != null)
                    {
                        //door left
                    }
                    if (y + 1 < 9 &&  floor[x, y + 1] != null)
                    {
                        //door down
                    }
                    if (y - 1 > 0 && floor[x, y - 1] != null)
                    {
                        //door up
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
        for (int a = 0; a < 9; a++)
            for (int b = 0; b < 9; b++)
                if (floor[a, b] != null)                
                    floor[a, b].Draw(gameTime, spriteBatch, a, b);
    }

}

