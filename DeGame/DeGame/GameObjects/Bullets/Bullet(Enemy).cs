using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class E_Bullet : SpriteGameObject
{
    Vector2 direction;
    float speed;
    public E_Bullet(Vector2 Startpositon, int layer = 0, string id = "EnemyBullet") : base("Sprites/Random", layer, id)
    {

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
            PlayingState.player.health -= 0;
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

