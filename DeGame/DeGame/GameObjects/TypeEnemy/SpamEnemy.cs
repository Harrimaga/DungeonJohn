using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class SpamEnemy : Enemy
{
    public static GameObjectList bullets;
    int Counter = 50;
    int BulletCounter = 0;

    public SpamEnemy(Vector2 startPosition, Vector2 roomposition, int layer = 0, string id = "Enemy") : base(startPosition, roomposition, layer, id)
    {
        bullets = new GameObjectList();
        bulletsprite = GameEnvironment.assetManager.GetSprite("Sprites/EnemyBullet");
    }

    public void Range()
    {
        Counter--;

        if (Counter <= 10)
        {
            Shoot();
            BulletCounter++;
            Counter = 15;
        }
        if (BulletCounter == 30)
        {
            BulletCounter = 0;
            Counter = 100;
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
    public override void Update(GameTime gameTime)
    {
        bullets.Update(gameTime);
        base.Update(gameTime);
        if (PlayingState.currentFloor.currentRoom.position == Roomposition)
            Range();
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        bullets.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/BearEnemy"), position);
    }
}

