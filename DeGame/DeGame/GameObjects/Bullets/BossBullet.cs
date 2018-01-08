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
    
    public BossBullet(Vector2 Startposition, int layer = 0, string id = "BossBullet") : base("Sprites/BossBullet", 0, "BossBullet") 
    {
        healthbar = new HealthBar(health, maxhealth, position);
        position = Startposition;
        
        //direction = (PlayingState.player.position - position);
        //direction.Normalize();
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        healthbar.Update(gameTime, health, maxhealth, position);
        //position += direction * speed;
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




}
