using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Pit : Tiles
{
    public Pit(Vector2 startPosition, int layer = 0, string id = "Pit")
        : base(startPosition, layer, id)
    {
        position = startPosition;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        Vector2 MiddleofPlayer = new Vector2(PlayingState.player.position.X + GameEnvironment.assetManager.GetSprite("Sprites/Player").Width / 2, PlayingState.player.position.Y + GameEnvironment.assetManager.GetSprite("Sprites/Player").Height / 2);
        if (BoundingBox.Contains(MiddleofPlayer.X, MiddleofPlayer.Y))
        {
            GameEnvironment.gameStateManager.AddGameState("PitState", new PitState());
            GameEnvironment.gameStateManager.SwitchTo("PitState");
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Tiles/PitTile"), position);
    }
}
