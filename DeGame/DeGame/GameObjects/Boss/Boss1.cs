using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

public class Boss1 : Boss
{
    BossBullet bullet1, bullet2, bullet3;
    GameObjectList Bullets, HomingBullets;
    Vector2 Roomposition;
    Texture2D bulletsprite;
    int Counter = 30;
    float speed = 0.3f;
    float bulletdamage = 0;

    public Boss1(Vector2 startPosition, Vector2 roomposition, int layer = 0, string id = "Boss") : base(startPosition, roomposition, layer, id)
    {
        bulletsprite = GameEnvironment.assetManager.GetSprite("Sprites/Bullets/BossBullet");
        Bullets = new GameObjectList();
        HomingBullets = new GameObjectList();
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
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/Boss"), position);
        Bullets.Draw(gameTime, spriteBatch);
        HomingBullets.Draw(gameTime, spriteBatch);
    }
    public void Shoot()
    {
        Counter--;
        if (Counter <= 0)
        {
            bullet1 = new BossBullet(bulletdamage, speed, position);
            bullet2 = new BossBullet(bulletdamage + 4, speed, position + new Vector2(sprite.Width / 2 - bulletsprite.Width / 2, 0), true);
            bullet3 = new BossBullet(bulletdamage, speed, position + new Vector2(sprite.Width - bulletsprite.Width, 0));
            Room.enemybullets.Add(bullet1);
            Room.homingenemybullets.Add(bullet2);
            Room.enemybullets.Add(bullet3);
            Counter = 300;
            //if (PlayingState.player.position.Y < position.Y && PlayingState.player.position.X < position.X + sprite.Width && PlayingState.player.position.X > position.X)
            //{
            //    bullet1 = new BossBullet(bulletdamage, speed, position);
            //    bullet2 = new BossBullet(bulletdamage + 4, speed, position + new Vector2(sprite.Width / 2 - bulletsprite.Width / 2, 0), true);
            //    bullet3 = new BossBullet(bulletdamage, speed, position + new Vector2(sprite.Width - bulletsprite.Width, 0));
            //    Room.enemybullets.Add(bullet1);
            //    Room.homingenemybullets.Add(bullet2);
            //    Room.enemybullets.Add(bullet3);
            //    Counter = 300;
            //}
            //if (PlayingState.player.position.Y < position.Y && PlayingState.player.position.X > position.X + sprite.Width)
            //{
            //    bullet1 = new BossBullet(bulletdamage, speed, position + new Vector2(sprite.Width / 2 - bulletsprite.Width / 2, 0));
            //    bullet2 = new BossBullet(bulletdamage + 4, speed, position + new Vector2(sprite.Width - bulletsprite.Width, 0), true);
            //    bullet3 = new BossBullet(bulletdamage, speed, position + new Vector2(sprite.Width - bulletsprite.Width, sprite.Height / 2 - bulletsprite.Height / 2));
            //    Room.enemybullets.Add(bullet1);
            //    Room.homingenemybullets.Add(bullet2);
            //    Room.enemybullets.Add(bullet3);
            //    Counter = 30;
            //}
            //if(PlayingState.player.position.Y > position.Y && PlayingState.player.position.X > position.X + sprite.Width && PlayingState.player.position.Y < position.Y + sprite.Height)
            //{
            //    bullet1 = new BossBullet(bulletdamage, speed, position + new Vector2(sprite.Width - bulletsprite.Width, 0));
            //    bullet2 = new BossBullet(bulletdamage + 4, speed, position + new Vector2(sprite.Width - bulletsprite.Width, sprite.Height / 2 - bulletsprite.Height / 2), true);
            //    bullet3 = new BossBullet(bulletdamage, speed, position + new Vector2(sprite.Width - bulletsprite.Width, sprite.Height - bulletsprite.Height));
            //    Room.enemybullets.Add(bullet1);
            //    Room.homingenemybullets.Add(bullet2);
            //    Room.enemybullets.Add(bullet3);
            //    Counter = 300;
            //}
            //if (PlayingState.player.position.Y > position.Y + sprite.Height && PlayingState.player.position.X > position.X + sprite.Width && PlayingState.player.position.X > position.X + sprite.Width)
            //{
            //    bullet1 = new BossBullet(bulletdamage, speed, position + new Vector2(sprite.Width - bulletsprite.Width, sprite.Height / 2 - bulletsprite.Height / 2));
            //    bullet2 = new BossBullet(bulletdamage + 4, speed, position + new Vector2(sprite.Width - bulletsprite.Width, sprite.Height - bulletsprite.Height), true);
            //    bullet3 = new BossBullet(bulletdamage, speed, position + new Vector2(sprite.Width / 2 - bulletsprite.Width / 2, sprite.Height - bulletsprite.Height));
            //    Room.enemybullets.Add(bullet1);
            //    Room.homingenemybullets.Add(bullet2);
            //    Room.enemybullets.Add(bullet3);
            //    Counter = 300;
            //}
            //if (PlayingState.player.position.Y > position.Y + sprite.Height && PlayingState.player.position.X > position.X + sprite.Width && PlayingState.player.position.X > position.X + sprite.Width)
            //{
            //    bullet1 = new BossBullet(bulletdamage, speed, position + new Vector2(sprite.Width - bulletsprite.Width, sprite.Height - bulletsprite.Height));
            //    bullet2 = new BossBullet(bulletdamage + 4, speed, position + new Vector2(sprite.Width / 2 - bulletsprite.Width / 2, sprite.Height - bulletsprite.Height), true);
            //    bullet3 = new BossBullet(bulletdamage, speed, position + new Vector2(0, sprite.Height - bulletsprite.Height));
            //    Room.enemybullets.Add(bullet1);
            //    Room.homingenemybullets.Add(bullet2);
            //    Room.enemybullets.Add(bullet3);
            //    Counter = 300;
            //}
            //if (PlayingState.player.position.Y > position.Y + sprite.Height && PlayingState.player.position.X > position.X + sprite.Width && PlayingState.player.position.X > position.X + sprite.Width)
            //{
            //    bullet1 = new BossBullet(bulletdamage, speed, position + new Vector2(sprite.Width / 2 - bulletsprite.Width / 2, sprite.Height - bulletsprite.Height));
            //    bullet2 = new BossBullet(bulletdamage + 4, speed, position + new Vector2(0, sprite.Height - bulletsprite.Height), true);
            //    bullet3 = new BossBullet(bulletdamage, speed, position + new Vector2(0, sprite.Height / 2 - bulletsprite.Height / 2));
            //    Room.enemybullets.Add(bullet1);
            //    Room.homingenemybullets.Add(bullet2);
            //    Room.enemybullets.Add(bullet3);
            //    Counter = 300;
            //}
            ////if (PlayingState.player.position.Y > position.Y + sprite.Height && PlayingState.player.position.X > position.X + sprite.Width && PlayingState.player.position.X > position.X + sprite.Width)
            ////{
            ////    bullet1 = new BossBullet(bulletdamage, speed, position);
            ////    bullet2 = new BossBullet(bulletdamage + 4, speed, position + new Vector2(0, sprite.Height / 2 - bulletsprite.Height / 2), true);
            ////    bullet3 = new BossBullet(bulletdamage, speed, position + new Vector2(0, sprite.Height / 2 - bulletsprite.Height / 2));
            ////    Room.enemybullets.Add(bullet1);
            ////    Room.homingenemybullets.Add(bullet2);
            ////    Room.enemybullets.Add(bullet3);
            ////    Counter = 300;
            ////}
        }
    }
}