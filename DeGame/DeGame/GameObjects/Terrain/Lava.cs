using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Lava : Tiles
{
    public Lava(Vector2 startPosition, int layer = 0, string id = "Lava")
    : base(startPosition, layer, id)
    {
        position = startPosition;
    }
    public override void Update(GameTime gameTime)
    {
        if (CollidesWith(PlayingState.player))
        {
            PlayingState.player.health -= 1;
        }
    }

         public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Lava"), position);
    }
}
