using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class EnemyBullet : SpriteGameObject
{
    public EnemyBullet(int layer = 0, string id = "EnemyBullet") : base("Sprites/Random", layer, id)
    {
        //position = Startposition;

        // Determine the direction of the bullets
    }

    // Update the bullets
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        position += velocity;
    }

    // Draw the bullets
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Random"), position);
    }
}

