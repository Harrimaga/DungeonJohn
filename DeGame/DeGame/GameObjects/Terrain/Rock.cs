using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Rock : Solid
{
    bool IceRock;

    public Rock(Vector2 startPosition, int layer = 0, string id = "Rock", bool icerock = false)
    : base(startPosition, layer, id)
    {
        position = startPosition;
        IceRock = icerock;
    }


    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!IceRock)
            spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Tiles/Rock Sprite"), position);
        else
            spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Tiles/Ice Rock Sprite"), position);
    }
}
