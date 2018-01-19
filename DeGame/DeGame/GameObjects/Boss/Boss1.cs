using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class Boss1 : Boss
{
    BossBullet bullet1, bullet2, bullet3;
    GameObjectList Bullets, HomingBullets;
    Vector2 Roomposition;
    Texture2D bulletsprite;
    int Counter = 30;
    float speed = 0.3f;
    float bulletdamage;
    public Boss1(Vector2 startPosition, Vector2 roomposition, int Difficulty = 0, int layer = 0, string id = "Boss") : base(startPosition, roomposition, "Sprites/Enemies/CowboyBoss",Difficulty , layer, id)
    {
        bulletsprite = GameEnvironment.assetManager.GetSprite("Sprites/Bullets/BossBullet");
        Bullets = new GameObjectList();
        HomingBullets = new GameObjectList();
        velocity = new Vector2(1, 1);
        velocity.Normalize();
        Roomposition = roomposition;
        expGive = (int)(240 * statmultiplier);
        maxhealth = 600 * statmultiplier;
        bulletdamage = 0 * statmultiplier;
        health = maxhealth;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        Bullets.Update(gameTime);
        HomingBullets.Update(gameTime);
        if (PlayingState.currentFloor.currentRoom.position == Roomposition && alive == true)
        {
            Shoot();
        }
        FinalStage();
    }

    public void Shoot()
    {
        bool Hmng = false;
        Counter--;

        if (Counter <= 0)
        {
            Random r = new Random();
            if (r.Next(2) == 1)
            {
                Hmng = true;
            }

            if (PlayingState.player.position.X > position.X + sprite.Width / 2)
            {
                
                bullet1 = new BossBullet(bulletdamage, speed, position + new Vector2(sprite.Width, (sprite.Height / 2) - (2 * bulletsprite.Height) + 10), SpriteEffects.None, Hmng);
                //bullet1 = new BossBullet(bulletdamage + 4, speed, position + new Vector2(sprite.Width / 2 - bulletsprite.Width / 2, 0), SpriteEffects.None, Hmng);
                //bullet1 = new BossBullet(bulletdamage, speed, position + new Vector2(sprite.Width - bulletsprite.Width, 0), SpriteEffects.None, Hmng) ;
                //PlayingState.currentFloor.currentRoom.enemybullets.Add(bullet1);uogifdsfdaasthdjfkjljlk
                //PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].homingenemybullets.Add(bullet2);
                PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemybullets.Add(bullet1);
                Counter = 100;
        
            }
            else if (PlayingState.player.position.X < position.X + sprite.Width / 2)
            {
                bullet1 = new BossBullet(bulletdamage, speed, position + new Vector2(0, (sprite.Height / 2) - (2 *bulletsprite.Height) + 10), SpriteEffects.None , Hmng);
                //bullet2 = new BossBullet(bulletdamage + 4, speed, position + new Vector2(sprite.Width / 2 - bulletsprite.Width / 2, 0), SpriteEffects.FlipHorizontally, Hmng);
                //bullet3 = new BossBullet(bulletdamage, speed, position + new Vector2(sprite.Width - bulletsprite.Width, 0), SpriteEffects.FlipHorizontally, Hmng);
                //PlayingState.currentFloor.currentRoom.enemybullets.Add(bullet1);
                //PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].homingenemybullets.Add(bullet2);
                PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemybullets.Add(bullet1);
                Counter = 100;
            }
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        if (LevelofBoss == 0 || poisoncounter > 0)
            spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/CowboyBoss"), position, color);
        else
            spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/CowboyBoss"), position, Color.MediumVioletRed);
        Bullets.Draw(gameTime, spriteBatch);
        HomingBullets.Draw(gameTime, spriteBatch);
    }
}