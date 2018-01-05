﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Ice : Tiles
{
    public Ice(Vector2 startPosition, int layer = 0, string id = "Ice")
    : base(startPosition, layer, id)
    {
        position = startPosition;
    }
    public override void Update(GameTime gameTime)
    {
        if (CollidesWith(PlayingState.player))
        {
            PlayingState.player.onIce = true;
        }
        else
        {
            PlayingState.player.onIce = false;
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Background Sprite"), position,Color.Aqua);
    }
}