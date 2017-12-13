﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class StartRoom : Room
    {
    int RoomIndexList;
    public StartRoom(int roomListIndex, int p, int q, int layer = 0, string id = "") : base(p, q, layer)
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

