﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Shopkeeper : Tiles
{
    public Shopkeeper(Vector2 startPosition, int layer = 0, string id = "Tiles")
    : base(startPosition, layer, id)
    {
        position = startPosition;
    }
    public override void Update(GameTime gameTime)
    {
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Shopkeeper"), position);
    }

}