﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class IceEnemy : Enemy
{
    int Counter = 0;
    float bulletdamage = 5;
    float speed = 1.5f;
    Vector2 direction;

    public IceEnemy(Vector2 startPosition, Vector2 roomposition, int layer = 0, string id = "Enemy") : base(startPosition, roomposition, layer, id)
    {
        position = startPosition;
        bulletsprite = GameEnvironment.assetManager.GetSprite("Sprites/EnemyIceBullet");
        velocity = new Vector2(0.5f, 0.5f);
        Console.WriteLine("Playerposition" + PlayingState.player.position);
        Console.WriteLine("position = " + position);
        Console.WriteLine("direction =" + direction);
    }

    public void Range()
    {
        Counter++;
        if (Counter >= 150)
        {
            if (PlayingState.player.position.X + range > position.X || PlayingState.player.position.X - range < position.X ||
            PlayingState.player.position.Y + range > position.Y || PlayingState.player.position.Y - range < position.Y)
            {
                Shoot();
                Counter = 0;
            }
        }

    }

    public void Shoot()
    {
        Vector2 MiddenOfSprite = new Vector2(sprite.Width / 4, sprite.Height / 4);
        IceBullet bullet = new IceBullet(bulletdamage, speed, position + MiddenOfSprite, direction);
        Room.enemybullets.Add(bullet);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (PlayingState.currentFloor.currentRoom.position == Roomposition)
            Range();
        direction = (PlayingState.player.position - position);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/IceEnemy"), position, null, Color.White, 0f, Vector2.Zero, 1f, Effects, 0f);

    }
}