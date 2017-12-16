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
    public GameObjectList bullets;
    int counter = 60;

    public RangedEnemy(Vector2 startPosition, int layer = 0, string id = "Enemy") : base(startPosition, layer, id)
    {
        bullets = new GameObjectList();
    }

    public override void Chase()
    {        
        if(PlayingState.player.position.X + 200 < position.X || PlayingState.player.position.X - 200 > position.X ||
            PlayingState.player.position.Y + 200 < position.Y || PlayingState.player.position.Y - 200 > position.Y)
            {
                Chase();
            }
        else if (counter == 0)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        EnemyBullet bullet = new EnemyBullet(position);
        bullets.Add(bullet);
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        bullets.Update(gameTime);
        Chase();
        counter--;
        if (counter < 0)
            counter = 60;
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        bullets.Draw(gameTime, spriteBatch);
        base.Draw(gameTime, spriteBatch);
        if (Die == false)
        {
            spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/BearEnemy"), position);
        }
    }

}
