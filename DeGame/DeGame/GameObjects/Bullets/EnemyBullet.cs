﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//TODO: Damage regulation;
//TODO: Friendly Fire?;
class EnemyBullet : E_Bullet
{
    Vector2 direction;
    float speed;
    public EnemyBullet(Vector2 Startpositon, int layer = 0, string id = "EnemyBullet") : base("Sprites/Random", layer, id)
    {
        position = Startpositon;
        direction = (PlayingState.player.position - position);
        speed = 0.1f;
        
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        direction.Normalize();
        position += direction * speed;
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Random"), position);
    }
}