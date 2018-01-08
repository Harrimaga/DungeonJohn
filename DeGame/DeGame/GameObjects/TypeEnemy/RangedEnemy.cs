using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;


//TODO: Bullets;
//TODO: Stay at a distance; Range;
//TODO: Sprite;
//TODO: Damage
//TODO: Health
public class RangedEnemy : Enemy
{
    public static GameObjectList bullets;
    int Counter = 300;

    public RangedEnemy(Vector2 startPosition, Vector2 roomposition, int layer = 0, string id = "Enemy") : base(startPosition, roomposition, layer, id)
    {
        bullets = new GameObjectList();
        bulletsprite = GameEnvironment.assetManager.GetSprite("Sprites/Random");
    }

    public void Range()
    {
        Counter--;
        if (PlayingState.player.position.X + range < position.X || PlayingState.player.position.X - range > position.X ||
            PlayingState.player.position.Y + range < position.Y || PlayingState.player.position.Y - range > position.Y)
        {
            Chase();
        }
        if (Counter <= 0)
        {
            Shoot();
            Counter = 300;
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
            bullets.Add(bullet);
        }
        if (PlayingState.player.position.Y < position.Y)
        {
            EnemyBullet bullet = new EnemyBullet(position + new Vector2(sprite.Width / 2 - bulletsprite.Width / 2, 0));
            bullets.Add(bullet);
        }
        if (PlayingState.player.position.X > position.X)
        {
            EnemyBullet bullet = new EnemyBullet(position + new Vector2(sprite.Width, sprite.Height / 2 - bulletsprite.Height / 2));
            bullets.Add(bullet);
        }
        if (PlayingState.player.position.X < position.Y)
        {
            EnemyBullet bullet = new EnemyBullet(position + new Vector2(bulletsprite.Width, sprite.Height / 2 - bulletsprite.Height / 2));
            bullets.Add(bullet);
        }
       
        //if (PlayingState.player.position.Y > position.Y && PlayingState.player.position.X > position.X && PlayingState.player.position.X < position.X)
        //{
        //    EnemyBullet bullet = new EnemyBullet(position + new Vector2(sprite.Width / 2 - bulletsprite.Width / 2, sprite.Height));
        //    bullets.Add(bullet);
        //}
        //if (PlayingState.player.position.Y < position.Y && PlayingState.player.position.X > position.X && PlayingState.player.position.X < position.X)
        //{
        //    EnemyBullet bullet = new EnemyBullet(position + new Vector2(sprite.Width / 2 - bulletsprite.Width / 2, 0));
        //    bullets.Add(bullet);
        //}
        //if (PlayingState.player.position.X > position.X && PlayingState.player.position.Y > position.Y && PlayingState.player.position.Y < position.Y)
        //{
        //    EnemyBullet bullet = new EnemyBullet(position + new Vector2(sprite.Width, sprite.Height / 2 - bulletsprite.Height / 2));
        //    bullets.Add(bullet);
        //}
        //if (PlayingState.player.position.X < position.X && PlayingState.player.position.Y > position.Y && PlayingState.player.position.Y < position.Y)
        //{
        //    EnemyBullet bullet = new EnemyBullet(position + new Vector2(bulletsprite.Width, sprite.Height / 2 - bulletsprite.Height / 2));
        //    bullets.Add(bullet);
        //}
    }
    public override void Update(GameTime gameTime)
    {
        bullets.Update(gameTime);
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
        bullets.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/BearEnemy"), position);
    }
}
