﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



public interface IGameObject
{
    void HandleInput(InputHelper inputHelper, GameTime gameTime);

    void Update(GameTime gameTime);

    void Draw(GameTime gameTime, SpriteBatch spriteBatch);

    void Reset();
}

