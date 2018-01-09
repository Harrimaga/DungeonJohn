using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Boss1 : Boss
{
    BossBullet bullet1, bullet2, bullet3;
    GameObjectList Bullets, HomingBullets;
    Vector2 Roomposition;
    int Counter = 300;
    float speed = 0.3f;
    float bulletdamage = 4;
    
    public Boss1(Vector2 startPosition, Vector2 roomposition, int layer = 0, string id = "Boss") : base(startPosition, roomposition, layer, id)
    {
        Bullets = new GameObjectList();
        HomingBullets = new GameObjectList();
        velocity = new Vector2(1, 1);
        velocity.Normalize();
        Roomposition = roomposition;
        expGive = 240;
        maxhealth = 600;
        health = maxhealth;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        Bullets.Update(gameTime);
        HomingBullets.Update(gameTime);
        if (PlayingState.currentFloor.currentRoom.position == Roomposition)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        Counter--;
        if (Counter <= 0)
        {
            bullet1 = new BossBullet(bulletdamage, speed, position);
            bullet2 = new BossBullet(bulletdamage + 4, speed, position + new Vector2(40, 40), true);
            bullet3 = new BossBullet(bulletdamage, speed, position + new Vector2(20, 20));
            Room.enemybullets.Add(bullet1);
            Room.homingenemybullets.Add(bullet2);
            Room.enemybullets.Add(bullet3);
            Counter = 300;
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        Bullets.Draw(gameTime, spriteBatch);
        HomingBullets.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Boss"), position);
    }
}
