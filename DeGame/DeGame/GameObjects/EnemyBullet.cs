﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class EnemyBullet : SpriteGameObject
{
    public EnemyBullet(Vector2 Startpositon, int layer = 0, string id = "EnemyBullet") : base("Sprites/Random", layer, id)
    {
        position = Startpositon;
        velocity = (PlayingState.player.position - position) / 50;
    }
    public override void Update(GameTime gameTime)
    {
        position += velocity;
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Random"), position);
    }
}
