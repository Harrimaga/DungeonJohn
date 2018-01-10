using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

class E_Bullet : SpriteGameObject
{
    float Damage, Speed;
    Random random;
    int reflectchance = 0;
    public bool reflected = false;
    protected bool changedirection = false;

    public E_Bullet(float damage, float speed, string assetname, int layer = 0, string id = "EnemyBullet") : base(assetname, layer, id)
    {
        Damage = damage;
        Speed = speed;
        random = new Random();
    }

    public override void Update(GameTime gameTime)
    {
        if (PlayingState.player.Mirror)
            reflectchance = random.Next(100);
        else
            reflectchance = 0;
        CheckCollision();
    }

    public void CheckCollision()
    {
        if (reflected)
        {
            foreach (Enemy e in PlayingState.currentFloor.currentRoom.enemies.Children)
                if (CollidesWith(e))
                {
                    e.health -= Damage;
                    GameObjectList.RemovedObjects.Add(this);
                }
            foreach (Boss b in Room.bosses.Children)
                if (CollidesWith(b))
                {
                    b.health -= Damage;
                    GameObjectList.RemovedObjects.Add(this);
                }
        }
        else
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
                    Speed += 1;
                    changedirection = true;
                }
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

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {

    }
}

