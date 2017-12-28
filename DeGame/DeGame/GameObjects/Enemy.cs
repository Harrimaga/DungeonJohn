﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoyT.AStar;

public class Enemy : SpriteGameObject
{
    protected float health = 100;
    protected float maxhealth = 100;
    protected float attack;
    protected float attackspeed;
    protected float range;
    protected int counter = 100;
    protected float expGive = 120;
    protected Vector2 basevelocity = new Vector2(2, 2);
    public SpriteEffects Effects;
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
       // base.Update(gameTime);
        healthbar.Update(gameTime, health, maxhealth, position);
        if (CollidesWith(PlayingState.player))
        {
            counter--;
            if (counter == 0)
            {
                velocity = Vector2.Zero;
                //PlayingState.player.health -= 5;
                counter = 100;
            }
        }
        else
            velocity = basevelocity;

        List<GameObject> RemoveBullets = new List<GameObject>();

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
        if (health <= 0)
        {
            GameObjectList.RemovedObjects.Add(this);
            //PlayingState.currentFloor.currentRoom.DropConsumable(position);
            //PlayingState.player.exp += expGive;
            //PlayingState.player.NextLevel();
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        healthbar.Draw(spriteBatch, position);
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

    public virtual void Chase()
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

        if (CheckUp() == true && position.X + playersprite.Width > PlayingState.player.position.X + 1 && CheckLeft() == false)
        {
            position.X -= velocity.X;
        }
        if (CheckUp() == true && position.X + playersprite.Width < PlayingState.player.position.X - 1 && CheckRight() == false)
        {
            position.X += velocity.X;
        }
        if (CheckDown() == true && position.X + playersprite.Width > PlayingState.player.position.X + 1 && CheckLeft() == false)
        {
            position.X -= velocity.X;
        }
        if (CheckDown() == true && position.X + playersprite.Width < PlayingState.player.position.X - 1 && CheckRight() == false)
        {
            position.X += velocity.X;
        }
        if (CheckRight() == true && position.Y - playersprite.Height < PlayingState.player.position.Y - 1 && CheckDown() == false)
        {
            position.Y += velocity.Y;
        }
        if (CheckRight() == true && position.Y + playersprite.Height > PlayingState.player.position.Y + 1 && CheckUp() == false)
        {
            position.Y -= velocity.Y;
        }
        if (CheckLeft() == true && position.Y - playersprite.Height < PlayingState.player.position.Y - 1 && CheckDown() == false)
        {
            position.Y += velocity.Y;
        }
        if (CheckLeft() == true && position.Y + playersprite.Height > PlayingState.player.position.Y + 1 && CheckUp() == false)
        {
            position.Y -= velocity.Y;
        }
    }
}



