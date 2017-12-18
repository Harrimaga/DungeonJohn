using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Wall : Solid
{
    public Wall(Vector2 startPosition, int layer = 0, string id = "Wall")
    : base(startPosition, layer, id)
    {
        position = startPosition;
    }


    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Rock Sprite"), position);
    }
}
