using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Floor
{
    Room[,] floor;
    int maxRooms = 20;
    int minRooms = 15;
    int a = 7, b = 7;

    public Floor()
    {
        floor = new Room[9, 9];
        //TODO iets wat elke room aan een plekje in een floorarray linked met coordinaat [a,b]
        //hele simpele layout voor testen
        //floor[5, 5] = new StartRoom(new Vector2(5,5));
        //floor[6, 5] = new Room();
        //floor[7, 5] = new EndRoom(new Vector2(7,5));
    }

    void FloorGenerator()
    {
        //TODO
    }

    void NextFloor()
    {
        //TODO dus new floor maken en oude weg halen
    }
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        foreach (Room room in floor)
        {
            if (room != null)
            {
                room.Draw(gameTime, spriteBatch, a, b);
            }
        }
    }
    public virtual void Update(GameTime gameTime)
    {
        foreach (Room room in floor)
        {
            if(room != null)
            {
                room.Update(gameTime);
            }
        }
        //TODO als nextFloor true is dan voor NextFloor() uit
    }

}

