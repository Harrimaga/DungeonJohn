﻿using Microsoft.Xna.Framework;
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
    public SpriteEffects Effects;
    protected Vector2 basevelocity = new Vector2(1, 1);
    Texture2D playersprite;
    HealthBar healthbar;

    public Enemy(Vector2 startPosition, int layer = 0, string id = "Enemy")
    : base("Sprites/BearEnemy", layer, id)
    {
        healthbar = new HealthBar(health, maxhealth, position);
        playersprite = GameEnvironment.assetManager.GetSprite("Sprites/Random");
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

        healthbar.Update(gameTime, health, maxhealth, position);
        if (health <= 0)
        {
            GameObjectList.RemovedObjects.Add(this);
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        healthbar.Draw(spriteBatch, position);
    }

    public bool CheckDown()
    {
        Rectangle CheckDown = new Rectangle((int)position.X, (int)position.Y, sprite.Width, sprite.Height + 120);
        foreach (Solid solid in Room.solid.Children)
        if (CheckDown.Intersects(solid.BoundingBox))
        {
            return true;
        }
        return false;
    }
    public bool CheckUp()
    {
        Rectangle CheckUp = new Rectangle((int)position.X, (int)position.Y, sprite.Width, sprite.Height - 10);
        foreach (Solid solid in Room.solid.Children)
            if (CheckUp.Intersects(solid.BoundingBox))
            {
                return true;
            }
        return false;
    }
    public bool CheckLeft()
    {
        Rectangle CheckLeft = new Rectangle((int)position.X, (int)position.Y, sprite.Width - 10, sprite.Height);
        foreach (Solid solid in Room.solid.Children)
            if (CheckLeft.Intersects(solid.BoundingBox))
            {
                return true;
            }
        return false;
    }
    public bool CheckRight()
    {
        Rectangle CheckRight = new Rectangle((int)position.X, (int)position.Y, sprite.Width + 10, sprite.Height);
        foreach (Solid solid in Room.solid.Children)
            if (CheckRight.Intersects(solid.BoundingBox))
            {
                return true;
            }
        return false;
    }

    public virtual void Chase()
    {
            
            if (position.Y + playersprite.Height > PlayingState.player.position.Y + 1 && CheckUp() == false)
            {
                position.Y -= velocity.Y;
            }
            if (position.Y - playersprite.Height < PlayingState.player.position.Y - 1 && CheckDown() == false)
            {
                position.Y += velocity.Y;
            }
            if (position.X + playersprite.Width > PlayingState.player.position.X + 1 && CheckLeft() == false)
            {
                position.X -= velocity.X;
                Effects = SpriteEffects.None;
            }
            if (position.X + playersprite.Width < PlayingState.player.position.X - 1 && CheckRight() == false)
            {
                position.X += velocity.X;
                Effects = SpriteEffects.FlipHorizontally;
            }
    }
}



