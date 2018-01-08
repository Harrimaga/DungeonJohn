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
    int Counter = 30;
    Texture2D playersprite, bulletsprite;
    BossBullet bullet1, bullet2, bullet3;
    public Boss1(Vector2 startPosition, Vector2 roomposition, int layer = 0, string id = "Boss") : base(startPosition, roomposition, layer, id)
    {
        Bullets = new GameObjectList();
        HomingBullets = new GameObjectList();
        playersprite = GameEnvironment.assetManager.GetSprite("Sprites/Random");
        bulletsprite = GameEnvironment.assetManager.GetSprite("Sprites/BossBullet");
        velocity = new Vector2(1, 1);
    }

    public override void Update(GameTime gameTime)
    {
        Bullets.Update(gameTime);
        HomingBullets.Update(gameTime);
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
        if (Counter <= 0)
        {
            bullet1 = new BossBullet(position);
            bullet2 = new BossBullet(position + new Vector2(sprite.Width / 2 - bulletsprite.Width / 2, 0));
            bullet3 = new BossBullet(position + new Vector2(sprite.Width - bulletsprite.Width, 0));
            Bullets.Add(bullet1);
            HomingBullets.Add(bullet2);
            Bullets.Add(bullet3);
            Counter = 30;
        }
    }

    public void HomingBullet()
    {
        foreach (BossBullet bullet2 in HomingBullets.Children)
        {
            if (bullet2 != null)
            {
                velocity.Normalize();
                //Vector2 direction = (PlayingState.player.position - bullet2.position);
                //direction.Normalize();
                //bullet2.position += direction;
                if (bullet2.position.Y + playersprite.Height > PlayingState.player.position.Y + 1)
                {
                    bullet2.position.Y -= velocity.Y;
                    velocity.Normalize();
                }
                if (bullet2.position.Y - playersprite.Height < PlayingState.player.position.Y - 1)
                {
                    bullet2.position.Y += velocity.Y;
                    velocity.Normalize();
                }
                if (bullet2.position.X + playersprite.Width > PlayingState.player.position.X + 1)
                {
                    bullet2.position.X -= velocity.X;
                    velocity.Normalize();
                }
                if (bullet2.position.X + playersprite.Width < PlayingState.player.position.X - 1)
                {
                    bullet2.position.X += velocity.X;
                    velocity.Normalize();
                }
            }
        }
    }
}