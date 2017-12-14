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
    public EndRoom(int roomListIndex, int a, int b, int layer = 0, string id = "") : base(a, b, layer)
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

