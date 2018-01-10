using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class RangedEnemy : Enemy
{
    int Counter = 300;
    float bulletdamage = 5;
    float speed = 2f;
    Vector2 direction;
    public RangedEnemy(Vector2 startPosition, Vector2 roomposition, int layer = 0, string id = "Enemy") : base(startPosition, roomposition, layer, id)
    {
        position = startPosition;
        bulletsprite = GameEnvironment.assetManager.GetSprite("Sprites/Bullets/EnemyBullet");
        velocity = new Vector2(0.5f, 0.5f);
        Console.WriteLine("Playerposition" + PlayingState.player.position);
        Console.WriteLine("position = " + position);
        Console.WriteLine("direction =" + direction);
    }

    public void Range()
    {
        Counter++;
        if (PlayingState.player.position.X + range < position.X || PlayingState.player.position.X - range > position.X ||
           PlayingState.player.position.Y + range < position.Y || PlayingState.player.position.Y - range > position.Y)
        {
        Chase();
        }
        if(Counter >= 300)
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
        Vector2 MiddenOfSprite = new Vector2(sprite.Width / 2, sprite.Height / 2);
        EnemyBullet bullet = new EnemyBullet(bulletdamage, speed, position + MiddenOfSprite, direction);
        Room.enemybullets.Add(bullet);

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
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Enemies/ShootingEnemy1"), position, null, Color.White, 0f, Vector2.Zero, 1f, Effects, 0f);
    }
}
