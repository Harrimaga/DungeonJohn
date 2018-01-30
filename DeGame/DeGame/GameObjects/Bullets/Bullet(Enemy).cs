﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

class E_Bullet : SpriteGameObject
{
    protected float Damage, Speed;
    Random random;
    int reflectchance = 0, blockchance = 0;
    protected bool reflected = false;
    protected bool changedirection = false;
    Vector2 Roomposition;

    public E_Bullet(float damage, float speed, string assetname, int layer = 0, string id = "EnemyBullet") : base(assetname, layer, id)
    {
        Damage = damage;
        Speed = speed;
        random = new Random();
        Roomposition = PlayingState.currentFloor.currentRoom.position;
    }

    /// <summary>
    /// Executes check collission
    /// </summary>
    public override void Update(GameTime gameTime)
    {
        CheckCollision();
    }

    /// <summary>
    /// Checks if the bullet collides with enemies or bosses when it is reflected and if it collides deals damage; Decides the change of it being blocked/reflected by an equiped shield,
    /// if its either of those it removes itself without doing damage, if not it does do damage; If it Collides with Solids and/or door it removes itself;
    /// </summary>
    public void CheckCollision()
    {
        if (reflected)
        {
            foreach (Enemy e in PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemies.Children)
                if (CollidesWith(e))
                {
                    e.health -= Damage;
                    GameObjectList.RemovedObjects.Add(this);
                }
            foreach (Boss b in PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].bosses.Children)
                if (CollidesWith(b))
                {
                    b.health -= Damage;
                    GameObjectList.RemovedObjects.Add(this);
                }
        }
        else
        {
            reflectchance = 100;
            blockchance = 100;
            if (PlayingState.player.CrestShield)
                blockchance = random.Next(100);
            else if (PlayingState.player.Mirror)
                reflectchance = random.Next(100);

            if (CollidesWith(PlayingState.player))
            {
                if (reflectchance < 20)
                {
                    PlayingState.player.TakeDamage((float)(Damage * PlayingState.player.damagereduction / 2));
                    reflected = true;
                    Speed += 1;
                    changedirection = true;
                }
                else if (blockchance < 20)
                {
                    GameObjectList.RemovedObjects.Add(this);
                    return;
                }
                else
                {
                    PlayingState.player.TakeDamage(Damage);
                    GameObjectList.RemovedObjects.Add(this);
                    return;
                }
            }
        }

        foreach (Solid solid in PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].solid.Children)
        {
            if (CollidesWith(solid) && solid.hittable)
            {
                GameObjectList.RemovedObjects.Add(this);
                return;
            }
        }

        foreach (Door door in Room.door.Children)
        {
            if (CollidesWith(door))
            {
                GameObjectList.RemovedObjects.Add(this);
                return;
            }
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
    }
}

