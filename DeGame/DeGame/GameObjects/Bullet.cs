﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

class Bullet : SpriteGameObject
{
    Texture2D Bulletsprite;
    SpriteEffects Effect = SpriteEffects.None;

    public Bullet(Vector2 Startposition, int Direction, int layer = 0, string id = "bullet")
    : base("Sprites/Random", layer, id)
    {
        position = Startposition;

        // Determine the direction of the bullets
        if (Direction == 0)
        {
            velocity.Y = 10;
            Bulletsprite = GameEnvironment.assetManager.GetSprite("Sprites/bulletup");
            Effect = SpriteEffects.FlipVertically;
        }
        else if (Direction == 1)
        {
            velocity.X = -10;
            Bulletsprite = GameEnvironment.assetManager.GetSprite("Sprites/bulletleft");
        }
        else if (Direction == 2)
        {
            velocity.Y = -10;
            Bulletsprite = GameEnvironment.assetManager.GetSprite("Sprites/bulletup");
        }
        else if (Direction == 3)
        {
            velocity.X = 10;
            Bulletsprite = GameEnvironment.assetManager.GetSprite("Sprites/bulletleft");
            Effect = SpriteEffects.FlipHorizontally;
        }
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
        spriteBatch.Draw(Bulletsprite, position, null, Color.White, 0f, Vector2.Zero, 1f, Effect, 0f);
    }
}
