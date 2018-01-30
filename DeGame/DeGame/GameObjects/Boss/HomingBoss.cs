using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class HomingBoss : Boss
{
    BossBullet bullet;
    GameObjectList Bullets, HomingBullets;
    Vector2 Roomposition, moving, MoveDirection;
    Texture2D bulletsprite;
    int Counter = 30;
    int MoveTimer = 0;
    float speed = 0.3f;
    float bulletdamage;

    public HomingBoss(Vector2 startPosition, Vector2 roomposition, int Difficulty = 0, int layer = 0, string id = "Boss") : base(startPosition, roomposition, "Sprites/Enemies/CowboyBoss",Difficulty , layer, id)
    {
        bulletsprite = GameEnvironment.assetManager.GetSprite("Sprites/Bullets/BossBullet");
        Bullets = new GameObjectList();
        HomingBullets = new GameObjectList();
        velocity = new Vector2(0.01f, 0.01f);
        velocity.Normalize();
        Roomposition = roomposition;
        expGive = (int)(240 * statmultiplier);
        maxhealth = 650 * statmultiplier;
        bulletdamage = 0 * statmultiplier;
        health = maxhealth;
        contactdamage = 20;
        MoveDirection = Vector2.Zero;
        MoveDirection.Normalize();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        Bullets.Update(gameTime);
        HomingBullets.Update(gameTime);
        if (PlayingState.currentFloor.currentRoom.position == Roomposition && alive == true)
        {
            Shoot(gameTime);
            Move(gameTime);
            velocity = new Vector2(0.01f, 0.01f);
        }
        FinalStage();
    }

    public void Shoot(GameTime gameTime)
    {
        bool Hmng = false;
        Counter -= 1 * gameTime.ElapsedGameTime.Milliseconds;

        if (Counter <= 0)
        {
            Random r = new Random();
            if (r.Next(3) == 1)
            {
                Hmng = true;
            }

            if (PlayingState.player.position.X > position.X + sprite.Width / 2)
            {
                
                bullet = new BossBullet(bulletdamage, speed, position + new Vector2(sprite.Width, (sprite.Height / 2) - (2 * bulletsprite.Height) + 10), SpriteEffects.None, Hmng);
                PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemybullets.Add(bullet);
                Counter = 900;
            }
            else if (PlayingState.player.position.X < position.X + sprite.Width / 2)
            {
                bullet = new BossBullet(bulletdamage, speed, position + new Vector2(0, (sprite.Height / 2) - (2 *bulletsprite.Height) + 10), SpriteEffects.None , Hmng);
                PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemybullets.Add(bullet);
                Counter = 900;
            }
        }
    }

    public void Move(GameTime gameTime)
    {
        MoveTimer++;
        if (MoveTimer < 30)
            MoveDirection = new Vector2(0, -1);
        else if (MoveTimer <= 90 && MoveTimer > 30)
            MoveDirection = new Vector2(0, 1);
        else if (MoveTimer <= 120 && MoveTimer > 90)
            MoveDirection = new Vector2(0, -1);
        else if (MoveTimer > 120)
            MoveTimer = 0;
        moving = MoveDirection * velocity;
        position += moving * gameTime.ElapsedGameTime.Milliseconds;
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