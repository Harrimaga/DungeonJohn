using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

class E_Bullet : SpriteGameObject
{
    float Damage;
    Random random;
    int reflectchance = 0;
    public bool reflected = false;

    public E_Bullet(float damage, float speed, string assetname, int layer = 0, string id = "EnemyBullet") : base(assetname, layer, id)
    {
        Damage = damage;
        random = new Random();
        if (PlayingState.player.Mirror)
            reflectchance = random.Next(100);
    }
    public override void Update(GameTime gameTime)
    {
        CheckCollision();
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {

    }

    public void CheckCollision()
    {
        if (!reflected)
        {
            if (CollidesWith(PlayingState.player))
            {
                if (reflectchance < 50)
                {
                    PlayingState.player.health -= (float)(Damage * PlayingState.player.damagereduction);
                    GameObjectList.RemovedObjects.Add(this);
                }
                else
                {
                    PlayingState.player.damagereduction *= 0.5;
                    PlayingState.player.health -= (float)(Damage * PlayingState.player.damagereduction);
                    PlayingState.player.damagereduction *= 2;
                    reflected = true;
                }
            }         
        }
        else
        {
            foreach (Enemy e in Room.enemies.Children)
                if (CollidesWith(e))
                {
                    e.health -= PlayingState.player.attack;
                    GameObjectList.RemovedObjects.Add(this);
                }
        }

        foreach (Solid solid in Room.solid.Children)
        {
            if (CollidesWith(solid))
            {
                GameObjectList.RemovedObjects.Add(this);
            }
        }
    }
}

