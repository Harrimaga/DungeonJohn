﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class TurretEnemy : Enemy
{
    int Counter = 0, Directioncount;
    float bulletdamage = 3;
    float speed = 3f;
    Vector2 direction;

    public TurretEnemy(Vector2 startPosition, Vector2 roomposition, int directioncount, int layer = 0, string id = "TurretEnemy") : base(startPosition, roomposition, layer, id)
    {

        Directioncount = directioncount;
        switch (Directioncount)
        {
            case 1:
                direction = new Vector2(0, -1);
                break;
            case 2:
                direction = new Vector2(0, 1);
                break;
            case 3:
                direction = new Vector2(-1, 0);
                break;
            default:
                direction = new Vector2(1, 0);
                break;
        }
    }

    public void Shoot()
    {
        Counter++;
        Vector2 MiddleOfSprite = new Vector2(sprite.Width / 2 - 25, sprite.Height / 2 + 10);
        if (Counter >= 50)
        {
            EnemyBullet bullet = new EnemyBullet(bulletdamage, speed, position + MiddleOfSprite, direction, GameEnvironment.assetManager.GetSprite("Sprites/Bullets/EnemyBullet"));
            Room.enemybullets.Add(bullet);
            Counter = 0;
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (PlayingState.currentFloor.currentRoom.position == Roomposition)
            Shoot();
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        switch (Directioncount)
        {
            case 1:
                spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/TurretEnemyUp"), position);
                break;
            case 2:
                spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/TurretEnemyDown"), position);
                break;
            case 3:
                spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/TurretEnemyLeft"), position);
                break;
            default:
                spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/TurretEnemyRight"), position);
                break;
        }
    }
}
