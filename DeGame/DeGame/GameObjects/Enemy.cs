using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public class Enemy : SpriteGameObject
{
    public float health;
    protected float maxhealth = 100;
    protected float attack;
    protected float attackspeed;
    protected float range = 300;
    protected float expGive = 120;
    protected bool alive = true, drop = true, flying = false, backgroundenemy = false, bossenemy = false, killable = true;
    protected int counter = 100;
    protected Vector2 basevelocity = Vector2.Zero;
    public SpriteEffects Effects;
    public Texture2D playersprite, bulletsprite;
    HealthBar healthbar;
    protected Vector2 Roomposition;

    public Enemy(Vector2 startPosition, Vector2 roomposition, int layer = 0, string id = "Enemy")
    : base("Sprites/Enemies/BearEnemy", layer, id)
    {
        healthbar = new HealthBar(health, maxhealth, position);
        playersprite = GameEnvironment.assetManager.GetSprite("Sprites/PlayerFront");
        position = startPosition;
        velocity = basevelocity;
        Roomposition = roomposition;
        health = maxhealth;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
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
        healthbar.Update(gameTime, health, maxhealth, position);
        if (health <= 0 && alive == true && killable || (PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].Type == "bossroom" && EndRoom.cleared && bossenemy))
        {
            PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemycounter--;
            if (drop)
                PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].DropConsumable(position);
            PlayingState.player.exp += expGive;
            alive = false;
            GameObjectList.RemovedObjects.Add(this);
        }
        if (position.X + playersprite.Width > PlayingState.player.position.X + 1 && CheckLeft() == false)
        {
            Effects = SpriteEffects.None;
        }
        if (position.X + playersprite.Width < PlayingState.player.position.X - 1 && CheckRight() == false)
        {
            Effects = SpriteEffects.FlipHorizontally;
        }
        Chase();
    }

    public bool CheckDown()
    {
        if (flying)
            return false;
        Rectangle CheckDown = new Rectangle((int)position.X, (int)position.Y + sprite.Height, 60, 60);
        foreach (Solid solid in Room.solid.Children)
            if (CheckDown.Intersects(solid.BoundingBox))
                return true;       
        return false;
    }
    public bool CheckUp()
    {
        if (flying)
            return false;
        Rectangle CheckUp = new Rectangle((int)position.X, (int)position.Y - 60, 60, 60);
        foreach (Solid solid in Room.solid.Children)
            if (CheckUp.Intersects(solid.BoundingBox))            
                return true;            
        return false;
    }
    public bool CheckLeft()
    {
        if (flying)
            return false;
        Rectangle CheckLeft = new Rectangle((int)position.X - 60, (int)position.Y, 60, 60);
        foreach (Solid solid in Room.solid.Children)
            if (CheckLeft.Intersects(solid.BoundingBox))            
                return true;            
        return false;
    }
    public bool CheckRight()
    {
        if (flying)
            return false;
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

        if (position.Y + playersprite.Height / 2 > PlayingState.player.position.Y + 1 && CheckUp() == false)
        {
            position.Y -= velocity.Y;
        }
        if (position.Y - playersprite.Height / 2 < PlayingState.player.position.Y - 1 && CheckDown() == false)
        {
            position.Y += velocity.Y;
        }
        if (position.X + playersprite.Width / 2 > PlayingState.player.position.X + 1 && CheckLeft() == false)
        {
            position.X -= velocity.X;
        }
        if (position.X + playersprite.Width / 2 < PlayingState.player.position.X - 1 && CheckRight() == false)
        {
            position.X += velocity.X;
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

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (killable)
        {
            healthbar.Draw(spriteBatch);
        }
    }
}



