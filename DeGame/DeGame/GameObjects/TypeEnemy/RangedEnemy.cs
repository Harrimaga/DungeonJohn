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
    }

    public void Range()
    {
        Counter--;
        if (PlayingState.player.position.X + range < position.X || PlayingState.player.position.X - range > position.X ||
            PlayingState.player.position.Y + range < position.Y || PlayingState.player.position.Y - range > position.Y)
        {
            Chase();
        }
        else if (Counter <= 0)
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
        EnemyBullet bullet = new EnemyBullet(position);
        bullets.Add(bullet);
    }
    public override void Update(GameTime gameTime)
    {
        bullets.Update(gameTime);
        base.Update(gameTime);
        if (PlayingState.currentFloor.currentRoom.position == Roomposition)
            Range();
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        bullets.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/BearEnemy"), position);
    }
}
