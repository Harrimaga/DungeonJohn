using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class TurretUpEnemy : Enemy
{
    int Counter = 0;
    float bulletdamage = 3;
    float speed = 3f;
    Vector2 direction;

    public TurretUpEnemy(Vector2 startPosition, Vector2 roomposition, int layer = 0, string id = "Enemy") : base(startPosition, roomposition, layer, id)
    {
        direction = new Vector2(0, -1);
    }

    public void Shoot()
    {
        Counter++;
        Vector2 MiddenOfSprite = new Vector2(sprite.Width / 2-25, sprite.Height / 2-35);
        if (Counter >= 50)
        {
            EnemyBullet bullet = new EnemyBullet(bulletdamage, speed, position + MiddenOfSprite, direction);
            Room.enemybullets.Add(bullet);
            Counter = 0;
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (PlayingState.currentFloor.currentRoom.position == Roomposition)
            Shoot();
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/TurretEnemyUp"), position);
    }
}