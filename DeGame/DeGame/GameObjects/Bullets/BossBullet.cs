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
    Vector2 direction, actualvelocity, difference, Homingdifference, Bulletorigin;
    Texture2D playersprite = GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerFront");
    SpriteEffects Effects;
    float speed = 5f;
    int health = 100, maxhealth = 100;
    bool Homing, reflected = false;
    double opposite;
    double adjacent;
    float Homingangle, HomingRotateAngle, RotateAngle, angle, circle = MathHelper.Pi * 2;
    
    public BossBullet(float damage, float speed, Vector2 Startposition, SpriteEffects effects, bool homing = false, int layer = 0, string id = "BossBullet") : base(damage, speed, "Sprites/Bullets/BossBullet", 0, "BossBullet") 
    {
        healthbar = new HealthBar(health, maxhealth, position);
        position = Startposition;
        direction = (PlayingState.player.position - position);      
        Homing = homing;
        Damage = damage;
        Effects = effects;
        Bulletorigin = new Vector2(sprite.Width / 2, sprite.Height / 2);
        difference = PlayingState.player.position - position;
        difference.Normalize();
        RotateAngle = (float)Math.Atan2(difference.Y, difference.X);

    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        Homingdifference = PlayingState.player.position - position;
        Homingdifference.Normalize();
        HomingRotateAngle = (float)Math.Atan2(Homingdifference.Y, Homingdifference.X);
        //opposite = PlayingState.player.position.Y - position.Y;
        //adjacent = PlayingState.player.position.X - position.Y;
        //Homingangle = (float)Math.Atan2(opposite, adjacent) / circle;
        //HomingRotateAngle = MathHelper.ToDegrees(Homingangle);
        healthbar.Update(gameTime, health, maxhealth, position);
        if(!reflected)
        {
            if (Homing)
            {
                HomingBullet();
                DestroyableBullet();
                actualvelocity = direction * speed;
                position += actualvelocity;
            }
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
        Vector2 MiddleofPlayer = new Vector2(PlayingState.player.position.X + GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerFront").Width / 2, PlayingState.player.position.Y + GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerFront").Height / 2);
        Vector2 newdirection = direction;
        if (position.X < MiddleofPlayer.X - GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerFront").Width / 2 || position.X > MiddleofPlayer.X + GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerFront").Width / 2)
        {
            newdirection.X = -direction.X;
        }
        if (position.Y < MiddleofPlayer.Y - GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerFront").Height / 2 || position.Y > MiddleofPlayer.Y + GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerFront").Height / 2)
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
        direction = PlayingState.player.position - position;
        direction.Normalize();
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //base.Draw(gameTime, spriteBatch);
        if(Homing == true)
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Bullets/BossBullet"), position, null, null, null, HomingRotateAngle, null, null, Effects, 0);
        else
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Bullets/BossBullet"), position, null, null, null, RotateAngle, null, null, Effects, 0);
        //healthbar.Draw(spriteBatch);
    }
}
