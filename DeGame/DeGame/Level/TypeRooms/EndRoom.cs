using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class EndRoom : Room
    {
    bool nextFloor = false;
    public EndRoom(Vector2 floorPosition)
    {

    }
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        
    }
    public virtual void Update(GameTime gameTime)
    {
        //TODO check player volgende floor mag nextFloor true maken
    }
}

