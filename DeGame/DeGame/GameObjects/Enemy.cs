﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

public class Enemy : SpriteGameObject
{
    public float health;
    protected float maxhealth = 100;
    protected float attack;
    protected float attackspeed;
    protected float range = 100;
    protected float expGive = 120;
    protected bool drop = true, flying = false, backgroundenemy = false, bossenemy = false, killable = true, moving = true;
    protected int counter = 100;
    protected Vector2 direction, basevelocity = Vector2.Zero;
    public SpriteEffects Effects;
    public Texture2D playersprite, bulletsprite;
    HealthBar healthbar;
    protected Vector2 Roomposition;
    Vector2 actualvelocity;
    public bool alive = true;
    string AssetName;

    public Enemy(Vector2 startPosition, Vector2 roomposition, string assetname, int layer = 0, string id = "Enemy")
    : base(assetname, layer, id)
    {
        healthbar = new HealthBar(health, maxhealth, position);
        playersprite = GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerFront");
        position = startPosition;
        velocity = basevelocity;
        Roomposition = roomposition;
        health = maxhealth;
        AssetName = assetname;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        healthbar.Update(gameTime, health, maxhealth, position);

        if (!flying)
            SolidCollision();

        if (moving)
        {
            actualvelocity = velocity * direction;
            position += actualvelocity;
        }

        List<GameObject> RemoveBullets = new List<GameObject>();
        CollisionWithEnemy();
        foreach (Bullet bullet in PlayingState.player.bullets.Children)        
            if (CollidesWith(bullet))
            {
                health -= PlayingState.player.attack;
                RemoveBullets.Add(bullet);
            }
        foreach (Bullet bullet in RemoveBullets)        
            PlayingState.player.bullets.Remove(bullet);
        RemoveBullets.Clear();
        CheckAlive();
    }

    void CheckAlive()
    {
        if (health <= 0 && alive == true && killable || (PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].Type == "bossroom" && EndRoom.cleared && bossenemy))
        {
            PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemycounter--;
            if (drop)
                PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].DropConsumable(position);
            PlayingState.player.exp += expGive;
            alive = false;
            GameObjectList.RemovedObjects.Add(this);
        }
    }

    public void Chase()
    {
        direction = PlayingState.player.position - position;
        direction.Normalize();
    }

    protected void CollisionWithEnemy()
    {
        foreach (Enemy enemy in PlayingState.currentFloor.currentRoom.enemies.Children)
        {
            if (enemy != this && !enemy.backgroundenemy)
            {
                if (CollidesWith(enemy) && BoundingBox.Left < enemy.position.X + enemy.Width && BoundingBox.Left + (enemy.Width / 2) > enemy.position.X + enemy.Width)
                {
                    if (CollidesWith(enemy))
                        enemy.position.X--;
                }

                if (CollidesWith(enemy) && BoundingBox.Right > enemy.position.X && BoundingBox.Right - (enemy.Width / 2) < enemy.position.X)
                {
                    if (CollidesWith(enemy))
                        enemy.position.X++;
                }

                if (CollidesWith(enemy) && BoundingBox.Top < enemy.position.Y + enemy.Height && BoundingBox.Top + (enemy.Height / 2) > enemy.position.Y + enemy.Height)
                {
                    if (CollidesWith(enemy))
                        enemy.position.Y--;
                }

                if (CollidesWith(enemy) && BoundingBox.Bottom > enemy.position.Y && BoundingBox.Bottom - (enemy.Height / 2) < enemy.position.Y)
                {
                    if (CollidesWith(enemy))
                        enemy.position.Y++;
                }
            }
        }
    }

    protected void SolidCollision()
    {
        foreach (Solid s in PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].solid.Children)
        {
            while (CollidesWith(s))
            {
                if (actualvelocity.X > 0)
                    position.X--;
                if (actualvelocity.X < 0)
                    position.X++;
                if (actualvelocity.Y > 0)
                    position.Y--;
                if (actualvelocity.Y < 0)
                    position.Y++;
            }
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (position.X + playersprite.Width > PlayingState.player.position.X + 1)
        {
            Effects = SpriteEffects.None;
        }
        if (position.X + playersprite.Width < PlayingState.player.position.X - 1)
        {
            Effects = SpriteEffects.FlipHorizontally;
        }
        if (killable)
        {
            healthbar.Draw(spriteBatch);
        }
    }
}



