//using Microsoft.Xna.Framework;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.Xna.Framework.Graphics;

//public class SpamBoss : Boss
//{
//    public static GameObjectList bullets;
//    int Counter = 100;
//    int BulletCounter = 0;

//    public SpamBoss(Vector2 startPosition, Vector2 roomposition, int layer = 0, string id = "Boss") : base(startPosition, roomposition, layer, id)
//    {
//       bullets = new GameObjectList();
//       bulletsprite = GameEnvironment.assetManager.GetSprite("Sprites/EnemyBullet");
//    }


//    public void Range()
//    {
//        Counter--;
//        if (Counter <= 10)
//        {
//            Shoot();
//            BulletCounter++;
//            Counter = 20;
//        }
//        if (BulletCounter == 25)
//        {
//            BulletCounter = 0;
//            Counter = 200;
//        }
//    }

//    public void Shoot()
//    {
//        if (PlayingState.player.position.Y > position.Y)
//        {
//            EnemyBullet bullet = new EnemyBullet(position + new Vector2(sprite.Width / 2 - bulletsprite.Width / 2, sprite.Height));
//            bullets.Add(bullet);
//        }
//        if (PlayingState.player.position.Y < position.Y)
//        {
//            EnemyBullet bullet = new EnemyBullet(position + new Vector2(sprite.Width / 2 - bulletsprite.Width / 2, 0));
//            bullets.Add(bullet);
//        }
//        if (PlayingState.player.position.X > position.X)
//        {
//            EnemyBullet bullet = new EnemyBullet(position + new Vector2(sprite.Width, sprite.Height / 2 - bulletsprite.Height / 2));
//            bullets.Add(bullet);
//        }
//        if (PlayingState.player.position.X < position.Y)
//        {
//            EnemyBullet bullet = new EnemyBullet(position + new Vector2(bulletsprite.Width, sprite.Height / 2 - bulletsprite.Height / 2));
//            bullets.Add(bullet);
//        }
//    }
//    public override void Update(GameTime gameTime)
//    {
//        bullets.Update(gameTime);
//        base.Update(gameTime);
//        if (PlayingState.currentFloor.currentRoom.position == Roomposition)
//            Range();
//    }
//    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
//    {
//        base.Draw(gameTime, spriteBatch);
//        bullets.Draw(gameTime, spriteBatch);
//        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/BearEnemy"), position);
//    }
//}

//}
