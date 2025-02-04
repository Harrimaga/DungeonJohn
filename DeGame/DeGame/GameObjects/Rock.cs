﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Rock : SpriteGameObject
{

    public Rock(Vector2 startPosition, int layer = 0, string id = "Rock")
    : base("Sprites/Rock Sprite", layer, id)
    {
        position = startPosition;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        //right of rock
        if (CollidesWith(PlayingState.player) && BoundingBox.Left >= PlayingState.player.position.X)
        {
            PlayingState.player.velocityRightDown.X = Vector2.Zero.X;
        }
        else if (!CollidesWith(PlayingState.player) && !(BoundingBox.Left >= PlayingState.player.position.X))
        {
            PlayingState.player.velocityRightDown.X = PlayingState.player.velocitybase.X;
        }

        /*
        if (CollidesWith(PlayingState.player) && BoundingBox.Right <= PlayingState.player.position.X) - PlayingState.player.Width)
        {
            PlayingState.player.velocityLeftUp.X = Vector2.Zero.X;
        }
        /*else if (!CollidesWith(PlayingState.player) && !(BoundingBox.Right <= PlayingState.player.position.X - PlayingState.player.Width))
        {
            PlayingState.player.velocityLeftUp.X = PlayingState.player.velocitybase.X;
        }

        /*
        if (CollidesWith(PlayingState.player) && position.X >= PlayingState.player.position.X)
        {
            PlayingState.player.velocityRightDown.X = Vector2.Zero.X;
        }
        else if (!CollidesWith(PlayingState.player) && !(position.X >= PlayingState.player.position.X))
        {
            PlayingState.player.velocityRightDown.X = PlayingState.player.velocitybase.X;
        }

        /*
        if (CollidesWith(PlayingState.player) && position.X >= PlayingState.player.position.X)
        {
            PlayingState.player.velocityRightDown.X = Vector2.Zero.X;
        }
        else if (!CollidesWith(PlayingState.player) && !(position.X >= PlayingState.player.position.X))
        {
            PlayingState.player.velocityRightDown.X = PlayingState.player.velocitybase.X;
        }*/
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Rock Sprite"), position);
    }
}

