using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

public class Boss1 : Boss
{
    float bulletdamage = 4;
    GameObjectList Bullets, HomingBullets;
    int Counter = 300;
    Texture2D playersprite, bulletsprite;
    BossBullet bullet1, bullet2, bullet3;
    Vector2 Roomposition;

    public Boss1(Vector2 startPosition, Vector2 roomposition, int layer = 0, string id = "Boss") : base(startPosition, roomposition, layer, id)
    {
        Bullets = new GameObjectList();
        HomingBullets = new GameObjectList();
        playersprite = GameEnvironment.assetManager.GetSprite("Sprites/Random");
        bulletsprite = GameEnvironment.assetManager.GetSprite("Sprites/BossBullet");
        velocity = new Vector2(1, 1);
        velocity.Normalize();
        Roomposition = roomposition;
    }

    public override void Update(GameTime gameTime)
    {
        Bullets.Update(gameTime);
        HomingBullets.Update(gameTime);
        base.Update(gameTime);
        if (PlayingState.currentFloor.currentRoom.position == Roomposition)
        {
            Shoot();
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Boss"), position);
        Bullets.Draw(gameTime, spriteBatch);
        HomingBullets.Draw(gameTime, spriteBatch);
    }
    public void Shoot()
    {
        Counter--;
        if (Counter <= 0)
        {
            bullet1 = new BossBullet(bulletdamage, position);
            bullet2 = new BossBullet(bulletdamage, position + new Vector2(20, 20));
            bullet3 = new BossBullet(bulletdamage + 4, position + new Vector2(40, 40), true);
            Room.enemybullets.Add(bullet1);
            Room.enemybullets.Add(bullet2);
            Room.homingenemybullets.Add(bullet3);
            Counter = 300;
        }
    }

    public void HomingBullet()
    {
        foreach (BossBullet bullet2 in HomingBullets.Children)
        {
            if (bullet2 != null)
            {
                if (bullet2.position.Y + playersprite.Height > PlayingState.player.position.Y + 1)
                {
                    bullet2.position.Y -= velocity.Y;
                }
                if (bullet2.position.Y - playersprite.Height < PlayingState.player.position.Y - 1)
                {
                    bullet2.position.Y += velocity.Y;
                }
                if (bullet2.position.X + playersprite.Width > PlayingState.player.position.X + 1)
                {
                    bullet2.position.X -= velocity.X;
                }
                if (bullet2.position.X + playersprite.Width < PlayingState.player.position.X - 1)
                {
                    bullet2.position.X += velocity.X;
                }
            }
        }
    }
}
