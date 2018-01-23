using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

class E_Bullet : SpriteGameObject
{
    public float Damage, Speed;
    Random random;
    int reflectchance = 0, blockchance = 0;
    public bool reflected = false;
    protected bool changedirection = false;
    Vector2 Roomposition;

    public E_Bullet(float damage, float speed, string assetname, int layer = 0, string id = "EnemyBullet") : base(assetname, layer, id)
    {
        Damage = damage;
        Speed = speed;
        random = new Random();
        Roomposition = PlayingState.currentFloor.currentRoom.position;
    }

    public override void Update(GameTime gameTime)
    {
        CheckCollision();
    }

    public void CheckCollision()
    {
        if (reflected)
        {
            foreach (Enemy e in PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemies.Children)
                if (CollidesWith(e))
                {
                    e.health -= Damage;
                    GameObjectList.RemovedObjects.Add(this);
                }
            foreach (Boss b in PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].bosses.Children)
                if (CollidesWith(b))
                {
                    b.health -= Damage;
                    GameObjectList.RemovedObjects.Add(this);
                }
        }
        else
        {
            reflectchance = 100;
            blockchance = 100;
            if (PlayingState.player.CrestShield)
                blockchance = random.Next(100);
            else if (PlayingState.player.Mirror)
                reflectchance = random.Next(100);

            if (CollidesWith(PlayingState.player))
            {
                if (reflectchance < 20)
                {
                    PlayingState.player.health -= (float)(Damage * PlayingState.player.damagereduction / 2);
                    reflected = true;
                    Speed += 1;
                    changedirection = true;
                }
                else if (blockchance < 20)
                {
                    GameObjectList.RemovedObjects.Add(this);
                    return;
                }
                else
                {
                    PlayingState.player.health -= (float)(Damage * PlayingState.player.damagereduction);
                    GameObjectList.RemovedObjects.Add(this);
                    return;
                }
            }
        }

        foreach (Solid solid in PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].solid.Children)
        {
            if (CollidesWith(solid))
            {
                GameObjectList.RemovedObjects.Add(this);
                return;
            }
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {

    }
}

