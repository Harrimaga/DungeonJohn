using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Enemy : SpriteGameObject
{
    protected float health;
    protected float maxhealth;
    protected float attack;
    protected float attackspeed;
    protected float range;

    public Enemy(Vector2 startPosition, int layer = 0, string id = "Enemy")
    : base("Sprites/BearEnemy", layer, id)
    {
        position = startPosition;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        //CheckBulletCollision();
        //checkPlayerDetection();
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
    }

    public void Chase()
    {
        if (position.Y + sprite.Height > PlayingState.player.position.Y)
        {
            position.Y--;
        }
        if (position.Y - sprite.Height < PlayingState.player.position.Y)
        {
            position.Y++;
        }
        if (position.X + sprite.Width > PlayingState.player.position.X)
        {
            position.X--;
        }
        if (position.X - sprite.Width < PlayingState.player.position.X)
        {
            position.X++;
        }
    }
}



