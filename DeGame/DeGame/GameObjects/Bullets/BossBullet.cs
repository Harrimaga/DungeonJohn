using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class BossBullet : E_Bullet
{
    Vector2 direction;
    float speed = 0.5f;
    int health = 100, maxhealth = 100;
    HealthBar healthbar;
    public SpriteEffects Effects;
    bool Homing;
    Texture2D playersprite = GameEnvironment.assetManager.GetSprite("Sprites/Random");
    
    public BossBullet(float damage, float speed, Vector2 Startposition, bool homing = false, int layer = 0, string id = "BossBullet") : base(damage, speed, "Sprites/BossBullet", 0, "BossBullet") 
    {
        healthbar = new HealthBar(health, maxhealth, position);
        position = Startposition;
        Homing = homing;
        direction = (PlayingState.player.position - position);
        direction.Normalize();
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        healthbar.Update(gameTime, health, maxhealth, position);
        if (!Homing)
        {
            position += direction * speed;
        }
        else
        {
            HomingBullet();
        }
        DestroyableBullet();
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/BossBullet"), position);
        //healthbar.Draw(spriteBatch);
    }

    public void DestroyableBullet()
    {
        List<GameObject> RemoveBullets = new List<GameObject>();

        foreach (Bullet bullet in PlayingState.player.bullets.Children)
            if (CollidesWith(bullet))
            {
                health -= (int)PlayingState.player.attack;
                RemoveBullets.Add(bullet);
            }

        foreach (Bullet bullet in RemoveBullets)
            PlayingState.player.bullets.Remove(bullet);
        RemoveBullets.Clear();

        if (health <= 0)
        {
            GameObjectList.RemovedObjects.Add(this);
        }
    }
    public void HomingBullet()
    {

        if (position.Y + playersprite.Height > PlayingState.player.position.Y + 1)
        {
            position.Y -= speed;
        }
        if (position.Y - playersprite.Height < PlayingState.player.position.Y - 1)
        {
            position.Y += speed;
        }
        if (position.X + playersprite.Width > PlayingState.player.position.X + 1)
        {
            position.X -= speed;
        }
        if (position.X + playersprite.Width < PlayingState.player.position.X - 1)
        {
            position.X += speed;
        }


    }
}
