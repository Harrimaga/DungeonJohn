using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Pit : Solid
{
    public Pit(Vector2 startPosition, int layer = 0, string id = "Pit")
        : base(startPosition, layer, id)
    {
        position = startPosition;
        hittable = false;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Tiles/PitTile"), position);
    }
}
