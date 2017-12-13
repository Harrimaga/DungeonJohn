using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Enemy : SpriteGameObject
{
    protected float health = 100;
    protected float maxhealth = 100;
    protected float attack;
    protected float attackspeed;
    protected float range;
    protected Vector2 basevelocity = new Vector2(1, 1);
    HealthBar healthbar;

    public Enemy(Vector2 startPosition, int layer = 0, string id = "Enemy")
    : base("Sprites/BearEnemy", layer, id)
    {
        healthbar = new HealthBar(health, maxhealth, position);
        position = startPosition;
        velocity = basevelocity;
    }

    public override void Update(GameTime gameTime)
    {
        healthbar.Update(gameTime, health, maxhealth, position);
        if(health <= 0)
        {
            Die = true;
        }

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

        List<GameObject> RemoveBullets = new List<GameObject>();

        foreach (Bullet bullet in PlayingState.player.bullets.Children)
        {
            if (CollidesWith(bullet))
            {
                health -= 20;
                RemoveBullets.Add(bullet);
            }
        }

        foreach (Bullet bullet in RemoveBullets)
        {
            PlayingState.player.bullets.Remove(bullet);
        }

        RemoveBullets.Clear();
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        healthbar.Draw(spriteBatch, position);
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



