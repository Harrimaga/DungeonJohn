using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Bullet : SpriteGameObject
{
    public Bullet(Vector2 Startposition, int Direction)
    {
        this.position = Startposition;
        if (Direction == 0)
            velocity.Y = 10;
        else if (Direction == 1)
            velocity.X = -10;
        else if (Direction == 2)
            velocity.Y = -10;
        else if (Direction == 3)
            velocity.X = 10;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        position += velocity;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Random"), position);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        position += velocity;
    }
}
