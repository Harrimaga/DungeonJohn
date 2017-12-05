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

    public Floor()
    {
        floor = new Room[11, 11];
        //hele simpele layout voor testen
        floor[6, 6] = new StartRoom(new Vector2(6,6));
        floor[7, 6] = new Room();
        floor[8, 6] = new EndRoom(new Vector2(8, 6));
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
        
    }
    public virtual void Update(GameTime gameTime)
    {
        //TODO als nextFloor true is dan voor NextFloor() uit
    }

}

