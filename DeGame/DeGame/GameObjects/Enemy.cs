﻿using Microsoft.Xna.Framework;
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
    protected Vector2 position;
    //public bool Die = true;
    public Enemy(int layer = 0, string id = "Enemy")
    : base("Sprites/BearEnemy", layer, id)
    {
       
        
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        CheckBulletCollision();
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
    }

    public void Chase()
    {
        if (position.Y > PlayingState.player.position.Y)
        {
            position.Y--;
        }
        if (position.Y < PlayingState.player.position.Y)
        {
            position.Y++;
        }
        if (position.X > PlayingState.player.position.X)
        {
            position.X--;
        }
        if (position.X < PlayingState.player.position.X)
        {
            position.X++;
        }
    }

    public void checkPlayerDetection()
    {
        // Omdate player static is zou je dit hier zonder Find kunnen doen
    }

    public void CheckBulletCollision()
    {
        GameObjectList bullets = Floor.currentRoom.Find("bullet") as GameObjectList;
        foreach (SpriteGameObject bullet in bullets.Children)
        {
            if (CollidesWith(bullet))
            {
                Die = false;
            }
        }
    }
}

public class ChasingEnemy : Enemy
{
    public override void Update(GameTime gameTime)
    {
        Chase();
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (Die == true)
        {
            spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/BearEnemy"), position);
        }
    }

}



