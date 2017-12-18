using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Rock : SpriteGameObject
{
    Vector2 positionOld;

    public Rock(Vector2 startPosition, int layer = 0, string id = "Rock")
    : base("Sprites/Rock Sprite", layer, id)
    {
        position = startPosition;
    }


    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (CollidesWith(PlayingState.player))
        {
            PlayingState.player.position = positionOld;
        }
        else
        {
            positionOld = PlayingState.player.position;
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Rock Sprite"), position);
    }
}
