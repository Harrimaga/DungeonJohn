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
    Texture2D playersprite;
    BossBullet bullet1, bullet2, bullet3;
    Vector2 Roomposition;

    public Boss1(Vector2 startPosition, Vector2 roomposition, int layer = 0, string id = "Boss") : base(startPosition, roomposition, layer, id)
    {
        Bullets = new GameObjectList();
        HomingBullets = new GameObjectList();
        playersprite = GameEnvironment.assetManager.GetSprite("Sprites/Random");
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
            bullet2 = new BossBullet(position + new Vector2(20, 20));
            bullet3 = new BossBullet(position + new Vector2(40, 40));
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
}
