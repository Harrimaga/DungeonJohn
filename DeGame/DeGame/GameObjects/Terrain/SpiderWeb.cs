﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class SpiderWeb : Tiles
{
    public bool OnThisTile = false;
    public SpiderWeb(Vector2 startPosition, int layer = 0, string id = "SpiderWeb")
    : base(startPosition, layer, id)
    {
        position = startPosition;
    }
    public override void Update(GameTime gameTime)
    {
        if (CollidesWith(PlayingState.player))
        {
            OnThisTile = true;
        }
        else
        {
            OnThisTile = false;
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Tiles/SpiderWeb"), position);
    }
}
