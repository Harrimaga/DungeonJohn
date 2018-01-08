﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public class Enemy : SpriteGameObject
{
    public float health = 100;
    protected float maxhealth = 100;
    protected float attack;
    protected float attackspeed;
    protected float range = 200;
    protected float expGive = 120;
    protected bool alive = true;
    protected int counter = 100;
    protected Vector2 basevelocity = new Vector2((float) 0.5, (float)0.5);
    public SpriteEffects Effects;
    public Texture2D playersprite, bulletsprite;
    HealthBar healthbar;
    protected Vector2 Roomposition;
    public GameObjectList bullets;

    public Enemy(Vector2 startPosition, Vector2 roomposition, int layer = 0, string id = "Enemy")
    : base("Sprites/BearEnemy", layer, id)
    {
        bullets = new GameObjectList();
        healthbar = new HealthBar(health, maxhealth, position);
        playersprite = GameEnvironment.assetManager.GetSprite("Sprites/Random");
        position = startPosition;
        velocity = basevelocity;
        Roomposition = roomposition;
    }

    public override void Update(GameTime gameTime)
    {
        List<GameObject> RemoveBullets = new List<GameObject>();

        base.Update(gameTime);
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
        healthbar.Update(gameTime, health, maxhealth, position);
        bullets.Update(gameTime);

        if (health <= 0 && alive == true)
        {
            PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemycounter--;
            PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].DropConsumable(position);
            PlayingState.player.exp += expGive;
            alive = false;
            GameObjectList.RemovedObjects.Add(this);
        }
    
        
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        bullets.Draw(gameTime, spriteBatch);
        healthbar.Draw(spriteBatch);
        
    }

    public bool CheckDown()
    {
        Rectangle CheckDown = new Rectangle((int)position.X, (int)position.Y + sprite.Height, 60, 60);
        foreach (Solid solid in Room.solid.Children)
            if (CheckDown.Intersects(solid.BoundingBox))
                return true;       
        return false;
    }
    public bool CheckUp()
    {
        Rectangle CheckUp = new Rectangle((int)position.X, (int)position.Y - 60, 60, 60);
        foreach (Solid solid in Room.solid.Children)
            if (CheckUp.Intersects(solid.BoundingBox))            
                return true;            
        return false;
    }
    public bool CheckLeft()
    {
        Rectangle CheckLeft = new Rectangle((int)position.X - 60, (int)position.Y, 60, 60);
        foreach (Solid solid in Room.solid.Children)
            if (CheckLeft.Intersects(solid.BoundingBox))            
                return true;            
        return false;
    }
    public bool CheckRight()
    {
        Rectangle CheckRight = new Rectangle((int)position.X + sprite.Width, (int)position.Y, 60, 60);
        foreach (Solid solid in Room.solid.Children)
            if (CheckRight.Intersects(solid.BoundingBox))            
                return true;            
        return false;
    }

    public void Chase()
    {
        // Create a new grid and let each cell have a default traversal cost of 1.0
        //var grid = new Grid(100, 100, 1.0f);

        // Block some cells (for example walls)
        //grid.BlockCell(new Position(5, 5));

        // Make other cells harder to traverse (for example water)
        //grid.SetCellCost(new Position(6, 5), 3.0f);

        // And finally start the search for the shortest path form start to end
        // Use one of the built-in ranges of motion
        //var path = grid.GetPath(new Position(0, 0), new Position(99, 99), MovementPatterns.DiagonalOnly);

        // Or define the movement pattern of an agent yourself
        // For example, here is an agent that can only move left and up
        // var movementPattern = new[] { new Offset(-1, 0), new Offset(0, -1) };
        // var path = grid.GetPath(new Position(0, 0), new Position(99, 99), movementPattern);

        //Position[] path = grid.GetPath(new Position(0, 0), new Position(99, 99));

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

        if (CheckUp())
        {
            if (CheckLeft() == false && position.X + playersprite.Width > PlayingState.player.position.X + 1)
            {
                position.X -= velocity.X;
            }
            if (CheckRight() == false && position.X + playersprite.Width < PlayingState.player.position.X - 1)
            {
                position.X += velocity.X;
            }
        }
        else
        {
            if (CheckRight() == true && position.Y + playersprite.Height > PlayingState.player.position.Y + 1)
            {
                position.Y -= velocity.Y;
            }
            if (CheckLeft() == true && position.Y + playersprite.Height > PlayingState.player.position.Y + 1)
            {
                position.Y -= velocity.Y;
            }
        }
        if (CheckDown())
        {
            if (CheckLeft() == false && position.X + playersprite.Width > PlayingState.player.position.X + 1)
            {
                position.X -= velocity.X;
            }
            if (CheckRight() == false && position.X + playersprite.Width < PlayingState.player.position.X - 1)
            {
                position.X += velocity.X;
            }
        }
        else
        {
            if (CheckRight() == true && position.Y - playersprite.Height < PlayingState.player.position.Y - 1)
            {
                position.Y += velocity.Y;
            }

            if (CheckLeft() == true && position.Y - playersprite.Height < PlayingState.player.position.Y - 1)
            {
                position.Y += velocity.Y;
            }
        }
    }

    public void CollisionWithEnemy()
    {
        foreach (Enemy enemy in Room.enemies.Children)
        {
            if (enemy != this)
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

    public void Shoot()
    {
        if (PlayingState.player.position.Y > position.Y)
        {
            EnemyBullet bullet = new EnemyBullet(position + new Vector2(sprite.Width / 2 - bulletsprite.Width / 2, sprite.Height));
            bullets.Add(bullet);
        }
        if (PlayingState.player.position.Y < position.Y)
        {
            EnemyBullet bullet = new EnemyBullet(position + new Vector2(sprite.Width / 2 - bulletsprite.Width / 2, 0));
            bullets.Add(bullet);
        }
        if (PlayingState.player.position.X > position.X)
        {
            EnemyBullet bullet = new EnemyBullet(position + new Vector2(sprite.Width, sprite.Height / 2 - bulletsprite.Height / 2));
            bullets.Add(bullet);
        }
        if (PlayingState.player.position.X < position.Y)
        {
            EnemyBullet bullet = new EnemyBullet(position + new Vector2(bulletsprite.Width, sprite.Height / 2 - bulletsprite.Height / 2));
            bullets.Add(bullet);
        }
    }
}



