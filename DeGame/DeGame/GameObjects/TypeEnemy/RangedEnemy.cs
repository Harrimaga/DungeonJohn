using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class RangedEnemy : Enemy
{
    int Counter = 175;
    float bulletdamage;
    float speed = 0.13f;
    Vector2 direction;
    
    public RangedEnemy(Vector2 startPosition, Vector2 roomposition, int Difficulty = 0, int layer = 0, string id = "Enemy") : base(startPosition, roomposition, "Sprites/Enemies/ShootingEnemy1", Difficulty, layer, id)
    {
        position = startPosition;
        bulletsprite = GameEnvironment.assetManager.GetSprite("Sprites/Bullets/EnemyBullet");
        velocity = new Vector2(0.05f, 0.05f);
        bulletdamage = 3 * statmultiplier;
        health = 100 * statmultiplier;
        maxhealth = 100 * statmultiplier;
        expGive = 100 * statmultiplier;
    }

    public void Range()
    {
        Counter++;
        if (PlayingState.player.position.X + range < position.X || PlayingState.player.position.X - range > position.X ||
           PlayingState.player.position.Y + range < position.Y || PlayingState.player.position.Y - range > position.Y)
        {
            Chase();
        }
        if(Counter >= 175)
        {
            Shoot();
            Counter = 0;
        }
    }

    //public Circle PlayerCircle
    //{
    //    get
    //    {
    //        int radius = 200;
    //        int x = (int)PlayingState.player.position.X;
    //        int y = (int)PlayingState.player.position.Y;
    //        return new Circle(x, y, radius);
    //    }        
    //}
    public void Shoot()
    {
        Vector2 middleofsprite = new Vector2(sprite.Width / 2, sprite.Height / 2);
        Vector2 direction = (PlayingState.player.position - position);
        EnemyBullet bullet = new EnemyBullet(bulletdamage, speed, position + middleofsprite, direction, GameEnvironment.assetManager.GetSprite("Sprites/Bullets/EnemyBullet"));
        PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemybullets.Add(bullet);

        //if (PlayingState.player.position.Y > position.Y)
        //{
        //    EnemyBullet bullet = new EnemyBullet(bulletdamage, speed, position + new Vector2(sprite.Width / 2 - bulletsprite.Width / 2, sprite.Height));
        //    Room.enemybullets.Add(bullet);
        //}
        //if (PlayingState.player.position.Y < position.Y)
        //{
        //    EnemyBullet bullet = new EnemyBullet(bulletdamage, speed, position + new Vector2(sprite.Width / 2 - bulletsprite.Width / 2, 0));
        //    Room.enemybullets.Add(bullet);
        //}
        //if (PlayingState.player.position.X > position.X)
        //{
        //    EnemyBullet bullet = new EnemyBullet(bulletdamage, speed, position + new Vector2(sprite.Width, sprite.Height / 2 - bulletsprite.Height / 2));
        //    Room.enemybullets.Add(bullet);
        //}
        //if (PlayingState.player.position.X < position.Y)
        //{
        //    EnemyBullet bullet = new EnemyBullet(bulletdamage, speed, position + new Vector2(bulletsprite.Width, sprite.Height / 2 - bulletsprite.Height / 2));
        //    Room.enemybullets.Add(bullet);
        //}

        //if (PlayingState.player.position.Y > position.Y && PlayingState.player.position.X > position.X && PlayingState.player.position.X < position.X)
        //{
        //    EnemyBullet bullet = new EnemyBullet(position + new Vector2(sprite.Width / 2 - bulletsprite.Width / 2, sprite.Height));
        //    Room.enemybullets.Add(bullet);
        //}
        //if (PlayingState.player.position.Y < position.Y && PlayingState.player.position.X > position.X && PlayingState.player.position.X < position.X)
        //{
        //    EnemyBullet bullet = new EnemyBullet(position + new Vector2(sprite.Width / 2 - bulletsprite.Width / 2, 0));
        //    Room.enemybullets.Add(bullet);
        //}
        //if (PlayingState.player.position.X > position.X && PlayingState.player.position.Y > position.Y && PlayingState.player.position.Y < position.Y)
        //{
        //    EnemyBullet bullet = new EnemyBullet(position + new Vector2(sprite.Width, sprite.Height / 2 - bulletsprite.Height / 2));
        //    Room.enemybullets.Add(bullet);
        //}
        //if (PlayingState.player.position.X < position.X && PlayingState.player.position.Y > position.Y && PlayingState.player.position.Y < position.Y)
        //{
        //    EnemyBullet bullet = new EnemyBullet(position + new Vector2(bulletsprite.Width, sprite.Height / 2 - bulletsprite.Height / 2));
        //    Room.enemybullets.Add(bullet);
        //}
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
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/ShootingEnemy1"), position, null, color, 0f, Vector2.Zero, 1f, Effects, 0f);
    }
}
