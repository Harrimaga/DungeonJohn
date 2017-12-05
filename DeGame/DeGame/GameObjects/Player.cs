﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Player : SpriteGameObject
{
    protected float health;
    protected float maxhealth;
    protected float attack;
    protected float attackspeed;
    protected float range;
    protected Vector2 position;

    public Player()
    {
        position = new Vector2(0, 0);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Random"), position);
    }
}


