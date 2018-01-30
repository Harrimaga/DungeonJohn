﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public class Enemy : SpriteGameObject
{
    public float health;
    protected float maxhealth;
    protected float attack;
    protected float contactdamage;
    protected float attackspeed;
    protected float EnemyLevel;
    protected float statmultiplier;
    protected float range = 100;
    protected float expGive = 50;
    protected float bulletdamage;
    protected bool drop = true, flying = false, backgroundenemy = false, bossenemy = false, killable = true, moving = true, hpdisplay = false;
    protected int counter = 0, poisoncounter = 0, hitcounter = 0;
    protected Vector2 direction, basevelocity = Vector2.Zero, PlayerOrigin;
    protected SpriteEffects Effects;
    protected Texture2D playersprite, bulletsprite;
    HealthBar healthbar;
    protected Vector2 Roomposition;
    Vector2 actualvelocity;
    public bool alive = true;
    string AssetName;
    protected Color color = Color.White;

    public Enemy(Vector2 startPosition, Vector2 roomposition, string assetname, int difficulty = 0, int layer = 0, string id = "Enemy")
    : base(assetname, layer, id)
    {
        playersprite = GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerDown");
        if (difficulty > 0)
            statmultiplier = (float)(difficulty - 1) / 10 + 1;
        else
            statmultiplier = (float)difficulty / 10 + 1;
        EnemyLevel = difficulty;
        position = startPosition;
        velocity = basevelocity;
        Roomposition = roomposition;
        AssetName = assetname;
        healthbar = new HealthBar(health, maxhealth, position);
    }

    /// <summary>
    /// Updates the PlayerOrigin to its currentposition; Checks if it is hit with a bullet and if so, takes damage; Handles poisondamage.
    /// </summary>
    /// <param name="gameTime"></param>
    public override void Update(GameTime gameTime)
    {
        bool ok;
        base.Update(gameTime);
        PlayerOrigin = new Vector2(PlayingState.player.position.X + playersprite.Width / 2, PlayingState.player.position.Y + playersprite.Height / 2);
        healthbar.Update(gameTime, health, maxhealth, position);
        PlayerCollision();
        if (!flying)
            SolidCollision();
        counter -= 1 * gameTime.ElapsedGameTime.Milliseconds;

        if (moving)
        {
            actualvelocity = velocity * direction;
            if (float.IsNaN(direction.X))
                actualvelocity = Vector2.Zero;
            position.X += actualvelocity.X * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            position.Y += actualvelocity.Y * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }
        List<GameObject> RemoveBullets = new List<GameObject>();
        CollisionWithEnemy();
        foreach (Bullet bullet in PlayingState.player.bullets.Children)
            if (CollidesWith(bullet))
            {
                health -= PlayingState.player.attack;
                if (PlayingState.player.poisonchance > 0 && bullet.poisonbullet)
                    poisoncounter = 5000;
                RemoveBullets.Add(bullet);
                hitcounter = 200;
            }
        foreach (Bullet bullet in RemoveBullets)
            PlayingState.player.bullets.Remove(bullet);
        RemoveBullets.Clear();
        if (hitcounter > 0)
        {
            hitcounter -= gameTime.ElapsedGameTime.Milliseconds;
            color = Color.Salmon;
        }
        else if (poisoncounter > 0)
        {
            color = Color.YellowGreen;
            if (poisoncounter % 600 <= 20 && poisoncounter < 5000)
            {
                health -= 4;
                color = Color.Salmon;
            }
            poisoncounter -= gameTime.ElapsedGameTime.Milliseconds;
        }
        else
            color = Color.White;
        CheckAlive();
    }

    /// <summary>
    /// If it is shot by the enemy, determines if it is possible to be killed and if it is killed drops consumable and removes it.
    /// </summary>
    public void CheckAlive()
    {
        if ((int)health <= 0 && alive == true && killable || (bossenemy && EndRoom.cleared))
        {
            if (!bossenemy)
                PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemycounter--;
            if (drop)
                PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].DropConsumable(position);
            PlayingState.player.exp += expGive;
            alive = false;
            GameObjectList.RemovedObjects.Add(this);
        }
    }

    /// <summary>
    /// Makes the enemy chase after the player
    /// </summary>
    public void Chase()
    {
        direction = PlayerOrigin - position;
        direction.Normalize();
    }

    /// <summary>
    /// Checks if it collides with anaother enemy and if it does moves them apart.
    /// </summary>
    protected void CollisionWithEnemy()
    {
        if (!backgroundenemy)
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
    }

    /// <summary>
    /// Checks if it collides with solids and/or door and if so moves them so the dont pass through them.
    /// </summary>
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
        foreach (Door d in Room.door.Children)
        {
            while (CollidesWith(d))
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

    /// <summary>
    /// Checks if it collides with player and if so, it does damage.
    /// </summary>
    protected void PlayerCollision()
    {
        if (CollidesWith(PlayingState.player))
        {
            direction = Vector2.Zero;
            if (counter <= 0)
            {
                PlayingState.player.TakeDamage(contactdamage);
                counter = 500;
            }
        }
    }

    /// <summary>
    /// Draws the enemy and flips it so it faces the player; Draws the healtbar if the enemy should have one.
    /// </summary>
    /// <param name="gameTime"></param>
    /// <param name="spriteBatch"></param>
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
        if (killable || hpdisplay)
        {
            healthbar.Draw(spriteBatch);
        }
    }
}



