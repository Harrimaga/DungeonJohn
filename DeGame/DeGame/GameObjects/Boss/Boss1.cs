using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

public class Boss1 : Boss
{
    GameObjectList Bullets;
    int Counter = 300;

    public Boss1(Vector2 startPosition, Vector2 roomposition, int layer = 0, string id = "Boss") : base(startPosition, roomposition, layer, id)
    {
        Bullets = new GameObjectList();
    }

    public override void Update(GameTime gameTime)
    {
        Bullets.Update(gameTime);
        base.Update(gameTime);
        Shoot();
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Boss"), position);
        Bullets.Draw(gameTime, spriteBatch);
    }
    public void Shoot()
    {
        Counter--;
        if (Counter <= 0)
        {
            BossBullet bullet1 = new BossBullet(position);
            BossBullet bullet2 = new BossBullet(position + new Vector2(20, 20));
            BossBullet bullet3 = new BossBullet(position + new Vector2(40, 40));
            Bullets.Add(bullet1);
            Bullets.Add(bullet2);
            Bullets.Add(bullet3);
            Counter = 300;
        }
    }
}
