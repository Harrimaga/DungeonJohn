using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class BossBullet : E_Bullet
{
    HealthBar healthbar;
    Vector2 direction;
    Texture2D playersprite = GameEnvironment.assetManager.GetSprite("Sprites/Random");
    float speed = 0.5f;
    int health = 100, maxhealth = 100;
    public SpriteEffects Effects;
    bool Homing, reflected = false;
    
    public BossBullet(float damage, float speed, Vector2 Startposition, bool homing = false, int layer = 0, string id = "BossBullet") : base(damage, speed, "Sprites/Bullets/BossBullet", 0, "BossBullet") 
    {
        healthbar = new HealthBar(health, maxhealth, position);
        position = Startposition;
        direction = (PlayingState.player.position - position);      
        Homing = homing;
        Damage = damage;
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        healthbar.Update(gameTime, health, maxhealth, position);
        if(!reflected)
        {
            if (Homing)
                HomingBullet();
            DestroyableBullet();
        }
        if (!Homing)
        {
            direction.Normalize();
            position += direction * speed;
        }
        if (changedirection)
        {
            if (!Homing)
            {
                direction = CalculateReflect(direction);
            }
            changedirection = false;
            Homing = false;
            reflected = true;
        }
    }

    Vector2 CalculateReflect(Vector2 direction)
    {
        Vector2 MiddleofPlayer = new Vector2(PlayingState.player.position.X + GameEnvironment.assetManager.GetSprite("Sprites/Random").Width / 2, PlayingState.player.position.Y + GameEnvironment.assetManager.GetSprite("Sprites/Random").Height / 2);
        Vector2 newdirection = direction;
        if (position.X < MiddleofPlayer.X - GameEnvironment.assetManager.GetSprite("Sprites/Random").Width / 2 || position.X > MiddleofPlayer.X + GameEnvironment.assetManager.GetSprite("Sprites/Random").Width / 2)
        {
            newdirection.X = -direction.X;
        }
        if (position.Y < MiddleofPlayer.Y - GameEnvironment.assetManager.GetSprite("Sprites/Random").Height / 2 || position.Y > MiddleofPlayer.Y + GameEnvironment.assetManager.GetSprite("Sprites/Random").Height / 2)
        {
            newdirection.Y = -direction.Y;
        }
        return newdirection;
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

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Bullets/BossBullet"), position);
        //healthbar.Draw(spriteBatch);
    }
}
