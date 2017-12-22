using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Solid : SpriteGameObject
     
{
    public Solid(Vector2 startPosition, int layer = 0, string id = "Rock")
    : base("Sprites/Rock Sprite", layer, id)
    {
        position = startPosition;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (CollidesWith(PlayingState.player) && BoundingBox.Left < PlayingState.player.position.X + PlayingState.player.Width && BoundingBox.Left + (PlayingState.player.Width/2) > PlayingState.player.position.X + PlayingState.player.Width)
        {
            while (CollidesWith(PlayingState.player))
                PlayingState.player.position.X--;
        }

        if (CollidesWith(PlayingState.player) && BoundingBox.Right > PlayingState.player.position.X && BoundingBox.Right - (PlayingState.player.Width / 2) < PlayingState.player.position.X)
        {
            while (CollidesWith(PlayingState.player))
                PlayingState.player.position.X++;
        }

        if (CollidesWith(PlayingState.player) && BoundingBox.Top < PlayingState.player.position.Y + PlayingState.player.Height && BoundingBox.Top + (PlayingState.player.Height / 2) > PlayingState.player.position.Y + PlayingState.player.Height)
        {
            while (CollidesWith(PlayingState.player))
                PlayingState.player.position.Y--;
        }

        if (CollidesWith(PlayingState.player) && BoundingBox.Bottom > PlayingState.player.position.Y && BoundingBox.Bottom - (PlayingState.player.Height / 2) <  PlayingState.player.position.Y)
        {
            while (CollidesWith(PlayingState.player))
                PlayingState.player.position.Y++;
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Rock Sprite"), position);
    }
}

