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
    protected Vector2 basevelocity = new Vector2(1, 1);

    public Enemy(Vector2 startPosition, int layer = 0, string id = "Enemy")
    : base("Sprites/BearEnemy", layer, id)
    {
        position = startPosition;
        velocity = basevelocity;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (CollidesWith(PlayingState.player))
        {
            velocity = Vector2.Zero;
            PlayingState.player.health -= 1;
        }
        if (!CollidesWith(PlayingState.player))
        {
            velocity = basevelocity;
        }
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
    }

    public virtual void Chase()
    {
        if (position.Y + sprite.Height > PlayingState.player.position.Y)
        {
            position.Y -= velocity.Y;
        }
        if (position.Y - sprite.Height < PlayingState.player.position.Y)
        {
            position.Y += velocity.Y;
        }
        if (position.X + sprite.Width > PlayingState.player.position.X)
        {
            position.X -= velocity.X;
        }
        if (position.X - sprite.Width < PlayingState.player.position.X)
        {
            position.X += velocity.X;
        }
    }
}



