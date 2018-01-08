using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

public class RangedEnemy : Enemy
{
    int Counter = 300;

    public RangedEnemy(Vector2 startPosition, Vector2 roomposition, int layer = 0, string id = "Enemy") : base(startPosition, roomposition, layer, id)
    {
        bulletsprite = GameEnvironment.assetManager.GetSprite("Sprites/EnemyBullet");
    }

    public void Range()
    {
        Counter++;
        if (PlayingState.player.position.X + range < position.X || PlayingState.player.position.X - range > position.X ||
            PlayingState.player.position.Y + range < position.Y || PlayingState.player.position.Y - range > position.Y)
        {
            Chase();
        }
        else if(Counter >= 300)
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
        if (PlayingState.player.position.Y > position.Y)
        {
            EnemyBullet bullet = new EnemyBullet(position + new Vector2(sprite.Width / 2 - bulletsprite.Width / 2, sprite.Height));
            Room.enemybullets.Add(bullet);
        }
        if (PlayingState.player.position.Y < position.Y)
        {
            EnemyBullet bullet = new EnemyBullet(position + new Vector2(sprite.Width / 2 - bulletsprite.Width / 2, 0));
            Room.enemybullets.Add(bullet);
        }
        if (PlayingState.player.position.X > position.X)
        {
            EnemyBullet bullet = new EnemyBullet(position + new Vector2(sprite.Width, sprite.Height / 2 - bulletsprite.Height / 2));
            Room.enemybullets.Add(bullet);
        }
        if (PlayingState.player.position.X < position.Y)
        {
            EnemyBullet bullet = new EnemyBullet(position + new Vector2(bulletsprite.Width, sprite.Height / 2 - bulletsprite.Height / 2));
            Room.enemybullets.Add(bullet);
        }
       
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
        //if (health <= 0 && alive == true && PlayingState.currentFloor.currentRoom.position == Roomposition)
        //{
        //    PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemycounter--;
        //    PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].DropConsumable(position);
        //    PlayingState.player.exp += expGive;
        //    alive = false;
        //    GameObjectList.RemovedObjects.Add(this);
        //}
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/ShootingEnemy1"), position);
    }
}
