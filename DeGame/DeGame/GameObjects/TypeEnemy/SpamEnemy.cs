﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class SpamEnemy : Enemy
{
    int Counter = 50;
    int BulletCounter = 0;
    float bulletdamage = 3;
    float speed = 3f;

    public SpamEnemy(Vector2 startPosition, Vector2 roomposition, int layer = 0, string id = "Enemy") : base(startPosition, roomposition, layer, id)
    {
        bulletsprite = GameEnvironment.assetManager.GetSprite("Sprites/EnemyBullet");
        basevelocity = new Vector2(0, 0);
    }

    public void Range()
    {
        Counter--;

        if (Counter <= 10)
        {
            Shoot();
            BulletCounter++;
            Counter = 20;
        }
        if (BulletCounter == 30)
        {
            BulletCounter = 0;
            Counter = 100;
        }
    }

    public void Shoot()
    {
        Vector2 middleofsprite = new Vector2(sprite.Width / 2, sprite.Height / 2);
        EnemyBullet bullet = new EnemyBullet(bulletdamage, speed, position + middleofsprite, GameEnvironment.assetManager.GetSprite("Sprites/EnemyBullet"));
        Room.enemybullets.Add(bullet);

        //if (PlayingState.player.position.Y > position.Y)
        //{
        //    EnemyBullet bullet = new EnemyBullet(bulletdamage, speed, position + middleofsprite);
        //    Room.enemybullets.Add(bullet);
        //}
        //if (PlayingState.player.position.Y < position.Y)
        //{
        //    EnemyBullet bullet = new EnemyBullet(bulletdamage, speed, position + middleofsprite);
        //    Room.enemybullets.Add(bullet);
        //}
        //if (PlayingState.player.position.X > position.X)
        //{
        //    EnemyBullet bullet = new EnemyBullet(bulletdamage, speed, position + middleofsprite);
        //    Room.enemybullets.Add(bullet);
        //}
        //if (PlayingState.player.position.X < position.Y)
        //{
        //    EnemyBullet bullet = new EnemyBullet(bulletdamage, speed, position + middleofsprite);
        //    Room.enemybullets.Add(bullet);
        //}
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (PlayingState.currentFloor.currentRoom.position == Roomposition)
            Range();
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/CutieEnemyPixel"), position, null, Color.White, 0f, Vector2.Zero, 1f, Effects, 0f);
    }
}