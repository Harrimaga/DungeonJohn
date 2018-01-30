using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class BossBullet : E_Bullet
{
    HealthBar healthbar;
    Vector2 Homingdirection, direction, actualvelocity, difference, BulletOrigin, HomingPlayerOrigin, PlayerOrigin;
    Texture2D playersprite = GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerDown");
    SpriteEffects Effects;
    float speed = 0.28f;
    int health = 30, maxhealth = 30;
    bool Homing;

    public BossBullet(float damage, float speed, Vector2 Startposition, SpriteEffects effects, bool homing = false, int layer = 0, string id = "BossBullet") : base(damage, speed, "Sprites/Bullets/BossBullet", 0, "BossBullet") 
    {
        PlayerOrigin = new Vector2(PlayingState.player.position.X + playersprite.Width / 2, PlayingState.player.position.Y + playersprite.Height / 2);
        BulletOrigin = new Vector2(0, sprite.Height / 2);
        healthbar = new HealthBar(health, maxhealth, position);
        position = Startposition;
        direction = (PlayerOrigin - position);      
        Homing = homing;
        Damage = damage;
        Effects = effects;
        difference = PlayerOrigin - position;
        difference.Normalize();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        HomingPlayerOrigin = new Vector2(PlayingState.player.position.X + playersprite.Width / 2, PlayingState.player.position.Y + playersprite.Height / 2);
        healthbar.Update(gameTime, health, maxhealth, position);
        if(!reflected)
        {
            if (Homing)
            {
                HomingBullet();
                DestroyableBullet();
                actualvelocity = Homingdirection * speed;
                position.X += actualvelocity.X * gameTime.ElapsedGameTime.Milliseconds;
                position.Y += actualvelocity.Y * gameTime.ElapsedGameTime.Milliseconds;
                Damage = 20;
            }
        }
        if (!Homing)
        {
            direction.Normalize();
            actualvelocity = direction * speed;
            position.X += actualvelocity.X * gameTime.ElapsedGameTime.Milliseconds;
            position.Y += actualvelocity.Y * gameTime.ElapsedGameTime.Milliseconds;
            Damage = 30;
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
        Vector2 MiddleofPlayer = new Vector2(PlayingState.player.position.X + GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerDown").Width / 2, PlayingState.player.position.Y + GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerDown").Height / 2);
        Vector2 newdirection = direction;
        if (position.X < MiddleofPlayer.X - GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerDown").Width / 2 || position.X > MiddleofPlayer.X + GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerDown").Width / 2)
        {
            newdirection.X = -direction.X;
        }
        if (position.Y < MiddleofPlayer.Y - GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerDown").Height / 2 || position.Y > MiddleofPlayer.Y + GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerDown").Height / 2)
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
        Homingdirection = HomingPlayerOrigin - position;
        Homingdirection.Normalize();
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if(Homing == true)
            spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Bullets/BossBulletHoming"), position, null, null, BulletOrigin, 0, null, null, Effects, 0);
        else
            spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Bullets/BossBullet"), position, null, null, BulletOrigin, 0, null, null, Effects, 0);
    }
}
