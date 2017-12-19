using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

class Goldpiece : SpriteGameObject
{
    Vector2 dropposition;
    public Goldpiece(Vector2 startPosition, int layer = 0, string id = "Gold")
    : base("Sprites/Coin", layer, id)
    {
        dropposition = position;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (CollidesWith(PlayingState.player))
        {
            PlayingState.player.gold += 5;
            GameObjectList.RemovedObjects.Add(this);
        }
    }

    void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        spriteBatch.Draw((GameEnvironment.assetManager.GetSprite("Sprites/Coin")), dropposition, Color.White);
    }
}

