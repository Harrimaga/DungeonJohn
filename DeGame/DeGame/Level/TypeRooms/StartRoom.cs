using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class StartRoom : Room
    {
    int RoomIndexList;
    public StartRoom(string map, int roomListIndex, int a, int b, int layer = 0, string id = "") : base(map, a, b, layer)
    {
        RoomIndexList = roomListIndex;
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        
    }
    public virtual void Update(GameTime gameTime)
    {

    }

}

