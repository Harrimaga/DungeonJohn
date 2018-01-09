using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class E_Bullet : SpriteGameObject
{
    float Damage;

    public E_Bullet(float damage, float speed, string assetname, int layer = 0, string id = "EnemyBullet") : base(assetname, layer, id)
    {
        Damage = damage;
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
        if (CollidesWith(PlayingState.player))
        {
            GameObjectList.RemovedObjects.Add(this);
            if(PlayingState.player.HardHelmet)
            {
                PlayingState.player.health -= 0.8f * Damage;
            }
            if (!PlayingState.player.HardHelmet)
            {
                PlayingState.player.health -= Damage;
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

