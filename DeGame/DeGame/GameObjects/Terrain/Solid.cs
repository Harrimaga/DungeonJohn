using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Solid : SpriteGameObject     
{
    public bool OnThisTile = false;

    public Solid(Vector2 startPosition, int layer = 0, string id = "Rock")
    : base("Sprites/Tiles/Rock Sprite", layer, id)
    {
        position = startPosition;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (CollidesWith(PlayingState.player))
        {
            OnThisTile = true;
        }
        else
            OnThisTile = false;
        SolidCollision();
    }

    void SolidCollision()
    {
        Vector2 MiddleofPlayer = new Vector2(PlayingState.player.position.X + GameEnvironment.assetManager.GetSprite("Sprites/PlayerFront").Width / 2, PlayingState.player.position.Y + GameEnvironment.assetManager.GetSprite("Sprites/PlayerFront").Height / 2);

        if (BoundingBox.Contains(MiddleofPlayer + new Vector2(0, -(GameEnvironment.assetManager.GetSprite("Sprites/PlayerFront").Height / 2))))
            while (CollidesWith(PlayingState.player))
                PlayingState.player.position.Y++;
        if (BoundingBox.Contains(MiddleofPlayer + new Vector2(0, (GameEnvironment.assetManager.GetSprite("Sprites/PlayerFront").Height / 2))))
            while (CollidesWith(PlayingState.player))
                PlayingState.player.position.Y--;
        if (BoundingBox.Contains(MiddleofPlayer + new Vector2(-(GameEnvironment.assetManager.GetSprite("Sprites/PlayerFront").Width / 2), 0)))
            while (CollidesWith(PlayingState.player))
                PlayingState.player.position.X++;
        if (BoundingBox.Contains(MiddleofPlayer + new Vector2((GameEnvironment.assetManager.GetSprite("Sprites/PlayerFront").Width / 2), 0)))
            while (CollidesWith(PlayingState.player))
                PlayingState.player.position.X--;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/Rock Sprite"), position);
    }
}

