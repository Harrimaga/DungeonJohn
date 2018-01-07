using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

public class Boss1 : Boss
{
    GameObjectList Bullets, HomingBullets;
    int Counter = 300;
    Texture2D playersprite, bulletsprite;
    BossBullet bullet1, bullet2, bullet3;
    public Boss1(Vector2 startPosition, Vector2 roomposition, int layer = 0, string id = "Boss") : base(startPosition, roomposition, layer, id)
    {
        position = startPosition;
        Bullets = new GameObjectList();
        HomingBullets = new GameObjectList();
        playersprite = GameEnvironment.assetManager.GetSprite("Sprites/Random");
        bulletsprite = GameEnvironment.assetManager.GetSprite("Sprites/BossBullet");
        velocity = new Vector2(1, 1);
        velocity.Normalize();
    }

    public override void Update(GameTime gameTime)
    {
        Bullets.Update(gameTime);
        HomingBullets.Update(gameTime);
        Boom();
        base.Update(gameTime);
        Shoot();
        HomingBullet();
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
        if (Counter <= 0 && PlayingState.player.position.Y < position.Y)
        {
            bullet1 = new BossBullet(position);
            bullet2 = new BossBullet(position + new Vector2(sprite.Width / 2, 0));
            bullet3 = new BossBullet(position + new Vector2(sprite.Width - bulletsprite.Width, 0));
            Bullets.Add(bullet1);
            HomingBullets.Add(bullet2);
            Bullets.Add(bullet3);
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
    public void Boom()
    {
        if (health <= 0)
        {
            Rectangle DamageRectangle = new Rectangle((int)position.X - 100, (int)position.Y - 100, 200 + sprite.Width, 200 + sprite.Height);
            if (DamageRectangle.Intersects(PlayingState.player.BoundingBox))
            {
                PlayingState.player.health -= 150;
            }
        }
    }
}